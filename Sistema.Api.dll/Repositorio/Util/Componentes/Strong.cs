using System.Text;

#pragma warning disable 0108 // variable assigned but not used.
namespace Sistema.Api.dll.Repositorio.Util.Componentes
{
    public class Strong : AbstractComponent
    {
        public GroupComponent Content { get; set; }
        public string Text { get; set; }
        public string AjaxCall { get; set; }

        public Strong()
            : base()
        {
            Content = new GroupComponent();
        }

        private void SetI()
        {
            SbComponent = new StringBuilder();
            SbComponent.AppendLine("<strong class='" + Class + "' id='" + ContainerId + "' style='" + ContainerStyle + "'");


            if (!string.IsNullOrEmpty(InjectDataAttr))
            {
                SbComponent.AppendLine(InjectDataAttr);
            }

            if (!string.IsNullOrEmpty(Class))
            {
                SbComponent.Append("class='").Append(Class).AppendLine("' ");
            }

            if (!string.IsNullOrEmpty(Style))
            {
                SbComponent.Append("style='").Append(Style).AppendLine("' ");
            }

            if (!string.IsNullOrEmpty(Id))
            {
                SbComponent.Append("id='").Append(Id).AppendLine("' ");
            }

            if (!string.IsNullOrEmpty(Onclick))
            {
                SbComponent.Append("onclick=\"").Append(Onclick).AppendLine("\" ");
            }

            if (!string.IsNullOrEmpty(Onload))
            {
                SbComponent.Append("onload=\"").Append(Onload).AppendLine("\" ");
            }

            SbComponent.AppendLine(">");

            if (!string.IsNullOrEmpty(Content.ToString()))
            {
                SbComponent.Append(Content);
            }

            SbComponent.Append(StrongText).AppendLine("</strong>");

            if (!string.IsNullOrEmpty(Text))
            {
                SbComponent.Append(Text);
            }

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
