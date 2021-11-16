using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src;
using Sistema.Api.dll.Template.Mensageria.Master;
using System;

namespace Sistema.Web.UI.Mensageria.View.MasterPage
{
    public partial class Submodulo : CommonMasterPage
    {
        public MensageriaSubmoduloMasterTemplate MensageriaSubmoduloMasterTemplate { get; set; }

        //Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            SetTemplateSubmoduloMaster();
        }

        //Set template submódulo master
        public void SetTemplateSubmoduloMaster()
        {

            if (Dominio.AppState != Dominio.ApplicationState.Debug)
            {
                MensageriaSubmoduloMasterTemplate = new MensageriaSubmoduloMasterTemplate(Sessao.IdCampus, GetUrlModulo(), Sessao.IdUsuario, Sessao.AcessoExterno)
                {
                    ImgLinhaTopoIcone = { Src = "http://repositorio.univag.edu.br/View/Img/icones/iconeHelpDesk.png" },
                    ATitle = { Title = "Mensageria", Href = "../Page/UltimosAcessos.aspx", Text = "Mensageria" },
                    InicioTopo = { Style = "background-color:#6f5499;" },
                };

            }
            else
            {
                MensageriaSubmoduloMasterTemplate = new MensageriaSubmoduloMasterTemplate(Dominio.IdCampusDebug, GetUrlModulo(), Dominio.IdUsuarioDebug, false)
                {
                    ImgLinhaTopoIcone = { Src = "http://repositorio.univag.edu.br/View/Img/icones/iconeHelpDesk.png" },
                    ATitle = { Title = "Mensageria", Href = "../Page/UltimosAcessos.aspx", Text = "Mensageria" },
                    InicioTopo = { Style = "background-color:#6f5499;" },
                };
            }
        }
    }
}