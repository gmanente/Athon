using Sistema.Api.dll.Src.Comum.BE;
using Sistema.Api.dll.Repositorio.Util;
using System;
using Sistema.Api.dll.Src;
using System.IO;
using Sistema.Api.dll.Src.Comum.VO;
using System.Web.Services;
using Sistema.Api.dll.Src.Seguranca.BE;

namespace Sistema.Web.UI.Sistema
{
    public partial class LoginSistema : System.Web.UI.Page
    {
        public string Ambiente { get; set; }
        public string Status { get; set; }
        public int NumeroLoginsHabilitarCaptcha { get; set; }
        public string UltimaModificacao { get; set; }
        public string UsuarioNomeDebug { get; set; }
        public string UsuarioSenhaDebug { get; set; }
        public string QuantidadeTentativasLogin { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            AuditoriaBE auditoriaBE = null;

            try
            {
                auditoriaBE = new AuditoriaBE();


                string scheme = Request.Url.Scheme;
                string host = Request.Url.Host;
                string path = Request.Url.AbsolutePath;
                string ipCliente = GetIPAddress();


                // --------------- Se o acesso for pelo protocolo http e externo
                if (scheme == "http" && host != "localhost" && Funcoes.IsExternalIp(ipCliente, ""))
                {
                    // Redireciona
                    Response.Redirect("https://" + host + path, true);
                }


                // --------------- Recupera e Define o status
                Status = Request.QueryString["status"];


                // --------------- Recupera e Define o numeroLoginsHabilitarCaptcha
                NumeroLoginsHabilitarCaptcha = Dominio.NumeroLoginsHabilitarCaptcha;


                // --------------- Recupera a quantidade de tentativas de login
                var qLogins = Session["qLogins"];

                QuantidadeTentativasLogin = qLogins != null ? qLogins.ToString() : "1";


                // --------------- Recupera a data ultimaModificacao
                UltimaModificacao = File.GetLastWriteTime(Server.MapPath(@"~\LoginSistema.aspx")).ToString("dd/MM/yy HH:mm:ss");


                // --------------- Recupera e Define o appState
                string appState = Dominio.AppState.ToString();


                // --------------- Define o Ambiente
                if (appState == "Debug")
                    Ambiente = "Ambiente de Desenvolvimento";
                else if (appState == "Teste")
                    Ambiente = "Ambiente de Teste";
                else if (host == "Homologacao")
                    Ambiente = "Ambiente de Homologação";


                // --------------- Define o usuário e senha padrão
                /* desabilitado em 11/096/2021
                if (appState == "Debug" || appState == "Teste")
                {
                    UsuarioNomeDebug = Dominio.SuperAdminDesenvolvimento;
                    UsuarioSenhaDebug = Dominio.SenhaAdminDesenvolvimento;
                }
                */

                // --------------- Se encontrar o Cookie chaveUnica
                if (Request.Cookies["chaveUnica"] != null)
                {
                    var chaveUnica = new Guid(Request.Cookies["chaveUnica"].Value);


                    // ---------- Consulta o AuditoriaVO
                    var auditoriaConsultaVO = auditoriaBE.Consultar(new AuditoriaVO() { ChaveUnica = chaveUnica });


                    if (auditoriaConsultaVO != null)
                    {
                        // ---------- Verifica se a sessão é válida 
                        bool sessaoValida = auditoriaBE.SessaoValida(auditoriaConsultaVO.DataWho);


                        // ---------- Desativa a sessão de sistema na Auditoria
                        if (sessaoValida)
                        {
                            auditoriaBE.AtivarDesativarSessao(auditoriaConsultaVO.IdUsuario, false);
                        }
                    }


                    // --------------- Remove o Cookie chaveUnica
                    Response.Cookies["chaveUnica"].Expires = DateTime.Now.AddDays(-1);
                }


                // --------------- Remove o Cookie CT
                if (Request.Cookies["CT"] != null)
                {
                    Response.Cookies["CT"].Expires = DateTime.Now.AddDays(-1);
                }


                // --------------- Remove a sessão SessaoSistema
                Session["SessaoSistema"] = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (auditoriaBE != null)
                    auditoriaBE.FecharConexao();
            }
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


        /// <summary>
        /// Entrar
        /// </summary>
        /// <param name="idCampus"></param>
        /// <returns></returns>
        [WebMethod]
        public static string Entrar(long idCampus)
        {
            var ajax = new Ajax();

            LoginBE be = null;

            try
            {
                be = new LoginBE();

                be.Entrar(idCampus);

                ajax.StatusOperacao = true;
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


        /// <summary>
        /// RecuperarSenha
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="captcha"></param>
        /// <returns></returns>
        [WebMethod]
        public static string RecuperarSenha(string usuario, string captcha)
        {
            var ajax = new Ajax();

            LoginBE be = null;

            try
            {
                be = new LoginBE();

                var emailUsuario = be.RecuperarSenha(usuario, captcha);

                ajax.StatusOperacao = true;

                ajax.Variante = emailUsuario;
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
    }
}