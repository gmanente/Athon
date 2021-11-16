using System;
using System.Web;
using Sistema.Api.dll.Src.Comum.BE;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src;
using System.IO;
using Sistema.Api.dll.Src.Comum.VO;

namespace Sistema.Web.UI.Sistema
{
    /// <summary>
    /// Autor: Evander Costa
    /// Data: 10.10.2014
    /// Descrição: Classe da Página Login
    /// </summary>
    public partial class Login : System.Web.UI.Page
    {
        public string ambiente { get; set; }
        public string status { get; set; }
        public string senhaAlterada { get; set; }
        public int numeroLoginsHabilitarCaptcha { get; set; }
        public string ultimaModificacao { get; set; }
        public string usuarioNomeDebug { get; set; }
        public string usuarioSenhaDebug { get; set; }
        public string quantidadeTentativasLogin { get; set; }


        protected void Page_Load(object sender, EventArgs e)

        {
            AuditoriaBE auditoriaBE = null;

            try
            {
                auditoriaBE = new AuditoriaBE();


                string scheme = Request.Url.Scheme;
                string host = Request.Url.Host;
                string ipCliente = GetIPAddress();


                // --------------- Se o acesso for pelo protocolo http e externo
                if (scheme == "http" && host != "localhost" && Funcoes.IsExternalIp(ipCliente, ""))
                {
                    // Redireciona
                    Response.Redirect("https://" + host + "/Login.aspx", true);
                }


                // --------------- Recupera e Define o status
                status = Request.QueryString["status"];


                // --------------- Recupera e Define o senhaAlterada
                senhaAlterada = Request.QueryString["senhaAlterada"];


                // --------------- Recupera o dominio da aplicação
                string dominioAplicacao = Dominio.GetDominioAplicacao();


                // --------------- Recupera e Define o numeroLoginsHabilitarCaptcha
                numeroLoginsHabilitarCaptcha = Dominio.NumeroLoginsHabilitarCaptcha;


                // --------------- Recupera a quantidade de tentativas de login
                var qLogins = Session["qLogins"];

                quantidadeTentativasLogin = qLogins != null ? qLogins.ToString() : "1";


                // --------------- Recupera a data ultimaModificacao
                ultimaModificacao = File.GetLastWriteTime(Server.MapPath(@"~\Login.aspx")).ToString("dd/MM/yy HH:mm:ss");


                // --------------- Recupera e Define o appState
                string appState = Dominio.AppState.ToString();


                // --------------- Define o ambiente
                if (host == "localhost")
                    ambiente = "Ambiente de Desenvolvimento";
                else if (host == "datanortebackup.ddns.net:9998")
                    ambiente = "Ambiente de Teste";
                else if (host == "datanortebackup.ddns.net:9998")
                    ambiente = "Ambiente de Homologação";
                else
                    ambiente = "";


                // --------------- Define o usuário e senha padrão
                /*if (appState == "Debug" || appState == "Teste")
                {
                    usuarioNomeDebug = Dominio.SuperAdminDesenvolvimento;
                    usuarioSenhaDebug = Dominio.SenhaAdminDesenvolvimento;
                }*/
                

                // --------------- Se eonctrar o Cookie chaveUnica
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


                //HttpCookie aCookie;
                //string cookieName;

                //int cookieCount = Request.Cookies.Count;

                //for (int i = 0; i < cookieCount; i++)
                //{
                //    cookieName = Request.Cookies[i].Name;

                //    if (cookieName != "qLogins")
                //    {
                //        aCookie = new HttpCookie(cookieName);
                //        aCookie.Expires = DateTime.Now.AddDays(-1);
                //        aCookie.Domain = dominioAplicacao;

                //        Response.Cookies.Add(aCookie);
                //    }
                //}


                ////---------------Desabilita o Cache
                //Response.AppendHeader("Cache-Control", "no-cache");
                //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //Response.Cache.SetNoStore();

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
        /// GetIPAddress
        /// </summary>
        /// <returns></returns>
        protected string GetIPAddress()
        {
            var context = HttpContext.Current;

            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');

                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }
            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
    }
}