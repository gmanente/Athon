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
    public class MetodologiaBE : AbstractBE, IBE<MetodologiaVO>
    {
        public MetodologiaBE(SqlCommand sqlComm)
                    : base(sqlComm)
        {
        }

        public MetodologiaBE()
            : base()
        {
        }

        //Alterar                                                                                                                    
        public long Alterar(MetodologiaVO objVO, string where = null)
        {
            MetodologiaDAO metodologiaDao = null;
            try
            {
                long id;
                BeginTransaction();
                metodologiaDao = new MetodologiaDAO(GetSqlCommand());
                id = metodologiaDao.Alterar(objVO, where);
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
        public void Deletar(MetodologiaVO objVO)
        {
            MetodologiaDAO metodologiaDao = null;
            try
            {
                BeginTransaction();
                metodologiaDao = new MetodologiaDAO(GetSqlCommand());
                metodologiaDao.Deletar(objVO);
                Commit();
            }
            catch (Exception exception)
            {
                Rollback();
                throw exception;
            }
        }

        //Inserir                                                                                                                        
        public long Inserir(MetodologiaVO objVO)
        {
            MetodologiaDAO metodologiaDao = null;
            try
            {
                long id;
                BeginTransaction();
                metodologiaDao = new MetodologiaDAO(GetSqlCommand());
                id = metodologiaDao.Inserir(objVO);
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
        public List<MetodologiaVO> Selecionar(MetodologiaVO objVO, int top = 0, bool detalhar = false)
        {
            try
            {
                var dao = new MetodologiaDAO(GetSqlCommand());

                return dao.Selecionar(objVO, top);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Consultar
        /// </summary>
        /// <param name="objVO"></param>
        /// <returns></returns>        
        public MetodologiaVO Consultar(MetodologiaVO objVO)
        {
            try
            {
                var dao = new MetodologiaDAO(GetSqlCommand());

                return dao.Consultar(objVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        /// <summary>
        /// Listar
        /// </summary>
        /// <param name="objVO"></param>
        /// <param name="detalhar"></param>
        /// <returns></returns>        
        public List<MetodologiaVO> Listar(MetodologiaVO objVO = null, bool detalhar = false)
        {
            try
            {
                var dao = new MetodologiaDAO(GetSqlCommand());

                return dao.Listar(objVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// ConsultarWeb
        /// </summary>
        /// <param name="idMetodologia"></param>
        /// <returns></returns>
        public MetodologiaVO ConsultarWeb(long idMetodologia)
        {
            try
            {
                var dao = new MetodologiaDAO(GetSqlCommand());

                return dao.ConsultarWeb(idMetodologia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // Fim dos métodos
    }
}
