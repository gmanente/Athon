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
    public class BairroBE : AbstractBE
    {
        public BairroBE()
           : base()
        {
        }

        public BairroBE(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public long Inserir(BairroVO objVO)
        {
            BairroDAO BairroDao = null;
            try
            {
                long id;
                BeginTransaction();
                BairroDao = new BairroDAO(GetSqlCommand());
                id = BairroDao.Inserir(objVO);
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

        public long Alterar(BairroVO objVO, string where = null)
        {
            BairroDAO BairroDao = null;
            try
            {
                long id;
                BeginTransaction();
                BairroDao = new BairroDAO(GetSqlCommand());
                id = BairroDao.Alterar(objVO, where);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public void Deletar(BairroVO objVO)
        {
            BairroDAO BairroDao = null;
            try
            {
                BeginTransaction();
                BairroDao = new BairroDAO(GetSqlCommand());
                BairroDao.Deletar(objVO);
                Commit();
            }
            catch (Exception exception)
            {
                Rollback();
                throw exception;
            }
        }

        public List<BairroVO> Selecionar(BairroVO objVO, int top = 0, bool detalhar = false)
        {
            try
            {
                var dao = new BairroDAO(GetSqlCommand());

                return dao.Selecionar(objVO, top);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BairroVO Consultar(BairroVO objVO)
        {
            BairroDAO BairroDao = null;
            try
            {
                BairroDao = new BairroDAO(GetSqlCommand());
                return BairroDao.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<BairroVO> Listar(BairroVO objVO = null, bool detalhar = false)
        {
            BairroDAO BairroDao = null;
            try
            {
                BairroDao = new BairroDAO(GetSqlCommand());
                return BairroDao.Listar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}