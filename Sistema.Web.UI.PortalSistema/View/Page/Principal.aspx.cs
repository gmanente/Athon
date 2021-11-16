using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistema.Api.dll.Src.Comum.BE;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Web.UI.PortalProfessor.Util;
using Sistema.Web.UI.PortalProfessor.View.MasterPage;

namespace Sistema.Web.UI.PortalProfessor.View.Page
{
    public partial class Principal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string VerificarSessaoNewTab()
        {
            Ajax ajax = new Ajax();
            AuditoriaBE auditoriaBe = null;
            try
            {
                auditoriaBe = new AuditoriaBE();
                if (auditoriaBe.SessaoAtiva(ProfessorMaster.GetSession().IdUsuario, HttpContext.Current.Request.UserHostAddress))
                {
                    ajax.StatusOperacao = true;
                }
                else
                {
                    ajax.StatusOperacao = false;
                    ajax.Variante = "/View/Page/Login.aspx?status=sessao-expirada";
                }
            }
            catch (Exception e)
            {
                ajax.StatusOperacao = false;
                ajax.Variante = "/View/Page/Login.aspx?status=sessao-expirada";
                ajax.TextoMensagem += e.Message;
            }
            finally
            {
                if (auditoriaBe != null)
                    auditoriaBe.FecharConexao();
            }

            // Retorna os parametros
            return ajax.GetAjaxJson();
        }

        [WebMethod]
        public static string RecarregarPortal()
        {
            Ajax ajax = new Ajax();
            try
            {
                var sessao = ProfessorMaster.GetSession();

                if (sessao.IdModuloLogado == 18)
                {
                    sessao.IdModuloLogado = 27;
                }
                else
                {
                    sessao.IdModuloLogado = 18;
                }

                var sessaoSistema = new SessionPortalProfessor();

                sessaoSistema = sessao;
                //{
                //    IdUsuario = sessao.IdUsuario,
                //    NomeUsuario = sessao.NomeUsuario,
                //    EmailUsuario = sessao.EmailUsuario,
                //    IdAuditoria = sessao.IdAuditoria,
                //    AcessoExterno = sessao.AcessoExterno,
                //    IdsCampus =sessao.IdsCampus,
                //    IdProfessor = sessao.IdProfessor,
                //    HostName = sessao.HostName,
                //    IdModuloLogado = sessao.IdModuloLogado,
                //    IdModuloNormal = ,
                //    IdModuloMedicina = idModuloMedicina
                //};

                var s = new SessionHandler()
                {
                    Objeto = sessaoSistema,
                    Portal = true
                };

                // Cria a sessão Session
                s.NewSession("SessionPortalProfessor", Global.SessionCookieTimeout);


                ajax.StatusOperacao = true;

            }
            catch (Exception)
            {

                ajax.StatusOperacao = false;
            }
            return ajax.GetAjaxJson();
        }
    }
}