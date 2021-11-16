using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src;
using Sistema.Api.dll.Template.Datanorte.Master;
using System;

namespace Sistema.Web.UI.Datanorte.View.masterPage
{
    public partial class SubModulo : CommonMasterPage
    {
        public DatanorteMasterTemplate MasterTemplate { get; set; }

        //Page_Load
        protected new virtual void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            SetTemplateSubmoduloMaster();
        }

        //SetTemplateSubmoduloMaster
        public void SetTemplateSubmoduloMaster()
        {
            string urlModulo = GetUrlModulo();

            if (Dominio.AppState != Dominio.ApplicationState.Debug)
            {
                MasterTemplate = new DatanorteMasterTemplate(Sessao.IdCampus, urlModulo, Sessao.IdUsuario, Sessao.AcessoExterno)
                {
                    ImgLinhaTopoIcone = { Src = "/View/Img/icones/iconePortal.png" },
                    ATitle = { Title = "Datanorte", Href = "../Page/UltimosAcessos.aspx", Text = "Datanorte" },
                    InicioTopo = { Style = "background-color:#527a7a;" },
                };
            }
            else
            {
                MasterTemplate = new DatanorteMasterTemplate(Dominio.IdCampusDebug, urlModulo, Dominio.IdUsuarioDebug, false)
                {
                    ImgLinhaTopoIcone = { Src = "/View/Img/icones/iconePortal.png" },
                    ATitle = { Title = "Datanorte", Href = "../Page/UltimosAcessos.aspx", Text = "Datanorte" },
                    InicioTopo = { Style = "background-color:#527a7a;" },
                };
            }
        }

    }
}