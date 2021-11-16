using System;
using System.Text;

#pragma warning disable 0108 // variable assigned but not used.
namespace Sistema.Api.dll.Repositorio.Util.Componentes
{
    public class DatetimePicker : AbstractComponentText
    {
        public DateTime? DataTempo { get; set; }
        public bool? DesabilitarData { get; set; }

        public DatetimePicker() 
            : base()
        {
            DataTempo = DateTime.Now;
            DesabilitarData = false;
        }


        private void SetDatetimePicker()
        {

            SbComponent = new StringBuilder();
            SbComponent.AppendLine("<div class='" + ContainerClass + "'>");
            SbComponent.Append("<label ");
            if (!string.IsNullOrEmpty(LabelFor))
            {
                SbComponent.Append("for='").Append(LabelFor).AppendLine("' ");
            }
            SbComponent.Append(">").Append(LabelText).AppendLine("</label>");

            SbComponent.Append("<div class='").Append("input-group date").AppendLine("' id='datetime-picker" + Id + "'>");
            SbComponent.AppendLine("<input type='text' ");
            if (!string.IsNullOrEmpty(Class))
            {
                SbComponent.Append("class='" + Class + " validate" + Id + "'");
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
                SbComponent.Append("disabled='disabled'");
            }
            if (Readonly)
            {
                SbComponent.Append("readonly='readonly'");
            }
            if (!string.IsNullOrEmpty(Name))
            {
                SbComponent.Append("name='").Append(Name).AppendLine("' ");
            }
            if (!string.IsNullOrEmpty(Id))
            {
                SbComponent.Append("id='").Append(Id).AppendLine("' ");
            }
            if (!string.IsNullOrEmpty(Name))
            {
                SbComponent.Append("placeholder='").Append(Placeholder).AppendLine("' ");
            }

            if (!string.IsNullOrEmpty(Value))
            {
                SbComponent.Append("value='").Append(Value).AppendLine("' ");
            }

            SbComponent.AppendLine("/><span class='input-group-addon'><span class='fa fa-calendar-o'></span>");
            SbComponent.Append("<script type='text/javascript'>jQuery.validator.addClassRules('validate" + Id + "', {" + Validate + "});</script>");
            SbComponent.AppendLine("</div>");
            SbComponent.AppendLine("</div>");
        }
      
        private void SetScript()        

        {
            SbComponent.AppendLine("<script type='text/javascript'>");
            SbComponent.AppendLine("$(document).ready(function () {");

            if (DesabilitarData == false)
            {
                SbComponent.AppendLine("$('#datetime-picker" + Id + "').datetimepicker({useSeconds: true});");
            }
            else if (DesabilitarData == true)
            {
                SbComponent.AppendLine("$('#datetime-picker" + Id + "').datetimepicker({ pickDate: false , useSeconds: true   });");
            }

            SbComponent.AppendLine("});");
            SbComponent.AppendLine("</script>");
        }

        private string ReplaceDate(DateTime? Data)
        {
            string dt = Data.ToString().Replace("-", "/")+" 00:00 AM";
            return dt.Substring(0, 10);
        }

        public override string ToString()
        {
            SetDatetimePicker();
            SetScript();
            return base.ToString();
        }

        public virtual void Render()
        {
            SetDatetimePicker();
            SetScript();
            base.Render();
        }
    }
}
