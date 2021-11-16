using System.Text;

#pragma warning disable 0108 // variable assigned but not used.
namespace Sistema.Api.dll.Repositorio.Util.Componentes
{
    public class Div : AbstractComponent
    {
        public GroupComponent Content { get; set; }
        public string AjaxCall { get; set; }

        public Div()
            : base()
        {
            Content = new GroupComponent();
        }


        private void SetDiv()
        {
            SbComponent = new StringBuilder();
            SbComponent.AppendLine("<div ");

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

            if (!string.IsNullOrEmpty(Name))
            {
                SbComponent.Append("name='").Append(Name).AppendLine("' ");
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

            SbComponent.AppendLine("</div>");

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
            SetDiv();
            return base.ToString();
        }

        public virtual void Render()
        {
            SetDiv();
            base.Render();
        }


    }
}
