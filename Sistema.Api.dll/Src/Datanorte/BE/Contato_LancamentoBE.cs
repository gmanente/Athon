using Sistema.Api.dll.Src.Datanorte.DAO;
using Sistema.Api.dll.Src.Datanorte.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Api.dll.Src.Datanorte.BE
{
    public class Contato_LancamentoBE : AbstractBE
    {
        public Contato_LancamentoBE()
            : base()
        {
        }

        public Contato_LancamentoBE(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public long Inserir(Contato_LancamentoVO objVO)
        {
            Contato_LancamentoDAO Contato_LancamentoDao = null;
            try
            {
                long id;
                BeginTransaction();
                Contato_LancamentoDao = new Contato_LancamentoDAO(GetSqlCommand());
                id = Contato_LancamentoDao.Inserir(objVO);
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

        public long Alterar(Contato_LancamentoVO objVO, string where = null)
        {
            Contato_LancamentoDAO Contato_LancamentoDao = null;
            try
            {
                long id;
                BeginTransaction();
                Contato_LancamentoDao = new Contato_LancamentoDAO(GetSqlCommand());
                id = Contato_LancamentoDao.Alterar(objVO, where);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public void Deletar(Contato_LancamentoVO objVO)
        {
            Contato_LancamentoDAO Contato_LancamentoDao = null;
            try
            {
                BeginTransaction();
                Contato_LancamentoDao = new Contato_LancamentoDAO(GetSqlCommand());
                Contato_LancamentoDao.Deletar(objVO);
                Commit();
            }
            catch (Exception exception)
            {
                Rollback();
                throw exception;
            }
        }

        public Contato_LancamentoVO Consultar(Contato_LancamentoVO objVO)
        {
            Contato_LancamentoDAO Contato_LancamentoDao = null;
            try
            {
                Contato_LancamentoDao = new Contato_LancamentoDAO(GetSqlCommand());
                return Contato_LancamentoDao.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Contato_LancamentoVO> Listar(Contato_LancamentoVO objVO = null, bool detalhar = false)
        {
            Contato_LancamentoDAO Contato_LancamentoDao = null;
            try
            {
                Contato_LancamentoDao = new Contato_LancamentoDAO(GetSqlCommand());
                return Contato_LancamentoDao.Listar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}