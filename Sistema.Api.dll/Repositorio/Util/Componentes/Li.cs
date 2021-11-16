using System.Text;

#pragma warning disable 0108 // variable assigned but not used.
namespace Sistema.Api.dll.Repositorio.Util.Componentes
{
    public class Li : AbstractComponent
    {
        public string PreText { get; set; }
        public string Text { get; set; }
        public A Link { get; set; }
        public GroupComponent Content { get; set; }
        
        public Li()
            : base()
         {
            Link = new A();
            Content = new GroupComponent();
         }

        private void SetLi()
        {
            SbComponent = new StringBuilder();
            SbComponent.AppendLine("<li ");

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

            if (!string.IsNullOrEmpty(PreText))
            {
                SbComponent.AppendLine(PreText);
            }

            if (!string.IsNullOrEmpty(Link.Text))
            {
                SbComponent.AppendLine(Link.ToString());
            }

            if (!string.IsNullOrEmpty(Text))
            {
                SbComponent.AppendLine(Text);
            }

            if (!string.IsNullOrEmpty(Content.ToString()))
            {
                SbComponent.AppendLine(Content.ToString());
            }

            SbComponent.AppendLine("</li>");
        }

        public void AddComponentContent(AbstractComponent componet)
        {
            Content.Add(componet);
        }

         public override string ToString()
         {
               SetLi();
               return base.ToString();
          }

         public virtual void Render()
         {
              SetLi();
              base.Render();
          }
    }
}

