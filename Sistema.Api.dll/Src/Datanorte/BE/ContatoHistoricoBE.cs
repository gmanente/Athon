using Sistema.Api.dll.Src.Datanorte.DAO;
using Sistema.Api.dll.Src.Datanorte.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Datanorte.BE
{
    public class ContatoHistoricoBE : AbstractBE
    {
        public ContatoHistoricoBE()
           : base()
        {
        }

        public ContatoHistoricoBE(SqlCommand sqlConn)
            : base(sqlConn)
        {
        }


        public long Inserir(ContatoHistoricoVO objVO)
        {
            try
            {
                BeginTransaction();

                ContatoHistoricoDAO dao = new ContatoHistoricoDAO(GetSqlCommand());

                long id = dao.Inserir(objVO);

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

        public ContatoHistoricoVO Consultar(ContatoHistoricoVO objVO)
        {
            try
            {
                ContatoHistoricoDAO dao = new ContatoHistoricoDAO(GetSqlCommand());

                return dao.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ContatoHistoricoVO> Listar(ContatoHistoricoVO objVO = null, bool detalhar = false)
        {
            try
            {
                ContatoHistoricoDAO dao = new ContatoHistoricoDAO(GetSqlCommand());

                return dao.Listar(objVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
