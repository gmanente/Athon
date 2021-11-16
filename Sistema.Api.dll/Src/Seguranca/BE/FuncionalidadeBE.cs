using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Seguranca.VO;
using Sistema.Api.dll.Src.Seguranca.DAO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Seguranca.BE
{
    public class FuncionalidadeBE : AbstractBE, IBE<FuncionalidadeVO>
    {
        public FuncionalidadeBE(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public FuncionalidadeBE()
            : base()
        {

        }

        public long Inserir(FuncionalidadeVO objVO)
        {
            FuncionalidadeDAO funcionalidadeDao = null;
            try
            {
                long id;
                BeginTransaction();
                funcionalidadeDao = new FuncionalidadeDAO(GetSqlCommand());
                id = funcionalidadeDao.Inserir(objVO);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public long Alterar(FuncionalidadeVO objVO, string where = null)
        {
            FuncionalidadeDAO funcionalidadeDao = null;
            try
            {
                long id;
                BeginTransaction();
                funcionalidadeDao = new FuncionalidadeDAO(GetSqlCommand());
                id = funcionalidadeDao.Alterar(objVO, where);
                Commit();
                return id;

            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public void Deletar(FuncionalidadeVO objVO)
        {
            FuncionalidadeDAO funcionalidadeDao = null;
            try
            {
                BeginTransaction();
                funcionalidadeDao = new FuncionalidadeDAO(GetSqlCommand());
                funcionalidadeDao.Deletar(objVO);
                Commit();
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public List<FuncionalidadeVO> Selecionar(FuncionalidadeVO funcionalidadeVo = null, int top = 0, bool detalhar = false)
        {
            FuncionalidadeDAO funcionalidadeDao = null;
            SubmoduloBE subModuloBE = null;
            List<FuncionalidadeVO> lstFuncionalidade = null;
            try
            {
                subModuloBE = new SubmoduloBE(GetSqlCommand());
                funcionalidadeDao = new FuncionalidadeDAO(GetSqlCommand());
                lstFuncionalidade = funcionalidadeDao.Selecionar(funcionalidadeVo, top);

                if (detalhar)
                {
                    foreach (var funcionalidade in lstFuncionalidade)
                    {
                        funcionalidade.SubModulo = subModuloBE.Consultar(new SubmoduloVO() { Id = funcionalidade.SubModulo.Id });
                    }
                }
                return lstFuncionalidade;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public FuncionalidadeVO Consultar(FuncionalidadeVO funcionalidadeVo)
        {
            FuncionalidadeDAO funcionalidadeDao = null;
            FuncionalidadeBE funcionalidadeBE = null;
            FuncionalidadeVO funcionalidade = null;
            try
            {
                funcionalidadeBE = new FuncionalidadeBE(GetSqlCommand());
                funcionalidadeDao = new FuncionalidadeDAO(GetSqlCommand());
                funcionalidade = funcionalidadeDao.Consultar(funcionalidadeVo);
                //if (funcionalidade != null)
                //{
                //    funcionalidade.SubModulo = funcionalidadeBE.Consultar(new FuncionalidadeVO() { Id = funcionalidade.SubModulo.Id });
                // }

                return funcionalidade;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public List<FuncionalidadeVO> Listar(FuncionalidadeVO funcionalidadeVo = null, bool detalhar = false)
        {
            FuncionalidadeDAO funcionalidadeDao = null;
            SubmoduloBE subModuloBE = null;
            List<FuncionalidadeVO> lstFuncionalidade = null;
            try
            {
                subModuloBE = new SubmoduloBE(GetSqlCommand());
                funcionalidadeDao = new FuncionalidadeDAO(GetSqlCommand());
                lstFuncionalidade = funcionalidadeDao.Listar(funcionalidadeVo);
                if (detalhar)
                {
                    foreach (var funcionalidade in lstFuncionalidade)
                    {
                        funcionalidade.SubModulo = subModuloBE.Consultar(new SubmoduloVO() { Id = funcionalidade.SubModulo.Id });
                    }
                }
                return lstFuncionalidade;
            }
            catch (Exception e)
            {
                throw e;
            }
            //finally
            //{
            //    if (subModuloBE != null)
            //    {
            //        subModuloBE.FecharConexao();
            //        subModuloBE = null;
            //    }
            //}
        }

        public Dictionary<int, List<FuncionalidadeVO>> Paginar(FuncionalidadeVO objVO)
        {
            FuncionalidadeDAO funcionalidadeDao = null;
            try
            {
                funcionalidadeDao = new FuncionalidadeDAO(GetSqlCommand());
                return funcionalidadeDao.Paginar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}