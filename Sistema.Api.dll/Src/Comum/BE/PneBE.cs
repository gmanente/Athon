using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.DAO;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Comum.BE
{
    public class PneBE : AbstractBE, IBE<PneVO>
    {
        public PneBE()
            : base()
        {
        }

        public PneBE(SqlCommand sqlConn)
            : base(sqlConn)
        {
        }

        public long Inserir(PneVO objVO)
        {
            throw new NotImplementedException();
        }

        public long Alterar(PneVO objVO, string where = null)
        {
            throw new NotImplementedException();
        }

        public void Deletar(PneVO objVO)
        {
            throw new NotImplementedException();
        }


        public List<PneVO> Selecionar(PneVO objVO, int top = 0, bool detalhar = false)
        {
            PneDAO pneDao = null;
            try
            {
                pneDao = new PneDAO(GetSqlCommand());
                return pneDao.Selecionar(objVO, top);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public PneVO Consultar(PneVO objVO)
        {
            PneDAO pneDao = null;
            try
            {
                pneDao = new PneDAO(GetSqlCommand());
                return pneDao.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Listar
        /// </summary>
        /// <param name="objVO"></param>
        /// <param name="detalhar"></param>
        /// <returns></returns>
        public List<PneVO> Listar(PneVO objVO = null, bool detalhar = false)
        {
            try
            {
                var dao = new PneDAO(GetSqlCommand());

                return dao.Listar(objVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Listar somente PNE
        /// </summary>
        /// <param name="objVO"></param>
        /// <param name="detalhar"></param>
        /// <returns></returns>
        public List<PneVO> ListarPne(PneVO objVO = null, bool detalhar = false)
        {
            try
            {
                var dao = new PneDAO(GetSqlCommand());

                return dao.ListarPne(objVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}