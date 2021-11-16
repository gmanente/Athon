using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.VO;
using Sistema.Api.dll.Src.Comum.DAO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Comum.BE
{
    public class DepartamentoBE : AbstractBE , IBE<DepartamentoVO>
    {
        public DepartamentoBE() : base() {}

        public DepartamentoBE(SqlCommand sqlConn) : base(sqlConn) {}


        // Alterar
        public long Alterar(DepartamentoVO objVO, string where = null)
        {
            try
            {
                var dao = new DepartamentoDAO(GetSqlCommand());

                long id = dao.Alterar(objVO, where);

                return id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        // Delerar
        public void Deletar(DepartamentoVO objVO)
        {
            try
            {
                var dao = new DepartamentoDAO(GetSqlCommand());

                dao.Deletar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        // Inserir
        public long Inserir(DepartamentoVO objVO)
        {
            try
            {
                var dao = new DepartamentoDAO(GetSqlCommand());

                long id = dao.Inserir(objVO);

                objVO.Id = id;

                return id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Consultar
        /// </summary>
        /// <param name="objVO"></param>
        /// <returns></returns>
        public DepartamentoVO Consultar(DepartamentoVO objVO)
        {
            try
            {
                var dao = new DepartamentoDAO(GetSqlCommand());

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
        /// <param name="departamentoVo"></param>
        /// <param name="detalhar"></param>
        /// <returns></returns>
        public List<DepartamentoVO> Listar(DepartamentoVO departamentoVo = null, bool detalhar = false)
        {
             try
            {
                var dao = new DepartamentoDAO(GetSqlCommand());

                return dao.Listar(departamentoVo);
            }
            catch (Exception ex)
            {
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
        public List<DepartamentoVO> Selecionar(DepartamentoVO objVO, int top = 0, bool detalhar = false)
        {
            try
            {
                var dao = new DepartamentoDAO(GetSqlCommand());

                return dao.Selecionar(objVO, top);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DepartamentoVO> SelecionarDepartamentoDisponivel(DepartamentoVO objVO, int top = 0, bool detalhar = false)
        {
            try
            {
                var dao = new DepartamentoDAO(GetSqlCommand());

                return dao.SelecionarDepartamentoDisponivel(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// SelecionarSimples
        /// </summary>
        /// <param name="objVO"></param>
        /// <returns></returns>
        public List<DepartamentoVO> SelecionarSimples(DepartamentoVO objVO)
        {
            try
            {
                var dao = new DepartamentoDAO(GetSqlCommand());

                return dao.SelecionarSimples(objVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
