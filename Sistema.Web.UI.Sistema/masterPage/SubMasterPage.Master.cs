using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src;
using Sistema.Api.dll.Template.Seguranca.Master;
using System;


namespace Sistema.Web.UI.Sistema.masterPage
{
    public partial class SubMasterPage : CommonMasterPage
    {
        public SegurancaSubmoduloMasterTemplate SegurancaSubmoduloTemplate { get; set; }

        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            SetTemplateSubmoduloMaster();
        }


        public void SetTemplateSubmoduloMaster()
        {
            if (Dominio.AppState != Dominio.ApplicationState.Debug)
            {
                SegurancaSubmoduloTemplate = new SegurancaSubmoduloMasterTemplate(Sessao.IdCampus, GetUrlModulo(), Sessao.IdUsuario, Sessao.AcessoExterno)
                {
                    ImgLinhaTopoIcone = { Src = "/View/Img/icones/iconeAcessoSeguranca.png" },
                    ATitle = { Title = "Segurança", Href = "../Page/UltimosAcessos.aspx", Text = "Segurança" },
                    InicioTopo = { Style = "background-color:#00bac6;" },
                };
            }
            else
            {
                SegurancaSubmoduloTemplate = new SegurancaSubmoduloMasterTemplate(Dominio.IdCampusDebug, GetUrlModulo(), Dominio.IdUsuarioDebug, false)
                {
                    ImgLinhaTopoIcone = { Src = "/View/Img/icones/iconeAcessoSeguranca.png" },
                    ATitle = { Title = "Segurança", Href = "../Page/UltimosAcessos.aspx", Text = "Segurança" },
                    InicioTopo = { Style = "background-color:#00bac6;" }
                };
            }
        }
    }
}