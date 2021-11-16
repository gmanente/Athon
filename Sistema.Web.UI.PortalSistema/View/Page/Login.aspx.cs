using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using Sistema.Api.dll.Src.Comum.BE;
using Sistema.Api.dll.Src.Comum.VO;
using Sistema.Api.dll.Repositorio.Util;
using System.Linq;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using Sistema.Api.dll.Src.Repositorio.BE;
using Sistema.Api.dll.Src.CarteirinhaAluno.VO;
using Sistema.Api.dll.Src.CarteirinhaAluno.BE;
using Sistema.Web.UI.PortalSistema.Util;
using Sistema.Web.UI.PortalProfessor.View.MasterPage;
using Sistema.Api.dll.Src.Comum.DAO;
using Sistema.Api.dll.Src;

namespace Sistema.Web.UI.PortalSistema.View.Page
{
    public partial class Login : System.Web.UI.Page
    {
        public string Ambiente { get; set; }
        public static string SenhaDesenvolvimento { get; set; } = "univ@g";


        /// <summary>
        /// Autores: Vagner da Costa Fragoso e Evander Costa
        /// Data: 05.05.2014
        /// Descrição: Load da pagina verifica o status da sessão
        /// Autor Alteração: Lucas Melanias Holanda
        /// Data Alteração: 16.04.2016
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            AuditoriaBE AuditoriaBe = null;

            try
            {
                var status = Request.QueryString["status"];

                var sessao = ProfessorMaster.GetSession();


                // --------------- Se o Status for logout
                if (status == "logoff" && sessao != null)
                {                    
                    AuditoriaBe = new AuditoriaBE();

                    AuditoriaBe.AtivarDesativarSessao(sessao.IdUsuario, false);

                    // Remove a sessão Session
                    SessionHandler.RemoveSession("SessionPortalSistema"); 
                }


                // --------------- Recupera os parametros do URL
                string scheme = Request.Url.Scheme;
                string host = Request.Url.Host;
                string ipCliente = GetIPAddress();


                // --------------- Redireciona para o Protocolo Seguro Https
                if (scheme == "http" && host != "localhost")
                {
                    if (Funcoes.IsExternalIp(ipCliente, ""))
                        Response.Redirect("https://" + Request.Url.Host + Request.Url.PathAndQuery);
                }


                // --------------- Define o Ambiente
                if (host == "localhost")
                    Ambiente = "Ambiente de Desenvolvimento";
                else if (host == "portalprofessor.univag.teste.edu.br")
                    Ambiente = "Ambiente de Teste";
                else if (host == "portalprofessor.univaglabs.edu.br")
                    Ambiente = "Ambiente de Homologação";
                else
                    Ambiente = "";


                // --------------- Remove o Cookie "trocarSenhaPadrao" caso exista
                if (Request.Cookies["trocarSenhaPadrao"] != null)
                {
                    Response.SetCookie(new HttpCookie("trocarSenhaPadrao")
                    {
                        Value = null,
                        Expires = DateTime.Now.AddDays(-1d)
                    });
                }  
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (AuditoriaBe != null)
                    AuditoriaBe.FecharConexao();
            }
        }


        /// <summary>
        /// GetIPAddress
        /// </summary>
        /// <returns></returns>
        protected string GetIPAddress()
        {
            string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');

                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return Request.ServerVariables["REMOTE_ADDR"];
        }


        /// <summary>
        /// VerificarCaptcha
        /// </summary>
        /// <returns></returns>
        protected static bool VerificarCaptcha()
        {
            // Recupera o limite de logins
            int lLogins = Dominio.NumeroLoginsHabilitarCaptcha;
            int qLogins = 0;

            // Se ainda não foi criada a Sessão de verificação de tentativas de login
            if (HttpContext.Current.Session["qLoginsPortalProfessor"] == null)
            {
                HttpContext.Current.Session["qLoginsPortalProfessor"] = qLogins;
            }
            else
            {
                qLogins = Convert.ToInt32(HttpContext.Current.Session["qLoginsPortalProfessor"]);
            }


            // Se ainda não foi criado o Cookie de verificação de tentativas de login
            if (HttpContext.Current.Request.Cookies["qLoginsPortalProfessor"] == null)
            {
                // Cria os cookies de verificação
                var cookieQLogins = new HttpCookie("qLoginsPortalProfessor");

                cookieQLogins.Value = qLogins.ToString();

                var cookieTempo = new TimeSpan(0, 12, 0, 0); // 12 horas

                cookieQLogins.Expires = DateTime.Now + cookieTempo;

                HttpContext.Current.Response.Cookies.Add(cookieQLogins);
            }


            return qLogins >= lLogins;
        }


