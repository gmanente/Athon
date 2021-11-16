using System.Text;

#pragma warning disable 0108 // variable assigned but not used.
namespace Sistema.Api.dll.Repositorio.Util.Componentes
{
    public class OlUl : AbstractComponetInput
    {
        public GroupComponent Content { get; set; }
        public string ListType { get; set; }
        
        public OlUl() 
            : base()
        {
            Content = new GroupComponent();
        }

        private void SetOlUlList()
        {
            SbComponent = new StringBuilder();
            SbComponent.AppendLine("<"+ListType+" ");

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

            SbComponent.AppendLine(">");

            if (!string.IsNullOrEmpty(Content.ToString()))
            {
                SbComponent.Append(Content);
            }

            SbComponent.AppendLine("</"+ListType+">");

        }

        public void AddComponentContent(AbstractComponent componet)
        {
            Content.Add(componet);
        }


        public override string ToString()
        {
            SetOlUlList();
            return base.ToString();
        }

        public virtual void Render()
        {
            SetOlUlList();
            base.Render();
        }
    }

    public static class ListType
    {
        public static string Ul { get { return "ul"; } }
        public static string Ol { get { return "ol"; } }
    }
}
