using System.Collections.Generic;
using System.Web;

namespace Sistema.Api.dll.Repositorio.Util.Componentes.Templates
{
    public class UltimosAcessosTemplate
    {
        public GroupComponent Content { get; set; }
        public Heading TituloPagina { get; set; }
        public Div ContainerUltimosAcessos { get; set; }
        public List<Div> ListaUltimosAcessos { get; set; }

        public UltimosAcessosTemplate()
        {
            Content = new GroupComponent();
            TituloPagina = new Heading();
            ContainerUltimosAcessos = new Div();
            ListaUltimosAcessos = new List<Div>();
        }

        public void SetTemplate()
        {
            //Titulo página
            TituloPagina.Id = "titulo-modulo";

            //Últimos acessos
            ContainerUltimosAcessos.Id = "ultimos-acessos";

            //ListaUltimosAcessos de últimos acessos
            if (ListaUltimosAcessos != null)
            {
                foreach (var box in ListaUltimosAcessos)
                {
                    ContainerUltimosAcessos.AddComponentContent(box);
                }
            }

            //Content
            Content.Add(TituloPagina);
            Content.Add(ContainerUltimosAcessos);
        }

        public override string ToString()
        {
            SetTemplate();
            return Content.ToString();
        }

        public virtual void Render()
        {
            HttpContext.Current.Response.Write(ToString());
        }
    }
}