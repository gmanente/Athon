using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src;
using Sistema.Api.dll.Template.Sistema.Master;
using System;

namespace Sistema.Web.UI.Sistema.View.Master
{
    public partial class SubModuloWebAPI : CommonMasterPage
    {
        public SistemaMasterTemplate SistemaSubmoduloMasterTemplate { get; set; }

        protected new virtual void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            SetTemplateSubmoduloMaster();
        }

        public void SetTemplateSubmoduloMaster()
        {
            string urlModulo = GetUrlModulo();

            if (Dominio.AppState != Dominio.ApplicationState.Debug)
            {
                SistemaSubmoduloMasterTemplate = new SistemaMasterTemplate(Sessao.IdCampus, urlModulo, Sessao.IdUsuario, Sessao.AcessoExterno)
                {
                    ImgLinhaTopoIcone = { Src = "/View/Img/icones/iconeSistema.png" },
                    ATitle = { Title = "Gestão de Serviços", Href = "../Page/UltimosAcessos.aspx", Text = "Datanorte" },
                    InicioTopo = { Style = "background-color:#527a7a;" },
                };
            }
            else
            {
                SistemaSubmoduloMasterTemplate = new SistemaMasterTemplate(Dominio.IdCampusDebug, urlModulo, Dominio.IdUsuarioDebug, false)
                {
                    ImgLinhaTopoIcone = { Src = "/View/Img/icones/iconeSistema.png" },
                    ATitle = { Title = "Gestão de Serviços", Href = "../Page/UltimosAcessos.aspx", Text = "Datanorte" },
                    InicioTopo = { Style = "background-color:#527a7a;" },
                };
            }
        }
    }
}