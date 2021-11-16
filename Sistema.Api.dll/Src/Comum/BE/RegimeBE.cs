using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.DAO;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Comum.BE
{
    public class RegimeBE : AbstractBE, IBE<RegimeVO>
    {
        public RegimeBE()
            : base()
        {
        }

        public RegimeBE(SqlCommand sqlConn)
            : base(sqlConn)
        {
        }

        public long Inserir(RegimeVO objVO)
        {
            RegimeDAO regimeDAO = null;
            try
            {
                BeginTransaction();
                regimeDAO = new RegimeDAO(GetSqlCommand());
                long id = regimeDAO.Inserir(objVO);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public long Alterar(RegimeVO objVO, string where = null)
        {
            RegimeDAO regimeDAO = null;
            try
            {
                long id;
                BeginTransaction();
                regimeDAO = new RegimeDAO(GetSqlCommand());
                id = regimeDAO.Alterar(objVO, where);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public void Deletar(RegimeVO objVO)
        {
            RegimeDAO regimeDAO = null;
            try
            {
                BeginTransaction();
                regimeDAO = new RegimeDAO(GetSqlCommand());
                regimeDAO.Deletar(objVO);
                Commit();
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }


        public List<RegimeVO> Selecionar(RegimeVO objVO, int top = 0, bool detalhar = false)
        {
            RegimeDAO regimeDao = null;
            try
            {
                regimeDao = new RegimeDAO(GetSqlCommand());
                return regimeDao.Selecionar(objVO, top);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public RegimeVO Consultar(RegimeVO objVO)
        {
            RegimeDAO regimeDao = null;
            try
            {
                regimeDao = new RegimeDAO(GetSqlCommand());
                return regimeDao.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Dictionary<int, List<RegimeVO>> Paginar(string sql, int inicio, int fim)
        {
            RegimeDAO regimeDAO = null;

            try
            {
                regimeDAO = new RegimeDAO(GetSqlCommand());
                return regimeDAO.Paginar(sql, inicio, fim);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<RegimeVO> Listar(RegimeVO objVO = null, bool detalhar = false)
        {
            RegimeDAO regimeDao = null;
            try
            {
                regimeDao = new RegimeDAO(GetSqlCommand());
                return regimeDao.Listar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}