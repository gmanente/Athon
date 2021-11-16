using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Seguranca.DAO;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Seguranca.BE
{
    public class PerfilBE : AbstractBE, IBE<PerfilVO>
    {
        public PerfilBE(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public PerfilBE()
            : base()
        {
        }

        public long Inserir(PerfilVO objVO)
        {
            PerfilDAO perfilDao = null;
            try
            {
                long id;
                BeginTransaction();
                perfilDao = new PerfilDAO(GetSqlCommand());
                id = perfilDao.Inserir(objVO);
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

        public long Alterar(PerfilVO objVO, string where = null)
        {
            PerfilDAO perfilDao = null;
            try
            {
                long id;
                BeginTransaction();
                perfilDao = new PerfilDAO(GetSqlCommand());
                id = perfilDao.Alterar(objVO, where);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public void Deletar(PerfilVO objVO)
        {
            PerfilDAO perfilDao = null;
            try
            {
                BeginTransaction();
                perfilDao = new PerfilDAO(GetSqlCommand());
                perfilDao.Deletar(objVO);
                Commit();
            }
            catch (Exception exception)
            {
                Rollback();
                throw exception;
            }
        }

        public List<PerfilVO> Selecionar(PerfilVO objVO, int top = 0, bool detalhar = false)
        {
            PerfilDAO perfilDao = null;
            try
            {
                perfilDao = new PerfilDAO(GetSqlCommand());
                return perfilDao.Selecionar(objVO, top);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public PerfilVO Consultar(PerfilVO objVO)
        {
            PerfilDAO perfilDao = null;
            try
            {
                perfilDao = new PerfilDAO(GetSqlCommand());
                return perfilDao.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<PerfilVO> Listar(PerfilVO objVO = null, bool detalhar = false)
        {
            PerfilDAO perfilDao = null;
            try
            {
                perfilDao = new PerfilDAO(GetSqlCommand());
                return perfilDao.Listar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Dictionary<int, List<PerfilVO>> Paginar(string sql, int inicio, int fim)
        {
            PerfilDAO perfilDao = null;
            try
            {
                perfilDao = new PerfilDAO(GetSqlCommand());
                return perfilDao.Paginar(sql, inicio, fim);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}