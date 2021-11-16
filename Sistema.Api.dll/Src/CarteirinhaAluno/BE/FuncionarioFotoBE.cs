using Sistema.Api.dll.Src.CarteirinhaAluno.DAO;
using Sistema.Api.dll.Src.CarteirinhaAluno.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.CarteirinhaAluno.BE
{
    public class FuncionarioFotoBE : AbstractBE
    {
        public FuncionarioFotoBE(SqlCommand sqlComm)
            : base(sqlComm)
        { }

        public FuncionarioFotoBE()
            : base()
        { }

        public long Inserir(FuncionarioFotoVO objVO)
        {
            try
            {
                long id;

                BeginTransaction();

                var funcionarioFotoDAO = new FuncionarioFotoDAO(GetSqlCommand());
                id = funcionarioFotoDAO.Inserir(objVO);

                Commit();

                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public long Alterar(FuncionarioFotoVO objVO, string where = null)
        {
            try
            {
                long id;

                BeginTransaction();

                var funcionarioFotoDAO = new FuncionarioFotoDAO(GetSqlCommand());
                id = funcionarioFotoDAO.Alterar(objVO, where);

                Commit();

                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public void Deletar(FuncionarioFotoVO objVO)
        {
            try
            {
                BeginTransaction();

                var funcionarioFotoDAO = new FuncionarioFotoDAO(GetSqlCommand());
                funcionarioFotoDAO.Deletar(objVO);

                Commit();
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }


        public long AlterarDados(FuncionarioFotoVO objVO, string where = null)
        {
            try
            {
                long id;

                BeginTransaction();

                var funcionarioFotoDAO = new FuncionarioFotoDAO(GetSqlCommand());
                id = funcionarioFotoDAO.AlterarDados(objVO, where);

                Commit();

                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }



        public List<FuncionarioFotoVO> Selecionar(FuncionarioFotoVO objVO, int top = 0, bool detalhar = false)
        {
            try
            {
                var funcionarioFotoDAO = new FuncionarioFotoDAO(GetSqlCommand());
                return funcionarioFotoDAO.Selecionar(objVO, top);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<FuncionarioFotoVO> Listar(FuncionarioFotoVO objVO = null, bool detalhar = false)
        {
            try
            {
                var funcionarioFotoDAO = new FuncionarioFotoDAO(GetSqlCommand());
                return funcionarioFotoDAO.Listar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public FuncionarioFotoVO Consultar(FuncionarioFotoVO objVO)
        {
            try
            {
                var funcionarioFotoDAO = new FuncionarioFotoDAO(GetSqlCommand());
                return funcionarioFotoDAO.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        public List<FuncionarioFotoVO> ConsultarPorCpfCarteirinha(string cpf)
        {
            try
            {
                var funcionarioFotoDAO = new FuncionarioFotoDAO(GetSqlCommand());

                var paramFuncionarioSituacao = Dominio.GetParametro("SituacaoFuncionarioCarteirinha").Valor;

                return funcionarioFotoDAO.ConsultarPorCpfCarteirinha(cpf, paramFuncionarioSituacao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FuncionarioFotoVO> ConsultarFuncionarioFoto(string instrucaoSql)
        {
            try
            {
                var funcionarioFotoDAO = new FuncionarioFotoDAO(GetSqlCommand());

                return funcionarioFotoDAO.Selecionar(instrucaoSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<FuncionarioFotoVO> ListarCargos(FuncionarioFotoVO objVO = null)
        {
            try
            {
                var funcionarioFotoDAO = new FuncionarioFotoDAO(GetSqlCommand());
                return funcionarioFotoDAO.ListarCargos(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }



    }
}
