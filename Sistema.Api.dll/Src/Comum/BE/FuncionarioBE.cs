using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.DAO;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Comum.BE
{
    public class FuncionarioBE : AbstractBE, IBE<FuncionarioVO>
    {
        public FuncionarioBE()
               : base()
        {
        }

        public FuncionarioBE(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public long Inserir(FuncionarioVO objVO)
        {
            FuncionarioDAO FuncionarioDao = null;
            try
            {
                long id;
                BeginTransaction();
                FuncionarioDao = new FuncionarioDAO(GetSqlCommand());
                id = FuncionarioDao.Inserir(objVO);
                Commit();
                return id;
            }
            catch (Exception ex)
            {
                Rollback();
                throw ex;
            }
        }

        public long Alterar(FuncionarioVO objVO, string where = null)
        {
            FuncionarioDAO FuncionarioDao = null;
            try
            {
                long id;
                BeginTransaction();
                FuncionarioDao = new FuncionarioDAO(GetSqlCommand());
                id = FuncionarioDao.Alterar(objVO, where);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public void Deletar(FuncionarioVO objVO)
        {
            FuncionarioDAO FuncionarioDao = null;
            try
            {
                BeginTransaction();
                FuncionarioDao = new FuncionarioDAO(GetSqlCommand());
                FuncionarioDao.Deletar(objVO);
                Commit();
            }
            catch (Exception exception)
            {
                Rollback();
                throw exception;
            }
        }

        public List<FuncionarioVO> Selecionar(FuncionarioVO objVO, int top = 0, bool detalhar = false)
        {
            FuncionarioDAO FuncionarioDao = null;
            try
            {
                FuncionarioDao = new FuncionarioDAO(GetSqlCommand());
                return FuncionarioDao.Selecionar(objVO, top);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public FuncionarioVO Consultar(FuncionarioVO objVO)
        {
            FuncionarioDAO FuncionarioDao = null;
            try
            {
                FuncionarioDao = new FuncionarioDAO(GetSqlCommand());
                return FuncionarioDao.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<FuncionarioVO> ListarSimples(FuncionarioVO objVO = null, bool detalhar = false)
        {
            FuncionarioDAO FuncionarioDao = null;
            try
            {
                FuncionarioDao = new FuncionarioDAO(GetSqlCommand());
                return FuncionarioDao.ListarSimples(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<FuncionarioVO> Listar(FuncionarioVO objVO = null, bool detalhar = false)
        {
            FuncionarioDAO FuncionarioDao = null;
            try
            {
                FuncionarioDao = new FuncionarioDAO(GetSqlCommand());
                return FuncionarioDao.Listar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<FuncionarioVO> ListarGestorContrato(FuncionarioVO objVO = null, bool detalhar = false)
        {
            FuncionarioDAO FuncionarioDao = null;
            try
            {
                FuncionarioDao = new FuncionarioDAO(GetSqlCommand());
                return FuncionarioDao.ListarGestorContrato(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}