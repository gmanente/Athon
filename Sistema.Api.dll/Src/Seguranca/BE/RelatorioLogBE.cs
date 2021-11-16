using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Seguranca.DAO;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Seguranca.BE
{
    public class RelatorioLogBE : AbstractBE, IBE<RelatorioLogVO>
    {
        public RelatorioLogBE()
            : base()
        {
        }

        public RelatorioLogBE(SqlCommand sqlConn)
            : base(sqlConn)
        {
        }

        public long Inserir(RelatorioLogVO objVO)
        {
            RelatorioLogDAO RelatorioLogDao = null;
            try
            {
                RelatorioLogDao = new RelatorioLogDAO(GetSqlCommand());
                //BeginTransaction();
                long id = RelatorioLogDao.Inserir(objVO);
                //Commit();
                return id;

            }
            catch (Exception)
            {
                //Rollback();
                throw;
            }

        }

        public long Alterar(RelatorioLogVO objVO, string where = null)
        {
            throw new NotImplementedException();
        }

        public void Deletar(RelatorioLogVO objVO)
        {
            throw new NotImplementedException();
        }

        public List<RelatorioLogVO> Selecionar(RelatorioLogVO objVO, int top = 0, bool detalhar = false)
        {
            throw new NotImplementedException();
        }

        public RelatorioLogVO Consultar(RelatorioLogVO objVO)
        {
            throw new NotImplementedException();
        }

        public List<RelatorioLogVO> Listar(RelatorioLogVO objVO, bool detalhar = false)
        {
            throw new NotImplementedException();
        }
    }
}