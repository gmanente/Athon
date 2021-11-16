using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.DAO;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Comum.BE
{
    public class NacionalidadeBE : AbstractBE, IBE<NacionalidadeVO>
    {
        public NacionalidadeBE()
            : base()
        {
        }

        public NacionalidadeBE(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public long Inserir(NacionalidadeVO objVO)
        {
            NacionalidadeDAO nacionalidadeDao = null;
            try
            {
                long id;
                BeginTransaction();
                nacionalidadeDao = new NacionalidadeDAO(GetSqlCommand());
                id = nacionalidadeDao.Inserir(objVO);
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

        public long Alterar(NacionalidadeVO objVO, string where = null)
        {
            NacionalidadeDAO nacionalidadeDao = null;
            try
            {
                long id;
                BeginTransaction();
                nacionalidadeDao = new NacionalidadeDAO(GetSqlCommand());
                id = nacionalidadeDao.Alterar(objVO, where);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public void Deletar(NacionalidadeVO objVO)
        {
            NacionalidadeDAO nacionalidadeDao = null;
            try
            {
                BeginTransaction();
                nacionalidadeDao = new NacionalidadeDAO(GetSqlCommand());
                nacionalidadeDao.Deletar(objVO);
                Commit();
            }
            catch (Exception exception)
            {
                Rollback();
                throw exception;
            }
        }


        public List<NacionalidadeVO> Selecionar(NacionalidadeVO objVO, int top = 0, bool detalhar = false)
        {
            NacionalidadeDAO nacionalidadeDao = null;
            try
            {
                nacionalidadeDao = new NacionalidadeDAO(GetSqlCommand());
                return nacionalidadeDao.Selecionar(objVO, top);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public NacionalidadeVO Consultar(NacionalidadeVO objVO)
        {
            NacionalidadeDAO nacionalidadeDao = null;
            try
            {
                nacionalidadeDao = new NacionalidadeDAO(GetSqlCommand());
                return nacionalidadeDao.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<NacionalidadeVO> Listar(NacionalidadeVO objVO = null, bool detalhar = false)
        {
            NacionalidadeDAO nacionalidadeDao = null;
            try
            {
                nacionalidadeDao = new NacionalidadeDAO(GetSqlCommand());
                return nacionalidadeDao.Listar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}