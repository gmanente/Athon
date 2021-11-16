using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Comum.BE;
using Sistema.Api.dll.Src.Comum.VO;
using Sistema.Api.dll.Src.Repositorio.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Script.Serialization;

namespace Sistema.Api.dll.Src.Seguranca.BE
{
    public class LoginBE : AbstractBE
    {
        public LoginBE(SqlCommand sqlComm) : base(sqlComm)
        {
        }

        public LoginBE() : base()
        {
        }


        /// <summary>
        /// Validar
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="senha"></param>
        /// <param name="captcha"></param>
        /// <param name="forcarLogin"></param>
        /// <returns></returns>
        public List<UsuarioCampusVO> Validar(string usuario, string senha, string captcha, bool forcarLogin)
        {
            try
            {
                // --------------- Checa os dados de autenticação
                if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(senha))
                    throw new Exception("error-data|usuario-senha|Usuário ou Senha não informado corretamente.|");


                var usuarioBE = new UsuarioBE(GetSqlCommand());
                var usuarioCampusBE = new UsuarioCampusBE(GetSqlCommand());
                var auditoriaBE = new AuditoriaBE(GetSqlCommand());
                var campusBE = new CampusBE(GetSqlCommand());
                var consultaBE = new ConsultaBE(GetSqlCommand());


                // --------------- Recupera e Define o appState
                string appState = Dominio.AppState.ToString();


                // --------------- Recupera o Contexto
                var httpContext = HttpContext.Current;

                // Recupera o dominio da aplicação
                string host = httpContext.Request.Url.Host;

                string sessionId = httpContext.Session.SessionID;
                string serverName = httpContext.Request.ServerVariables["SERVER_NAME"];
                string enderecoIp = httpContext.Request.UserHostAddress;
                string browserNome = httpContext.Request.Browser.Browser;
                string browserVersao = httpContext.Request.Browser.Version;
                string browserTipo = httpContext.Request.Browser.Type;
                string hostName = WindowsIdentity.GetCurrent().Name.ToString();


                // --------------- Recupera e define o lLogins - o limite de logins
                int lLogins = Dominio.NumeroLoginsHabilitarCaptcha;


                // --------------- Recupera e define o qLogins - número de tentativas de login
                int qLogins = httpContext.Session["qLogins"] != null ? Convert.ToInt32(httpContext.Session["qLogins"]) : 1;


                // --------------- Se o numero de login atingiu o limite, requere o captcha
                bool captchaShow = qLogins > lLogins;


                if (captchaShow)
                {
                    if (string.IsNullOrEmpty(captcha))
                        throw new Exception("error-data|captcha|O captcha não foi informado.|" + captchaShow);


                    // ---------- Recupera e define o captchaImagem - o texto gerado da imagem captcha
                    string captchaImagem = httpContext.Session["CaptchaImageText"] != null ? httpContext.Session["CaptchaImageText"].ToString() : null;


                    // ---------- Se o captcha não confere
                    if (captcha != captchaImagem)
                    {
                        httpContext.Session["qLogins"] = ++qLogins;

                        throw new Exception("error-data|captcha-invalido|Os dígitos da imagem captcha não conferem.<br>Por favor tente novamente.|" + captchaShow);
                    }
                }


                // --------------- Autentica o UsuarioVO
                var usuarioVO = usuarioBE.AutenticarUsuario(usuario);


                // --------------- Se encontrou o UsuarioVO, o AppState for Debug ou Teste 
                /* desabilitado em 11/09/2021

                 if (usuarioVO != null && (appState == "Debug" || appState == "Teste"))
                {
                    // ---------- Se a senha informada for padrão
                    // Define a senha padrão para o usuarioVO
                    if (Criptografia.MD5(senha) == Criptografia.MD5(Dominio.SenhaAdminDesenvolvimento))
                        usuarioVO.UsuarioSenha.Senha = Criptografia.MD5(Dominio.SenhaAdminDesenvolvimento);
                }
                */

                // --------------- Se Não encontrou o UsuarioVO ou a senha informada Não corresponde ao do usuário
                if (usuarioVO == null || usuarioVO.UsuarioSenha.Senha.Equals(senha) == false)
                {
                    httpContext.Session["qLogins"] = ++qLogins;

                    captchaShow = qLogins > lLogins;

                    throw new Exception("error-data|usuario-senha-invalido|Usuário ou Senha inválido.|" + captchaShow);
                }


                // --------------- Zera a sessão qLogins - contador de tentativas de login
                httpContext.Session["qLogins"] = 1;


                // --------------- Se a senha do usuário está expirada
                if (usuarioVO.UsuarioSenha.DataTermino < DateTime.Now)
                {
                    throw new Exception("error-data|senha-expirada|A Senha de acesso expirou.|" + captchaShow);
                }


                // --------------- Lista os UsuarioCampusVO - os campus do usuário
                var lstUsuarioCampusVO = usuarioCampusBE.Listar(new UsuarioCampusVO()
                {
                    Usuario = { Id = usuarioVO.Id },
                    Ativar = true
                });


                // --------------- Se Não encontrar o UsuarioCampusVO em lstUsuarioCampusVO
                if (lstUsuarioCampusVO.Count == 0)
                {
                    throw new Exception("error-data|campus-polo|O Usuário não têm permissão de acesso a nenhum Campus/Polo no sistema.|" + captchaShow);
                }


                // --------------- Define os idsCampus
                string idsCampus = "";

                // --------------- Define o acessoExterno do usuário
                bool acessoExterno = false;


                // --------------- Loop em lstUsuarioCampusVO
                foreach (var item in lstUsuarioCampusVO)
                {
                    idsCampus += item.Campus.Id.ToString() + ",";

                    string ipCampus = item.Campus.IpFixo;

                    // ---------- Define o acessoExterno
                    if (!acessoExterno)
                        acessoExterno = Funcoes.IsExternalIp(enderecoIp, ipCampus);
                };


                // --------------- Trata os idsCampus
                if (idsCampus.Length > 0)
                    idsCampus = idsCampus.Substring(0, idsCampus.Length - 1);


                // --------------- Se o acesso for externo e o usuário Não possui Campus/Polo com acesso de fora
                if (acessoExterno == true && lstUsuarioCampusVO.Where(t => t.AcessoExterno == true).Any() == false)
                {
                    throw new Exception("error-data|campus-polo-externo|O Usuário não têm acesso externo ao sistema.|" + captchaShow);
                }


                // --------------- Define o UsuarioCampusVO
                // o primeiro Campus que encontrar para não atrapalhar
                var usuarioCampusVO = lstUsuarioCampusVO.OrderBy(t => t.Id).FirstOrDefault();


                // --------------- Define a chaveUnica - chave única de acesso ao sistema
                var chaveUnica = Guid.NewGuid().ToString();


                // --------------- Recupera o chaveUnicaCookie
                var CookieChaveUnica = httpContext.Request.Cookies["chaveUnica"];


                // --------------- Se o chaveUnicaCookie for deferente de nulo
                if (CookieChaveUnica != null)
                {
                    chaveUnica = CookieChaveUnica.Value;
                }


                // --------------- Consulta o AuditoriaVO
                var auditoriaConsultaVO = auditoriaBE.Consultar(new AuditoriaVO() { IdUsuario = usuarioVO.Id });


                // --------------- Define o AuditoriaVO
                var auditoriaVO = new AuditoriaVO()
                {
                    Login = usuario,
                    DataLogin = DateTime.Now,
                    Senha = Criptografia.MD5(senha),
                    SessionId = sessionId,
                    ServerName = serverName,
                    IdUsuario = usuarioVO.Id,
                    EnderecoIp = enderecoIp,
                    IdCampus = usuarioCampusVO.Campus.Id,
                    BrowserNome = browserNome,
                    BrowserVersao = browserVersao,
                    BrowserTipo = browserTipo,
                    HostName = hostName,
                    ChaveUnica = new Guid(chaveUnica)
                };


                // --------------- Se Não encontrar o AuditoriaConsultaVO
                if (auditoriaConsultaVO == null)
                {
                    // ---------- Insere o auditoriaVO
                    auditoriaVO.Id = auditoriaBE.Inserir(auditoriaVO);
                }

                // --------------- Se encontrar AuditoriaConsultaVO
                else
                {
                    // ---------- Recupera dados da auditoriaConsultaVO
                    bool sessaoAtiva = auditoriaConsultaVO.SessaoAtiva;

                    bool sessaoValida = auditoriaBE.SessaoValida(auditoriaConsultaVO.DataWho);

                    var chaveUnicaAuditoria = auditoriaConsultaVO.ChaveUnica;

                    bool chavesDiferentes = (chaveUnicaAuditoria == null ||
                        chaveUnicaAuditoria.ToString() == "00000000-0000-0000-0000-000000000000" || 
                        chaveUnicaAuditoria.ToString() == chaveUnica) ? false : true;


                    // ---------- Se a sessão esta ativa na auditoriaConsultaVO, na validade e as chaves únicas são diferentes
                    if (sessaoAtiva && sessaoValida && chavesDiferentes && !forcarLogin)
                    {
                        throw new Exception("error-data|sessao-aberta|IP: <span style='color:blue'>" + auditoriaVO.EnderecoIp + 
                            "</span> Navegador: <span style='color:blue'>" + auditoriaVO.BrowserNome + "</span>|");
                    }

                    auditoriaVO.Id = auditoriaConsultaVO.Id;
                }


                // --------------- Audita o AuditoriaVO
                auditoriaBE.Auditar(auditoriaVO);


                // --------------- Recupera e define o idProfessor
                //long idProfessor = consultaBE.GetIdProfessor(usuarioVO.Id);


                // --------------- Se o chaveUnicaCookie for nulo
                if (CookieChaveUnica == null)
                {
                    // ---------- Define o Cookie chaveUnica
                    CookieChaveUnica = new HttpCookie("ChaveUnicaSistema");

                    CookieChaveUnica.Value = chaveUnica.ToString();
                    CookieChaveUnica.Expires = DateTime.Now.AddDays(1d); // 1 dia
                    CookieChaveUnica.Domain = host;

                    httpContext.Response.Cookies.Add(CookieChaveUnica);
                }


                // --------------- Se login foi realizado com a senha Padrão do sistema
                // Define o trocarSenhaPadrao - força a mudança de senha pelo usuário após o login
                bool trocarSenhaPadrao = (senha == Dominio.SenhaPadrao) ? true : false;


                // --------------- Define a Sessão sessaoSistema - Dados do Usuário no Sistema
                var sessaoSistema = new SessaoSistema()
                {
                    IdUsuario = usuarioVO.Id,
                    IdUsuarioCampus = usuarioCampusVO.Id,
                    IdCampus = usuarioCampusVO.Campus.Id,
                    NomeCampus = usuarioCampusVO.Campus.Nome,
                    LoginNome = usuarioCampusVO.Usuario.NomeLogin,
                    NomeUsuario = usuarioCampusVO.Usuario.Nome,
                    EmailUsuario = usuarioCampusVO.Usuario.Email,
                    IdAuditoria = auditoriaVO.Id,
                    AcessoExterno = acessoExterno,
                    IdsCampus = idsCampus,
                    IdProfessor = 0,
                    HostName = hostName,
                    Portal = false,
                    TrocarSenhaPadrao = trocarSenhaPadrao
                };


                // --------------- Define o Cookie SessaoSistema
                GerarCookieSessaoSistema(sessaoSistema);


                return lstUsuarioCampusVO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Entrar
        /// </summary>
        /// <param name="idCampus"></param>
        public void Entrar(long idCampus)
        {
            try
            {
                // --------------- Checa os dados de autenticação
                if (idCampus == 0)
                    throw new Exception("error-data|campus|O campus/Polo não informado corretamente.");


                var usuarioCampusBE = new UsuarioCampusBE(GetSqlCommand());


                // --------------- Recupera a Sessão Sistema
                var sessaoSistema = RecuperarSessaoSistema();


                long idUsuario = sessaoSistema.IdUsuario;
                bool acessoExterno = sessaoSistema.AcessoExterno;


                // --------------- Consulta os UsuarioCampusVO
                var usuarioCampusVO = usuarioCampusBE.Consultar(new UsuarioCampusVO()
                {
                    Id = idCampus,
                    Usuario = { Id = idUsuario },
                    Ativar = true
                });

                if (usuarioCampusVO == null)
                    throw new Exception("error-data|campus|O usuário não tem acesso ao Campus/Polo informado.Por favor escolha outro.");


                // --------------- Se o acesso for externo e o usuário Não possui Campus/Polo com acesso de fora
                if (acessoExterno == true && usuarioCampusVO.AcessoExterno == false)
                    throw new Exception("error-data|campus|O usuário não possui permissão de acesso externo para o Campus/Polo informado.Por favor escolha outro.");


                // --------------- Atualiza a Sessão sessaoSistema com os dados do Campus
                sessaoSistema.IdUsuarioCampus = usuarioCampusVO.Id;
                sessaoSistema.IdCampus = usuarioCampusVO.Campus.Id;
                sessaoSistema.NomeCampus = usuarioCampusVO.Campus.Nome;


                // --------------- Define o Cookie SessaoSistema
                GerarCookieSessaoSistema(sessaoSistema);
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }


        /// <summary>
        /// RecuperarSenha
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="captcha"></param>
        /// <returns></returns>
        public string RecuperarSenha(string usuario, string captcha)
        {
            try
            {
                // --------------- Checa os dados de autenticação
                if (string.IsNullOrEmpty(usuario))
                    throw new Exception("error-data|usuario|Usuário não informado corretamente.");

                if (string.IsNullOrEmpty(captcha))
                    throw new Exception("error-data|captcha|Captcha não informado corretamente.");


                var session = HttpContext.Current.Session;

                string captchaImagem = null;


                if (session["CaptchaImageText"] == null || string.IsNullOrEmpty(session["CaptchaImageText"].ToString()))
                    throw new Exception("error-data|captcha|Os dígitos do captcha não foram gerados corretamente.");


                captchaImagem = session["CaptchaImageText"].ToString();


                // Se o o valor não confere com o captcha da imagem
                if (captcha != captchaImagem)
                    throw new Exception("error-data|captcha|Os dígitos da imagem não conferem. Por favor tente novamente.");


                var be = new UsuarioBE(GetSqlCommand());


                // ------------ Consulta o e envia a nova senha para o usuário pelo nomeLogin
                var usuarioEmail = be.VerificarUsuario(new UsuarioVO()
                {
                    NomeLogin = usuario
                });


                return usuarioEmail;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// RemoverCookieSessaoSistema
        /// </summary>
        public void RemoverCookieSessaoSistema()
        {
            try
            {
                // --------------- Recupera e Define o appState
                string appState = Dominio.AppState.ToString();


                // --------------- Recupera o Contexto
                var httpContext = HttpContext.Current;


                // --------------- Exclui os Cookies SessaoSistema
                if (httpContext.Response.Cookies["SessaoSistema"] != null)
                    httpContext.Response.Cookies["SessaoSistema"].Expires = DateTime.Now.AddDays(-1);


                if (appState != "Producao")
                {
                    if (httpContext.Response.Cookies["SessaoSistemaDebug"] != null)
                        httpContext.Response.Cookies["SessaoSistemaDebug"].Expires = DateTime.Now.AddDays(-1);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// GerarCookieSessaoSistema
        /// </summary>
        /// <param name="sessaoSistema"></param>
        public void GerarCookieSessaoSistema(SessaoSistema sessaoSistema)
        {
            try
            {
                // --------------- Recupera e Define o appState
                string appState = Dominio.AppState.ToString();


                // --------------- Recupera o Contexto
                var httpContext = HttpContext.Current;

                // Recupera o dominio da aplicação
                string host = httpContext.Request.Url.Host;


                // --------------- Define o Cookie SessaoSistema
                var CookieSessaoSistema = new HttpCookie("SessaoSistema");

                string Base64SessaoSistema = Criptografia.Base64Encode(new JavaScriptSerializer().Serialize(sessaoSistema));

                CookieSessaoSistema.Value = Base64SessaoSistema;
                CookieSessaoSistema.Expires = DateTime.Now.AddDays(1d); // 1 dia
                CookieSessaoSistema.Domain = host;

                httpContext.Response.Cookies.Add(CookieSessaoSistema);


                // --------------- Define o Cookie SessaoSistemaDebug
                if (appState != "Producao")
                {
                    CookieSessaoSistema = new HttpCookie("SessaoSistemaDebug");

                    Base64SessaoSistema = new JavaScriptSerializer().Serialize(sessaoSistema);

                    CookieSessaoSistema.Value = Base64SessaoSistema;
                    CookieSessaoSistema.Expires = DateTime.Now.AddDays(1d); // 1 dia
                    CookieSessaoSistema.Domain = host;

                    httpContext.Response.Cookies.Add(CookieSessaoSistema);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// RecuperarSessaoSistema
        /// </summary>
        /// <returns></returns>
        public SessaoSistema RecuperarSessaoSistema()
        {
            try
            {
                // --------------- Recupera o Contexto
                var httpContext = HttpContext.Current;


                // --------------- Recupera o Cookie SessaoSistema
                var CookieSessaoSistema = httpContext.Request.Cookies["SessaoSistema"];

                if (CookieSessaoSistema == null || string.IsNullOrEmpty(CookieSessaoSistema.Value))
                    throw new Exception("error-data|logout|A sessão do usuário foi expirada. Por favor refaça o login.");


                SessaoSistema sessaoSistema = null;

                try
                {
                    sessaoSistema = new JavaScriptSerializer().Deserialize<SessaoSistema>(Criptografia.Base64Decode(CookieSessaoSistema.Value));

                    if (sessaoSistema == null || sessaoSistema.IdUsuario == 0)
                        throw new Exception();
                }
                catch
                {
                    throw new Exception("error-data|logout|Falha na autenticação do usuário. Por favor refaça o login.");
                }


                return sessaoSistema;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// VerificarRenovarSessaoSistema
        /// </summary>
        /// <returns></returns>
        public SessaoSistema VerificarRenovarSessaoSistema()
        {
            try
            {
                // --------------- Recupera a Sessão Sistema
                var sessaoSistema = RecuperarSessaoSistema();


                // --------------- Atualiza o Cookie SessaoSistema
                GerarCookieSessaoSistema(sessaoSistema);


                return sessaoSistema;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
