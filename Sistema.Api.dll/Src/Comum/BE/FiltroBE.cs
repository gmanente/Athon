using Sistema.Api.dll.Src.Comum.DAO;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Comum.BE
{
    public class FiltroBE : AbstractBE//, IBE<ConsultaVO>
    {
        public FiltroBE(SqlCommand sqlComm)
            : base(sqlComm)
        {}

        public FiltroBE()
            : base()
        {}

        public long Inserir(FiltroVO objVO)
        {
            try
            {
                long id;

                BeginTransaction();

                var filtroDAO = new FiltroDAO(GetSqlCommand());
                id = filtroDAO.Inserir(objVO);

                Commit();

                return id;
            }
            catch (Exception ex)
            {
                Rollback();
                throw ex;
            }
        }

        public long Alterar(FiltroVO objVO)
        {
            try
            {
                long id;

                BeginTransaction();

                var filtroDAO = new FiltroDAO(GetSqlCommand());
                id = filtroDAO.Alterar(objVO);

                Commit();

                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public void Deletar(FiltroVO objVO)
        {
            try
            {
                BeginTransaction();

                var filtroDAO = new FiltroDAO(GetSqlCommand());
                filtroDAO.Deletar(objVO);

                Commit();
            }
            catch (Exception exception)
            {
                Rollback();
                throw exception;
            }
        }


        // InserirFiltroCampo
        public long InserirFiltroCampo(FiltroCampoVO objVO)
        {
            try
            {
                long id;

                BeginTransaction();

                var filtroDAO = new FiltroDAO(GetSqlCommand());
                id = filtroDAO.InserirFiltroCampo(objVO);

                Commit();

                return id;
            }
            catch (Exception ex)
            {
                Rollback();
                throw ex;
            }
        }

        // AlterarFiltroCampo
        public long AlterarFiltroCampo(FiltroCampoVO objVO)
        {
            try
            {
                long id;

                BeginTransaction();

                var filtroDAO = new FiltroDAO(GetSqlCommand());
                id = filtroDAO.AlterarFiltroCampo(objVO);

                Commit();

                return id;
            }
            catch (Exception ex)
            {
                Rollback();
                throw ex;
            }
        }

        // DeletarFiltroCampo
        public void DeletarFiltroCampo(FiltroCampoVO objVO)
        {
            try
            {
                BeginTransaction();

                var filtroDAO = new FiltroDAO(GetSqlCommand());
                filtroDAO.DeletarFiltroCampo(objVO);

                Commit();
            }
            catch (Exception exception)
            {
                Rollback();
                throw exception;
            }
        }


        // AlterarInstrucaoSQL
        public void AlterarInstrucaoSQL(long idFiltro, string InstrucaoSQL)
        {
            try
            {
                BeginTransaction();

                var filtroDAO = new FiltroDAO(GetSqlCommand());
                filtroDAO.AlterarInstrucaoSQL(idFiltro, InstrucaoSQL);

                Commit();
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        // AtualizarSequence
        public void AtualizarSequence()
        {
            try
            {
                BeginTransaction();

                var filtroDAO = new FiltroDAO(GetSqlCommand());
                filtroDAO.AtualizarSequence();

                Commit();
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }


        public List<ConsultaVO> Selecionar(ConsultaVO objVO, int top = 0, bool detalhar = false)
        {
            try
            {
                var filtroDAO = new FiltroDAO(GetSqlCommand());
                return filtroDAO.Selecionar(objVO, top);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ConsultaVO> Listar(ConsultaVO objVO, bool detalhar = false)
        {
            try
            {
                var filtroDAO = new FiltroDAO(GetSqlCommand());
                return filtroDAO.Listar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ConsultaVO Consultar(ConsultaVO objVO)
        {
            try
            {
                var filtroDAO = new FiltroDAO(GetSqlCommand());
                return filtroDAO.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<FiltroVO> SelecionarFiltro(FiltroVO objVO, int top = 0, bool detalhar = false)
        {
            try
            {
                var filtroDAO = new FiltroDAO(GetSqlCommand());
                return filtroDAO.SelecionarFiltro(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public FiltroVO ConsultarFiltro(FiltroVO objVO)
        {
            try
            {
                var filtroDAO = new FiltroDAO(GetSqlCommand());
                return filtroDAO.ConsultarFiltro(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public ConsultaVO ConsultarSubmoduloUrl(ConsultaVO objVO)
        {
            try
            {
                var filtroDAO = new FiltroDAO(GetSqlCommand());
                return filtroDAO.ConsultarSubmoduloUrl(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public Dictionary<int, List<FiltroVO>> Paginar(FiltroVO objVO)
        {
            try
            {
                var filtroDAO = new FiltroDAO(GetSqlCommand());
                return filtroDAO.Paginar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}