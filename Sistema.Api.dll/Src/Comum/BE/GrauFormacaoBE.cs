using Sistema.Api.dll.Src.Comum.DAO;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Comum.BE
{
    public class GrauFormacaoBE : AbstractBE
    {
        public GrauFormacaoBE()
            : base()
        {
        }

        public GrauFormacaoBE(SqlCommand sqlComm)
               : base(sqlComm)
        {
        }


        public List<GrauFormacaoVO> Selecionar(GrauFormacaoVO objVO, int top = 0, bool detalhar = false)
        {
            try
            {
                GrauFormacaoDAO dao = new GrauFormacaoDAO(GetSqlCommand());

                return dao.Selecionar(objVO, top);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public GrauFormacaoVO Consultar(GrauFormacaoVO objVO)
        {
            try
            {
                GrauFormacaoDAO dao = new GrauFormacaoDAO(GetSqlCommand());

                return dao.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<GrauFormacaoVO> Listar(GrauFormacaoVO objVO = null, bool detalhar = false)
        {
            try
            {
                GrauFormacaoDAO dao = new GrauFormacaoDAO(GetSqlCommand());

                return dao.Listar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}