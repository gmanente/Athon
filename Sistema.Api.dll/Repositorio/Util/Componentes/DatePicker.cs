using System;
using System.Text;

#pragma warning disable 0108 // variable assigned but not used.
namespace Sistema.Api.dll.Repositorio.Util.Componentes
{
    public class DatePicker : AbstractComponentText
    {
        public DateTime? Data { get; set; }

        public DatePicker() 
            : base()
        {
            Data = DateTime.Now;
        }

        private void SetDatePicker()
        {
            
            SbComponent = new StringBuilder();
            SbComponent.AppendLine("<div class='" + ContainerClass + "'>");
            SbComponent.Append("<label ");
            if (!string.IsNullOrEmpty(LabelFor))
            {
                SbComponent.Append("for='").Append(LabelFor).AppendLine("' ");
            }
            SbComponent.Append(">").Append(LabelText).AppendLine("</label>");
            SbComponent.Append("<div class='").Append("input-append date").AppendLine("'");
            SbComponent.Append("data-date='").Append(ReplaceDate(Data)).AppendLine("'");
            SbComponent.Append("data-date-format='dd/mm/yyyy'");
            SbComponent.Append("data-date-viewmode='years'>");
            SbComponent.AppendLine("<input type='text' ");
            SbComponent.AppendLine("size='10'");
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

            SbComponent.AppendLine("/>");
            SbComponent.Append("<script type='text/javascript'>jQuery.validator.addClassRules('validate" + Id + "', {" + Validate + "});</script>");
            SbComponent.AppendLine("</div>");
            SbComponent.AppendLine("</div>");

        }

        private void SetScript(){
             SbComponent.AppendLine("<script type='text/javascript'>");
             SbComponent.AppendLine("$(document).ready(function () {");
             SbComponent.AppendLine("var datePickerOptions = { format: 'dd/mm/yyyy' };");
            SbComponent.AppendLine("$('#" + Id + "').mask('99/99/9999');");
             SbComponent.AppendLine("$('#"+Id+"').datepicker(datePickerOptions).on('changeDate', function () {");
             SbComponent.AppendLine("$(this).datepicker('hide');");
             SbComponent.AppendLine("});");
             SbComponent.AppendLine("});");
             SbComponent.AppendLine("</script>");
        }

        private string ReplaceDate(DateTime? Data)
        {
            return Data.ToString().Replace("-", "/").Trim().Substring(0,10);
        }
        public override string ToString()
        {
            SetDatePicker();
            SetScript();
            return base.ToString();
        }

        public virtual void Render()
        {
            SetDatePicker();
            SetScript();
            base.Render();
        }
    }
}
