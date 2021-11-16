using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Seguranca.DAO;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Seguranca.BE
{
    public class SubmoduloBE : AbstractBE, IBE<SubmoduloVO>
    {
        public SubmoduloBE(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public SubmoduloBE()
            : base()
        {

        }

        public long Inserir(SubmoduloVO objVO)
        {
            //throw new NotImplementedException();
            SubModuloDAO subModuloDao = null;
            try
            {
                long id;
                BeginTransaction();
                subModuloDao = new SubModuloDAO(GetSqlCommand());
                id = subModuloDao.Inserir(objVO);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public long Alterar(SubmoduloVO objVO, string where = null)
        {
            SubModuloDAO subModuloDao = null;
            try
            {
                long id;
                BeginTransaction();
                subModuloDao = new SubModuloDAO(GetSqlCommand());
                id = subModuloDao.Alterar(objVO, where);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public void Deletar(SubmoduloVO objVO)
        {
            //throw new NotImplementedException();
            SubModuloDAO subModuloDao = null;
            try
            {
                BeginTransaction();
                subModuloDao = new SubModuloDAO(GetSqlCommand());
                subModuloDao.Deletar(objVO);
                Commit();
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public List<SubmoduloVO> Selecionar(SubmoduloVO subModuloVo = null, int top = 0, bool detalhar = false)
        {
            SubModuloDAO subModuloDAO = null;
            ModuloBE moduloBE = null;
            List<SubmoduloVO> lstSubModulo = null;
            try
            {
                subModuloDAO = new SubModuloDAO(GetSqlCommand());
                moduloBE = new ModuloBE(GetSqlCommand());
                lstSubModulo = subModuloDAO.Selecionar(subModuloVo, top);
                if (detalhar)
                {
                    foreach (var subModulo in lstSubModulo)
                    {
                        subModulo.Modulo = moduloBE.Consultar(new ModuloVO() { Id = subModulo.Modulo.Id });
                    }
                }
                return lstSubModulo;
            }
            catch (Exception e)
            {
                throw e;
            }
            //finally
            //{
            //    if (moduloBE != null)
            //    {
            //        moduloBE.FecharConexao();
            //        moduloBE = null;
            //    }
            //}
        }

        public SubmoduloVO Consultar(SubmoduloVO subModuloVo)
        {
            SubModuloDAO subModuloDAO = null;
            SubmoduloVO subModulo = null;

            try
            {
                subModuloDAO = new SubModuloDAO(GetSqlCommand());
                subModulo = subModuloDAO.Consultar(subModuloVo);

                return subModulo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<SubmoduloVO> Listar(SubmoduloVO subModuloVo = null, bool detalhar = false)
        {
            SubModuloDAO subModuloDAO = null;
            ModuloBE moduloBE = null;
            List<SubmoduloVO> lstSubModulo = null;
            try
            {
                subModuloDAO = new SubModuloDAO(GetSqlCommand());
                moduloBE = new ModuloBE(GetSqlCommand());
                lstSubModulo = subModuloDAO.Listar(subModuloVo);
                if (detalhar)
                {
                    foreach (var subModulo in lstSubModulo)
                    {
                        subModulo.Modulo = moduloBE.Consultar(new ModuloVO() { Id = subModulo.Modulo.Id });
                    }
                }
                return lstSubModulo;
            }
            catch (Exception e)
            {
                throw e;
            }
            //finally
            //{
            //    if (moduloBE != null)
            //    {
            //        moduloBE.FecharConexao();
            //        moduloBE = null;
            //    }
            //}
        }

        public Dictionary<int, List<SubmoduloVO>> Paginar(SubmoduloVO objVO)
        {
            SubModuloDAO subModuloDao = null;
            try
            {
                subModuloDao = new SubModuloDAO(GetSqlCommand());
                return subModuloDao.Paginar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}