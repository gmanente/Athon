using System.Collections.Generic;
using System.Web;
using Sistema.Api.dll.Repositorio.Util.Componentes;
using Sistema.Api.dll.Repositorio.Util.Componentes.Templates;

namespace Sistema.Api.dll.Template.CarteirinhaAluno.Grid
{
    public class RelatorioGridTemplate : SubmoduloWireFrameTemplate
    {
        private long IdSubModulo { get; set; }
        private long IdUsuario { get; set; }
        private long IdCampus { get; set; }

        public RelatorioGridTemplate(long idSubModulo, long idUsuario, long idCampus)
            : base()
        {
            IdSubModulo = idSubModulo;
            IdUsuario = idUsuario;
            IdCampus = idCampus;
        }

        public RelatorioGridTemplate()
            : base()
        {

        }
        public void SetRelatorioTemplate()
        {

            SetTemplate();

            //Set titulo página
            TituloPagina.Text = "Relatórios da Carteirinha";
            TituloPagina.Style = "background-color:#0072C6;";

            //Breadcrumb template
            var l = new List<Li>();
            var li1 = new Li()
            {
                Text = "Relatório"
            };

            l.Add(li1);

            var li2 = new Li()
            {
                Text = "Emissão",
                Class = "active"
            };

            l.Add(li2);

            BreadcrumbTemplate.LiList = l;

            //Add content
            Content.Add(TituloPagina);
            Content.Add(BreadcrumbTemplate);
        }

        public override string ToString()
        {
            SetRelatorioTemplate();
            return Content.ToString();
        }

        public override void Render()
        {
            HttpContext.Current.Response.Write(ToString());
        }
    }
}