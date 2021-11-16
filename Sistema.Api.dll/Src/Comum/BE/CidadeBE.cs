using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.DAO;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Comum.BE
{
    public class CidadeBE : AbstractBE, IBE<CidadeVO>
    {
        public CidadeBE(SqlCommand sqlConn)
            : base(sqlConn)
        {
        }

        public CidadeBE()
            : base()
        {
        }

        public long Alterar(CidadeVO objVO, string where = null)
        {
            CidadeDAO cidadeDAO = null;
            try
            {
                long id;
                BeginTransaction();
                cidadeDAO = new CidadeDAO(GetSqlCommand());
                id = cidadeDAO.Alterar(objVO, where);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public void Deletar(CidadeVO objVO)
        {
            CidadeDAO cidadeDAO = null;
            try
            {
                BeginTransaction();
                cidadeDAO = new CidadeDAO(GetSqlCommand());
                cidadeDAO.Deletar(objVO);
                Commit();
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public long Inserir(CidadeVO objVO)
        {
            CidadeDAO cidadeDAO = null;
            try
            {
                BeginTransaction();
                cidadeDAO = new CidadeDAO(GetSqlCommand());
                long id = cidadeDAO.Inserir(objVO);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public List<CidadeVO> Selecionar(CidadeVO objVO, int top = 0, bool detalhar = false)
        {
            CidadeDAO cidadeDAO = null;
            try
            {
                cidadeDAO = new CidadeDAO(GetSqlCommand());
                return cidadeDAO.Selecionar(objVO, top);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public CidadeVO Consultar(CidadeVO objVO)
        {
            CidadeDAO cidadeDAO = null;
            try
            {
                cidadeDAO = new CidadeDAO(GetSqlCommand());
                return cidadeDAO.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Dictionary<int, List<CidadeVO>> Paginar(string sql, int inicio, int fim)
        {
            CidadeDAO cidadeDAO = null;

            try
            {
                cidadeDAO = new CidadeDAO(GetSqlCommand());
                return cidadeDAO.Paginar(sql, inicio, fim);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CidadeVO> Listar(CidadeVO objVO = null, bool detalhar = false)
        {
            CidadeDAO cidadeDAO = null;
            try
            {
                cidadeDAO = new CidadeDAO(GetSqlCommand());
                return cidadeDAO.Listar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
