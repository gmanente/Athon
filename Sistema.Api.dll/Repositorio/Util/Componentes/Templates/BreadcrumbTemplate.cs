using System.Collections.Generic;
using System.Web;

#pragma warning disable 0108 // variable assigned but not used.
namespace Sistema.Api.dll.Repositorio.Util.Componentes.Templates
{
    public class BreadcrumbTemplate : AbstractComponent
    {
        public GroupComponent Content { get; set; }
        public Div BreadcrumbContainer { get; set; }
        public Heading TituloBreadcrumb { get; set; }
        public OlUl Ol { get; set; }
        public List<Li> LiList { get; set; }

        public BreadcrumbTemplate()
            : base()
        {
            Content = new GroupComponent();
            BreadcrumbContainer = new Div();
            TituloBreadcrumb = new Heading();
            Ol = new OlUl();
        }

        public void SetBreadcrumbTemplate()
        {
            //Breadcrumb container
            BreadcrumbContainer.Class = "";

            //TituloBreadcrumb
            TituloBreadcrumb.HeadingType = HeadingType.H4;
            TituloBreadcrumb.Text = "Navegação";

            //Ol
            Ol.ListType = ListType.Ol;
            Ol.Class = "breadcrumb";
            
            //Lista de li
            if (LiList != null)
            {
                foreach (var item in LiList)
                {
                    Ol.AddComponentContent(item);
                }
            }

            //Add component
            //BreadcrumbContainer.AddComponentContent(TituloBreadcrumb);
            BreadcrumbContainer.AddComponentContent(Ol);

            //Add content 
            Content.Add(BreadcrumbContainer);

        }

        public override string ToString()
        {
            SetBreadcrumbTemplate();
            return Content.ToString();
        }

        public virtual void Render()
        {
            HttpContext.Current.Response.Write(ToString());
        }

    }
}