        /// <summary>
        /// Criado por: Evander Costa
        /// Modified: Carlos Cortez
        /// Autor: by Evander Costa Carlos Cortez
        /// Date: 21/08/2015
        /// Descrição: Método Serviço para realizar a validação do usuário no login.
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        [WebMethod]
        public static string Entrar(Object inputs)
        {
            Ajax ajax = new Ajax();


            UsuarioBE usuarioBe = null;


            // --------------- Seta os parametros padrão de retorno
            var retorno = new Autenticacao()
            {
                Resposta = false,
                RespostaErro = "500",
                MensagemTipo = "danger",
                MensagemTexto = "Erro Interno do servidor.",
                CaptchaShow = false,
            };


            try
            {
                usuarioBe = new UsuarioBE();
                var usuarioCampusBe = new UsuarioCampusBE(usuarioBe.GetSqlCommand());
                var campusBe = new CampusBE(usuarioBe.GetSqlCommand());
                var consultaBE = new ConsultaBE(usuarioBe.GetSqlCommand());
                var auditoriaBe = new AuditoriaBE(usuarioBe.GetSqlCommand());
                var professorBE = new ProfessorBE(usuarioBe.GetSqlCommand());


                var current = HttpContext.Current;


                // --------------- Recupera os Campos
                var campo = ajax.ToDynamic(inputs);


                string usuario = campo.Usuario;
                string senha = campo.Senha;
                string captcha = campo.Captcha;

                string sessionId = current.Session.SessionID;
                string serverName = campo.ServerName;
                string enderecoIp = campo.EnderecoIp;
                string browserVersao = campo.BrowserVersao;
                string BrowserTipo = campo.BrowserTipo;
                string browserNome = campo.BrowserNome;


                bool captchaOk = true;
                bool verificarCaptcha = VerificarCaptcha();
                int qLogins = Convert.ToInt32(current.Session["qLoginsPortalProfessor"]);


                // --------------- Verifica o Ambiente de Operação
                string host = current.Request.Url.Host;
                bool ambienteDesenvolvimento = host != "portalprofessor.univag.edu.br" ? true : false;


                // Se o numero de login atingiu o limite, requere o captcha
                if (verificarCaptcha)
                {
                    // Recupera o texto gerado da imagem captcha
                    string captchaImagem = current.Session["CaptchaImageText"] != null ? current.Session["CaptchaImageText"].ToString() : null;


                    // Se o o valor não confere com o captcha da imagem
                    if (captcha != captchaImagem)
                    {
                        captchaOk = false;

                        retorno.RespostaErro = "captcha";
                        retorno.MensagemTipo = "warning";
                        retorno.MensagemTexto = "Os dígitos da imagem não confere. Por favor tente novamente.";
                    }

                    // Requera o captcha
                    retorno.CaptchaShow = true;
                }


                // ---------- Se o captcha for verdadeiro
                if (captchaOk)
                {
                    // ---------- Consulta o UsuarioVo
                    var usuarioVo = usuarioBe.AutenticarUsuario(usuario);


                    if (usuarioVo != null)
                    {
                        if (!usuarioVo.Email.Contains("@univag.edu.br") || usuarioVo.Email.Contains("altereseuemail"))
                        {
                            retorno.Resposta = false;
                            retorno.RespostaErro = "cadastro";
                            retorno.MensagemTipo = "info";
                            retorno.MensagemTexto = "Foi identificado que você não possui um e-mail institucional atualizado em sua conta de usuário!<br />Para prosseguir com as operações do Portal do Professor, será necessário que você entre em contato com a coordenação do seu curso para a atualização cadastral da sua conta de usuário.";

                            ajax.StatusOperacao = true;
                            ajax.Variante = Json.Serialize(retorno);

                            return ajax.GetAjaxJson();
                        }


                        // ---------- Se o usuário ou a senha informada pelo usuário for igual a do banco de dados
                        if (usuarioVo.UsuarioSenha.Senha.Equals(Criptografia.MD5(senha)) || (ambienteDesenvolvimento && senha == SenhaDesenvolvimento) )
                        {                           
                            var UsuarioCampusVo = usuarioCampusBe.Consultar(new UsuarioCampusVO() { Usuario = { Id = usuarioVo.Id } });


                            if (UsuarioCampusVo.Id > 0)
                            {
                                usuarioVo.UsuarioCampus = UsuarioCampusVo;
                            }


                            long idUsuario = usuarioVo.Id;
                            long idCampusUsuario = usuarioVo.UsuarioCampus.Id;


                            // Se a senha não estiver expirada
                            if (usuarioVo.UsuarioSenha.DataTermino > DateTime.Now)
                            {
                                // Seta os parametros da Auditoria
                                var auditoriaVo = new AuditoriaVO()
                                {
                                    DataLogin = DateTime.Now,
                                    Login = usuario,
                                    Senha = Criptografia.MD5(senha),
                                    SessionId = sessionId,
                                    ServerName = serverName,
                                    IdUsuario = idUsuario,
                                    EnderecoIp = enderecoIp,
                                    IdCampus = usuarioVo.UsuarioCampus.Campus.Id,
                                    BrowserVersao = browserVersao,
                                    BrowserTipo = Convert.ToBoolean(BrowserTipo) ? "Win32" : "Win64",
                                    BrowserNome = browserNome
                                  //  ChaveUnica = Guid.NewGuid()
                                };


                                string[] computer_name = new string[] { };
                                try
                                {
                                    computer_name = System.Net.Dns.GetHostEntry(current.Request.ServerVariables["REMOTE_ADDR"]).HostName.Split(new Char[] { '.' });
                                }
                                catch
                                {
                                    computer_name = new string[] { "HostName não Identificado" };
                                }

                                auditoriaVo.HostName = computer_name[0];


                                var auditoria = auditoriaBe.Consultar(new AuditoriaVO() { IdUsuario = idUsuario });


                                if (Dominio.UnicaSessao && auditoria != null && auditoriaBe.SessaoAtiva(idUsuario, current.Request.UserHostAddress, false))
                                {
                                    retorno.RespostaErro = "sessao-aberta";
                                    retorno.MensagemTipo = "danger";
                                    retorno.MensagemTexto = Json.Serialize(new string[] { "Você possui uma sessão aberta", auditoria.EnderecoIp, auditoria.BrowserNome, auditoria.ServerName });

                                    ajax.StatusOperacao = true;
                                    ajax.Variante = Json.Serialize(retorno);

                                    return ajax.GetAjaxJson();
                                }
                                else
                                {
                                    if (auditoria == null)
                                    {
                                        // Insere o registro na Auditoria
                                        auditoriaVo.Id = auditoriaBe.Inserir(auditoriaVo);
                                    }
                                    else
                                    {
                                        auditoriaVo.Id = auditoria.Id;
                                    }

                                    
                                    // ---------- Audita
                                    auditoriaBe.Auditar(auditoriaVo);
                                }


                                long idProfessor = consultaBE.GetIdProfessor(idUsuario);


                                ProfessorVO professorVO = null;

                                if (idProfessor > 0)
                                {
                                    professorVO = new ProfessorVO()
                                    {
                                        Id = idProfessor
                                    };
                                    
                                    professorVO = professorBE.Consultar(professorVO);
                                }

                                
                                if (professorVO != null && professorVO.Ativo == true)
                                {
                                    bool acessoExterno = false;

                                    String[] IdsCampus = campusBe.CampusPorUsuario(idUsuario).Split(',');

                                    CampusVO campusVO = new CampusVO();

                                    foreach (var item in IdsCampus)
                                    {
                                        if (!string.IsNullOrEmpty(item))
                                        {
                                            campusVO.Id = Convert.ToInt32(item);

                                            String IpCampus = campusBe.Consultar(campusVO).IpFixo;

                                            acessoExterno = Funcoes.IsExternalIp(enderecoIp, IpCampus);
                                        }
                                    }


                                    var lstCursoProfessor = new CursoDAO(campusBe.GetSqlCommand()).ListarCursoPrProfessor(professorVO.Id);


                                    long idModuloNormal = 0;
                                    long idModuloMedicina = 0;


                                    foreach (var item in lstCursoProfessor)
                                    {
                                        if (item.Sigla == "MED")
                                        {
                                            idModuloMedicina = 27;
                                        }
                                        else
                                        {
                                            idModuloNormal = 18;
                                        }
                                    }


                                    // Se o usuário não tiver nenhum curso vinculado, colocar no modulo normal
                                    idModuloNormal = (idModuloNormal == 0 && idModuloMedicina == 0 ? 18 : idModuloNormal);
                                    

                                    // --------------- Define o SessionPortalProfessor
                                    var sessaoSistema = new SessionPortalSistema()
                                    {
                                        IdUsuario = idUsuario,
                                        NomeUsuario = usuarioVo.Nome,
                                        EmailUsuario = usuarioVo.Email,
                                        IdAuditoria = auditoriaVo.Id,
                                        AcessoExterno = true, //acessoExterno,
                                        IdsCampus = campusBe.CampusPorUsuario(idUsuario),
                                        IdProfessor = 0, //idProfessor,
                                        HostName = auditoriaVo.HostName,
                                        Portal = true
                                        //IdModuloLogado = (idModuloMedicina > 0 && idModuloNormal > 0 ? idModuloNormal : (idModuloMedicina > 0 ? idModuloMedicina : idModuloNormal)),
                                        //IdModuloNormal = idModuloNormal,
                                        //IdModuloMedicina = idModuloMedicina
                                    };


                                    // --------------- Cria a sessão Session
                                    var sessao = new SessionHandler()
                                    {
                                        Objeto = sessaoSistema,
                                        Portal = true
                                    };

                                    sessao.NewSession("SessionPortalSistema", Global.SessionCookieTimeout);


                                    // --------------- Cria a sessão DataTelaBloqueio
                                    var telaBloqueio = new SessionHandler()
                                    {
                                        Objeto = DateTime.Now.AddMinutes(Dominio.MinutosBloqueioTela)
                                    };

                                    telaBloqueio.NewSession("DataTelaBloqueioPortalProfessor");


                                    // Seta os parametros de sucesso
                                    retorno.Resposta = true;
                                    retorno.RespostaErro = null;
                                    retorno.MensagemTipo = "success";
                                    retorno.MensagemTexto = "Entrada realizada com sucesso! Redirecionando para tela inicial do sistema...";


                                    // Se a senha for padrão do sistema
                                    if (Dominio.SenhaPadrao == senha)
                                    {
                                        current.Response.SetCookie(new HttpCookie("trocarSenhaPadrao")
                                        {
                                            Value = "s"
                                        });
                                    }


                                    current.Response.Cookies["qLoginsPortalProfessor"].Value = "0";
                                    current.Session["qLoginsPortalProfessor"] = "0";
                                }
                                else
                                {
                                    retorno.Resposta = false;
                                    retorno.RespostaErro = "perfil";
                                    retorno.MensagemTipo = "info";
                                    retorno.MensagemTexto = "O Usuário informado não possui acesso ao Portal do Professor!";
                                }
                            }
                            else
                            {
                                retorno.RespostaErro = "expiracao";
                                retorno.MensagemTipo = "warning";
                                retorno.MensagemTexto = "Sua senha expirou. Por favor acesse a opção <strong>Recuperar senha</strong>:";
                            }
                        }
                        else
                        {
                            ++qLogins;
                            current.Response.Cookies["qLoginsPortalProfessor"].Value = qLogins.ToString();
                            current.Session["qLoginsPortalProfessor"] = qLogins;

                            retorno.RespostaErro = "login";
                            retorno.MensagemTipo = "warning";
                            retorno.MensagemTexto = "Usuário ou senha inválida!";
                        }
                    }
                    else
                    {
                        ++qLogins;
                        current.Response.Cookies["qLoginsPortalProfessor"].Value = qLogins.ToString();
                        current.Session["qLoginsPortalProfessor"] = qLogins;

                        retorno.RespostaErro = "login";
                        retorno.MensagemTipo = "warning";
                        retorno.MensagemTexto = "Usuário não encontrato. Entrar em contato com o seu Diretor de Área.";
                    }
                }


                ajax.StatusOperacao = true;

                ajax.Variante = Json.Serialize(retorno);
            }
            catch (Exception e)
            {
                retorno.MensagemTexto += "<br>" + e.Message;

                ajax.StatusOperacao = false;

                ajax.Variante = Json.Serialize(retorno);
            }
            finally
            {
                if (usuarioBe != null)
                    usuarioBe.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }


        /// <summary>
        /// Autor: Evander Costa
        /// Data: 10.10.2014
        /// Descrição: Método Serviço para recuperar a senha do usuário.
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="captcha"></param>
        /// <returns>Objeto json</returns> 
        [WebMethod]
        public static string RecuperarSenha(Object s)
        {
            Ajax ajax = new Ajax();


            //NameValueCollection campo = HttpUtility.ParseQueryString(new StreamReader(s).ReadToEnd());
            string captchaImagem = null;

            bool captchaOk = true;
            UsuarioBE usuarioBE = null;

            // Seta os parametros padrão de retorno
            Autenticacao retorno = new Autenticacao()
            {
                Resposta = false,
                RespostaErro = "500",
                MensagemTipo = "danger",
                MensagemTexto = "Erro! "
            };

            try
            {
                var campo = ajax.ToDynamic(s);

                string usuario = campo.Usuario;
                string captcha = campo.Captcha;



                // Se a Sessão CaptchaImageText não for nulo
                if (HttpContext.Current.Session["CaptchaImageText"] != null)
                {
                    // Recupera o valor
                    captchaImagem = HttpContext.Current.Session["CaptchaImageText"].ToString();
                }

                // Se o o valor não confere com o captcha da imagem
                if (captcha != captchaImagem)
                {
                    // Seta o captcha como falso
                    captchaOk = false;

                    // Seta os parametros
                    retorno.RespostaErro = "captcha";
                    retorno.MensagemTipo = "warning";
                    retorno.MensagemTexto = "Os dígitos da imagem não confere. Por favor tente novamente.";


                    ajax.StatusOperacao = true;
                    ajax.Variante = Json.Serialize(retorno);
                }

                // Se o captcha for verdadeiro
                if (captchaOk)
                {
                    usuarioBE = new UsuarioBE();

                    // Consulta o usuário pelo nomeLogin
                    var usuarioEmail = usuarioBE.VerificarUsuario(new UsuarioVO()
                    {
                        NomeLogin = usuario
                    });

                    // Seta os parametros
                    retorno.Resposta = true;
                    retorno.RespostaErro = null;
                    retorno.MensagemTipo = "success";
                    retorno.MensagemTexto = "Solicitação de recuperação de senha processada com sucesso! Por favor verifique a caixa postal do e-mail: <strong>" + usuarioEmail + "</strong> para novas instruções.";

                    ajax.StatusOperacao = true;
                    ajax.Variante = Json.Serialize(retorno);
                }
            }
            catch (Exception e)
            {
                // Seta a mensagem de erro
                retorno.MensagemTexto += e.Message;

                ajax.StatusOperacao = true;
                ajax.Variante = Json.Serialize(retorno);
            }
            finally
            {
                if (usuarioBE != null)
                    usuarioBE.FecharConexao();
            }

            // Retorna os parametros
            return ajax.GetAjaxJson();
        }


        /// <summary>
        /// Create By: Evander Costa
        /// Create date: 
        /// Modified: Carlos Cortez
        /// Modified Data: 21/08/2015
        /// Description: Class Model Autentication
        /// </summary>
        private class Autenticacao
        {
            public bool Resposta { get; set; }
            public string RespostaErro { get; set; }
            public string MensagemTipo { get; set; }
            public string MensagemTexto { get; set; }
            public bool CaptchaShow { get; set; }
        }


        /// <summary>
        /// Método responsável por realizar a busca da foto do usuariário
        /// </summary>
        /// <param name="Cpf"></param>
        /// <returns></returns>
        [WebMethod]
        public static string AvatarUsuario(String login)
        {
            Ajax ajax = new Ajax();
            String img = String.Empty;

            try
            {
                img = Avatar(login);
                ajax.StatusOperacao = true; ;
                ajax.Variante = img;

            }
            catch (Exception ex)
            {
                img = ex.Message;

                ajax.StatusOperacao = false; ;
                ajax.Variante = img;

            }

            return ajax.GetAjaxJson();
        }


        /// <summary>
        /// Avatar
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public static string Avatar(String login)
        {
            String img = String.Empty;

            FuncionarioFotoBE funcionarioFotoBE = null;
            FuncionarioFotoVO funcionarioFotoVO = null;
            UsuarioVO usuarioVO = null;
            try
            {
                funcionarioFotoBE = new FuncionarioFotoBE();
                usuarioVO = new UsuarioVO();


                usuarioVO = new UsuarioBE(funcionarioFotoBE.GetSqlCommand()).Consultar(new UsuarioVO()
                {
                    NomeLogin = login
                });


                if (!String.IsNullOrEmpty(usuarioVO.Cpf))
                {


                    funcionarioFotoVO = new FuncionarioFotoVO()
                    {
                        Cpf = usuarioVO.Cpf
                    };

                    funcionarioFotoVO = funcionarioFotoBE.Consultar(funcionarioFotoVO);

                    if (funcionarioFotoVO != null)
                    {
                        img = @"data:image/png;base64," + funcionarioFotoVO.ImagemBase64;
                    }
                    else
                    {
                        img = @"/View/Img/avatars/male.png";
                    }
                }
                else
                {
                    img = @"/View/Img/avatars/male.png";
                }


            }
            catch (Exception ex)
            {
                //
            }
            finally
            {
                if (funcionarioFotoBE != null)
                    funcionarioFotoBE.FecharConexao();
            }

            return img;
        }


        /// <summary>
        /// EncerrarSessaoAberta
        /// </summary>
        /// <param name="nomeLogin"></param>
        /// <returns></returns>
        [WebMethod]
        public static string EncerrarSessaoAberta(string nomeLogin)
        {
            Ajax ajax = new Ajax();
            string usuario = nomeLogin;
            UsuarioBE usuarioBE = null;
            try
            {
                usuarioBE = new UsuarioBE();
                var AuditoriaBe = new AuditoriaBE(usuarioBE.GetSqlCommand());
                // Consulta o usuário pelo nomeLogin
                var usuarioVo = usuarioBE.Consultar(new UsuarioVO()
                {
                    NomeLogin = usuario
                });

                AuditoriaBe.AtivarDesativarSessao(usuarioVo.Id, false);
                ajax.StatusOperacao = true;
                //retorno.MensagemTexto = "Solicitação de recuperação de senha processada com sucesso! Por favor verifique a caixa postal do e-mail: <strong>" + usuarioEmail + "</strong> para novas instruções.";                
            }
            catch (Exception e)
            {
                // Seta a mensagem de erro
                ajax.TextoMensagem += e.Message;
            }
            finally
            {
                if (usuarioBE != null)
                    usuarioBE.FecharConexao();
            }

            // Retorna os parametros
            return ajax.GetAjaxJson();
        }

        /// <summary>
        /// Validar
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="senha"></param>
        /// <param name="captcha"></param>
        /// <param name="forcarLogin"></param>
        /// <returns></returns>
        [WebMethod]
        public static string Validar(string usuario, string senha, string captcha, bool forcarLogin)
        {
            var ajax = new Ajax();

            LoginBE be = null;

            try
            {
                be = new LoginBE();

                var lst = be.Validar(usuario, senha, captcha, forcarLogin);

                ajax.StatusOperacao = true;

                ajax.Variante = Json.Serialize(lst);
            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;

                ajax.ObjMensagem = ex.Message;
            }
            finally
            {
                if (be != null)
                    be.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }
    }
}