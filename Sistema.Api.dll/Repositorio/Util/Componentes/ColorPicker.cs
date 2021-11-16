using System.Text;

#pragma warning disable 0108 // variable assigned but not used.
namespace Sistema.Api.dll.Repositorio.Util.Componentes
{
    public class ColorPicker : AbstractComponentText
    {
        public ColorPicker() 
            : base()
        {

        }

        private void SetColorPicker()
        {

            SbComponent = new StringBuilder();
            SbComponent.AppendLine("<div class='" + ContainerClass + " w2'>");
            SbComponent.Append("<label>"+LabelText+"</label>");

            SbComponent.Append("<div class='input-group "+Id+"'>");
            SbComponent.Append("<input readonly='readonly' id='" + Id + "' name='" + Name + "' type='text' value='"+Value+"' class='validate"+Id+" " + Class + "'");
            SbComponent.Append(" />");
            SbComponent.Append("<span class='input-group-addon'><i></i></span>");
            SbComponent.Append("</div><br/>");
          
            SbComponent.Append("<script type='text/javascript'>jQuery.validator.addClassRules('validate" + Id + "', {" + Validate + "});</script>");
            SbComponent.Append("</div>");
        }

        private void SetScript()
        {
            SbComponent.AppendLine("<script type='text/javascript'>");
            SbComponent.AppendLine("$(document).ready(function () {");
            SbComponent.AppendLine(" $('."+Id+"').colorpicker(); ");
            SbComponent.AppendLine("});");
            SbComponent.AppendLine("</script>");
        }

        public override string ToString()
        {
            SetColorPicker();
            SetScript();
            return base.ToString();
        }

        public virtual void Render()
        {
            SetColorPicker();
            SetScript();
            base.Render();
        }

    }
}
