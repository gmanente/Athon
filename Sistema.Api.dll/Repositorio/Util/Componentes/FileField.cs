using System.Text;
using System.Web;

#pragma warning disable 0108 // variable assigned but not used.
namespace Sistema.Api.dll.Repositorio.Util.Componentes
{
    public class FileField : AbstractComponent
    {
        public string Pasta { get; set; }
        public string ExtensoesPermitidas { get; set; }
        public bool Console { get; set; }
        public long IdCampus { get; set; }
        public bool IsMultiple { get; set; }

        public FileField(string pasta, string extensoespermitidas, bool console = false , long idCampus = 0)
            : base()
        {
            Pasta = pasta;
            ExtensoesPermitidas = extensoespermitidas;
            Console = console;
            IdCampus = idCampus;
        }

        private void SetFileField()
        {
            SbComponent = new StringBuilder();
            SbComponent.AppendLine("<div class='" + ContainerClass + "' id='" + ContainerId + "' style='" + ContainerStyle + "' >");
            SbComponent.AppendLine("<label id='LabelUpload' ");

            if (!string.IsNullOrEmpty(LabelFor))
            {
                SbComponent.Append("for='").Append(LabelFor).AppendLine("'");
            }

            SbComponent.Append(">").Append(LabelText).AppendLine(" [Tamanho máximo: 30 MB]</label>");

            SbComponent.Append("<input type='file' onchange='ajaxUpload(event, this , \"" + Pasta + "\" , \"" + ExtensoesPermitidas + "\" , " + Console.ToString().ToLower() + "," + IdCampus + "); return false;' class='validate" + Id + " " + Class + "' name='" + Name + "' id='" + Id + "' ");
           
            if (IsMultiple)
            {
                SbComponent.Append(" multiple='multiple' ");
            }

            SbComponent.Append(" /> ");

            SbComponent.AppendLine("<br/><div id='box-barra-progresso-" + Id + "' class='progress hide active'>");

            SbComponent.AppendLine("<div id='barra-progresso-" + Id + "' class='progress-bar progress-bar-success' role='progressbar' aria-valuenow='0' aria-valuemin='0' aria-valuemax='100' style='width: 45%'>");

            SbComponent.AppendLine("0%</div>");
            SbComponent.AppendLine("</div>");
            SbComponent.AppendLine("</div>");
            SbComponent.AppendLine("<div id='image-preview-" + Id + "' class='image-preview' style='padding:10px;display:none;'></div>");

            SbComponent.Append("<script type='text/javascript'>jQuery.validator.addClassRules('validate" + Id + "', {" + Validate + "});</script>");
        }

        public override string ToString()
        {
            SetFileField();
            return base.ToString();
        }

        public virtual void Render()
        {
            HttpContext.Current.Response.Write(ToString());
        }
    }
}
