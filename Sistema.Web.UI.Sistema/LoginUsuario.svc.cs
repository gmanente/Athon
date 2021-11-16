using System;
using System.Security.Principal;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.ServiceModel.Web;
using System.Web;
using Sistema.Api.dll.Src.Comum.BE;
using Sistema.Api.dll.Src.Comum.VO;
using Sistema.Api.dll.Src.Repositorio.BE;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using Sistema.Api.dll.Src;
using System.Linq;


namespace Sistema.Web.UI.Sistema
{
    /// <summary>
    /// Autor: Evander Costa
    /// Data: 10.10.2014
    /// Descrição: Classe de Serviços LoginUsuario. Realiza operações de autenticação para o usuário.
    /// </summary>
    #region ILoginUsuario Operacoes
    public class LoginUsuario : CommonPage, ILoginUsuario
    {
        /// <summary>
        /// Autor: Evander Costa
        /// Data: 10.10.2014
        /// Descrição: Método Serviço para realizar a validação do usuário no login.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public string Validar(Stream stream)
        {
            UsuarioBE usuarioBE = null;
            WebOperationContext webOperationContext = WebOperationContext.Current;


            // --------------- Define a autenticacao com os parametros padrão
            var autenticacao = new Autenticacao()
            {
                Resposta = false,
                RespostaErro = "500",
                CaptchaShow = false,
                MensagemTipo = "warning",
                MensagemTexto = "",
                LstUsuarioCampus = new List<UsuarioCampusVO>()
            };

            try
            {
                usuarioBE = new UsuarioBE();
                var usuarioCampusBE = new UsuarioCampusBE(usuarioBE.GetSqlCommand());
                //var auditoriaBE = new AuditoriaBE(usuarioBE.GetSqlCommand());
                var campusBE = new CampusBE(usuarioBE.GetSqlCommand());
                var consultaBE = new ConsultaBE(usuarioBE.GetSqlCommand());


                // --------------- Recupera o dominio da aplicação
                string dominioAplicacao = Dominio.GetDominioAplicacao();


                // --------------- Recupera e Define o appState
                string appState = Dominio.AppState.ToString();


                // --------------- Recupera os dados de autenticação
                string usuario = "";
                string senha = "";
                string captcha = "";
                bool forcarLogin = false;

                try
                {
                    NameValueCollection dados = HttpUtility.ParseQueryString(new StreamReader(stream).ReadToEnd());

                    usuario = dados["usuario"];
                    senha = dados["senha"];
                    captcha = dados["captcha"];
                    forcarLogin = Convert.ToBoolean(dados["forcarLogin"]);
                }
                catch
                {
                    throw new Exception("Os dados da autenticação não foram enviados corretamente.");
                }


                // --------------- Define o httpContext
                var httpContext = HttpContext.Current;


                // --------------- Define o sessionId, serverName, enderecoIp, browserNome, browserVersao e BrowserTipo
                string sessionId = httpContext.Session.SessionID;
                string serverName = httpContext.Request.ServerVariables["SERVER_NAME"];
                string enderecoIp = httpContext.Request.UserHostAddress;
                string browserNome = httpContext.Request.Browser.Browser;
                string browserVersao = httpContext.Request.Browser.Version;
                string browserTipo = httpContext.Request.Browser.Win32 ? "Win32" : "Win64";
                string hostName = WindowsIdentity.GetCurrent().Name.ToString();


                // --------------- Define o StatusCode padrão
                webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.OK;


                // --------------- Recupera e define o lLogins - limite de logins
                int lLogins = Dominio.NumeroLoginsHabilitarCaptcha;


                // --------------- Recupera e define o qLogins - número de tentativas de login
                int qLogins = httpContext.Session["qLogins"] != null ? Convert.ToInt32(httpContext.Session["qLogins"]) : 1;


                // --------------- Se o numero de login atingiu o limite, requere o captcha
                if (qLogins > lLogins)
                {
                    // ---------- Requere o captcha do usuário
                    autenticacao.CaptchaShow = true;


                    // ---------- Se não foi informado o captcha
                    if (string.IsNullOrEmpty(captcha))
                    {
                        autenticacao.RespostaErro = "captcha";
                        autenticacao.MensagemTexto = "A imagem captcha não foi informada.<br>Por favor tente novamente.";

                        return Json.Serialize(autenticacao);
                    }


                    // ---------- Recupera e define o captchaImagem - o texto gerado da imagem captcha
                    string captchaImagem = null;

                    if (httpContext.Session["CaptchaImageText"] != null)
                    {
                        captchaImagem = httpContext.Session["CaptchaImageText"].ToString();
                    }

                    // ---------- Se o captcha não confere
                    if (captcha != captchaImagem)
                    {
                        autenticacao.RespostaErro = "captcha";
                        autenticacao.MensagemTexto = "Os dígitos da imagem captcha não confere.<br>Por favor tente novamente.";

                        // Define a sessão qLogins 
                        httpContext.Session["qLogins"] = qLogins + 1;

                        return Json.Serialize(autenticacao);
                    }
                }
                

                // --------------- Autentica o UsuarioVO
                var usuarioVO = usuarioBE.AutenticarUsuario(usuario);


                // --------------- Se encontrou o UsuarioVO, o AppState for Debug ou Teste 
                // desabilitado em 11/096/2021
                /*if (usuarioVO != null && (appState == "Debug" || appState == "Teste"))
                {
                    // ---------- Se a senha informada for padrão
                    // Define a senha padrão para o usuarioVO
                    if (Criptografia.MD5(senha) == Criptografia.MD5(Dominio.SenhaAdminDesenvolvimento))
                        usuarioVO.UsuarioSenha.Senha = Criptografia.MD5(Dominio.SenhaAdminDesenvolvimento);
                }*/


                //string senhaDigitada = "0x7DBF822728E4F15B3F51E9A64AF76A9A"; //Criptografia.MD5(senha);

                //if (usuarioVO == null || usuarioVO.UsuarioSenha.Senha.Equals(senhaDigitada) == false)

                // --------------- Se Não encontrou o UsuarioVO ou a senha informada Não corresponde ao do usuário
                //if (usuarioVO == null || usuarioVO.UsuarioSenha.Senha.Equals(Criptografia.MD5(senha)) == false)
                if (usuarioVO == null || usuarioVO.UsuarioSenha.Senha.Equals(senha) == false)
                {
                    // ---------- Define a sessão qLogins
                    ++qLogins;
                    httpContext.Session["qLogins"] = qLogins;


                    // ---------- Requere o captcha do usuário
                    autenticacao.CaptchaShow = qLogins > lLogins ? true : false;


                    // ---------- Define a autenticacao
                    autenticacao.RespostaErro = "login";
                    autenticacao.MensagemTexto = "O Usuário/senha são inválidos. Verifique!";

                    return Json.Serialize(autenticacao);
                }
                else
                {
                    // -------------- Zera a sessão qLogins - contador de tentativas de login
                    httpContext.Session["qLogins"] = 1;
                }


                // --------------- Se a senha do usuário está expirada
                if (usuarioVO.UsuarioSenha.DataTermino < DateTime.Now)
                {
                    // ---------- Define a autenticacao
                    autenticacao.RespostaErro = "expiracao";
                    autenticacao.MensagemTexto = "Sua senha expirou. Por favor acesse a opção <strong>Recuperar senha</strong>:";

                    return Json.Serialize(autenticacao);
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
                    // ---------- Define a autenticacao
                    autenticacao.RespostaErro = "login";
                    autenticacao.MensagemTexto = "O usuário não tem a unidade ativa para acesso.";

                    return Json.Serialize(autenticacao);
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
                    // ---------- Define a autenticacao
                    autenticacao.RespostaErro = "login";
                    autenticacao.MensagemTexto = "O usuário não possui a unidade com permissão para acesso externo.";

                    return Json.Serialize(autenticacao);
                }


                // --------------- Define o UsuarioCampusVO
                // o primeiro Campus que encontrar para não atrapalhar
                var usuarioCampusVO = lstUsuarioCampusVO[0];


                // --------------- Define a chaveUnica - chave única de acesso ao sistema
                var chaveUnica = Guid.NewGuid();


                // --------------- Recupera o chaveUnicaCookie
                var chaveUnicaCookie = httpContext.Request.Cookies["chaveUnica"];


                // --------------- Se o chaveUnicaCookie for deferente de nulo
                if (chaveUnicaCookie != null)
                {
                    // ---------- Recupera a chaveUnica do valor do cookie
                    chaveUnica = Guid.Parse(chaveUnicaCookie.Value);
                }


                // --------------- Consulta o AuditoriaVO
                /*var auditoriaConsultaVO = auditoriaBE.Consultar(new AuditoriaVO() { IdUsuario = usuarioVO.Id });


                // --------------- Define o AuditoriaVO
                var auditoriaVO = new AuditoriaVO()
                {
                    DataLogin = DateTime.Now,
                    Login = usuario,
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
                    ChaveUnica = chaveUnica
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
                    bool unicaSessao = true; //Dominio.UnicaSessao;
                    bool sessaoAtiva = auditoriaConsultaVO.SessaoAtiva;

                    bool sessaoValida = auditoriaBE.SessaoValida(auditoriaConsultaVO.DataWho);

                    bool chavesDiferentes = auditoriaConsultaVO.ChaveUnica == null ? false : 
                            (auditoriaConsultaVO.ChaveUnica.ToString() == "00000000-0000-0000-0000-000000000000" ? false :
                            (auditoriaConsultaVO.ChaveUnica.ToString() == chaveUnica.ToString() ? false : true)
                         );


                    // ---------- Se a sessão esta ativa na auditoriaConsultaVO, na validade e as chaves únicas são diferentes
                    if (unicaSessao && sessaoAtiva && sessaoValida && chavesDiferentes && !forcarLogin)
                    {
                        // ---------- Define a autenticacao
                        autenticacao.RespostaErro = "sessao-aberta";
                        autenticacao.MensagemTexto = Json.Serialize(new string[] {
                            "Você possui uma sessão aberta",
                            auditoriaVO.EnderecoIp,
                            auditoriaVO.BrowserNome,
                            auditoriaVO.ServerName
                        });

                        return Json.Serialize(autenticacao);
                    }

                    auditoriaVO.Id = auditoriaConsultaVO.Id;
                }

                
                // --------------- Audita o AuditoriaVO
                auditoriaBE.Auditar(auditoriaVO);
                */

                // --------------- Recupera e define o idProfessor
                //long idProfessor = consultaBE.GetIdProfessor(usuarioVO.Id);


                // --------------- Se o chaveUnicaCookie for nulo
                if (chaveUnicaCookie == null)
                {
                    // ---------- Define o Cookie chaveUnica
                    chaveUnicaCookie = new HttpCookie("chaveUnica");

                    chaveUnicaCookie.Value = chaveUnica.ToString();
                    chaveUnicaCookie.Expires = DateTime.Now.AddDays(1d); // 1 dia
                    chaveUnicaCookie.Domain = appState == "Debug" ? "" : dominioAplicacao;
                    httpContext.Response.Cookies.Add(chaveUnicaCookie);
                }


                // --------------- Se login foi realizado com a senha Padrão do sistema
                // Define o trocarSenhaPadrao - força a mudança de senha pelo usuário após o login
                bool trocarSenhaPadrao = (senha == Dominio.SenhaPadrao) ? true :false;


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
                    IdAuditoria = 1,
                    AcessoExterno = acessoExterno,
                    IdsCampus = idsCampus,
                    //IdProfessor = idProfessor,
                    HostName = hostName,
                    Portal = false,
                    TrocarSenhaPadrao = trocarSenhaPadrao
                };


                // --------------- Define a SessaoSistema
                httpContext.Session["SessaoSistema"] = sessaoSistema;


                // ---------- Define o StatusCode
                webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.Created;


                // ---------- Define a autenticacao
                autenticacao.Resposta = true;
                autenticacao.RespostaErro = null;
                autenticacao.MensagemTipo = "success";
                autenticacao.LstUsuarioCampus = lstUsuarioCampusVO;
                autenticacao.MensagemTexto = "Autenticação realizada com sucesso!<br><br><strong>Selecione a empresa para acesso ao sistema</strong>:";

            }
            catch (Exception ex)
            {
                // ---------- Define o StatusCode
                webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.InternalServerError;


                // ---------- Define a autenticacao
                autenticacao.MensagemTipo = "danger";
                autenticacao.MensagemTexto = "Erro interno do servidor.<br>" + ex.Message;
            }
            finally
            {
                if (usuarioBE != null)
                    usuarioBE.FecharConexao();
            }


