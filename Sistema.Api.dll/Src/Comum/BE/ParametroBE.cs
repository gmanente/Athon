using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Sistema.Api.dll.Src.Comum.DAO;
using Sistema.Api.dll.Src.Comum.VO;

namespace Sistema.Api.dll.Src.Comum.BE
{
    public class ParametroBE : AbstractBE
    {
        public ParametroBE(SqlCommand sqlComm)
            : base(sqlComm)
        { }

        public ParametroBE()
            : base()
        { }

        public long Alterar(ParametroCampusVO objVO)
        {
            try
            {
                long id;

                BeginTransaction();

                if (!ParametroCampusExiste(objVO))
                {
                    var parametroDao = new ParametroDAO(GetSqlCommand());

                    // Logar Parametro
                    Logar(objVO, 'A');

                    id = parametroDao.Alterar(objVO);

                    Commit();
                }
                else
                    throw new Exception("Este parametro já existe");

                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        //AlterarValorParametro
        public long AlterarValorParametro(ParametroCampusVO objVO)
        {
            try
            {
                long id;

                BeginTransaction();

                var parametroDao = new ParametroDAO(GetSqlCommand());
                id = parametroDao.AlterarValorParametro(objVO);

                Commit();

                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public void Deletar(ParametroVO objVO)
        {
            try
            {
                BeginTransaction();

                var parametroDao = new ParametroDAO(GetSqlCommand());
                parametroDao.Deletar(objVO);

                Commit();
            }
            catch (Exception exception)
            {
                Rollback();
                throw exception;
            }
        }

        //Deletar ParametroCampus
        public void DeletarParametroCampus(ParametroCampusVO objVO)
        {
            try
            {
                BeginTransaction();

                var parametroDao = new ParametroDAO(GetSqlCommand());

                bool deletarParametroJunto = parametroDao.Selecionar(new ParametroCampusVO() { Parametro = objVO.Parametro }).Count == 1;
                if (deletarParametroJunto)
                {
                    parametroDao.DeletarParametroCampus(objVO);
                    parametroDao.Deletar(objVO.Parametro);
                }
                else
                    parametroDao.DeletarParametroCampus(objVO);

                Commit();
            }
            catch (Exception exception)
            {
                Rollback();
                throw exception;
            }
        }

        public long Inserir(ParametroCampusVO objVO)
        {
            try
            {
                long id;

                BeginTransaction();

                if (!ParametroCampusExiste(objVO))
                {
                    var parametroDao = new ParametroDAO(GetSqlCommand());

                    objVO = parametroDao.Inserir(objVO);
                    id = objVO.Id;

                    // Logar Parametro
                    Logar(objVO, 'I');
                    Commit();
                }
                else
                    throw new Exception("Este parametro já existe");

                return id;
            }
            catch (Exception ex)
            {
                Rollback();
                throw ex;
            }
        }

        //Inserir ParametroCampus
        public long InserirParametroCampus(ParametroCampusVO objVO)
        {
            try
            {
                long id;

                BeginTransaction();

                if (!ParametroCampusExiste(objVO))
                {
                    var parametroDao = new ParametroDAO(GetSqlCommand());

                    objVO = parametroDao.InserirParametroCampus(objVO);
                    id = objVO.Id;
                    objVO.Parametro = null;

                    // Logar Parametro
                    Logar(objVO, 'I');

                    Commit();
                }
                else
                    throw new Exception("Este parametro já existe");

                return id;
            }
            catch (Exception ex)
            {
                Rollback();
                throw ex;
            }
        }


        //Logar
        public void Logar(ParametroCampusVO objVO, char Tipo)
        {
            ParametroDAO parametroDao = null;
            try
            {
                //BeginTransaction();
                var ParametroLogVo = new ParametroLogVO();
                var PropriedadesParametroCampus = objVO.GetType().GetProperties();

                // LOG DO TIPO 'INSERIR'
                #region LOG DO TIPO 'INSERIR'
                if (Tipo == 'I')
                {
                    foreach (var propPC in PropriedadesParametroCampus)
                    {
                        var NomeTabela = objVO.GetType().Name.Substring(0, objVO.GetType().Name.Length - 2);
                        var objValorParametroCampus = propPC.GetValue(objVO);
                        if (objValorParametroCampus != null && objValorParametroCampus.ToString() != "0")
                        {
                            if (objValorParametroCampus.GetType() == typeof(ParametroVO))
                            {
                                var PropriedadesParametro = objValorParametroCampus.GetType().GetProperties();
                                foreach (var propP in PropriedadesParametro)
                                {
                                    var objValorParametro = propP.GetValue(objValorParametroCampus);
                                    if (objValorParametro != null && objValorParametro.ToString() != "0")
                                    {
                                        parametroDao = new ParametroDAO(GetSqlCommand());
                                        ParametroLogVo = new ParametroLogVO();

                                        ParametroLogVo.Campo = propP.Name;
                                        if (ParametroLogVo.Campo == "Id")
                                            ParametroLogVo.Campo += NomeTabela;

                                        NomeTabela = objValorParametroCampus.GetType().Name.Substring(0, objValorParametroCampus.GetType().Name.Length - 2);
                                        ParametroLogVo.IdReferencia = objVO.Parametro.Id;
                                        ParametroLogVo.Tipo = Tipo;
                                        ParametroLogVo.Objeto = NomeTabela;
                                        ParametroLogVo.DataOperacao = DateTime.Now;
                                        ParametroLogVo.IdUsuario = objVO.IdUsuario;
                                        ParametroLogVo.ValorAntigo = "";
                                        ParametroLogVo.ValorNovo = objValorParametro.ToString();

                                        parametroDao.LogarParametro(ParametroLogVo);
                                    }
                                }
                            }
                            else
                            {
                                parametroDao = new ParametroDAO(GetSqlCommand());
                                ParametroLogVo = new ParametroLogVO();

                                ParametroLogVo.Campo = propPC.Name;
                                if (ParametroLogVo.Campo == "Id")
                                    ParametroLogVo.Campo += NomeTabela;

                                ParametroLogVo.IdReferencia = objVO.Id;
                                ParametroLogVo.Tipo = Tipo;
                                ParametroLogVo.Objeto = NomeTabela;
                                ParametroLogVo.DataOperacao = DateTime.Now;
                                ParametroLogVo.IdUsuario = objVO.IdUsuario;
                                ParametroLogVo.ValorAntigo = "";
                                ParametroLogVo.ValorNovo = objValorParametroCampus.ToString();

                                parametroDao.LogarParametro(ParametroLogVo);
                            }
                        }
                    }
                }
                #endregion
                // LOG DO TIPO 'ALTERAR'
                #region LOG DO TIPO 'ALTERAR'
                else if (Tipo == 'A')
                {
                    var ParametroCampusAntigo = Consultar(new ParametroCampusVO() { Id = objVO.Id });

                    foreach (var propPC in PropriedadesParametroCampus)
                    {
                        var NomeTabela = objVO.GetType().Name.Substring(0, objVO.GetType().Name.Length - 2);
                        var objValorParametroCampus = propPC.GetValue(objVO);
                        if (objValorParametroCampus != null && objValorParametroCampus.ToString() != "0")
                        {
                            if (objValorParametroCampus.GetType() == typeof(ParametroVO))
                            {
                                var PropriedadesParametro = objValorParametroCampus.GetType().GetProperties();
                                foreach (var propP in PropriedadesParametro)
                                {
                                    var objValorParametro = propP.GetValue(objValorParametroCampus);
                                    if (objValorParametro != null && objValorParametro.ToString() != "0")
                                    {
                                        var ValorNovo = objValorParametro.ToString();
                                        var ValorAntigo = ParametroCampusAntigo.Parametro.GetType().GetProperty(propP.Name).GetValue(ParametroCampusAntigo.Parametro).ToString();
                                        if (ValorNovo != ValorAntigo)
                                        {
                                            parametroDao = new ParametroDAO(GetSqlCommand());
                                            ParametroLogVo = new ParametroLogVO();

                                            ParametroLogVo.Campo = propP.Name;
                                            if (ParametroLogVo.Campo == "Id")
                                                ParametroLogVo.Campo += NomeTabela;

                                            NomeTabela = objValorParametroCampus.GetType().Name.Substring(0, objValorParametroCampus.GetType().Name.Length - 2);
                                            ParametroLogVo.IdReferencia = objVO.Parametro.Id;
                                            ParametroLogVo.Tipo = Tipo;
                                            ParametroLogVo.Objeto = NomeTabela;
                                            ParametroLogVo.DataOperacao = DateTime.Now;
                                            ParametroLogVo.IdUsuario = objVO.IdUsuario;
                                            ParametroLogVo.ValorAntigo = ValorAntigo;
                                            ParametroLogVo.ValorNovo = ValorNovo;

                                            parametroDao.LogarParametro(ParametroLogVo);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                var ValorNovo = objValorParametroCampus.ToString();
                                var objAntigo = ParametroCampusAntigo.GetType().GetProperty(propPC.Name).GetValue(ParametroCampusAntigo) ?? "";
                                var ValorAntigo = objAntigo.ToString();
                                if (ValorNovo != ValorAntigo)
                                {
                                    parametroDao = new ParametroDAO(GetSqlCommand());
                                    ParametroLogVo = new ParametroLogVO();

                                    ParametroLogVo.Campo = propPC.Name;
                                    if (ParametroLogVo.Campo == "Id")
                                        ParametroLogVo.Campo += NomeTabela;

                                    ParametroLogVo.IdReferencia = objVO.Id;
                                    ParametroLogVo.Tipo = Tipo;
                                    ParametroLogVo.Objeto = NomeTabela;
                                    ParametroLogVo.DataOperacao = DateTime.Now;
                                    ParametroLogVo.IdUsuario = objVO.IdUsuario;
                                    ParametroLogVo.ValorAntigo = ValorAntigo;
                                    ParametroLogVo.ValorNovo = ValorNovo;

                                    parametroDao.LogarParametro(ParametroLogVo);
                                }
                            }
                        }
                    }
                }
                #endregion
                //Commit();
            }
            catch (Exception ex)
            {
                //Rollback();
                throw ex;
            }
        }


        public List<ParametroVO> SelecionarParametros(ParametroVO objVO)
        {
            try
            {
                var parametroDao = new ParametroDAO(GetSqlCommand());
                return parametroDao.SelecionarParametros(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ParametroModeloVO> SelecionarParametrosPermitidos(ParametroModeloVO objVO)
        {
            try
            {
                var parametroDao = new ParametroDAO(GetSqlCommand());
                return parametroDao.SelecionarModeloPaginar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Selecionar
        public List<ParametroCampusVO> Selecionar(ParametroCampusVO objVO, int top = 0, bool detalhar = false)
        {
            try
            {
                var parametroDao = new ParametroDAO(GetSqlCommand());
                return parametroDao.Selecionar(objVO, top);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Consultar
        public ParametroCampusVO Consultar(ParametroCampusVO objVO)
        {
            try
            {
                var parametroDao = new ParametroDAO(GetSqlCommand());
                return parametroDao.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        //Consultar
        public bool ParametroCampusExiste(ParametroCampusVO objVO)
        {
            try
            {
                var parametroDao = new ParametroDAO(GetSqlCommand());
                return parametroDao.Consultar(objVO) != null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //SelecionarParametrosServicoProtocolo
        public List<ParametroCampusVO> SelecionarParametrosServicoProtocolo(ParametroCampusVO objVO = null)
        {
            try
            {
                var parametroDao = new ParametroDAO(GetSqlCommand());
                return parametroDao.SelecionarParametrosServicoProtocolo(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Listar
        public List<ParametroCampusVO> Listar(ParametroCampusVO objVO = null, bool detalhar = false)
        {
            try
            {
                var parametroDao = new ParametroDAO(GetSqlCommand());
                return parametroDao.Listar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Dictionary<int, List<ParametroModeloVO>> Paginar(ParametroModeloVO objVO)
        {
            try
            {
                var ParametroCampusDAO = new ParametroDAO(GetSqlCommand());
                return ParametroCampusDAO.Paginar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// RecuperarParametroEmArray
        /// </summary>
        /// <param name="nomeParametro"></param>
        /// <param name="idCampus"></param>
        /// <returns></returns>
        public long[] RecuperarParametroEmArray(string nomeParametro, long idCampus = 0)
        {
            try
            {
                var dao = new ParametroDAO(GetSqlCommand());

                var parametroVO = dao.Consultar(new ParametroCampusVO() {
                    Parametro =
                    {
                        Nome = nomeParametro
                    },
                    IdCampus = idCampus
                });

                long[]v = { };

                if (parametroVO != null)
                {
                    string[] valores = parametroVO.Valor.Split(',');

                    if (valores.Count() > 0)
                        v = valores.Select(long.Parse).ToArray();
                }

                return v;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
