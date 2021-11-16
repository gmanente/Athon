using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src;
using Sistema.Web.UI.Dashboard.Util.Template;
using System;

namespace Sistema.Web.UI.Dashboard.View.MasterPage
{
    public partial class Submodulo : CommonMasterPage
    {
        public DashboardSubmoduloMasterTemplate BibliotecaSubmoduloMasterTemplate { get; set; }

        //Page_Load
        protected new virtual void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            SetTemplateSubmoduloMaster();
        }

        //Set template submódulo master
        public void SetTemplateSubmoduloMaster()
        {
            if (Dominio.AppState != Dominio.ApplicationState.Debug)
            {
                BibliotecaSubmoduloMasterTemplate = new DashboardSubmoduloMasterTemplate(Sessao.IdCampus, GetUrlModulo(), Sessao.IdUsuario, Sessao.AcessoExterno)
                {
                    ImgLinhaTopoIcone = { Src= "/View/Img/icones/IconePrincipal.png" },
                    ATitle = { Title = "Dashboard", Href = "../Page/UltimosAcessos.aspx", Text = "Dashboard" }              
                };
            }
            else
            {
                BibliotecaSubmoduloMasterTemplate = new DashboardSubmoduloMasterTemplate(Dominio.IdCampusDebug, GetUrlModulo(), Dominio.IdUsuarioDebug, false)
                {
                    ImgLinhaTopoIcone = { Src = "/View/Img/icones/IconePrincipal.png" },
                    ATitle = { Title = "Dashboard", Href = "../Page/UltimosAcessos.aspx", Text = "Dashboard" }

                };
            }
        }

    }
}