            return Json.Serialize(autenticacao);
        }


        /// <summary>
        /// Autor: Evander Costa
        /// Data: 01/08/2018
        /// Descrição: Método Serviço para realizar entrada do usuário no sistema.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public string Entrar(Stream stream)
        {
            UsuarioCampusBE usuarioCampusBE = null;
            WebOperationContext webOperationContext = WebOperationContext.Current;


            // --------------- Define a autenticacao com os parametros padrão
            var autenticacao = new Autenticacao()
            {
                Resposta = false,
                RespostaErro = "500",
                CaptchaShow = false,
                MensagemTipo = "warning",
                MensagemTexto = "",
                LstUsuarioCampus = new List<UsuarioCampusVO>()
            };

            try
            {
                usuarioCampusBE = new UsuarioCampusBE();


                // --------------- Checa a sessão SessaoSistema
                if (Session["SessaoSistema"] == null)
                {
                    // ---------- Define a autenticacao
                    autenticacao.RespostaErro = "sessao";
                    autenticacao.MensagemTexto = "A sessão do usuário foi expirada! Por favor refaça o login.";

                    return Json.Serialize(autenticacao);
                }


                // ---------- Recupera a sessaoSistema
                var sessaoSistema = (SessaoSistema)Session["SessaoSistema"];
                long idUsuario = sessaoSistema.IdUsuario;
                bool acessoExterno = sessaoSistema.AcessoExterno;

                // --------------- Recupera o campus
                long campus = 0;

                try
                {
                    NameValueCollection dados = HttpUtility.ParseQueryString(new StreamReader(stream).ReadToEnd());

                    campus = Convert.ToInt64(dados["campus"]);
                }
                catch
                {
                    throw new Exception("O Campus/Polo não foi informado corretamente.");
                }                


                // --------------- Consulta os UsuarioCampusVO
                var usuarioCampusVO = usuarioCampusBE.Consultar(new UsuarioCampusVO()
                {
                    Id = campus,
                    Usuario = { Id = idUsuario },
                    Ativar = true
                });


                // --------------- Se Não encontrar o UsuarioCampusVO
                if (usuarioCampusVO == null)
                {
                    // ---------- Define a autenticacao
                    autenticacao.RespostaErro = "campus";
                    autenticacao.MensagemTexto = "O usuário não tem acesso ao Campus/Polo informado.";

                    return Json.Serialize(autenticacao);
                }


                // --------------- Se o acesso for externo e o usuário Não possui Campus/Polo com acesso de fora
                if (acessoExterno == true && usuarioCampusVO.AcessoExterno == false)
                {
                    // ---------- Define a autenticacao
                    autenticacao.RespostaErro = "login";
                    autenticacao.MensagemTexto = "O usuário não possui permissão de acesso externo para o Campus/Polo informado.";

                    return Json.Serialize(autenticacao);
                }


                // --------------- Atualiza a Sessão sessaoSistema com os dados do Campus
                sessaoSistema.IdUsuarioCampus = usuarioCampusVO.Id;
                sessaoSistema.IdCampus = usuarioCampusVO.Campus.Id;
                sessaoSistema.NomeCampus = usuarioCampusVO.Campus.Nome;


                // --------------- Cria a sessão Cookie Session
                var sessionHandler = new SessionHandler()
                {
                    Objeto = sessaoSistema
                };

                sessionHandler.NewSession("Session");


                // ---------- Define o StatusCode
                webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.Created;


                // ---------- Define a autenticacao
                autenticacao.Resposta = true;
                autenticacao.RespostaErro = null;
                autenticacao.MensagemTipo = "success";

            }
            catch (Exception ex)
            {
                // ---------- Define o StatusCode
                webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.InternalServerError;


                // ---------- Define a autenticacao
                autenticacao.MensagemTipo = "danger";
                autenticacao.MensagemTexto = "Erro interno do servidor.<br>" + ex.Message;
            }
            finally
            {
                if (usuarioCampusBE != null)
                    usuarioCampusBE.FecharConexao();
            }


            return Json.Serialize(autenticacao);
        }


