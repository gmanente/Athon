using System.Text;

#pragma warning disable 0108 // variable assigned but not used.
namespace Sistema.Api.dll.Repositorio.Util.Componentes
{
    public class P : AbstractComponent
    {
        public GroupComponent Content { get; set; }
        public string Text { get; set; }

        public P()
            : base()
        {
            Content = new GroupComponent();
        }


        private void SetP()
        {
            SbComponent = new StringBuilder();
            if (!string.IsNullOrEmpty(LabelFor))
            {
                SbComponent.AppendLine("<div class='" + ContainerClass + "' id='" + ContainerId + "' style='" + ContainerStyle + "'>");
                SbComponent.AppendLine("<label ");
                SbComponent.Append("for='").Append(LabelFor).AppendLine("' ");
                SbComponent.Append(">").Append(LabelText).AppendLine("</label>");
            }
            SbComponent.AppendLine("<p ");

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
            if (!string.IsNullOrEmpty(Text))
            {
                SbComponent.Append(Text);
            }


            if (!string.IsNullOrEmpty(Content.ToString()))
            {
                SbComponent.Append(Content);
            }

            SbComponent.AppendLine("</p>");

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
            SetP();
            return base.ToString();
        }

        public virtual void Render()
        {
            SetP();
            base.Render();
        }


    }
}
