using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.DAO;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Comum.BE
{
    public class CorBE : AbstractBE, IBE<CorVO>
    {
        public CorBE(SqlCommand sqlConn)
            : base(sqlConn)
        {
        }

        public CorBE()
            : base()
        {
        }

        public long Alterar(CorVO objVO, string where = null)
        {
            CorDAO corDAO = null;
            try
            {
                long id;
                BeginTransaction();
                corDAO = new CorDAO(GetSqlCommand());
                id = corDAO.Alterar(objVO, where);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public void Deletar(CorVO objVO)
        {
            CorDAO corDAO = null;
            try
            {
                BeginTransaction();
                corDAO = new CorDAO(GetSqlCommand());
                corDAO.Deletar(objVO);
                Commit();
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public long Inserir(CorVO objVO)
        {
            CorDAO corDAO = null;
            try
            {
                BeginTransaction();
                corDAO = new CorDAO(GetSqlCommand());
                long id = corDAO.Inserir(objVO);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public List<CorVO> Selecionar(CorVO objVO, int top = 0, bool detalhar = false)
        {
            CorDAO corDAO = null;
            try
            {
                corDAO = new CorDAO(GetSqlCommand());
                return corDAO.Selecionar(objVO, top);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public CorVO Consultar(CorVO objVO)
        {
            CorDAO corDAO = null;
            try
            {
                corDAO = new CorDAO(GetSqlCommand());
                return corDAO.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Dictionary<int, List<CorVO>> Paginar(string sql, int inicio, int fim)
        {
            CorDAO corDAO = null;

            try
            {
                corDAO = new CorDAO(GetSqlCommand());
                return corDAO.Paginar(sql, inicio, fim);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CorVO> Listar(CorVO objVO = null, bool detalhar = false)
        {
            CorDAO corDAO = null;
            try
            {
                corDAO = new CorDAO(GetSqlCommand());
                return corDAO.Listar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
