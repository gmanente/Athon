using System.Text;

#pragma warning disable 0108 // variable assigned but not used.
namespace Sistema.Api.dll.Repositorio.Util.Componentes
{
    public class Heading : AbstractComponent
    {
        public GroupComponent Content { get; set; }
        public string HeadingType { get; set; }
        public string Text { get; set; }

        public Heading()
            : base()
        {
            Content = new GroupComponent();;
        }

        private void SetHeading()
        {
            SbComponent = new StringBuilder();
            SbComponent.AppendLine("<" + HeadingType + " ");

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

            if (Text != null)
            {
                SbComponent.AppendLine(">" + Text + "</" + HeadingType + ">");
            }
            else
            {
                if (!string.IsNullOrEmpty(Content.ToString()))
                {
                    SbComponent.AppendLine(">" + Content + "</" + HeadingType + ">");
                }
                   
            }

        }


        public void AddComponentContent(AbstractComponent componet)
        {
            Content.Add(componet);
        }
        
        public override string ToString()
        {
            SetHeading();
            return base.ToString();
        }

        public virtual void Render()
        {
            SetHeading();
            base.Render();
        }
    }

    public static class HeadingType
    {
        public static string H1 { get { return "h1"; } }
        public static string H2 { get { return "h2"; } }
        public static string H3 { get { return "h3"; } }
        public static string H4 { get { return "h4"; } }
        public static string H5 { get { return "h5"; } }
        public static string H6 { get { return "h6"; } }
    }
}
