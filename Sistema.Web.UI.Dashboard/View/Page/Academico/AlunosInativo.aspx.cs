using Sistema.Web.RepositorioWebAPI.Attributes;
using Sistema.Web.RepositorioWebAPI.Tools;
using static Sistema.Web.RepositorioWebAPI.Tools.AngularTools;

namespace Sistema.Web.UI.Dashboard.View.Page.Academico
{
    [ConfigurePage(Modules.DashBoard)]
    [InitComponents(Modules.DashBoard,
      MyComponents.AngularFilter,
      MyComponents.FabForm,
      MyComponents.SmartTable,
      MyComponents.JQueryAndBootstrap,
      MyComponents.SweetAlert,
      MyComponents.AngularCookie,
      MyComponents.AngularBootstrapSwitch,
      MyComponents.TcCharts,
      MyComponents.AngularFullScreen,
      MyComponents.AngularStrap
   )]
    public partial class AlunosInativo : AbstractPage
    {

    }
}