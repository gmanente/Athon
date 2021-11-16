using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.DAO;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Comum.BE
{
    public class EstadoCivilBE : AbstractBE, IBE<EstadoCivilVO>
    {
        public EstadoCivilBE(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public EstadoCivilBE()
            : base()
        {
        }

        public long Inserir(EstadoCivilVO objVO)
        {
            EstadoCivilDAO estadoCivilDao = null;
            try
            {
                long id;
                BeginTransaction();
                estadoCivilDao = new EstadoCivilDAO(GetSqlCommand());
                id = estadoCivilDao.Inserir(objVO);
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

        public long Alterar(EstadoCivilVO objVO, string where = null)
        {
            EstadoCivilDAO estadoCivilDao = null;
            try
            {
                long id;
                BeginTransaction();
                estadoCivilDao = new EstadoCivilDAO(GetSqlCommand());
                id = estadoCivilDao.Alterar(objVO, where);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }


        public void Deletar(EstadoCivilVO objVO)
        {
            EstadoCivilDAO estadoCivilDao = null;
            try
            {
                BeginTransaction();
                estadoCivilDao = new EstadoCivilDAO(GetSqlCommand());
                estadoCivilDao.Deletar(objVO);
                Commit();
            }
            catch (Exception exception)
            {
                Rollback();
                throw exception;
            }
        }


        /// <summary>
        /// Selecionar
        /// </summary>
        /// <param name="objVO"></param>
        /// <param name="top"></param>
        /// <param name="detalhar"></param>
        /// <returns></returns>
        public List<EstadoCivilVO> Selecionar(EstadoCivilVO objVO, int top = 0, bool detalhar = false)
        {
            try
            {
                var dao = new EstadoCivilDAO(GetSqlCommand());

                return dao.Selecionar(objVO, top);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Consultar
        /// </summary>
        /// <param name="objVO"></param>
        /// <returns></returns>
        public EstadoCivilVO Consultar(EstadoCivilVO objVO)
        {
            try
            {
                var dao = new EstadoCivilDAO(GetSqlCommand());

                return dao.Consultar(objVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Listar
        /// </summary>
        /// <param name="objVO"></param>
        /// <param name="detalhar"></param>
        /// <returns></returns>
        public List<EstadoCivilVO> Listar(EstadoCivilVO objVO = null, bool detalhar = false)
        {
            try
            {
                var dao = new EstadoCivilDAO(GetSqlCommand());

                return dao.Listar(objVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// paginar
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="inicio"></param>
        /// <param name="fim"></param>
        /// <returns></returns>
        public Dictionary<int, List<EstadoCivilVO>> Paginar(string sql, int inicio, int fim)
        {
            try
            {
                var dao = new EstadoCivilDAO(GetSqlCommand());

                return dao.Paginar(sql, inicio, fim);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}