using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Seguranca.DAO;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Seguranca.BE
{
    public class ModuloBE : AbstractBE, IBE<ModuloVO>
    {
        public ModuloBE(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public ModuloBE()
            : base()
        {
        }


        /// <summary>
        /// Inserir
        /// </summary>
        /// <param name="objVO"></param>
        /// <returns></returns>
        public long Inserir(ModuloVO objVO)
        {
            try
            {
                BeginTransaction();

                ModuloDAO moduloDAO = new ModuloDAO(GetSqlCommand());

                long id = moduloDAO.Inserir(objVO);

                Commit();

                return id;
            }
            catch (Exception ex)
            {
                Rollback();

                throw ex;
            }
        }


        /// <summary>
        /// Alterar
        /// </summary>
        /// <param name="objVO"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public long Alterar(ModuloVO objVO, string where = null)
        {
            try
            {
                BeginTransaction();

                ModuloDAO moduloDAO = new ModuloDAO(GetSqlCommand());

                long id = moduloDAO.Alterar(objVO, where);

                Commit();

                return id;
            }
            catch (Exception e)
            {
                Rollback();

                throw e;
            }
        }


        /// <summary>
        /// Deletar
        /// </summary>
        /// <param name="objVO"></param>
        public void Deletar(ModuloVO objVO)
        {
            try
            {
                BeginTransaction();

                ModuloDAO moduloDAO = new ModuloDAO(GetSqlCommand());

                moduloDAO.Deletar(objVO);

                Commit();
            }
            catch (Exception e)
            {
                Rollback();

                throw e;
            }
        }


        /// <summary>
        /// Selecionar
        /// </summary>
        /// <param name="moduloVo"></param>
        /// <param name="top"></param>
        /// <param name="detalhar"></param>
        /// <returns></returns>
        public List<ModuloVO> Selecionar(ModuloVO moduloVo = null, int top = 0, bool detalhar = false)
        {
            try
            {
                ModuloDAO moduloDao = new ModuloDAO(GetSqlCommand());
                SistemaBE SistemaBE = new SistemaBE(GetSqlCommand());

                List<ModuloVO>lstModulo = moduloDao.Selecionar(moduloVo, top);

                if (detalhar)
                {
                    foreach (var modulo in lstModulo)
                    {
                        modulo.Sistema = SistemaBE.Consultar(new SistemaVO() { Id = modulo.Sistema.Id });
                    }
                }

                return lstModulo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Consultar
        /// </summary>
        /// <param name="moduloVo"></param>
        /// <returns></returns>
        public ModuloVO Consultar(ModuloVO moduloVo)
        {
            try
            {
                ModuloDAO moduloDao = new ModuloDAO(GetSqlCommand());
                SistemaBE sistemaBE = new SistemaBE(GetSqlCommand());

                ModuloVO modulo = moduloDao.Consultar(moduloVo);

                if (modulo != null)
                {
                    modulo.Sistema = sistemaBE.Consultar(new SistemaVO() { Id = modulo.Sistema.Id });
                }

                return modulo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Listar
        /// </summary>
        /// <param name="moduloVo"></param>
        /// <param name="detalhar"></param>
        /// <returns></returns>
        public List<ModuloVO> Listar(ModuloVO moduloVo = null, bool detalhar = false)
        {
            try
            {
                ModuloDAO moduloDao = new ModuloDAO(GetSqlCommand());
                SistemaBE sistemaBE = new SistemaBE(GetSqlCommand());

                List<ModuloVO>lstModulo = moduloDao.Listar(moduloVo);

                foreach (var modulo in lstModulo)
                {
                    modulo.Sistema = sistemaBE.Consultar(new SistemaVO() { Id = modulo.Sistema.Id });
                }

                return lstModulo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}