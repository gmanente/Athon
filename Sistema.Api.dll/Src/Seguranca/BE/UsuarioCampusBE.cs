using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Sistema.Api.dll.Src.Comum.DAO;
using Sistema.Api.dll.Src.Comum.VO;
using Sistema.Api.dll.Src.Seguranca.DAO;
using Sistema.Api.dll.Src.Seguranca.VO;

namespace Sistema.Api.dll.Src.Seguranca.BE
{
    public class UsuarioCampusBE : AbstractBE
    {
        public UsuarioCampusBE(SqlCommand sqlComm) : base(sqlComm)
        { }

        public UsuarioCampusBE() : base()
        { }


        /// <summary>
        /// Inserir
        /// </summary>
        /// <param name="objVO"></param>
        /// <returns></returns>
        public long Inserir(UsuarioCampusVO objVO)
        {
            try
            {
                BeginTransaction();

                var dao = new UsuarioCampusDAO(GetSqlCommand());

                long id = dao.Inserir(objVO);

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
        /// Inserir
        /// </summary>
        /// <param name="lstUsuarioCampusVo"></param>
        /// <param name="idUsuario"></param>
        public void Inserir(List<UsuarioCampusVO> lstUsuarioCampusVo, long idUsuario)
        {
            try
            {
                BeginTransaction();

                var dao = new UsuarioCampusDAO(GetSqlCommand());


                var lstUsuarioCampusVODB = dao.Listar(new UsuarioCampusVO() { Usuario = { Id = idUsuario } });


                foreach (var item in lstUsuarioCampusVODB)
                {
                    item.Ativar = false;

                    dao.Alterar(item);
                }


                foreach (var diff in lstUsuarioCampusVo)
                {
                    var lst = from p in lstUsuarioCampusVODB
                               where p.Usuario.Id == diff.Usuario.Id && p.Campus.Id == diff.Campus.Id
                              select p;

                    if (lst.Count() > 0)
                        dao.Alterar(diff);

                    else
                        dao.Inserir(diff);
                }


                Commit();
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
        /// <param name="usuarioCampusVo"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public long Alterar(UsuarioCampusVO usuarioCampusVo, string where = null)
        {
            try
            {
                BeginTransaction();

                var dao = new UsuarioCampusDAO(GetSqlCommand());

                long id = dao.Alterar(usuarioCampusVo);

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
        /// Deletar
        /// </summary>
        /// <param name="usuarioCampusVo"></param>
        public void Deletar(UsuarioCampusVO usuarioCampusVo)
        {
            try
            {
                BeginTransaction();

                var dao = new UsuarioCampusDAO(GetSqlCommand());

                dao.Deletar(usuarioCampusVo);

                Commit();
            }
            catch (Exception ex)
            {
                Rollback();

                throw ex;
            }
        }


        /// <summary>
        /// TrazerCampusNaoVinculados
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="idUsuarioCampus"></param>
        /// <returns></returns>
        public List<CampusVO> TrazerCampusNaoVinculados(long idUsuario, long idUsuarioCampus = 0)
        {
            try
            {
                var usuarioCampusDao = new UsuarioCampusDAO(GetSqlCommand());
                var campusDAO = new CampusDAO(GetSqlCommand());

                var lstUsuarioCampus = usuarioCampusDao.Listar(new UsuarioCampusVO() { Usuario = { Id = idUsuario } });

                var lstCampus = campusDAO.Listar();


                if (idUsuarioCampus == 0)
                    lstCampus = lstCampus.Where(m => !lstUsuarioCampus.Any(pm => pm.Campus.Id == m.Id)).ToList();
                else
                    lstCampus = lstCampus.Where(m => !lstUsuarioCampus.Any(pm => pm.Campus.Id == m.Id && pm.Id != idUsuarioCampus)).ToList();

                return lstCampus;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Selecionar
        /// </summary>
        /// <param name="usuarioCampusVo"></param>
        /// <param name="top"></param>
        /// <param name="detalhar"></param>
        /// <returns></returns>
        public List<UsuarioCampusVO> Selecionar(UsuarioCampusVO usuarioCampusVo = null, int top = 0, bool detalhar = false)
        {
            try
            {
                var dao = new UsuarioCampusDAO(GetSqlCommand());

                return dao.Selecionar(usuarioCampusVo, top);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Consultar
        /// </summary>
        /// <param name="usuarioCampusVo"></param>
        /// <returns></returns>
        public UsuarioCampusVO Consultar(UsuarioCampusVO usuarioCampusVo)
        {
            try
            {
                var dao = new UsuarioCampusDAO(GetSqlCommand());

                return dao.Consultar(usuarioCampusVo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Listar
        /// </summary>
        /// <param name="usuarioCampusVo"></param>
        /// <param name="detalhar"></param>
        /// <returns></returns>
        public List<UsuarioCampusVO> Listar(UsuarioCampusVO usuarioCampusVo = null, bool detalhar = false)
        {
            try
            {
                var dao = new UsuarioCampusDAO(GetSqlCommand());

                return dao.Listar(usuarioCampusVo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // ListarCampusModalidade
        public List<CampusVO> ListarCampusModalidade(long idUsuario, string idsModalidades)
        {
            try
            {
                var dao = new UsuarioCampusDAO(GetSqlCommand());

                return dao.ListarCampusModalidade(idUsuario, idsModalidades);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// ListarCampusSubModuloModalidade
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="idModulo"></param>
        /// <param name="idSubModulo"></param>
        /// <param name="acessoExterno"></param>
        /// <param name="idsModalidades"></param>
        /// <returns></returns>
        public List<CampusVO> ListarCampusSubModuloModalidade(long idUsuario, long idModulo, long idSubModulo, bool acessoExterno, string idsModalidades)
        {
            try
            {
                var dao = new UsuarioCampusDAO(GetSqlCommand());

                return dao.ListarCampusSubModuloModalidade(idUsuario, idModulo, idSubModulo, acessoExterno, idsModalidades);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Paginar
        /// </summary>
        /// <param name="objVO"></param>
        /// <returns></returns>
        public Dictionary<int, List<UsuarioCampusVO>> Paginar(UsuarioCampusVO objVO)
        {
            try
            {
                var  dao = new UsuarioCampusDAO(GetSqlCommand());

                return dao.Paginar(objVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}