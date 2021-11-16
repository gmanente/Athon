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
    public class CidadeBE : AbstractBE
    {
        public CidadeBE()
           : base()
        {
        }

        public CidadeBE(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }
        public long Inserir(CidadeVO objVO)
        {
            CidadeDAO CidadeDao = null;
            try
            {
                long id;
                BeginTransaction();
                CidadeDao = new CidadeDAO(GetSqlCommand());
                id = CidadeDao.Inserir(objVO);
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

        public long Alterar(CidadeVO objVO, string where = null)
        {
            CidadeDAO CidadeDao = null;
            try
            {
                long id;
                BeginTransaction();
                CidadeDao = new CidadeDAO(GetSqlCommand());
                id = CidadeDao.Alterar(objVO, where);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public void Deletar(CidadeVO objVO)
        {
            CidadeDAO CidadeDao = null;
            try
            {
                BeginTransaction();
                CidadeDao = new CidadeDAO(GetSqlCommand());
                CidadeDao.Deletar(objVO);
                Commit();
            }
            catch (Exception exception)
            {
                Rollback();
                throw exception;
            }
        }

        public List<CidadeVO> Selecionar(CidadeVO objVO, int top = 0, bool detalhar = false)
        {
            try
            {
                var dao = new CidadeDAO(GetSqlCommand());

                return dao.Selecionar(objVO, top);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CidadeVO Consultar(CidadeVO objVO)
        {
            CidadeDAO CidadeDao = null;
            try
            {
                CidadeDao = new CidadeDAO(GetSqlCommand());
                return CidadeDao.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CidadeVO> Listar(CidadeVO objVO = null, bool detalhar = false)
        {
            CidadeDAO CidadeDao = null;
            try
            {
                CidadeDao = new CidadeDAO(GetSqlCommand());
                return CidadeDao.Listar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}