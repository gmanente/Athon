using Sistema.Api.dll.Src.Datanorte.DAO;
using Sistema.Api.dll.Src.Datanorte.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Datanorte.BE
{
    public class PessoaCidadeBE : AbstractBE
    {
        public PessoaCidadeBE()
           : base()
        {
        }

        public PessoaCidadeBE(SqlCommand sqlConn)
            : base(sqlConn)
        {
        }
        public PessoaCidadeVO Consultar(PessoaCidadeVO objVO)
        {
            try
            {
                PessoaCidadeDAO dao = new PessoaCidadeDAO(GetSqlCommand());

                return dao.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<PessoaCidadeVO> Listar(PessoaCidadeVO objVO = null, bool detalhar = false)
        {
            try
            {
                PessoaCidadeDAO dao = new PessoaCidadeDAO(GetSqlCommand());

                return dao.Listar(objVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
