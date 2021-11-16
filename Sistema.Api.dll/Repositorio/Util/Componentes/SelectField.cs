using System.Text;

namespace Sistema.Api.dll.Repositorio.Util.Componentes
{
    public class SelectField : AbstractComponent
    {
        private GroupComponent OptionGroup;
        public bool IsMultiple { get; set; }
        public bool AutoComplete { get; set; }

        public SelectField()
            : base()
        {
            OptionGroup = new GroupComponent();
            AutoComplete = false;
        }
        public SelectField(string[] paramters)
            : base(paramters)
        {
            OptionGroup = new GroupComponent();
            AutoComplete = false;
        }

        private void SetSelectField()
        {
            SbComponent = new StringBuilder();
            SbComponent.AppendLine("<div class='form-group'>");
            SbComponent.AppendLine("<label ");
            if (!string.IsNullOrEmpty(LabelFor))
            {
                SbComponent.Append("for='").Append(LabelFor).AppendLine("' ");
            }
            SbComponent.Append(">").Append(LabelText).AppendLine("</label>");
            SbComponent.AppendLine("<select  ");

            if (IsMultiple)
            {
                SbComponent.Append(" multiple='multiple' ");
            }

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

            SbComponent.AppendLine(">");
            SbComponent.AppendLine(OptionGroup.ToString());
            SbComponent.AppendLine("</select>");
            SbComponent.AppendLine(SetScript());
            SbComponent.AppendLine("</div>");

        }

        //SetScript
        public string SetScript()
        {
            var js = new StringBuilder();
            js.AppendLine("<script type='text/javascript'>");
            js.AppendLine("$(document).ready(function(){");
            js.AppendLine("jQuery.validator.addClassRules('validate" + Id + "', { " + Validate + " } );");
            js.AppendLine("var objSelectField = $('#" + Id + "');");
            if (AutoComplete)
            {
                js.AppendLine("if(objSelectField.attr('multiple') != 'multiple'){");
                js.AppendLine("objSelectField.select2({});");
                js.AppendLine("}");
            }
            js.AppendLine("});");
            js.AppendLine("</script>");
            return js.ToString();
        }

        public void AddOption(Option opt)
        {
            OptionGroup.Add(opt);
        }
        public override string ToString()
        {
            SetSelectField();
            return base.ToString();
        }




    }

}
