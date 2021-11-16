using System.Text;

#pragma warning disable 0108 // variable assigned but not used.
namespace Sistema.Api.dll.Repositorio.Util.Componentes
{
    public class A : AbstractComponent
    {
        public GroupComponent Content { get; set; }
        public string Href { get; set; }
        public string Title { get; set; }
        public string Target { get; set; }
        public string Text { get; set; }
        
        public A()
            : base()
        {
            Content = new GroupComponent();
        }

        private void SetA()
        {
            SbComponent = new StringBuilder();
            SbComponent.AppendLine("<a ");

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

            if (!string.IsNullOrEmpty(Href))
            {
                SbComponent.Append("href='").Append(Href).AppendLine("' ");
            }

            if (!string.IsNullOrEmpty(Class))
            {
                SbComponent.Append("class='").Append(Class).AppendLine("' ");
            }

            if (!string.IsNullOrEmpty(Title))
            {
                SbComponent.Append("title='").Append(Title).AppendLine("' ");
            }

            if (!string.IsNullOrEmpty(Target))
            {
                SbComponent.Append("target='").Append(Target).AppendLine("' ");
            }

            SbComponent.AppendLine(">");

            if (!string.IsNullOrEmpty(Content.ToString()))
            {
                SbComponent.Append(Content);
            }

            if (!string.IsNullOrEmpty(Text))
            {
                SbComponent.Append(Text);
            }

            SbComponent.AppendLine("</a>");

        }

        public void AddComponentContent(AbstractComponent componet)
        {
            Content.Add(componet);
        }

        public override string ToString()
        {
            SetA();
            return base.ToString();
        }

        public virtual void Render()
        {
            SetA();
            base.Render();
        }
    }

    public static class Target
    {
        public static string Self { get { return "_self"; } }
        public static string New { get { return "_new"; } }
        public static string Top { get { return "_top"; } }
        public static string Blank { get { return "_blank"; } }
    }

}
