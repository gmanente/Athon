using System.Text;

namespace Sistema.Api.dll.Repositorio.Util.Componentes
{
    public class AbstractCheckRadio : AbstractComponetInput
    {
       
        public bool IsInLine { get; set; }
        public bool Checked { get; set; }
        public string Text { get; set; }

        protected AbstractCheckRadio()
        {
        }
        protected AbstractCheckRadio(string[] paramters)
            : base(paramters)
        {
        }
        protected void SetRadioOrCheck(string tipo)
        {
            SbComponent = new StringBuilder();
            SbComponent.Append("<div class='").Append(tipo).AppendLine("'>");
            SbComponent.AppendLine("<label ");
            if (!string.IsNullOrEmpty(LabelFor))
            {
                SbComponent.Append("for='").Append(LabelFor).AppendLine("' ");
            }
            SbComponent.AppendLine(">");
            SbComponent.Append("<input type='").Append(tipo).AppendLine("'");

            SbComponent.Append("class='" + Class + " validate" + Id + "'");

            if (!string.IsNullOrEmpty(InjectDataAttr))
            {
                SbComponent.AppendLine(InjectDataAttr);
            }
            if (!string.IsNullOrEmpty(Style))
            {
                SbComponent.Append("style='").Append(Style).AppendLine("' ");
            }
            if (!string.IsNullOrEmpty(Name))
            {
                SbComponent.Append("name='").Append(Name).AppendLine("' ");
            }
            if (!string.IsNullOrEmpty(Id))
            {
                SbComponent.Append("id='").Append(Id).AppendLine("' ");
            }
            if (Disabled)
            {
                SbComponent.Append("disabled='disabled'");
            }
            if (Readonly)
            {
                SbComponent.Append("onclick='return false'");
            }
            if (!string.IsNullOrEmpty(Value))
            {
                SbComponent.Append("value='").Append(Value).AppendLine("' ");
            }
            if (Checked)
            {
                SbComponent.Append("checked='checked'");
            }
            SbComponent.AppendLine("/>");
            if (!string.IsNullOrEmpty(Text))
            {
                SbComponent.AppendLine("<span>").Append(Text).Append("</span>");
            }
            SbComponent.AppendLine("</label>");

            SbComponent.AppendLine("</div>");
            SbComponent.Append("<script type='text/javascript'>jQuery.validator.addClassRules('validate" + Id + "', {" + Validate + "});</script>");
        }

        protected void SetRadioOrCheckInline(string tipo)
        {
            SbComponent = new StringBuilder();
            SbComponent.Append("<label class='").Append(tipo).AppendLine("-inline'");
            if (!string.IsNullOrEmpty(LabelFor))
            {
                SbComponent.Append("for='").Append(LabelFor).AppendLine("' ");
            }
            SbComponent.Append(">");

            SbComponent.Append("<input type='").Append(tipo).AppendLine("'");

            SbComponent.Append("class='" + Class + " validate" + Id + "'");

            if (!string.IsNullOrEmpty(InjectDataAttr))
            {
                SbComponent.AppendLine(InjectDataAttr);
            }
            if (Disabled)
            {
                SbComponent.Append("disabled='disabled'");
            }
            if (Readonly)
            {
                SbComponent.Append("disabled='disabled'");
            }
            if (!string.IsNullOrEmpty(Style))
            {
                SbComponent.Append("style='").Append(Style).AppendLine("' ");
            }
            if (!string.IsNullOrEmpty(Name))
            {
                SbComponent.Append("name='").Append(Name).AppendLine("' ");
            }
            if (!string.IsNullOrEmpty(Id))
            {
                SbComponent.Append("id='").Append(Id).AppendLine("' ");
            }
            if (Checked)
            {
                SbComponent.Append("checked='checked'");
            }
            if (!string.IsNullOrEmpty(Value))
            {
                SbComponent.Append("value='").Append(Value).AppendLine("' ");
            }
            SbComponent.AppendLine("/>");
            if (!string.IsNullOrEmpty(Text))
            {
                SbComponent.AppendLine("<span>").Append(Text).Append("</span>");
            }
            SbComponent.AppendLine("</label>");
            SbComponent.Append("<script type='text/javascript'>jQuery.validator.addClassRules('validate" + Id + "', {" + Validate + "});</script>");

        }
    }
}
