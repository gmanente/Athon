using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistema.Web.UI.Repositorio.View.Page
{
    public partial class Erro404 : System.Web.UI.Page
    {
        public string Title { get; set; }
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
        public string Icone { get; set; }
        public string Status { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Get query string com o erro
            Title = "Erro 404";
            Titulo += "Erro 404 - Página não encontrada!";
            Mensagem = "Está página não foi encontrada em nosso sistema, aguarde que você será redirecionado para a página principal.";
            Icone = "fa-warning text-info error-icon-shadow";
            
           
        }
    }
}