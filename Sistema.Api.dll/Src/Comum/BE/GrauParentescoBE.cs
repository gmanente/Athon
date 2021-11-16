using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.DAO;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Comum.BE
{
    public class GrauParentescoBE : AbstractBE, IBE<GrauParentescoVO>
    {
        public GrauParentescoBE(SqlCommand sqlConn)
            : base(sqlConn)
        {
        }

        public GrauParentescoBE()
            : base()
        {
        }

        public long Alterar(GrauParentescoVO objVO, string where = null)
        {
            GrauParentescoDAO GrauParentescoDAO = null;
            try
            {
                long id;
                BeginTransaction();
                GrauParentescoDAO = new GrauParentescoDAO(GetSqlCommand());
                id = GrauParentescoDAO.Alterar(objVO, where);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public void Deletar(GrauParentescoVO objVO)
        {
            GrauParentescoDAO GrauParentescoDAO = null;
            try
            {
                BeginTransaction();
                GrauParentescoDAO = new GrauParentescoDAO(GetSqlCommand());
                GrauParentescoDAO.Deletar(objVO);
                Commit();
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public long Inserir(GrauParentescoVO objVO)
        {
            GrauParentescoDAO GrauParentescoDAO = null;
            try
            {
                BeginTransaction();
                GrauParentescoDAO = new GrauParentescoDAO(GetSqlCommand());
                long id = GrauParentescoDAO.Inserir(objVO);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public List<GrauParentescoVO> Selecionar(GrauParentescoVO objVO, int top = 0, bool detalhar = false)
        {
            GrauParentescoDAO GrauParentescoDAO = null;
            try
            {
                GrauParentescoDAO = new GrauParentescoDAO(GetSqlCommand());
                return GrauParentescoDAO.Selecionar(objVO, top);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public GrauParentescoVO Consultar(GrauParentescoVO objVO)
        {
            GrauParentescoDAO GrauParentescoDAO = null;
            try
            {
                GrauParentescoDAO = new GrauParentescoDAO(GetSqlCommand());
                return GrauParentescoDAO.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Dictionary<int, List<GrauParentescoVO>> Paginar(string sql, int inicio, int fim)
        {
            GrauParentescoDAO GrauParentescoDAO = null;

            try
            {
                GrauParentescoDAO = new GrauParentescoDAO(GetSqlCommand());
                return GrauParentescoDAO.Paginar(sql, inicio, fim);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<GrauParentescoVO> Listar(GrauParentescoVO objVO = null, bool detalhar = false)
        {
            GrauParentescoDAO GrauParentescoDAO = null;
            try
            {
                GrauParentescoDAO = new GrauParentescoDAO(GetSqlCommand());
                return GrauParentescoDAO.Listar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
