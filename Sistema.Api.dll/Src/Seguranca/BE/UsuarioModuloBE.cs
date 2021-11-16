using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Seguranca.DAO;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Sistema.Api.dll.Src.Seguranca.BE
{
    public class UsuarioModuloBE : AbstractBE, IBE<UsuarioModuloVO>
    {

        public UsuarioModuloBE(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public UsuarioModuloBE()
            : base()
        {

        }
        public long Alterar(UsuarioModuloVO usuarioModuloVo, string where = null)
        {
            throw new NotImplementedException();
        }

        public void Deletar(UsuarioModuloVO usuarioModuloVo)
        {
            throw new NotImplementedException();
        }

        //Inserir
        public long Inserir(UsuarioModuloVO usuarioModuloVo)
        {
            throw new NotImplementedException();
        }

        //Inserir
        public void Inserir(List<UsuarioModuloVO> lstUsuarioModuloVo, long idUsuarioCampus = 0)
        {
            UsuarioModuloDAO usuarioModuloDAO = null;
            try
            {
                BeginTransaction();
                usuarioModuloDAO = new UsuarioModuloDAO(GetSqlCommand());
                usuarioModuloDAO.Inserir(lstUsuarioModuloVo, idUsuarioCampus);
                Commit();
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public List<UsuarioModuloVO> Selecionar(UsuarioModuloVO usuarioModuloVo = null, int top = 0, bool detalhar = false)
        {
            UsuarioModuloDAO usuarioModuloDao = null;
            UsuarioCampusBE usuarioCampusBe = null;
            ModuloBE moduloBe = null;
            List<UsuarioModuloVO> lstUsuarioModulo = null;
            try
            {
                usuarioModuloDao = new UsuarioModuloDAO(GetSqlCommand());
                usuarioCampusBe = new UsuarioCampusBE(GetSqlCommand());
                moduloBe = new ModuloBE(GetSqlCommand());
                lstUsuarioModulo = usuarioModuloDao.Selecionar(usuarioModuloVo, top);

            }
            catch (Exception e)
            {
                throw e;
            }

            return lstUsuarioModulo;
        }

        public UsuarioModuloVO Consultar(UsuarioModuloVO usuarioModuloVo)
        {
            UsuarioModuloDAO usuarioModuloDao = null;
            UsuarioCampusBE usuarioCampusBe = null;
            ModuloBE moduloBe = null;
            UsuarioModuloVO usuarioModulo = null;
            try
            {
                usuarioModuloDao = new UsuarioModuloDAO(GetSqlCommand());
                usuarioCampusBe = new UsuarioCampusBE(GetSqlCommand());
                moduloBe = new ModuloBE(GetSqlCommand());
                usuarioModulo = usuarioModuloDao.Consultar(usuarioModuloVo);

            }
            catch (Exception e)
            {
                throw e;
            }

            return usuarioModulo;
        }

        public List<UsuarioModuloVO> Listar(UsuarioModuloVO usuarioModuloVo = null, bool detalhar = false)
        {
            UsuarioModuloDAO usuarioModuloDao = null;
            UsuarioSubmoduloDAO usuarioSubmoduloDAO = null;
            UsuarioCampusBE usuarioCampusBe = null;
            ModuloBE moduloBe = null;
            List<UsuarioModuloVO> lstUsuarioModulo = null;
            try
            {
                usuarioModuloDao = new UsuarioModuloDAO(GetSqlCommand());
                usuarioCampusBe = new UsuarioCampusBE(GetSqlCommand());
                moduloBe = new ModuloBE(GetSqlCommand());
                usuarioSubmoduloDAO = new UsuarioSubmoduloDAO(GetSqlCommand());
                lstUsuarioModulo = usuarioModuloDao.Listar(usuarioModuloVo);
                if (lstUsuarioModulo.Any())
                {
                    foreach (var lstSubModulo in lstUsuarioModulo)
                    {
                        lstSubModulo.ListUsuarioSubModuloVO = usuarioSubmoduloDAO.Listar(new UsuarioSubModuloVO()
                        {
                            UsuarioModulo =
                            {
                                Id = lstSubModulo.Id,
                                UsuarioCampus =
                                {
                                    Usuario =
                                    {
                                        Id = lstSubModulo.UsuarioCampus.Usuario.Id
                                    },
                                    Campus =
                                    {
                                        Id = lstSubModulo.UsuarioCampus.Campus.Id
                                    },
                                    Ativar = true
                                }
                            },
                            Ativar = true
                        });
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return lstUsuarioModulo;
        }


        /// <summary>
        /// AutenticarModulos
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="idCampus"></param>
        /// <param name="acessoExterno"></param>
        /// <param name="portal"></param>
        /// <param name="idModulo"></param>
        /// <returns></returns>
        public List<UsuarioModuloVO> AutenticarModulos(long idUsuario, long idCampus, bool acessoExterno, bool? portal = null, long idModulo = 0)
        {
            try
            {
                var usuarioModuloDAO = new UsuarioModuloDAO(GetSqlCommand());
                var usuarioSubmoduloDAO = new UsuarioSubmoduloDAO(GetSqlCommand());


                // --------------- Lista os Módulos do usuário
                var lstUsuarioModuloVO = usuarioModuloDAO.AutenticarModulos(idUsuario, idCampus, acessoExterno, portal, idModulo);

                // --------------- Loop em lstUsuarioModuloVO
                foreach (var item in lstUsuarioModuloVO)
                {

                    // ---------- Se o link do módulo Não for nulo
                    if (item.Modulo.Link != null)
                    {
                        // ---------- Lista os Submódulos do módulo com acesso ao usuário
                        var lstUsuarioSubModuloVO = usuarioSubmoduloDAO.AutenticarSubModulos(idCampus, idUsuario, item.Modulo.Link, acessoExterno);

                        item.ListUsuarioSubModuloVO = lstUsuarioSubModuloVO;
                        //string linkLocal = Dominio.GetLinkLocalModulo(item.Modulo.Id);
                        //if (!string.IsNullOrEmpty(linkLocal))
                        //{
                        //    item.Modulo.Link = linkLocal;
                        //    item.Modulo.LinkDebug = linkLocal;
                        //    item.Modulo.LinkHomologacao = linkLocal;
                        //    item.Modulo.LinkTeste = linkLocal;
                        //}
                    }
                }

                return lstUsuarioModuloVO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
