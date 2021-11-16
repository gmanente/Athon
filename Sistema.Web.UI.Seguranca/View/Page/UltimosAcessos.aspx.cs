using System;
using System.Collections.Generic;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Repositorio.Util.Componentes;
using Sistema.Api.dll.Repositorio.Util.Componentes.Templates;

namespace Sistema.Web.UI.Seguranca.View.Page
{
    public partial class UltimosAcessos : CommonPage
    {
        //Seg
        protected void Page_Load(object sender, EventArgs e)
        {
            // Funcoes.IsIpExternal("172.17.0.3", "");
        }

        //Set template
        public UltimosAcessosTemplate SetTemplate()
        {
            var listaUltimosAcessos = new List<Div>();
            var boxUltimosAcessos = new Div()
            {
                Style = "background-color: #00bac6;",
                Class = "box-ultomos-acessos",
                InjectDataAttr = "data-hover='#333'",
                Onclick = @"window.location = '../Page/Modulo.aspx';"
            };

            var boxUltimosAcessos2 = new Div()
            {
                Style = "background-color: #00bac6;",
                Class = "box-ultomos-acessos",
                InjectDataAttr = "data-hover='#333'",
                Onclick = @"window.location ='../Page/Usuario.aspx';"
            };

            var span = new Span()
            {
                Text = "Módulo",
                Icon = "key",
            };

            var span2 = new Span()
            {
                Text = "Submodulo",
                Icon = "book",
            };
            boxUltimosAcessos.AddComponentContent(span);
            boxUltimosAcessos2.AddComponentContent(span2);

            listaUltimosAcessos.Add(boxUltimosAcessos);
            listaUltimosAcessos.Add(boxUltimosAcessos2);

            return new UltimosAcessosTemplate()
            {
                TituloPagina = { HeadingType = HeadingType.H1, Style = "background-color:#00bac6;", Text = "Últimos acessos" },
                ListaUltimosAcessos = listaUltimosAcessos
            };

        }
    }
}