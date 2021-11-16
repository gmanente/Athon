using System.Text;

#pragma warning disable 0108 // variable assigned but not used.
namespace Sistema.Api.dll.Repositorio.Util.Componentes
{
    public class Center : AbstractComponent
    {
        public GroupComponent Content { get; set; }
        public string AjaxCall { get; set; }

        public Center()
            : base()
        {
            Content = new GroupComponent();
        }

        private void SetI()
        {
            SbComponent = new StringBuilder();
            SbComponent.AppendLine("<center> ");

            if (!string.IsNullOrEmpty(Content.ToString()))
            {
                SbComponent.Append(Content);
            }

            SbComponent.AppendLine("</center>");

            if (!string.IsNullOrEmpty(AjaxCall))
            {
                SbComponent.AppendLine(AjaxCall);
            }
        }

        public void AddComponentContent(AbstractComponent componet)
        {
            Content.Add(componet);
        }


        public override string ToString()
        {
            SetI();
            return base.ToString();
        }

        public virtual void Render()
        {
            SetI();
            base.Render();
        }
    }
}
