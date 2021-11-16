using System;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src;
using Sistema.Api.dll.Template.Relatorio.Master;

namespace Sistema.Web.UI.Relatorio.View.MasterPage
{
    public partial class Submodulo : CommonMasterPage
    {
        public RelatorioSubmoduloMasterTemplate RelatorioSubmoduloMasterTemplate { get; set; }

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
                RelatorioSubmoduloMasterTemplate = new RelatorioSubmoduloMasterTemplate(Sessao.IdCampus, GetUrlModulo(), Sessao.IdUsuario, Sessao.AcessoExterno)
                {
                    ImgLinhaTopoIcone = { Src = Dominio.UrlRepositorioRemoto + "View/Img/icones/iconeCAE.png" },
                    ATitle = { Title = "Relatórios", Href = "../Page/UltimosAcessos.aspx", Text = "Relatórios" },
                    InicioTopo = { Style = "background-color:#444;" },
                };
            }
            else
            {
                RelatorioSubmoduloMasterTemplate = new RelatorioSubmoduloMasterTemplate(Dominio.IdCampusDebug, GetUrlModulo(), Dominio.IdUsuarioDebug, false)
                {
                    ImgLinhaTopoIcone = { Src = Dominio.UrlRepositorioLocal + "View/Img/icones/iconeCAE.png" },
                    ATitle = { Title = "Relatórios", Href = "../Page/UltimosAcessos.aspx", Text = "Relatórios" },
                    InicioTopo = { Style = "background-color:#444;" },
                };
            }
        }
    }
}