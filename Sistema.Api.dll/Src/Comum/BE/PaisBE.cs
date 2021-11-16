using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.DAO;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Comum.BE
{
    public class PaisBE : AbstractBE, IBE<PaisVO>
    {
        public PaisBE()
            : base()
        {
        }

        public PaisBE(SqlCommand sqlConn)
            : base(sqlConn)
        {
        }

        public long Inserir(PaisVO objVO)
        {
            PaisDAO paisDAO = null;
            try
            {
                BeginTransaction();
                paisDAO = new PaisDAO(GetSqlCommand());
                long id = paisDAO.Inserir(objVO);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public long Alterar(PaisVO objVO, string where = null)
        {
            PaisDAO paisDAO = null;
            try
            {
                long id;
                BeginTransaction();
                paisDAO = new PaisDAO(GetSqlCommand());
                id = paisDAO.Alterar(objVO, where);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public void Deletar(PaisVO objVO)
        {
            PaisDAO paisDAO = null;
            try
            {
                BeginTransaction();
                paisDAO = new PaisDAO(GetSqlCommand());
                paisDAO.Deletar(objVO);
                Commit();
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }


        public List<PaisVO> Selecionar(PaisVO objVO, int top = 0, bool detalhar = false)
        {
            PaisDAO paisDAO = null;
            try
            {
                paisDAO = new PaisDAO(GetSqlCommand());
                return paisDAO.Selecionar(objVO, top);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public PaisVO Consultar(PaisVO objVO)
        {
            PaisDAO paisDAO = null;
            try
            {
                paisDAO = new PaisDAO(GetSqlCommand());
                return paisDAO.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Dictionary<int, List<PaisVO>> Paginar(string sql, int inicio, int fim)
        {
            PaisDAO paisDAO = null;

            try
            {
                paisDAO = new PaisDAO(GetSqlCommand());
                return paisDAO.Paginar(sql, inicio, fim);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<PaisVO> Listar(PaisVO objVO = null, bool detalhar = false)
        {
            PaisDAO paisDAO = null;
            try
            {
                paisDAO = new PaisDAO(GetSqlCommand());
                return paisDAO.Listar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}