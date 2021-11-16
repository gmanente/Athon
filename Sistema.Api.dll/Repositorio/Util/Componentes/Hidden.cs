using System.Text;

namespace Sistema.Api.dll.Repositorio.Util.Componentes
{
    public class Hidden : AbstractComponetInput
    {
        public Hidden() 
            : base()
        {
        }
        public Hidden(string[] paramters)
            : base(paramters)
        {
        }

        private void SetHidden()
        {
            SbComponent = new StringBuilder();
            SbComponent.AppendLine("<input type='hidden' ");

            if (!string.IsNullOrEmpty(InjectDataAttr))
            {
                SbComponent.AppendLine(InjectDataAttr);
            }
            if (!string.IsNullOrEmpty(Name))
            {
                SbComponent.Append("name='").Append(Name).AppendLine("' ");
            }
            if (Disabled)
            {
                SbComponent.Append("disabled='disabled'");
            }
            if (Readonly)
            {
                SbComponent.Append("readonly='readonly'");
            }
            if (!string.IsNullOrEmpty(Id))
            {
                SbComponent.Append("id='").Append(Id).AppendLine("' ");
            }
            if (!string.IsNullOrEmpty(Value))
            {
                SbComponent.Append("value='").Append(Value).AppendLine("' ");
            }
            SbComponent.AppendLine("/>");
        }


        public override string ToString()
        {
            SetHidden();
            return base.ToString();
        }
    }
}
