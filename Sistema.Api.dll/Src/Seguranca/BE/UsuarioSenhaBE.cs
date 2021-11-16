using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Seguranca.DAO;
using Sistema.Api.dll.Src.Seguranca.VO;
using Sistema.Api.dll.Template.Repositorio.Mensageria;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Sistema.Api.dll.Src.Seguranca.BE
{
    public class UsuarioSenhaBE : AbstractBE, IBE<UsuarioSenhaVO>
    {
        public UsuarioSenhaBE(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public UsuarioSenhaBE()
            : base()
        {
        }

        //Alterar
        public long Alterar(UsuarioSenhaVO objVO, string where = null)
        {
            UsuarioSenhaDAO usuarioSenhaDAO = null;
            try
            {
                long id;
                BeginTransaction();
                usuarioSenhaDAO = new UsuarioSenhaDAO(GetSqlCommand());
                id = usuarioSenhaDAO.Alterar(objVO, where);
                Commit();
                return id;

            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        //ResetarSenha
        public long ResetarSenha(UsuarioSenhaVO objVO, string novaSenha = "", bool transacao = false)
        {
            UsuarioSenhaDAO usuarioSenhaDAO = null;
            long id = 0;
            try
            {
                usuarioSenhaDAO = new UsuarioSenhaDAO(GetSqlCommand());

                var lst = VerificarSenha(objVO, "TOP 1");

                if (transacao)
                    BeginTransaction();
                //Selecionar registro mais atual
                var usuarioSenhaVO = (from us in lst
                                      orderby us.DataCadastro descending
                                      select us).First();

                //Alterar a data da modidficação da data termino do registro mais atual
                usuarioSenhaVO.DataTermino = DateTime.Now;
                id = usuarioSenhaDAO.Alterar(usuarioSenhaVO);

                //Inserir novo registro usuário senha
                var usuarioSenhaVOInserir = new UsuarioSenhaVO()
                {
                    IdUsuario = objVO.IdUsuario,
                    DataCadastro = DateTime.Now,
                    DataTermino = DateTime.Now.AddDays(Dominio.DiasExpiracaoSenha)
                };

                //Nova senha
                if (novaSenha != "")
                {
                    usuarioSenhaVOInserir.Senha = novaSenha; //Criptografia.MD5(novaSenha);
                }
                else
                {
                    usuarioSenhaVOInserir.Senha = Dominio.SenhaPadrao; // Criptografia.MD5(Dominio.SenhaPadrao);
                }

                id = usuarioSenhaDAO.Inserir(usuarioSenhaVOInserir);
                if (transacao)
                    Commit();
                return id;

            }
            catch (Exception e)
            {
                if (transacao)
                    Rollback();
                throw e;
            }
        }



        //VerificarUltimasSenhas
        public long VerificarUltimasSenhas(UsuarioSenhaVO objVO, string senha, string nome = "", string login = "", string email = "")
        {
            long id = 0;
            try
            {
                //Selecionar registros mais atuais
                //objVO.Senha = Criptografia.MD5(senha);
                objVO.Senha = senha;
                var lst = VerificarSenha(objVO);

                // Resetar senha
                if (lst != null && lst.Count() == 0)
                {
                    objVO.Senha = null;

                    ResetarSenha(objVO, senha);

                    // Exclui a sessão de forçar a mudança de senha
                    if (HttpContext.Current.Session["useDefaultPassword"] != null)
                        HttpContext.Current.Session["useDefaultPassword"] = null;


                    // Envia um e-mail informando a nova senha
                    try
                    {
                        if (email != "")
                        {
                            var mensageriaSisUnivag = new SisUnivagMensageria();
                            mensageriaSisUnivag.EnviarEmailAlteracaoSenha(email, nome, login, senha);
                        }
                    }
                    catch (Exception)
                    {
                    }

                }
                else
                {
                    throw new Exception("Você não pode utilizar essa senha, pois a mesma se encontra na lista das últimas utilizadas. Por favor digite uma senha diferente.");
                }

                return id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        public void Deletar(UsuarioSenhaVO objVO)
        {

            UsuarioSenhaDAO usuarioSenhaDao = null;
            try
            {
                BeginTransaction();
                usuarioSenhaDao = new UsuarioSenhaDAO(GetSqlCommand());
                usuarioSenhaDao.Deletar(objVO);
                Commit();
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        //Inserir
        public long Inserir(UsuarioSenhaVO objVO)
        {
            UsuarioSenhaDAO usuarioSenhaDAO = null;
            try
            {
                long id;
                BeginTransaction();
                usuarioSenhaDAO = new UsuarioSenhaDAO(GetSqlCommand());
                id = usuarioSenhaDAO.Inserir(objVO);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public List<UsuarioSenhaVO> Selecionar(UsuarioSenhaVO usuarioSenhaVo = null, int top = 0, bool detalhar = false)
        {
            UsuarioSenhaDAO usuarioSenhaDao = null;
            try
            {
                usuarioSenhaDao = new UsuarioSenhaDAO(GetSqlCommand());
                return usuarioSenhaDao.Selecionar(usuarioSenhaVo, top);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public UsuarioSenhaVO Consultar(UsuarioSenhaVO usuarioSenhaVo)
        {
            UsuarioSenhaDAO usuarioSenhaDao = null;
            try
            {
                usuarioSenhaDao = new UsuarioSenhaDAO(GetSqlCommand());
                return usuarioSenhaDao.Consultar(usuarioSenhaVo);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public UsuarioSenhaVO ConsultarUsuarioSenhaValida(UsuarioSenhaVO usuarioSenhaVo)
        {
            List<UsuarioSenhaVO> lstUsuarioSenha = null;
            try
            {
                lstUsuarioSenha = Listar(usuarioSenhaVo);
                var usuarioSenhas = (from us in lstUsuarioSenha
                                     orderby us.Id descending
                                     select us).Take(1);
                if (usuarioSenhas.Count() > 0)
                {
                    return (UsuarioSenhaVO)usuarioSenhas.ToArray().GetValue(0);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Listar
        public List<UsuarioSenhaVO> Listar(UsuarioSenhaVO usuarioSenhaVo = null, bool detalhar = false)
        {
            UsuarioSenhaDAO usuarioSenhaDao = null;
            try
            {
                usuarioSenhaDao = new UsuarioSenhaDAO(GetSqlCommand());
                return usuarioSenhaDao.Listar(usuarioSenhaVo);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Listar
        public List<UsuarioSenhaVO> VerificarSenha(UsuarioSenhaVO usuarioSenhaVo, string top = " ")
        {
            UsuarioSenhaDAO usuarioSenhaDao = null;
            try
            {
                usuarioSenhaDao = new UsuarioSenhaDAO(GetSqlCommand());
                return usuarioSenhaDao.VerificarSenha(usuarioSenhaVo, top);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
