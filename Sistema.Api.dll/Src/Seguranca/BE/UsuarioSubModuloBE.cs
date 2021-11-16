using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Seguranca.DAO;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Seguranca.BE
{
    public class UsuarioSubModuloBE : AbstractBE, IBE<UsuarioSubModuloVO>
    {
        public UsuarioSubModuloBE(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public UsuarioSubModuloBE()
            : base()
        {

        }

        public long Alterar(UsuarioSubModuloVO usuarioSubModuloVo, string where = null)
        {
            throw new NotImplementedException();
        }

        public void Deletar(UsuarioSubModuloVO usuarioSubModuloVo)
        {
            throw new NotImplementedException();
        }

        public long Inserir(UsuarioSubModuloVO usuarioSubModuloVo)
        {
            throw new NotImplementedException();
        }

        public void Inserir(List<UsuarioSubModuloVO> lstUsuarioSubModuloVo, long idUsuarioCampus = 0, long idUsuarioModulo = 0)
        {
            UsuarioSubmoduloDAO usuarioSubmoduloDAO = null;
            try
            {
                BeginTransaction();
                usuarioSubmoduloDAO = new UsuarioSubmoduloDAO(GetSqlCommand());
                usuarioSubmoduloDAO.Inserir(lstUsuarioSubModuloVo, idUsuarioCampus, idUsuarioModulo);
                Commit();
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }


        public List<UsuarioSubModuloVO> Selecionar(UsuarioSubModuloVO usuarioSubModuloVo = null, int top = 0, bool detalhar = false)
        {
            UsuarioSubmoduloDAO usuarioSubModuloDao = null;
            UsuarioModuloBE usuarioModuloBe = null;
            SubmoduloBE subModuloBe = null;
            List<UsuarioSubModuloVO> lslUsuarioSubModulo = null;

            try
            {
                usuarioSubModuloDao = new UsuarioSubmoduloDAO(GetSqlCommand());
                usuarioModuloBe = new UsuarioModuloBE(GetSqlCommand());
                subModuloBe = new SubmoduloBE(GetSqlCommand());
                lslUsuarioSubModulo = usuarioSubModuloDao.Selecionar(usuarioSubModuloVo, top);

                //if (detalhar)
                //{
                //    foreach (var usuarioSubModulo in lslUsuarioSubModulo)
                //    {
                //        usuarioSubModulo.UsuarioModulo = usuarioModuloBe.Consultar(new UsuarioModuloVO() { Id = usuarioSubModulo.UsuarioModulo.Id });
                //        usuarioSubModulo.SubModulo = subModuloBe.Consultar(new SubModuloVO() { Id = usuarioSubModulo.SubModulo.Id });
                //    }
                //}

            }
            catch (Exception e)
            {
                throw e;
            }

            return lslUsuarioSubModulo;
        }

        public UsuarioSubModuloVO Consultar(UsuarioSubModuloVO usuarioSubModuloVo)
        {
            UsuarioSubmoduloDAO usuarioSubModuloDao = null;
            UsuarioModuloBE usuarioModuloBe = null;
            SubmoduloBE subModuloBe = null;
            UsuarioSubModuloVO usuarioSubModulo = null;

            try
            {
                usuarioSubModuloDao = new UsuarioSubmoduloDAO(GetSqlCommand());
                usuarioModuloBe = new UsuarioModuloBE(GetSqlCommand());
                subModuloBe = new SubmoduloBE(GetSqlCommand());
                usuarioSubModulo = usuarioSubModuloDao.Consultar(usuarioSubModuloVo);

                if (usuarioSubModulo != null)
                {
                    usuarioSubModulo.UsuarioModulo = usuarioModuloBe.Consultar(new UsuarioModuloVO() { Id = usuarioSubModulo.UsuarioModulo.Id });
                    usuarioSubModulo.SubModulo = subModuloBe.Consultar(new SubmoduloVO() { Id = usuarioSubModulo.SubModulo.Id });
                }

            }
            catch (Exception e)
            {
                throw e;
            }

            return usuarioSubModulo;
        }


        public List<UsuarioSubModuloVO> Autenticar(long idCampus, string UrlModulo, long idUsuario, bool acessoExteno)
        {
            UsuarioSubmoduloDAO usuarioSubModuloDao = null;
            List<UsuarioSubModuloVO> lslUsuarioSubModulo = null;

            try
            {
                usuarioSubModuloDao = new UsuarioSubmoduloDAO(GetSqlCommand());

                lslUsuarioSubModulo = usuarioSubModuloDao.AutenticarSubModulos(idCampus, idUsuario, UrlModulo, acessoExteno);
            }
            catch (Exception e)
            {
                throw e;
            }

            return lslUsuarioSubModulo;
        }


        public List<UsuarioSubModuloVO> Listar(UsuarioSubModuloVO usuarioSubModuloVo = null, bool detalhar = false)
        {
            UsuarioSubmoduloDAO usuarioSubModuloDao = null;
            UsuarioModuloBE usuarioModuloBe = null;
            SubmoduloBE subModuloBe = null;
            List<UsuarioSubModuloVO> lslUsuarioSubModulo = null;

            try
            {
                usuarioSubModuloDao = new UsuarioSubmoduloDAO(GetSqlCommand());
                usuarioModuloBe = new UsuarioModuloBE(GetSqlCommand());
                subModuloBe = new SubmoduloBE(GetSqlCommand());
                lslUsuarioSubModulo = usuarioSubModuloDao.Listar(usuarioSubModuloVo);
                //if (detalhar)
                //{
                //    foreach (var usuarioSubModulo in lslUsuarioSubModulo)
                //    {
                //        usuarioSubModulo.UsuarioModulo = usuarioModuloBe.Consultar(new UsuarioModuloVO() { Id = usuarioSubModulo.UsuarioModulo.Id });
                //        usuarioSubModulo.SubModulo = subModuloBe.Consultar(new SubModuloVO() { Id = usuarioSubModulo.SubModulo.Id });
                //    }
                //}


            }
            catch (Exception e)
            {
                throw e;
            }

            return lslUsuarioSubModulo;
        }
    }
}
