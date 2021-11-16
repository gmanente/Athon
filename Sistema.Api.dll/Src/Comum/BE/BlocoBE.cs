using Sistema.Api.dll.Src.Comum.DAO;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Comum.BE
{
    public class BlocoBE : AbstractBE
    {
        public BlocoBE()
            : base()
        {
        }

        public BlocoBE(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public long Inserir(BlocoVO objVO)
        {
            BlocoDAO blocoDao = null;
            try
            {
                long id;
                BeginTransaction();
                blocoDao = new BlocoDAO(GetSqlCommand());
                id = blocoDao.Inserir(objVO);
                objVO.Id = id;
                Commit();
                return id;
            }
            catch (Exception ex)
            {
                Rollback();
                throw ex;
            }
        }

        public long Alterar(BlocoVO objVO, string where = null)
        {
            BlocoDAO blocoDao = null;
            try
            {
                long id;
                BeginTransaction();
                blocoDao = new BlocoDAO(GetSqlCommand());
                id = blocoDao.Alterar(objVO, where);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public void Deletar(BlocoVO objVO)
        {
            BlocoDAO blocoDao = null;
            try
            {
                BeginTransaction();
                blocoDao = new BlocoDAO(GetSqlCommand());
                blocoDao.Deletar(objVO);
                Commit();
            }
            catch (Exception exception)
            {
                Rollback();
                throw exception;
            }
        }

        public BlocoVO Consultar(BlocoVO objVO)
        {
            BlocoDAO blocoDao = null;
            try
            {
                blocoDao = new BlocoDAO(GetSqlCommand());
                return blocoDao.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<BlocoVO> Listar(BlocoVO objVO = null, bool detalhar = false)
        {
            BlocoDAO blocoDao = null;
            try
            {
                blocoDao = new BlocoDAO(GetSqlCommand());
                return blocoDao.Listar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}