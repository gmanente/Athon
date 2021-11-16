using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.DAO;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Comum.BE
{
    public class EscolaridadeBE : AbstractBE, IBE<EscolaridadeVO>
    {
        public EscolaridadeBE(SqlCommand sqlConn)
            : base(sqlConn)
        {
        }

        public EscolaridadeBE()
            : base()
        {
        }

        public long Alterar(EscolaridadeVO objVO, string where = null)
        {
            EscolaridadeDAO EscolaridadeDAO = null;
            try
            {
                long id;
                BeginTransaction();
                EscolaridadeDAO = new EscolaridadeDAO(GetSqlCommand());
                id = EscolaridadeDAO.Alterar(objVO, where);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public void Deletar(EscolaridadeVO objVO)
        {
            EscolaridadeDAO EscolaridadeDAO = null;
            try
            {
                BeginTransaction();
                EscolaridadeDAO = new EscolaridadeDAO(GetSqlCommand());
                EscolaridadeDAO.Deletar(objVO);
                Commit();
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public long Inserir(EscolaridadeVO objVO)
        {
            EscolaridadeDAO EscolaridadeDAO = null;
            try
            {
                BeginTransaction();
                EscolaridadeDAO = new EscolaridadeDAO(GetSqlCommand());
                long id = EscolaridadeDAO.Inserir(objVO);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public List<EscolaridadeVO> Selecionar(EscolaridadeVO objVO, int top = 0, bool detalhar = false)
        {
            EscolaridadeDAO EscolaridadeDAO = null;
            try
            {
                EscolaridadeDAO = new EscolaridadeDAO(GetSqlCommand());
                return EscolaridadeDAO.Selecionar(objVO, top);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public EscolaridadeVO Consultar(EscolaridadeVO objVO)
        {
            EscolaridadeDAO EscolaridadeDAO = null;
            try
            {
                EscolaridadeDAO = new EscolaridadeDAO(GetSqlCommand());
                return EscolaridadeDAO.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Dictionary<int, List<EscolaridadeVO>> Paginar(string sql, int inicio, int fim)
        {
            EscolaridadeDAO EscolaridadeDAO = null;

            try
            {
                EscolaridadeDAO = new EscolaridadeDAO(GetSqlCommand());
                return EscolaridadeDAO.Paginar(sql, inicio, fim);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<EscolaridadeVO> Listar(EscolaridadeVO objVO = null, bool detalhar = false)
        {
            EscolaridadeDAO EscolaridadeDAO = null;
            try
            {
                EscolaridadeDAO = new EscolaridadeDAO(GetSqlCommand());
                return EscolaridadeDAO.Listar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
