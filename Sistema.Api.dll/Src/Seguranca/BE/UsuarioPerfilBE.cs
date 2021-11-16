using Sistema.Api.dll.Src.Seguranca.DAO;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Seguranca.BE
{
    public class UsuarioPerfilBE : AbstractBE
    {
        public UsuarioPerfilBE(SqlCommand sqlComm)
            : base(sqlComm)
        { }

        public UsuarioPerfilBE()
            : base()
        { }

        public long Inserir(UsuarioPerfilVO objVO)
        {
            try
            {
                long id;

                BeginTransaction();

                var usuarioPerfilDAO = new UsuarioPerfilDAO(GetSqlCommand());
                id = usuarioPerfilDAO.Inserir(objVO);

                Commit();

                return id;
            }
            catch (Exception ex)
            {
                Rollback();
                throw ex;
            }
        }

        public long Alterar(UsuarioPerfilVO objVO, string where = null)
        {
            try
            {
                long id;

                BeginTransaction();

                var usuarioPerfilDAO = new UsuarioPerfilDAO(GetSqlCommand());
                id = usuarioPerfilDAO.Alterar(objVO, where);

                Commit();

                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public void Deletar(UsuarioPerfilVO objVO)
        {
            try
            {
                BeginTransaction();

                var usuarioPerfilDAO = new UsuarioPerfilDAO(GetSqlCommand());
                usuarioPerfilDAO.Deletar(objVO);

                Commit();
            }
            catch (Exception exception)
            {
                Rollback();
                throw exception;
            }
        }


        public List<UsuarioPerfilVO> Selecionar(UsuarioPerfilVO objVO, int top = 0)
        {
            try
            {
                var usuarioPerfilDAO = new UsuarioPerfilDAO(GetSqlCommand());
                return usuarioPerfilDAO.Selecionar(objVO, top);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<UsuarioPerfilVO> Listar(UsuarioPerfilVO objVO = null)
        {
            try
            {
                var usuarioPerfilDAO = new UsuarioPerfilDAO(GetSqlCommand());
                return usuarioPerfilDAO.Listar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public UsuarioPerfilVO Consultar(UsuarioPerfilVO objVO)
        {
            try
            {
                var usuarioPerfilDAO = new UsuarioPerfilDAO(GetSqlCommand());
                return usuarioPerfilDAO.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        // TrazerPerfisNaoVinculados
        public List<PerfilVO> TrazerPerfisNaoVinculados(long IdUsuarioCampus, string idsDepartamento, long idPerfil = 0)
        {
            try
            {
                var usuarioPerfilDAO = new UsuarioPerfilDAO(GetSqlCommand());
                var perfilDAO = new PerfilDAO(GetSqlCommand());

                var lstUsuarioPerfil = usuarioPerfilDAO.SelecionarPerfilPorDepartamento(new UsuarioPerfilVO() { UsuarioCampus = { Id = IdUsuarioCampus, Usuario = { ListaDepartamentoOperar = idsDepartamento } }, Perfil = { Id = idPerfil } });

                return lstUsuarioPerfil;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PerfilPertenceAoDepartamento
        public bool PerfilPertenceAoDepartamento(long IdUsuarioPerfil, string idsDepartamento)
        {
            try
            {
                var usuarioPerfilDAO = new UsuarioPerfilDAO(GetSqlCommand());
                var perfilDAO = new PerfilDAO(GetSqlCommand());

                long idPerfil = usuarioPerfilDAO.Consultar(new UsuarioPerfilVO() { Id = IdUsuarioPerfil }).Perfil.Id;

                var lstUsuarioPerfil = usuarioPerfilDAO.SelecionarPerfilPorDepartamento(new UsuarioPerfilVO() { Id = IdUsuarioPerfil, UsuarioCampus = { Usuario = { ListaDepartamentoOperar = idsDepartamento } }, Perfil = { Id = idPerfil } }, false);

                return (lstUsuarioPerfil.Count > 0) ? true : false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Paginar
        public Dictionary<int, List<UsuarioPerfilVO>> Paginar(UsuarioPerfilVO objVO)
        {
            try
            {
                var usuarioPerfilDAO = new UsuarioPerfilDAO(GetSqlCommand());
                return usuarioPerfilDAO.Paginar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
