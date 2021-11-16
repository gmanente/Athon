#pragma warning disable 0108 // variable assigned but not used.
using System.Text;

namespace Sistema.Api.dll.Repositorio.Util.Componentes
{
    public class Btn : AbstractComponent
    {
        public string Tag { get; set; }
        public string BtnType { get; set; }
        public string BtnUrl { get; set; }
        public string Layout { get; set; }
        public string Size { get; set; }
        public string Icon { get; set; }
        public string Text { get; set; }
        public string Target { get; set; }
        public string AjaxCall { get; set; }

        public Btn() 
            : base()
        {
            Target = "_self";
        }

        private void SetBtn()
        {
            SbComponent = new StringBuilder();

            SbComponent.Append("<" + Tag).AppendLine(Tag.ToLower() == "a" ? " target='" + Target + "' href='" + BtnUrl + "'" : " type='" + BtnType + "'");
            if (!string.IsNullOrEmpty(Class) || !string.IsNullOrEmpty(Validate) || !string.IsNullOrEmpty(Layout) || !string.IsNullOrEmpty(Size))
            {
                SbComponent.Append("class='")
                           .Append(string.IsNullOrEmpty(Layout) ? "" : Layout + "")
                           .Append(string.IsNullOrEmpty(Class) ? "" : " " + Class)
                           .Append(string.IsNullOrEmpty(Validate) ? "" : " " + Validate)
                           .Append(Size)
                           .AppendLine("' ");
            }
            if (!string.IsNullOrEmpty(InjectDataAttr))
            {
                SbComponent.AppendLine(InjectDataAttr);
            }
            if (!string.IsNullOrEmpty(Style))
            {
                SbComponent.Append("style='").Append(Style).AppendLine("' ");
            }
            if (Disabled)
            {
                SbComponent.Append("disabled='disabled' ");
            }
            if (Readonly)
            {
                SbComponent.Append("readonly='readonly' ");
            }
            if (!string.IsNullOrEmpty(Onclick))
            {
                SbComponent.Append("onclick=\"" + Onclick + "\" ");
            }
            if (!string.IsNullOrEmpty(Name))
            {
                SbComponent.Append("name='").Append(Name).AppendLine("' ");
            }
            if (!string.IsNullOrEmpty(DataToggle))
            {
                SbComponent.Append("data-toggle='").Append(DataToggle).AppendLine("' ");
            }
            if (!string.IsNullOrEmpty(DataTarget))
            {
                SbComponent.Append("data-target='").Append(DataTarget).AppendLine("' ");
            }
            if (!string.IsNullOrEmpty(Id))
            {
                SbComponent.Append("id='").Append(Id).AppendLine("' ");
            }
            SbComponent.AppendLine(">");
            SbComponent.Append("<span class='").Append("fa fa-").Append(Icon).AppendLine("'></span>");
            SbComponent.AppendLine(Text);
            SbComponent.AppendLine("</" + Tag + ">");

            if (!string.IsNullOrEmpty(AjaxCall))
            {
                SbComponent.Append(AjaxCall);
            }
        }

        public override string ToString()
        {
            SetBtn();
            return base.ToString();
        }

        public virtual void Render()
        {
            SetBtn();
            base.Render();
        }


    }


    public static class Tag
    {
        public static string Button { get { return "button"; } }
        public static string Link { get { return "a"; } }
    }


    public static class Size
    {
        public static string Largo { get { return "btn-lg"; } }
        public static string Pequeno { get { return "btn-sm"; } }
        public static string ExtraPequeno { get { return "btn-xs"; } }
    }


    public static class Layout
    {
        public static string Primario { get { return "btn btn-primary"; } }
        public static string Sucesso { get { return "btn btn-success"; } }
        public static string Informacao { get { return "btn btn-info"; } }
        public static string Alerta { get { return "btn btn-warning"; } }
        public static string Perigo { get { return "btn btn-danger"; } }
        public static string Padrao { get { return "btn btn-default"; } }
        public static string Link { get { return "btn btn-link"; } }
    }
}

