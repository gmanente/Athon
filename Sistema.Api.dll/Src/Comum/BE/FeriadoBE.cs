using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.DAO;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Comum.BE
{
    public class FeriadoBE : AbstractBE, IBE<FeriadoVO>
    {
        public FeriadoBE(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public FeriadoBE()
            : base()
        {
        }

        /// <summary>
        /// Autor: Evander Costa
        /// Data: 10.03.2015
        /// Descrição: Responsável por inserir o feriado.
        /// </summary>
        /// <param name="objVO"></param>
        /// <returns></returns>
        public long Inserir(FeriadoVO objVO)
        {
            FeriadoDAO feriadoDao = null;
            CampusDAO campusDAO = null;

            try
            {
                long id = 0;

                BeginTransaction();

                feriadoDao = new FeriadoDAO(GetSqlCommand());
                campusDAO = new CampusDAO(GetSqlCommand());

                if (objVO.Campus.Id == 0)
                {
                    var lstCampus = campusDAO.Listar();
                    foreach (var item in lstCampus)
                    {
                        objVO.Campus.Id = item.Id;
                        id = feriadoDao.Inserir(objVO);
                    }
                }
                else
                {
                    id = feriadoDao.Inserir(objVO);
                }

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

        public long Alterar(FeriadoVO objVO, string where = null)
        {
            FeriadoDAO feriadoDao = null;
            try
            {
                long id;

                BeginTransaction();

                feriadoDao = new FeriadoDAO(GetSqlCommand());

                id = feriadoDao.Alterar(objVO, where);

                Commit();

                return id;
            }
            catch (Exception e)
            {
                Rollback();

                throw e;
            }
        }

        public void Deletar(FeriadoVO objVO)
        {
            FeriadoDAO feriadoDao = null;

            try
            {
                BeginTransaction();

                feriadoDao = new FeriadoDAO(GetSqlCommand());

                feriadoDao.Deletar(objVO);

                Commit();
            }
            catch (Exception exception)
            {
                Rollback();

                throw exception;
            }
        }


        public List<FeriadoVO> Selecionar(FeriadoVO objVO, int top = 0, bool detalhar = false)
        {
            FeriadoDAO feriadoDAO = null;

            try
            {
                feriadoDAO = new FeriadoDAO(GetSqlCommand());

                return feriadoDAO.Selecionar(objVO, top);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<FeriadoVO> Listar(FeriadoVO objVO, bool detalhar = false)
        {
            FeriadoDAO feriadoDAO = null;

            try
            {
                feriadoDAO = new FeriadoDAO(GetSqlCommand());

                return feriadoDAO.Listar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public FeriadoVO Consultar(FeriadoVO objVO)
        {
            FeriadoDAO feriadoDAO = null;

            try
            {
                feriadoDAO = new FeriadoDAO(GetSqlCommand());

                return feriadoDAO.Consultar(objVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Dictionary<int, List<FeriadoVO>> Paginar(string sql, int inicio, int fim)
        {
            FeriadoDAO feriadoDAO = null;

            try
            {
                feriadoDAO = new FeriadoDAO(GetSqlCommand());

                return feriadoDAO.Paginar(sql, inicio, fim);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}