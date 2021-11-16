using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.DAO;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Api.dll.Src.Comum.BE
{
    public class InformacaoBE : AbstractBE, IBE<InformacaoVO>
    {
        public InformacaoBE(SqlCommand sqlComm)
                    : base(sqlComm)
        {
        }

        public InformacaoBE()
            : base()
        {
        }

        //Alterar                                                                                                                    
        public long Alterar(InformacaoVO objVO, string where = null)
        {
            InformacaoDAO informacaoDao = null;
            try
            {
                long id;
                BeginTransaction();
                informacaoDao = new InformacaoDAO(GetSqlCommand());
                id = informacaoDao.Alterar(objVO, where);
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
        public void Deletar(InformacaoVO objVO)
        {
            InformacaoDAO informacaoDao = null;
            try
            {
                BeginTransaction();
                informacaoDao = new InformacaoDAO(GetSqlCommand());
                informacaoDao.Deletar(objVO);
                Commit();
            }
            catch (Exception exception)
            {
                Rollback();
                throw exception;
            }
        }

        //Inserir                                                                                                                        
        public long Inserir(InformacaoVO objVO)
        {
            InformacaoDAO informacaoDao = null;
            try
            {
                long id;
                BeginTransaction();
                informacaoDao = new InformacaoDAO(GetSqlCommand());
                id = informacaoDao.Inserir(objVO);
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
        public List<InformacaoVO> Selecionar(InformacaoVO objVO, int top = 0, bool detalhar = false)
        {
            InformacaoDAO informacaoDao = null;
            try
            {
                informacaoDao = new InformacaoDAO(GetSqlCommand());
                return informacaoDao.Selecionar(objVO, top);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Consultar                                                                                                                      
        public InformacaoVO Consultar(InformacaoVO objVO)
        {
            InformacaoDAO informacaoDao = null;
            try
            {
                informacaoDao = new InformacaoDAO(GetSqlCommand());
                return informacaoDao.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Listar                                                                                                                         
        public List<InformacaoVO> Listar(InformacaoVO objVO = null, bool detalhar = false)
        {
            InformacaoDAO informacaoDao = null;
            try
            {
                informacaoDao = new InformacaoDAO(GetSqlCommand());
                return informacaoDao.Listar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}
