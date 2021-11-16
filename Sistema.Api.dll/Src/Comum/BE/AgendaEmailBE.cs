using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.DAO;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Comum.BE
{
    public class AgendaEmailBE : AbstractBE, IBE<AgendaEmailVO>
    {
        public AgendaEmailBE()
            : base()
        {
        }

        public AgendaEmailBE(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public long Alterar(AgendaEmailVO objVO, string where = null)
        {
            throw new NotImplementedException();
        }

        public void Deletar(AgendaEmailVO objVO)
        {
            throw new NotImplementedException();
        }

        public long Inserir(AgendaEmailVO objVO)
        {
            AgendaEmailDAO agendaEmailDao = null;
            try
            {
                long id;
                BeginTransaction();
                agendaEmailDao = new AgendaEmailDAO(GetSqlCommand());
                id = agendaEmailDao.Inserir(objVO);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public List<AgendaEmailVO> Selecionar(AgendaEmailVO objVO, int top = 0, bool detalhar = false)
        {
            throw new NotImplementedException();
        }

        public AgendaEmailVO Consultar(AgendaEmailVO objVO)
        {
            throw new NotImplementedException();
        }

        public List<AgendaEmailVO> Listar(AgendaEmailVO objVO, bool detalhar = false)
        {
            throw new NotImplementedException();
        }
    }
}
