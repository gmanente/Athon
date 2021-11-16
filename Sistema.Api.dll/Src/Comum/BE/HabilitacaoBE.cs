using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.DAO;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Comum.BE
{
    public class HabilitacaoBE : AbstractBE, IBE<HabilitacaoVO>
    {
        public HabilitacaoBE()
            : base()
        {
        }

        public HabilitacaoBE(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public long Inserir(HabilitacaoVO objVO)
        {
            HabilitacaoDAO habilitacaoDAO = null;
            try
            {
                long id;
                BeginTransaction();
                habilitacaoDAO = new HabilitacaoDAO(GetSqlCommand());
                id = habilitacaoDAO.Inserir(objVO);
                Commit();
                return id;
            }
            catch (Exception ex)
            {
                Rollback();
                throw ex;
            }
        }

        public long Alterar(HabilitacaoVO objVO, string where = null)
        {
            HabilitacaoDAO habilitacaoDAO = null;
            try
            {
                long id;
                BeginTransaction();
                habilitacaoDAO = new HabilitacaoDAO(GetSqlCommand());
                id = habilitacaoDAO.Alterar(objVO, where);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public void Deletar(HabilitacaoVO objVO)
        {
            HabilitacaoDAO habilitacaoDAO = null;
            try
            {
                BeginTransaction();
                habilitacaoDAO = new HabilitacaoDAO(GetSqlCommand());
                habilitacaoDAO.Deletar(objVO);
                Commit();
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }


        public List<HabilitacaoVO> Selecionar(HabilitacaoVO objVO, int top = 0, bool detalhar = false)
        {
            HabilitacaoDAO habilitacaoDao = null;
            try
            {
                habilitacaoDao = new HabilitacaoDAO(GetSqlCommand());
                return habilitacaoDao.Selecionar(objVO, top);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public HabilitacaoVO Consultar(HabilitacaoVO objVO)
        {
            HabilitacaoDAO habilitacaoDao = null;
            try
            {
                habilitacaoDao = new HabilitacaoDAO(GetSqlCommand());
                return habilitacaoDao.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<HabilitacaoVO> Listar(HabilitacaoVO objVO = null, bool detalhar = false)
        {
            HabilitacaoDAO habilitacaoDao = null;
            try
            {
                habilitacaoDao = new HabilitacaoDAO(GetSqlCommand());
                return habilitacaoDao.Listar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Dictionary<int, List<HabilitacaoVO>> Paginar(string sql, int inicio, int fim)
        {
            HabilitacaoDAO habilitacaoDao = null;

            try
            {
                habilitacaoDao = new HabilitacaoDAO(GetSqlCommand());
                return habilitacaoDao.Paginar(sql, inicio, fim);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}