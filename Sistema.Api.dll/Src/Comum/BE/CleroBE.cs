using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.DAO;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Comum.BE
{
    public class CleroBE : AbstractBE, IBE<CleroVO>
    {
        public CleroBE(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public CleroBE()
            : base()
        {
        }

        //Alterar                                                                                                                    
        public long Alterar(CleroVO objVO, string where = null)
        {
            CleroDAO cleroDao = null;
            try
            {
                long id;
                BeginTransaction();
                cleroDao = new CleroDAO(GetSqlCommand());
                id = cleroDao.Alterar(objVO, where);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        //Deletar                                                                                                                        
        public void Deletar(CleroVO objVO)
        {
            CleroDAO cleroDao = null;
            try
            {
                BeginTransaction();
                cleroDao = new CleroDAO(GetSqlCommand());
                cleroDao.Deletar(objVO);
                Commit();
            }
            catch (Exception exception)
            {
                Rollback();
                throw exception;
            }
        }

        //Inserir                                                                                                                        
        public long Inserir(CleroVO objVO)
        {
            CleroDAO cleroDao = null;
            try
            {
                long id;
                BeginTransaction();
                cleroDao = new CleroDAO(GetSqlCommand());
                id = cleroDao.Inserir(objVO);
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

        //Selecionar                                                                                                                     
        public List<CleroVO> Selecionar(CleroVO objVO, int top = 0, bool detalhar = false)
        {
            CleroDAO cleroDao = null;
            try
            {
                cleroDao = new CleroDAO(GetSqlCommand());
                return cleroDao.Selecionar(objVO, top);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Consultar Clero                                                                                          
        public CleroVO Consultar(CleroVO objVO)
        {
            CleroDAO cleroDao = null;
            try
            {
                cleroDao = new CleroDAO(GetSqlCommand());
                return cleroDao.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Listar                                                                                                                         
        public List<CleroVO> Listar(CleroVO objVO = null, bool detalhar = false)
        {
            CleroDAO cleroDao = null;
            try
            {
                cleroDao = new CleroDAO(GetSqlCommand());
                return cleroDao.Listar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