        /// <summary>
        /// Autor: Evander Costa
        /// Data: 10.10.2014
        /// Descrição: Método Serviço para recuperar a senha do usuário.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns> 
        public Autenticacao RecuperarSenha(Stream s)
        {
            NameValueCollection campo = HttpUtility.ParseQueryString(new StreamReader(s).ReadToEnd());

            string usuario = campo["usuario"];
            string captcha = campo["captcha"];

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
                }
            }
            catch (Exception e)
            {
                // Seta a mensagem de erro
                retorno.MensagemTexto += e.Message;
            }
            finally
            {
                if (usuarioBE != null)
                    usuarioBE.FecharConexao();
            }

            // Retorna os parametros
            return retorno;
        }

    }
    #endregion


    /// <summary>
    /// Autor: Evander Costa
    /// Data: 10.10.2014
    /// Descrição: Classe Model Autenticacao
    /// </summary>
    public class Autenticacao : AbstractVO
    {
        public bool Resposta { get; set; }
        public string RespostaErro { get; set; }
        public string MensagemTipo { get; set; }
        public string MensagemTexto { get; set; }
        public bool CaptchaShow { get; set; }

        private List<UsuarioCampusVO> lstUsuarioCampus;

        public List<UsuarioCampusVO> LstUsuarioCampus
        {
            get
            {
                if (lstUsuarioCampus == null && IsInstantiable())
                    lstUsuarioCampus = new List<UsuarioCampusVO>();

                return lstUsuarioCampus;
            }
            set
            {
                lstUsuarioCampus = value;
            }
        }

    }


    public class MenuCampus : AbstractVO
    {
        public bool Resposta { get; set; }
        public string RespostaErro { get; set; }
        public string MensagemTipo { get; set; }
        public string MensagemTexto { get; set; }
        public bool CaptchaShow { get; set; }

        private List<UsuarioCampusVO> lstUsuarioCampus;

        public List<UsuarioCampusVO> LstUsuarioCampus
        {
            get
            {
                if (lstUsuarioCampus == null && IsInstantiable())
                    lstUsuarioCampus = new List<UsuarioCampusVO>();

                return lstUsuarioCampus;
            }
            set
            {
                lstUsuarioCampus = value;
            }
        }

    }


}
