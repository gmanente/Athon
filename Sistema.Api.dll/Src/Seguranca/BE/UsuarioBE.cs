using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Seguranca.DAO;
using Sistema.Api.dll.Src.Seguranca.VO;
using Sistema.Api.dll.Template.Repositorio.Mensageria;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Sistema.Api.dll.Src.Seguranca.BE
{
    public class UsuarioBE : AbstractBE, IBE<UsuarioVO>
    {

        public UsuarioBE(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public UsuarioBE()
            : base()
        {
            this.GetSqlCommand();
        }


        // Método AlterarCompleto
        public void AlterarCompleto(UsuarioVO objVO)
        {
            UsuarioDAO usuarioDao = null;

            try
            {
                BeginTransaction();

                usuarioDao = new UsuarioDAO(GetSqlCommand());
                usuarioDao.AlterarEmail(objVO);
                usuarioDao.AlterarDadosBasicos(objVO);

                Commit();


            }
            catch (Exception e)
            {
                Rollback();

                throw e;
            }
        }


        public long Alterar(UsuarioVO objVO, string where = null)
        {
            UsuarioDAO usuarioDao = null;
            try
            {
                long id;
                BeginTransaction();
                usuarioDao = new UsuarioDAO(GetSqlCommand());
                id = usuarioDao.Alterar(objVO, where);
                Commit();

                UsuarioDepartamentoDAO usuarioDepartamentoDAO = null;

                usuarioDepartamentoDAO = new UsuarioDepartamentoDAO(GetSqlCommand());
                var idUltimoUsuarioDepartamentoInserido = usuarioDepartamentoDAO.Alterar(new UsuarioDepartamentoVO()
                {
                    Usuario = { Id = Convert.ToInt64(id) },
                    Id = objVO.UsuarioDepartamento.Id,
                    Departamento = { Id = objVO.UsuarioDepartamento.Departamento.Id },
                    Ativar = objVO.Ativo,
                });

                return id;
            }
            catch (Exception e)
            {
                Rollback();

                throw e;
            }
        }


        public long AlterarUsuarioProfessor(UsuarioVO objVO, string where = null)
        {
            UsuarioDAO UsuarioDAO = null;
            UsuarioDepartamentoDAO UsuarioDepartamentoDAO = null;

            try
            {
                long IdUsuario;

                BeginTransaction();

                UsuarioDAO = new UsuarioDAO(GetSqlCommand());
                UsuarioDepartamentoDAO = new UsuarioDepartamentoDAO(GetSqlCommand());


                IdUsuario = UsuarioDAO.AlterarUsuarioProfessor(objVO, where);

                long IdUsuarioDepartamento = objVO.UsuarioDepartamento.Id;
                long IdDepartamento = objVO.UsuarioDepartamento.Departamento.Id;

                if (IdUsuarioDepartamento > 0)
                {
                    UsuarioDepartamentoDAO.Alterar(new UsuarioDepartamentoVO()
                    {
                        Id = IdUsuarioDepartamento,
                        Usuario = { Id = IdUsuario },
                        Departamento = { Id = IdDepartamento },
                        Ativar = objVO.Ativo,
                    });
                }
                else
                {
                    UsuarioDepartamentoDAO.Inserir(new UsuarioDepartamentoVO()
                    {
                        Usuario = { Id = IdUsuario },
                        Departamento = { Id = IdDepartamento },
                        Ativar = true,
                    });
                }

                Commit();

                return IdUsuario;
            }
            catch (Exception e)
            {
                Rollback();

                throw e;
            }
        }


        /// <summary>
        /// VerificarUsuario
        /// </summary>
        /// <param name="objVO"></param>
        /// <returns></returns>
        public string VerificarUsuario(UsuarioVO objVO)
        {
            try
            {
                var usuarioDao = new UsuarioDAO(GetSqlCommand());
                var usuarioSenhaBE = new UsuarioSenhaBE(GetSqlCommand());


                var usuarioVO = usuarioDao.Consultar(objVO);


                if (usuarioVO == null)
                    throw new Exception("Não existe nenhum usuário com o login digitado. Por favor tente novamente.");


                var novaSenha = Criptografia.MD5(usuarioVO.NomeLogin + DateTime.Now).Substring(0, 8);

                var usuarioSenhaVO = new UsuarioSenhaVO() { IdUsuario = usuarioVO.Id };

                if (usuarioSenhaBE.ResetarSenha(usuarioSenhaVO, novaSenha) == 0)
                    throw new Exception("Ocorreu um erro ao recuperar a nova senha do usuário. Por favor tente novamente.");


                // ---------- Enviar e-mail
                var mensageriaSisUnivag = new SisUnivagMensageria();

                mensageriaSisUnivag.EnviarEmailRecuperacaoSenha(usuarioVO.Email, Dominio.DiasExpiracaoSenha.ToString(), usuarioVO.Nome, usuarioVO.NomeLogin, novaSenha);


                return usuarioVO.Email;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //VerificarUsuario
        public bool UsuarioExiste(UsuarioVO objVO)
        {
            try
            {
                var dao = new UsuarioDAO(GetSqlCommand());

                objVO = dao.Consultar(objVO);

                return (objVO != null) ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //VerificarEmail
        public void VerificarEmail(string email, long idUsuario)
        {
            UsuarioDAO usuarioDao = null;
            UsuarioVO usuarioVO = null;

            try
            {
                usuarioDao = new UsuarioDAO(GetSqlCommand());

                usuarioVO = usuarioDao.Consultar(new UsuarioVO()
                {
                    Email = email
                });

                if (usuarioVO != null && usuarioVO.Id != idUsuario)
                {
                    throw new Exception("O e-mail " + usuarioVO.Email + " já se encontra em uso, por favor tente novamente digitando outro e-mail.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Deletar
        public void Deletar(UsuarioVO objVO)
        {
            //throw new NotImplementedException();

            UsuarioDAO usuarioDao = null;
            try
            {
                BeginTransaction();
                usuarioDao = new UsuarioDAO(GetSqlCommand());
                var usuarioSenhaDao = new UsuarioSenhaDAO(GetSqlCommand());
                usuarioSenhaDao.Deletar(new UsuarioSenhaVO() { IdUsuario = objVO.Id });

                usuarioDao.Deletar(objVO);
                Commit();
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }


        public long Inserir(UsuarioVO objVO)
        {
            //throw new NotImplementedException();
            UsuarioDAO UsuarioDao = null;
            UsuarioDepartamentoDAO usuarioDepartamentoDAO = null;
            UsuarioCampusDAO usuarioCampusDAO = null;
            UsuarioSenhaDAO usuarioSenhaDAO = null;

            try
            {
                long id;
                BeginTransaction();
                UsuarioDao = new UsuarioDAO(GetSqlCommand());
                usuarioSenhaDAO = new UsuarioSenhaDAO(GetSqlCommand());
                id = UsuarioDao.Inserir(objVO);

                usuarioSenhaDAO.Inserir(new UsuarioSenhaVO()
                {
                    IdUsuario = id,
                    DataTermino = DateTime.Now.AddDays(Dominio.DiasExpiracaoSenha),
                    DataCadastro = DateTime.Now,
                    Senha = Criptografia.MD5("univag")
                });

                usuarioDepartamentoDAO = new UsuarioDepartamentoDAO(GetSqlCommand());
                var idUltimoUsuarioDepartamentoInserido = usuarioDepartamentoDAO.Inserir(new UsuarioDepartamentoVO()
                {
                    Usuario = { Id = Convert.ToInt64(id) },
                    Departamento = { Id = objVO.UsuarioDepartamento.Departamento.Id },
                    Ativar = true,
                });


                usuarioCampusDAO = new UsuarioCampusDAO(GetSqlCommand());
                var idUltimoUsuarioCampusInserido = usuarioCampusDAO.Inserir(new UsuarioCampusVO()
                {
                    Usuario = { Id = Convert.ToInt64(id) },
                    Campus = { Id = objVO.UsuarioCampus.Campus.Id },
                    Ativar = true,
                    AcessoExterno = false,
                });


                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }


        public long InserirUsuarioProfessor(UsuarioVO objVO)
        {
            UsuarioDAO UsuarioDAO = null;
            UsuarioDepartamentoDAO UsuarioDepartamentoDAO = null;
            UsuarioCampusDAO UsuarioCampusDAO = null;
            UsuarioPerfilDAO UsuarioPerfilDAO = null;
            UsuarioSenhaDAO UsuarioSenhaDAO = null;

            try
            {
                long IdUsuario;

                BeginTransaction();

                UsuarioDAO = new UsuarioDAO(GetSqlCommand());
                UsuarioSenhaDAO = new UsuarioSenhaDAO(GetSqlCommand());
                UsuarioDepartamentoDAO = new UsuarioDepartamentoDAO(GetSqlCommand());
                UsuarioCampusDAO = new UsuarioCampusDAO(GetSqlCommand());
                UsuarioPerfilDAO = new UsuarioPerfilDAO(GetSqlCommand());


                IdUsuario = UsuarioDAO.Inserir(objVO);

                UsuarioSenhaDAO.Inserir(new UsuarioSenhaVO()
                {
                    IdUsuario = IdUsuario,
                    DataCadastro = DateTime.Now,
                    DataTermino = DateTime.Now.AddDays(Dominio.DiasExpiracaoSenha),
                    Senha = Criptografia.MD5(Dominio.SenhaPadrao)
                });


                var idUltimoUsuarioDepartamentoInserido = UsuarioDepartamentoDAO.Inserir(new UsuarioDepartamentoVO()
                {
                    Usuario = { Id = IdUsuario },
                    Departamento = { Id = objVO.UsuarioDepartamento.Departamento.Id },
                    Ativar = true,
                });


                var idUltimoUsuarioCampusInserido = UsuarioCampusDAO.Inserir(new UsuarioCampusVO()
                {
                    Usuario = { Id = IdUsuario },
                    Campus = { Id = objVO.UsuarioCampus.Campus.Id },
                    Ativar = true,
                    AcessoExterno = true,
                });


                var idUltimoUsuarioPerfilInserido = UsuarioPerfilDAO.Inserir(new UsuarioPerfilVO()
                {
                    Perfil = { Id = objVO.UsuarioPerfil.Perfil.Id },
                    DataInicio = DateTime.Now,
                    DataTermino = DateTime.Now.AddYears(1),
                    Ativar = true,
                    UsuarioCampus = { Id = idUltimoUsuarioCampusInserido }
                });

                Commit();

                return IdUsuario;
            }
            catch (Exception e)
            {
                Rollback();

                throw e;
            }
        }


        public List<UsuarioVO> Selecionar(UsuarioVO usuarioVo = null, int top = 0, bool detalhar = false)
        {
            UsuarioDAO usuarioDao = null;
            UsuarioSenhaBE usuarioSenhaBe = null;
            List<UsuarioVO> lstUsuario = null;
            try
            {
                usuarioDao = new UsuarioDAO(GetSqlCommand());
                usuarioSenhaBe = new UsuarioSenhaBE(GetSqlCommand());
                lstUsuario = usuarioDao.Selecionar(usuarioVo, top);

                if (detalhar)
                {
                    foreach (var usuario in lstUsuario)
                    {
                        usuario.UsuarioSenha = usuarioSenhaBe.ConsultarUsuarioSenhaValida(new UsuarioSenhaVO() { IdUsuario = usuario.Id });
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return lstUsuario;
        }


        /// <summary>
        /// Autor: Vagner da Costa Fragoso
        /// Data: 11.11.2014
        /// Descrição: Responsavel por um selecionar usuario cadastro basico
        /// </summary>
        /// <param name="objVO"></param>
        /// <returns></returns>
        public List<UsuarioVO> SelecionarCadastroBasico(UsuarioVO objVO)
        {
            UsuarioDAO usuarioDAO = null;

            try
            {
                usuarioDAO = new UsuarioDAO(GetSqlCommand());

                return usuarioDAO.SelecionarCadastroBasico(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public UsuarioVO Consultar(UsuarioVO usuarioVo)
        {

            UsuarioDAO usuarioDao = null;
            UsuarioSenhaBE usuarioSenhaBe = null;
            UsuarioVO usuario = null;

            try
            {
                usuarioDao = new UsuarioDAO(GetSqlCommand());
                usuarioSenhaBe = new UsuarioSenhaBE(GetSqlCommand());
                usuario = usuarioDao.Consultar(usuarioVo);

                if (usuario != null)
                {
                    usuario.UsuarioSenha = usuarioSenhaBe.ConsultarUsuarioSenhaValida(new UsuarioSenhaVO() { IdUsuario = usuario.Id });
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return usuario;
        }


        public UsuarioVO AutenticarUsuario(string nomeLogin)
        {

            UsuarioDAO usuarioDao = null;
            UsuarioVO usuario = null;

            try
            {
                if (nomeLogin == null)
                    return null;

                usuarioDao = new UsuarioDAO(GetSqlCommand());
                usuario = usuarioDao.AutenticarUsuario(nomeLogin);

            }
            catch (Exception e)
            {
                throw e;
            }

            return usuario;
        }


        /// <summary>
        /// Listar
        /// </summary>
        /// <param name="usuarioVo"></param>
        /// <param name="detalhar"></param>
        /// <returns></returns>
        public List<UsuarioVO> Listar(UsuarioVO usuarioVo = null, bool detalhar = false)
        {
            try
            {
                UsuarioDAO dao = new UsuarioDAO(GetSqlCommand());
                UsuarioSenhaBE be = new UsuarioSenhaBE(GetSqlCommand());

                List<UsuarioVO>lst = dao.Listar(usuarioVo);

                if (detalhar)
                {
                    foreach (var usuario in lst)
                    {
                        usuario.UsuarioSenha = be.ConsultarUsuarioSenhaValida(new UsuarioSenhaVO() { IdUsuario = usuario.Id });
                    }
                }

                if (lst.Count > 0)
                    lst = lst.OrderBy(x => x.Nome).ToList();

                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Descrição: Responsável por listar os funcionários ativos com base no RM
        /// Autor: Giovanni Ramos
        /// Data: 28.05.2021
        /// </summary>
        public List<UsuarioVO> SelecionarFuncionariosAtivos(UsuarioVO objVO = null)
        {
            try
            {
                var usuarioDAO = new UsuarioDAO(GetSqlCommand());

                return usuarioDAO.SelecionarFuncionariosAtivos(objVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<UsuarioVO> ListarPorPerfil(UsuarioVO usuarioVo = null, bool detalhar = false)
        {
            UsuarioDAO usuarioDao = null;
            UsuarioSenhaBE usuarioSenhaBe = null;
            List<UsuarioVO> lstUsuario = null;

            try
            {
                usuarioDao = new UsuarioDAO(GetSqlCommand());
                usuarioSenhaBe = new UsuarioSenhaBE(GetSqlCommand());
                lstUsuario = usuarioDao.ListarPorPerfil(usuarioVo);

                if (detalhar)
                {
                    foreach (var usuario in lstUsuario)
                    {
                        usuario.UsuarioSenha = usuarioSenhaBe.ConsultarUsuarioSenhaValida(new UsuarioSenhaVO() { IdUsuario = usuario.Id });
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return lstUsuario;
        }


        /// <summary>
        /// Autor: Lucas Melanias Holanda
        /// Data: 18.02.2015
        /// Descrição: Retorna os departamentos do usuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public string ListarDepartamentosUsuario(long idUsuario)
        {
            UsuarioDepartamentoBE usuarioDepartamentoBe = null;

            try
            {
                var listaDepartamentoOperar = "0";

                // Seleciona o departamento do usuario

                usuarioDepartamentoBe = new UsuarioDepartamentoBE(GetSqlCommand());
                UsuarioDepartamentoVO usuarioDepartamentoVo = new UsuarioDepartamentoVO();

                var lstUsuarioDepartamentoVo = usuarioDepartamentoBe.Selecionar(new UsuarioDepartamentoVO() { Usuario = { Id = idUsuario } });
                lstUsuarioDepartamentoVo.ForEach(x => listaDepartamentoOperar += "," + x.Departamento.Id.ToString());

                return listaDepartamentoOperar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Descrição: Responsavel por validar o Usuario Cliente da API
        /// Autor: Giovanni Ramos
        /// Data: 20.03.2018
        /// </summary>
        public List<UsuarioVO> ApiAuthenticate(UsuarioVO objVO)
        {
            try
            {
                var usuarioDAO = new UsuarioDAO(GetSqlCommand());

                return usuarioDAO.ApiAuthenticate(objVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Dictionary<int, List<UsuarioVO>> Paginar(string sql, int inicio, int fim)
        {
            UsuarioDAO usuarioDao = null;
            try
            {
                usuarioDao = new UsuarioDAO(GetSqlCommand());
                return usuarioDao.Paginar(sql, inicio, fim);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Dictionary<int, List<UsuarioVO>> PaginarDadosBasicos(string sql, int inicio, int fim)
        {
            UsuarioDAO usuarioDao = null;
            try
            {
                usuarioDao = new UsuarioDAO(GetSqlCommand());
                return usuarioDao.PaginarDadosBasicos(sql, inicio, fim);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}