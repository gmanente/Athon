using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.DAO;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Comum.BE
{
    public class EstadoBE : AbstractBE, IBE<EstadoVO>
    {
        public EstadoBE()
            : base()
        {
        }

        public EstadoBE(SqlCommand sqlConn)
            : base(sqlConn)
        {
        }

        public long Inserir(EstadoVO objVO)
        {
            EstadoDAO estadoDAO = null;
            try
            {
                BeginTransaction();
                estadoDAO = new EstadoDAO(GetSqlCommand());
                long id = estadoDAO.Inserir(objVO);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public long Alterar(EstadoVO objVO, string where = null)
        {
            EstadoDAO estadoDAO = null;
            try
            {
                long id;
                BeginTransaction();
                estadoDAO = new EstadoDAO(GetSqlCommand());
                id = estadoDAO.Alterar(objVO, where);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }


        /// <summary>
        /// Deletar
        /// </summary>
        /// <param name="objVO"></param>
        public void Deletar(EstadoVO objVO)
        {
            try
            {
                BeginTransaction();

                var dao = new EstadoDAO(GetSqlCommand());

                dao.Deletar(objVO);

                Commit();
            }
            catch (Exception ex)
            {
                Rollback();

                throw ex;
            }
        }


        

        /// <summary>
        /// Selecionar
        /// </summary>
        /// <param name="objVO"></param>
        /// <param name="top"></param>
        /// <param name="detalhar"></param>
        /// <returns></returns>
        public List<EstadoVO> Selecionar(EstadoVO objVO, int top = 0, bool detalhar = false)
        {
            try
            {
                var dao = new EstadoDAO(GetSqlCommand());

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
        public EstadoVO Consultar(EstadoVO objVO)
        {
            try
            {
                var dao = new EstadoDAO(GetSqlCommand());

                return dao.Consultar(objVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Paginar
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="inicio"></param>
        /// <param name="fim"></param>
        /// <returns></returns>
        public Dictionary<int, List<EstadoVO>> Paginar(string sql, int inicio, int fim)
        {
            try
            {
                var dao = new EstadoDAO(GetSqlCommand());

                return dao.Paginar(sql, inicio, fim);
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
        public List<EstadoVO> Listar(EstadoVO objVO = null, bool detalhar = false)
        {
            try
            {
                var dao = new EstadoDAO(GetSqlCommand());

                return dao.Listar(objVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EstadoVO> ListarComPais(EstadoVO objVO = null)
        {
            try
            {
                var dao = new EstadoDAO(GetSqlCommand());

                return dao.ListarComPais(objVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EstadoVO ConsultarComPais(EstadoVO objVO)
        {
            try
            {
                var dao = new EstadoDAO(GetSqlCommand());

                return dao.ConsultarComPais(objVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}