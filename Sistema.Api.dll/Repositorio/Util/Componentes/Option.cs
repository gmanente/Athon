using System.Collections.Generic;
using System.Text;

namespace Sistema.Api.dll.Repositorio.Util.Componentes
{
    public class Option : AbstractComponetInput
    {
        public bool Selected { get; set; }
        public string Text { get; set; }
        public GroupComponent Content { get; set; }
        public List<DataAtributte> LstDataAtributtes { get; set; }

        public Option()
            : base()
        {
            Content = new GroupComponent();
        }

        private void SetOption()
        {
            SbComponent = new StringBuilder();
            SbComponent.AppendLine("<option ");
            if (LstDataAtributtes != null)
            {
                foreach (var data in LstDataAtributtes)
                    SbComponent.Append("data-").Append(data.Name + "='").Append(data.Value).Append("' ");
            }
            if (!string.IsNullOrEmpty(Class))
            {
                SbComponent.Append("class='").Append(Class).Append(Validate).AppendLine("' ");
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
            if (Selected)
            {
                SbComponent.Append("selected='selected' ");
            }
            if (!string.IsNullOrEmpty(Name))
            {
                SbComponent.Append("name='").Append(Name).AppendLine("' ");
            }
            if (!string.IsNullOrEmpty(Id))
            {
                SbComponent.Append("id='").Append(Id).AppendLine("' ");
            }

            SbComponent.Append("value='").Append(Value).AppendLine("' ");

            SbComponent.AppendLine(">");
            SbComponent.AppendLine(Text);
            SbComponent.AppendLine("</option>");

        }
        public override string ToString()
        {
            SetOption();
            return base.ToString();
        }
    }

    public class DataAtributte
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
