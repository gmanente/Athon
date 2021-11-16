using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Seguranca.DAO;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Seguranca.BE
{
    public class SubmoduloUrlBE : AbstractBE, IBE<SubmoduloUrlVO>
    {
        public SubmoduloUrlBE(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public SubmoduloUrlBE()
            : base()
        {
        }

        public long Inserir(SubmoduloUrlVO objVO)
        {
            SubmoduloUrlDAO SubmoduloUrlDao = null;
            try
            {
                long id;
                BeginTransaction();
                if (!SubmoduloUrlCampusExiste(objVO))
                {
                    SubmoduloUrlDao = new SubmoduloUrlDAO(GetSqlCommand());
                    id = SubmoduloUrlDao.Inserir(objVO);
                    objVO.Id = id;
                    Commit();
                }
                else
                    throw new Exception("Este SubmoduloUrl já existe");

                return id;
            }
            catch (Exception ex)
            {
                Rollback();
                throw ex;
            }
        }

        public long Alterar(SubmoduloUrlVO objVO, string where = null)
        {
            SubmoduloUrlDAO SubmoduloUrlDao = null;
            try
            {
                long id;
                BeginTransaction();
                if (!SubmoduloUrlCampusExiste(objVO))
                {
                    SubmoduloUrlDao = new SubmoduloUrlDAO(GetSqlCommand());
                    id = SubmoduloUrlDao.Alterar(objVO, where);
                    Commit();
                }
                else
                    throw new Exception("Este SubmoduloUrl já existe");

                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public void Deletar(SubmoduloUrlVO objVO)
        {
            SubmoduloUrlDAO SubmoduloUrlDao = null;
            try
            {
                BeginTransaction();
                SubmoduloUrlDao = new SubmoduloUrlDAO(GetSqlCommand());
                SubmoduloUrlDao.Deletar(objVO);
                Commit();
            }
            catch (Exception exception)
            {
                Rollback();
                throw exception;
            }
        }

        public List<SubmoduloUrlVO> Selecionar(SubmoduloUrlVO objVO, int top = 0, bool detalhar = false)
        {
            SubmoduloUrlDAO SubmoduloUrlDao = null;
            try
            {
                SubmoduloUrlDao = new SubmoduloUrlDAO(GetSqlCommand());
                return SubmoduloUrlDao.Selecionar(objVO, top);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public SubmoduloUrlVO Consultar(SubmoduloUrlVO objVO)
        {
            SubmoduloUrlDAO SubmoduloUrlDao = null;
            try
            {
                SubmoduloUrlDao = new SubmoduloUrlDAO(GetSqlCommand());
                return SubmoduloUrlDao.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<SubmoduloUrlVO> Listar(SubmoduloUrlVO objVO = null, bool detalhar = false)
        {
            SubmoduloUrlDAO SubmoduloUrlDao = null;
            try
            {
                SubmoduloUrlDao = new SubmoduloUrlDAO(GetSqlCommand());
                return SubmoduloUrlDao.Listar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Dictionary<int, List<SubmoduloUrlVO>> Paginar(SubmoduloUrlVO objVO)
        {
            SubmoduloUrlDAO SubmoduloUrlDAO = null;
            try
            {
                SubmoduloUrlDAO = new SubmoduloUrlDAO(GetSqlCommand());
                return SubmoduloUrlDAO.Paginar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // SubmoduloUrlCampusExiste
        public bool SubmoduloUrlCampusExiste(SubmoduloUrlVO objVO)
        {
            SubmoduloUrlDAO SubmoduloUrlDao = null;
            try
            {
                SubmoduloUrlDao = new SubmoduloUrlDAO(GetSqlCommand());
                return SubmoduloUrlDao.Consultar(objVO) != null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}