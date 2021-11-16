using System.Text;

#pragma warning disable 0108 // variable assigned but not used.
namespace Sistema.Api.dll.Repositorio.Util.Componentes
{
    public class Span : AbstractComponent
    {
        public GroupComponent Content { get; set; }
        public string Text { get; set; }
        public string Icon { get; set; }

        public Span()
            : base()
        {
            Content = new GroupComponent();
        }

        private void SetSpan()
        {
            SbComponent = new StringBuilder();
            if (!string.IsNullOrEmpty(LabelFor))
            {
                SbComponent.AppendLine("<div class='" + ContainerClass + "' id='" + ContainerId + "' style='" + ContainerStyle + "'>");
                SbComponent.AppendLine("<label ");
                SbComponent.Append("for='").Append(LabelFor).AppendLine("' ");
                SbComponent.Append(">").Append(LabelText).AppendLine("</label><br>");
            }
            SbComponent.AppendLine("<span ");

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

            if (!string.IsNullOrEmpty(Icon))
            {
                SbComponent.Append("class='fa fa-").Append(Icon).AppendLine("' ");
            }

            if (Icon == null)
            {
                SbComponent.AppendLine(">" + Text + "</span>");
            }
            else
            {
                SbComponent.AppendLine("></span> " + Text + "");
            }
            if (!string.IsNullOrEmpty(LabelFor))
            {
                SbComponent.AppendLine("</div>");
            }

        }

        public void AddComponentContent(AbstractComponent componet)
        {
            Content.Add(componet);
        }


        public override string ToString()
        {
            SetSpan();
            return base.ToString();
        }

        public virtual void Render()
        {
            SetSpan();
            base.Render();
        }
    }
}
