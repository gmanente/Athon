using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.DAO;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Comum.BE
{
    public class FeriadoTipoBE : AbstractBE, IBE<FeriadoTipoVO>
    {
        public FeriadoTipoBE(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public FeriadoTipoBE()
            : base()
        {
        }

        /// <summary>
        /// Autor: Evander Costa
        /// Data: 10.03.2015
        /// Descrição: Responsável por inserir o tipo de feriado.
        /// </summary>
        /// <param name="objVO"></param>
        /// <returns></returns>
        public long Inserir(FeriadoTipoVO objVO)
        {
            FeriadoTipoDAO feriadoTipoDAO = null;

            try
            {
                long id;

                BeginTransaction();

                feriadoTipoDAO = new FeriadoTipoDAO(GetSqlCommand());

                id = feriadoTipoDAO.Inserir(objVO);

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

        public long Alterar(FeriadoTipoVO objVO, string where = null)
        {
            FeriadoTipoDAO feriadoTipoDAO = null;
            try
            {
                long id;

                BeginTransaction();

                feriadoTipoDAO = new FeriadoTipoDAO(GetSqlCommand());

                id = feriadoTipoDAO.Alterar(objVO, where);

                Commit();

                return id;
            }
            catch (Exception e)
            {
                Rollback();

                throw e;
            }
        }

        public void Deletar(FeriadoTipoVO objVO)
        {
            FeriadoTipoDAO feriadoTipoDAO = null;

            try
            {
                BeginTransaction();

                feriadoTipoDAO = new FeriadoTipoDAO(GetSqlCommand());

                feriadoTipoDAO.Deletar(objVO);

                Commit();
            }
            catch (Exception exception)
            {
                Rollback();

                throw exception;
            }
        }


        public List<FeriadoTipoVO> Selecionar(FeriadoTipoVO objVO, int top = 0, bool detalhar = false)
        {
            FeriadoTipoDAO feriadoTipoDAO = null;

            try
            {
                feriadoTipoDAO = new FeriadoTipoDAO(GetSqlCommand());

                return feriadoTipoDAO.Selecionar(objVO, top);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<FeriadoTipoVO> Listar(FeriadoTipoVO objVO, bool detalhar = false)
        {
            FeriadoTipoDAO feriadoTipoDAO = null;

            try
            {
                feriadoTipoDAO = new FeriadoTipoDAO(GetSqlCommand());

                return feriadoTipoDAO.Listar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public FeriadoTipoVO Consultar(FeriadoTipoVO objVO)
        {
            FeriadoTipoDAO feriadoTipoDAO = null;

            try
            {
                feriadoTipoDAO = new FeriadoTipoDAO(GetSqlCommand());

                return feriadoTipoDAO.Consultar(objVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Dictionary<int, List<FeriadoTipoVO>> Paginar(string sql, int inicio, int fim)
        {
            FeriadoTipoDAO feriadoTipoDAO = null;

            try
            {
                feriadoTipoDAO = new FeriadoTipoDAO(GetSqlCommand());

                return feriadoTipoDAO.Paginar(sql, inicio, fim);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}