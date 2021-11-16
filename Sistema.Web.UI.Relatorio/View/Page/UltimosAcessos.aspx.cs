using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Repositorio.Util.Componentes;
using Sistema.Api.dll.Repositorio.Util.Componentes.Templates;
using System;
using System.Collections.Generic;

namespace Sistema.Web.UI.Relatorio.View.Page
{
    public partial class UltimosAcessos : CommonPage
    {
        //Page_Load
        protected new void Page_Load(object sender, EventArgs e)
        {

        }

        //Set template
        public UltimosAcessosTemplate SetTemplate()
        {
            var listaUltimosAcessos = new List<Div>();

            var boxUltimosAcessos = new Div()
            {
                Style = "background-color: #ed8a2e;",
                Class = "box-ultomos-acessos",
                InjectDataAttr = "data-hover='#333'",
                Onclick = @"window.location = '../Page/Relatorio.aspx';"
            };

            listaUltimosAcessos.Add(boxUltimosAcessos);

            return new UltimosAcessosTemplate()
            {
                TituloPagina = { HeadingType = HeadingType.H1, Style = "background-color:#ed8a2e;", Text = "" },
                ListaUltimosAcessos = listaUltimosAcessos
            };

        }
    }
}