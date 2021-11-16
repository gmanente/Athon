using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Sistema.Api.dll.Src.Comum.DAO
{
    public class CampusDAO : AbstractDAO, IDAO<CampusVO>
    {
        public CampusDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {}

        public long Inserir(CampusVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();

                objVO.Id = GetCodigoSequece("DBAthon.dbo.SeqCampus");

                GetSqlCommand().Parameters.Clear();

                objSbInsert.AppendLine(@"INSERT INTO DBAthon.dbo.Campus
                                         (
                                                  IdCampus
                                                , IdEmpresa
                                                , Nome
                                                , Cnpj
                                                , IpFixo
                                                , CodigoInep
                                                , Cep
                                                , Endereco
                                                , Numero
                                                , Complemento
                                                , Bairro
                                                , IdCidade
                                                , Sigla
                                         )
                                          VALUES
                                         (        @IdCampus
                                                , @IdEmpresa
                                                , @Nome
                                                , @Cnpj
                                                , @IpFixo
                                                , @CodigoInep
                                                , @Cep
                                                , @Endereco
                                                , @Numero
                                                , @Complemento
                                                , @Bairro
                                                , @IdCidade
                                                , @Sigla
                                          )");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();

                GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = objVO.Id;
                GetSqlCommand().Parameters.Add("IdEmpresa", SqlDbType.Int).Value = objVO.Empresa.Id;
                GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Nome;
                GetSqlCommand().Parameters.Add("Cnpj", SqlDbType.VarChar).Value = objVO.Cnpj;
                GetSqlCommand().Parameters.Add("IpFixo", SqlDbType.VarChar).Value = objVO.IpFixo;
                GetSqlCommand().Parameters.Add("CodigoInep", SqlDbType.Int).Value = objVO.CodigoINEP;
                GetSqlCommand().Parameters.Add("Cep", SqlDbType.VarChar).Value = objVO.Cep;
                GetSqlCommand().Parameters.Add("Endereco", SqlDbType.VarChar).Value = objVO.Endereco;
                GetSqlCommand().Parameters.Add("Numero", SqlDbType.VarChar).Value = objVO.Numero;
                GetSqlCommand().Parameters.Add("Complemento", SqlDbType.VarChar).Value = objVO.Complemento;
                GetSqlCommand().Parameters.Add("Bairro", SqlDbType.VarChar).Value = objVO.Bairro;
                GetSqlCommand().Parameters.Add("Sigla", SqlDbType.VarChar).Value = objVO.Sigla;
                GetSqlCommand().Parameters.Add("IdCidade", SqlDbType.Int).Value = objVO.Cidade.Id;
                GetSqlCommand().Parameters.Add("ExibirPortalBiblioteca", SqlDbType.VarChar).Value = objVO.ExibirPortalBiblioteca;

                GetSqlCommand().ExecuteNonQuery();

                return objVO.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objSbInsert != null)
                    objSbInsert = null;
            }
        }

        public long Alterar(CampusVO objVO, string @where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();

                GetSqlCommand().Parameters.Clear();

                objSbUpdate.AppendLine(@"UPDATE DBAthon.dbo.Campus SET
                    IdEmpresa = @IdEmpresa,
                    Nome = @Nome,
                    Cnpj = @Cnpj,
                    IpFixo = @IpFixo,
                    CodigoInep = @CodigoInep,
                    Cep = @Cep,
                    Endereco = @Endereco,
                    Numero = @Numero,
                    Complemento = @Complemento,
                    Bairro = @Bairro,
                    IdCidade = @IdCidade,
                    Sigla = @Sigla,
                    ExibirPortalBiblioteca = @ExibirPortalBiblioteca
                ");

                if (where == null)
                {
                    objSbUpdate.AppendLine(@"WHERE IdCampus = @IdCampus");
                }
                else
                {
                    objSbUpdate.AppendLine(where);
                }

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbUpdate.ToString();

                GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = objVO.Id;
                GetSqlCommand().Parameters.Add("IdEmpresa", SqlDbType.Int).Value = objVO.Empresa.Id;
                GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Nome;
                GetSqlCommand().Parameters.Add("Cnpj", SqlDbType.VarChar).Value = objVO.Cnpj;
                GetSqlCommand().Parameters.Add("IpFixo", SqlDbType.VarChar).Value = objVO.IpFixo;
                GetSqlCommand().Parameters.Add("CodigoInep", SqlDbType.Int).Value = objVO.CodigoINEP;
                GetSqlCommand().Parameters.Add("Cep", SqlDbType.VarChar).Value = objVO.Cep;
                GetSqlCommand().Parameters.Add("Endereco", SqlDbType.VarChar).Value = objVO.Endereco;
                GetSqlCommand().Parameters.Add("Numero", SqlDbType.VarChar).Value = objVO.Numero;
                GetSqlCommand().Parameters.Add("Complemento", SqlDbType.VarChar).Value = objVO.Complemento;
                GetSqlCommand().Parameters.Add("Bairro", SqlDbType.VarChar).Value = objVO.Bairro;
                GetSqlCommand().Parameters.Add("Sigla", SqlDbType.VarChar).Value = objVO.Sigla;
                GetSqlCommand().Parameters.Add("IdCidade", SqlDbType.Int).Value = objVO.Cidade.Id;
                GetSqlCommand().Parameters.Add("ExibirPortalBiblioteca", SqlDbType.VarChar).Value = objVO.ExibirPortalBiblioteca;

                GetSqlCommand().ExecuteNonQuery();

                return objVO.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbUpdate != null)
                    objSbUpdate = null;
            }
        }


        public void Deletar(CampusVO objVO)
        {
            try
            {
                objSbDelete = new StringBuilder();
                objSbDelete.AppendLine(@"DELETE FROM DBAthon.dbo.Campus WHERE IdCampus = @IdCampus ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = objVO.Id;

                GetSqlCommand().ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                if (objSbDelete != null)
                    objSbDelete = null;
            }
        }


        public List<CampusVO> Selecionar(CampusVO campusVO = null, int top = 0)
        {
            CampusVO campus = null;
            List<CampusVO> lstCampus = null;

            try
            {
                objSbSelect = new StringBuilder();
                lstCampus = new List<CampusVO>();

                string varTop = "";

                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"SELECT
                                                Campus.IdCampus
                                              , Campus.IdEmpresa
                                              , Campus.Nome
                                              , Campus.Sigla
                                              , Campus.Cnpj
                                              , Campus.IpFixo
                                              , Campus.CodigoInep
                                              , Campus.Cep
                                              , Campus.Endereco
                                              , Campus.Numero
                                              , Campus.Complemento
                                              , Campus.Bairro
                                              , Campus.IdCidade
                                              , Campus.ExibirPortalBiblioteca
                                              , Cidade.Nome Cidade
                                              , Cidade.IdEstado
                                              , Estado.Nome Estado

                                         FROM DBAthon.dbo.Campus
                                    LEFT JOIN DBAthon.dbo.Cidade
                                           ON Cidade.CodigoIbge = Campus.IdCidade
                                    LEFT JOIN DBAthon.dbo.Estado
                                           ON Estado.CodigoIbge = Cidade.IdEstado
                                        WHERE 1 = 1                 ");

                if (campusVO != null)
                {

                    GetSqlCommand().Parameters.Clear();

                    if (campusVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND Campus.IdCampus = @IdCampus");
                        GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = campusVO.Id;
                    }

                    if (campusVO.Empresa.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND Campus.IdEmpresa = @IdEmpresa");
                        GetSqlCommand().Parameters.Add("IdEmpresa", SqlDbType.Int).Value = campusVO.Empresa.Id;
                    }

                    if (!string.IsNullOrEmpty(campusVO.Nome))
                    {
                        objSbSelect.AppendLine(@" AND Campus.Nome = @Nome");
                        GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = campusVO.Nome;
                    }
                }

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    campus = new CampusVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdCampus"))))
                        campus.Id = Convert.ToInt32(GetSqlDataReader()["IdCampus"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdEmpresa"))))
                        campus.Empresa.Id = Convert.ToInt32(GetSqlDataReader()["IdEmpresa"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        campus.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Sigla"))))
                        campus.Sigla = Convert.ToString(GetSqlDataReader()["Sigla"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Cnpj"))))
                        campus.Cnpj = Convert.ToString(GetSqlDataReader()["Cnpj"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IpFixo"))))
                        campus.IpFixo = Convert.ToString(GetSqlDataReader()["IpFixo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CodigoInep"))))
                        campus.CodigoINEP = Convert.ToString(GetSqlDataReader()["CodigoInep"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Cep"))))
                        campus.Cep = Convert.ToString(GetSqlDataReader()["Cep"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Endereco"))))
                        campus.Endereco = Convert.ToString(GetSqlDataReader()["Endereco"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Numero"))))
                        campus.Numero = Convert.ToString(GetSqlDataReader()["Numero"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Complemento"))))
                        campus.Complemento = Convert.ToString(GetSqlDataReader()["Complemento"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Bairro"))))
                        campus.Bairro = Convert.ToString(GetSqlDataReader()["Bairro"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdCidade"))))
                        campus.Cidade.Id = Convert.ToInt64(GetSqlDataReader()["IdCidade"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdEstado"))))
                        campus.Cidade.Estado.Id = Convert.ToInt64(GetSqlDataReader()["IdEstado"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Estado"))))
                        campus.Cidade.Estado.Descricao = Convert.ToString(GetSqlDataReader()["Estado"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Cidade"))))
                        campus.Cidade.Nome = Convert.ToString(GetSqlDataReader()["Cidade"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("ExibirPortalBiblioteca"))))
                        campus.ExibirPortalBiblioteca = Convert.ToBoolean(GetSqlDataReader()["ExibirPortalBiblioteca"]);

                    lstCampus.Add(campus);

                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbSelect != null)
                {
                    objSbSelect = null;
                }

                Close();
            }

            return lstCampus;
        }


        public List<CampusVO> SelecionarCampusDisponivelServicoProtocolo(CampusVO campusVO = null, int top = 0)
        {
            CampusVO campus = null;
            List<CampusVO> lstCampus = null;

            try
            {
                objSbSelect = new StringBuilder();
                lstCampus = new List<CampusVO>();

                string varTop = "";

                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"SELECT
                                                Campus.IdCampus
                                              , Campus.IdEmpresa
                                              , Campus.Nome
                                              , Campus.Sigla
                                              , Campus.Cnpj
                                              , Campus.IpFixo
                                              , Campus.CodigoInep
                                              , Campus.Cep
                                              , Campus.Endereco
                                              , Campus.Numero
                                              , Campus.Complemento
                                              , Campus.Bairro
                                              , Campus.IdCidade
                                              , Campus.ExibirPortalBiblioteca
                                              , Cidade.Nome Cidade
                                              , Cidade.IdEstado
                                              , Estado.Nome Estado
                                         FROM DBAthon.dbo.Campus
                                    LEFT JOIN DBAthon.dbo.Cidade ON Cidade.CodigoIbge = Campus.IdCidade
                                    LEFT JOIN DBAthon.dbo.Estado ON Estado.CodigoIbge = Cidade.IdEstado
                                        WHERE 1 = 1                 ");

                if (campusVO != null)
                {

                    GetSqlCommand().Parameters.Clear();

                    if (campusVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND Campus.IdCampus = @IdCampus");
                        GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = campusVO.Id;
                    }

                    if (campusVO.Empresa.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND Campus.IdEmpresa = @IdEmpresa");
                        GetSqlCommand().Parameters.Add("IdEmpresa", SqlDbType.Int).Value = campusVO.Empresa.Id;
                    }

                    if (!string.IsNullOrEmpty(campusVO.Nome))
                    {
                        objSbSelect.AppendLine(@" AND Campus.Nome = @Nome");
                        GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = campusVO.Nome;
                    }   
                    
                    if (campusVO.SomenteCampusComServicosProtocoloAtivo == true)
                    {
                        objSbSelect.AppendLine(@" AND Campus.IdCampus IN (SELECT Servico.IdCampus 
                                                                            FROM DBProtocolo.dbo.Servico 
                                                                           WHERE 1 = 1 
                                                                             AND Servico.Ativo = 1
                                                                             AND Servico.Historico = 0) ");
                    }
                    
                }

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    campus = new CampusVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdCampus"))))
                        campus.Id = Convert.ToInt32(GetSqlDataReader()["IdCampus"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdEmpresa"))))
                        campus.Empresa.Id = Convert.ToInt32(GetSqlDataReader()["IdEmpresa"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        campus.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Sigla"))))
                        campus.Sigla = Convert.ToString(GetSqlDataReader()["Sigla"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Cnpj"))))
                        campus.Cnpj = Convert.ToString(GetSqlDataReader()["Cnpj"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IpFixo"))))
                        campus.IpFixo = Convert.ToString(GetSqlDataReader()["IpFixo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CodigoInep"))))
                        campus.CodigoINEP = Convert.ToString(GetSqlDataReader()["CodigoInep"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Cep"))))
                        campus.Cep = Convert.ToString(GetSqlDataReader()["Cep"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Endereco"))))
                        campus.Endereco = Convert.ToString(GetSqlDataReader()["Endereco"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Numero"))))
                        campus.Numero = Convert.ToString(GetSqlDataReader()["Numero"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Complemento"))))
                        campus.Complemento = Convert.ToString(GetSqlDataReader()["Complemento"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Bairro"))))
                        campus.Bairro = Convert.ToString(GetSqlDataReader()["Bairro"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdCidade"))))
                        campus.Cidade.Id = Convert.ToInt64(GetSqlDataReader()["IdCidade"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdEstado"))))
                        campus.Cidade.Estado.Id = Convert.ToInt64(GetSqlDataReader()["IdEstado"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Estado"))))
                        campus.Cidade.Estado.Descricao = Convert.ToString(GetSqlDataReader()["Estado"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Cidade"))))
                        campus.Cidade.Nome = Convert.ToString(GetSqlDataReader()["Cidade"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("ExibirPortalBiblioteca"))))
                        campus.ExibirPortalBiblioteca = Convert.ToBoolean(GetSqlDataReader()["ExibirPortalBiblioteca"]);

                    lstCampus.Add(campus);

                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbSelect != null)
                {
                    objSbSelect = null;
                }

                Close();
            }

            return lstCampus;
        }


        public List<CampusVO> SelecionarPorUsuario(long idUsuario, CampusVO campusVO = null)
        {
            CampusVO campus = null;
            List<CampusVO> lstCampus = null;

            try
            {
                objSbSelect = new StringBuilder();
                lstCampus = new List<CampusVO>();


                objSbSelect.AppendLine(@"SELECT                                                                          ");
                objSbSelect.AppendLine(@"        DBAthon.dbo.Campus.IdCampus                                             ");
                objSbSelect.AppendLine(@"      , DBAthon.dbo.Campus.IdEmpresa                                            ");
                objSbSelect.AppendLine(@"      , DBAthon.dbo.Campus.Nome                                                 ");
                objSbSelect.AppendLine(@"  FROM  DBAthon.dbo.Campus                                                      ");
                objSbSelect.AppendLine(@"INNER JOIN DBAthon.dbo.UsuarioCampus                                        ");
                objSbSelect.AppendLine(@"        ON DBAthon.dbo.UsuarioCampus.IdCampus = DBAthon.dbo.Campus.IdCampus ");
                objSbSelect.AppendLine(@"WHERE 1 = 1                                                                     ");

                GetSqlCommand().Parameters.Clear();

                objSbSelect.AppendLine(@" AND DBAthon.dbo.UsuarioCampus.IdUsuario = @IdUsuario");
                GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = idUsuario;

                if (campusVO != null)
                {
                    if (campusVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Campus.IdCampus = @IdCampus");
                        GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = campusVO.Id;
                    }
                    if (campusVO.Empresa.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND IdEmpresa = @IdEmpresa");
                        GetSqlCommand().Parameters.Add("IdEmpresa", SqlDbType.Int).Value = campusVO.Empresa.Id;
                    }
                    if (!string.IsNullOrEmpty(campusVO.Nome))
                    {
                        objSbSelect.AppendLine(@" AND Nome = @Nome");
                        GetSqlCommand().Parameters.Add("Nome", SqlDbType.Int).Value = campusVO.Nome;
                    }
                }

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    campus = new CampusVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdCampus"))))
                        campus.Id = Convert.ToInt32(GetSqlDataReader()["IdCampus"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdEmpresa"))))
                        campus.Empresa.Id = Convert.ToInt32(GetSqlDataReader()["IdEmpresa"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        campus.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);


                    lstCampus.Add(campus);

                }

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbSelect != null)
                {
                    objSbSelect = null;
                }

                Close();
            }

            return lstCampus;
        }


        /// <summary>
        /// CampusPorUsuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public string CampusPorUsuario(long idUsuario)
        {
            try
            {
                objSbSelect = new StringBuilder();

                objSbSelect.AppendLine(@"SELECT     Campus.IdCampus                          ");
                objSbSelect.AppendLine(@"      FROM DBAthon.dbo.Campus                       ");
                objSbSelect.AppendLine(@"INNER JOIN DBAthon.dbo.UsuarioCampus            ");
                objSbSelect.AppendLine(@"        ON UsuarioCampus.IdCampus = Campus.IdCampus ");
                objSbSelect.AppendLine(@"     WHERE 1 = 1                                    ");

                GetSqlCommand().Parameters.Clear();

                objSbSelect.AppendLine(@" AND UsuarioCampus.IdUsuario = @IdUsuario");

                GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = idUsuario;

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                string idsCampus = "";

                while (GetSqlDataReader().Read())
                {
                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdCampus"))))
                    {
                        if (idsCampus.Length > 0)
                            idsCampus += ", " + Convert.ToString(GetSqlDataReader()["IdCampus"]);
                        else
                            idsCampus += Convert.ToString(GetSqlDataReader()["IdCampus"]);
                    }                     
                }

                //if (idsCampus.Length > 0)
                //    idsCampus.Substring(0, idsCampus.Length - 1);

                return idsCampus;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objSbSelect != null)
                {
                    objSbSelect = null;
                }

                Close();
            }
        }


        public CampusVO Consultar(CampusVO campusVo)
        {
            try
            {
                List<CampusVO> lstCampus = Selecionar(campusVo);
                return lstCampus.Count() > 0 ? (CampusVO)lstCampus.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<CampusVO> Listar(CampusVO campusVo = null)
        {
            try
            {
                List<CampusVO> lstCampus = Selecionar(campusVo);
                return lstCampus;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Autor: Gustavo Martins
        /// Data: 25.07.2015
        /// Descrição: Resonsavel por listar todos os campus validos dentro dos editais validos
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        private List<CampusVO> GetListaCampusEditaisValidos()
        {
            CampusVO campusVo = null;
            List<CampusVO> lstCampus = null;

            try
            {
                lstCampus = new List<CampusVO>();

                while (GetSqlDataReader().Read())
                {
                    campusVo = new CampusVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdCampus"))))
                        campusVo.Id = Convert.ToInt32(GetSqlDataReader()["IdCampus"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        campusVo.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdEmpresa"))))
                        campusVo.Empresa.Id = Convert.ToInt32(GetSqlDataReader()["IdEmpresa"]);

                    lstCampus.Add(campusVo);

                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return lstCampus;
        }


        public List<CampusVO> ListarCampusEditaisValidos(DateTime dataAtual)
        {
            try
            {
                objSbSelect = new StringBuilder();

                objSbSelect.AppendLine(@"SELECT DBAthon.dbo.Campus.IdCampus                                                                 ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.Campus.IdEmpresa                                                                ");
                objSbSelect.AppendLine(@"      ,DBAthon.dbo.Campus.Nome                                                                     ");
                objSbSelect.AppendLine(@"FROM DBAthon.dbo.Campus                                                                            ");
                objSbSelect.AppendLine(@"WHERE 1 = 1                                                                                        ");
                objSbSelect.AppendLine(@"AND DBAthon.dbo.Campus.IdCampus IN                                                                 ");
                objSbSelect.AppendLine(@"(                                                                                                  ");
                objSbSelect.AppendLine(@"SELECT DBVestibular.dbo.Edital.IdCampus                                                            ");
                objSbSelect.AppendLine(@"                                                                                                   ");
                objSbSelect.AppendLine(@"  FROM DBVestibular.dbo.Edital                                                                     ");
                objSbSelect.AppendLine(@"INNER JOIN DBAthon.dbo.PeriodoLetivo ON                                                            ");
                objSbSelect.AppendLine(@"       DBVestibular.dbo.Edital.IdPeriodoLetivo = DBAthon.dbo.PeriodoLetivo.IdPeriodoLetivo         ");
                objSbSelect.AppendLine(@"INNER JOIN DBAthon.dbo.Campus ON                                                                   ");
                objSbSelect.AppendLine(@"       DBVestibular.dbo.Edital.IdCampus = DBAthon.dbo.Campus.IdCampus                              ");

                GetSqlCommand().Parameters.Clear();
                objSbSelect.AppendLine(@" AND @DataAtual BETWEEN DBVestibular.dbo.Edital.DataInicio AND DBVestibular.dbo.Edital.DataTermino ");
                GetSqlCommand().Parameters.Add("DataAtual", SqlDbType.DateTime).Value = dataAtual;

                //AutorizaInscricao
                objSbSelect.AppendLine(@" AND AutorizaInscricao = @AutorizaInscricao                                                        ");

                GetSqlCommand().Parameters.Add("AutorizaInscricao", SqlDbType.Bit).Value = true;
                objSbSelect.AppendLine(@"WHERE 1 = 1                                                                                        ");
                objSbSelect.AppendLine(@")                                                                                                  ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                return GetListaCampusEditaisValidos();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbSelect != null)
                {
                    objSbSelect = null;
                }

                Close();
            }
        }


        public List<CampusVO> ListaCampusComCurso()
        {
            try
            {
                objSbSelect = new StringBuilder();

                objSbSelect.AppendLine(@"SELECT c.IdCampus
                                              , c.Nome
                                              , c.IdEmpresa
                                              , c.Cnpj
                                              , c.IpFixo
                                         FROM DBAthon.dbo.Campus c
                                         INNER JOIN DBSecretariaAcademica.dbo.GradeConsepe gc
                                                 ON gc.IdCampus = c.IdCampus
                                         GROUP BY c.IdCampus
                                                , c.Nome
                                                , c.IdEmpresa
                                                , c.Cnpj
                                                , c.IpFixo;");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                return GetListaCampusComCurso();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbSelect != null)
                    objSbSelect = null;

                Close();
            }
        }

        private List<CampusVO> GetListaCampusComCurso()
        {
            CampusVO campusVo = null;
            List<CampusVO> lstCampus = null;

            try
            {
                lstCampus = new List<CampusVO>();
                while (GetSqlDataReader().Read())
                {
                    campusVo = new CampusVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdCampus"))))
                        campusVo.Id = Convert.ToInt64(GetSqlDataReader()["IdCampus"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        campusVo.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdEmpresa"))))
                        campusVo.Empresa.Id = Convert.ToInt64(GetSqlDataReader()["IdEmpresa"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Cnpj"))))
                        campusVo.Cnpj = Convert.ToString(GetSqlDataReader()["Cnpj"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IpFixo"))))
                        campusVo.IpFixo = Convert.ToString(GetSqlDataReader()["IpFixo"]);

                    lstCampus.Add(campusVo);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return lstCampus;
        }


        public List<CampusVO> Paginar(CampusVO objVO, int inicio, int fim)
        {
            throw new NotImplementedException();
        }
    }
}