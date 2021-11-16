using Sistema.Web.RepositorioWebAPI.Attributes;
using Sistema.Web.RepositorioWebAPI.Tools;
using static Sistema.Web.RepositorioWebAPI.Tools.AngularTools;

namespace Sistema.Web.UI.Datanorte.View.Page
{
    [ConfigurePage(Modules.Datanorte)]
    [InitComponents(
        Modules.Datanorte,
        MyComponents.FabForm,
        MyComponents.AngularCookie,
        MyComponents.AngularStrap,
        MyComponents.SmartTable,
        MyComponents.SweetAlert,
        MyComponents.UiSelect,
        MyComponents.JQueryAndBootstrap,
        MyComponents.JQueryMaskedInput,
        MyComponents.Moment,
        MyComponents.MomentWithLocales,
        MyComponents.LocalePtBr,
        MyComponents.Trim,
        "CommonFactory"
        )]

    public partial class Contato : AbstractPage
    {

    }
}