using System.Text;

#pragma warning disable 0108 // variable assigned but not used.
namespace Sistema.Api.dll.Repositorio.Util.Componentes
{
    public class Img : AbstractComponent
    {
        public string Src { get; set; }

        public Img()
            : base()
        {

        }

        private void SetImg()
        {
            SbComponent = new StringBuilder();
            SbComponent.AppendLine("<img ");

            if (!string.IsNullOrEmpty(InjectDataAttr))
            {
                SbComponent.AppendLine(InjectDataAttr);
            }

            if (!string.IsNullOrEmpty(Id))
            {
                SbComponent.Append("id='").Append(Id).AppendLine("' ");
            }

            if (!string.IsNullOrEmpty(Style))
            {
                SbComponent.Append("style='").Append(Style).AppendLine("' ");
            }

            if (!string.IsNullOrEmpty(Class))
            {
                SbComponent.Append("class='").Append(Class).AppendLine("' ");
            }

            if (!string.IsNullOrEmpty(Src))
            {
                SbComponent.Append("src='").Append(Src).AppendLine("' ");
            }

            SbComponent.AppendLine("/>");



        }

        public override string ToString()
        {
            SetImg();
            return base.ToString();
        }

        public virtual void Render()
        {
            SetImg();
            base.Render();
        }

    }
}
