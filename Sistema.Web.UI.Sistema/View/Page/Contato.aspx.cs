using Sistema.Web.RepositorioWebAPI.Attributes;
using Sistema.Web.RepositorioWebAPI.Tools;
using static Sistema.Web.RepositorioWebAPI.Tools.AngularTools;


namespace Sistema.Web.UI.Sistema.View.Page
{
    [ConfigurePage(Modules.Sistema)]
    [InitComponents(
        Modules.Sistema,
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