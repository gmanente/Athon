using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistema.Web.UI.Repositorio.View.Page
{
    public partial class ErroRelatorio : System.Web.UI.Page
    {
        public string Title { get; set; }
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
        public string Icone { get; set; }
        public string Status { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

            //Get query string com o erro
            Title = "Não foi possível gerar o relatório";
            Mensagem = Request.QueryString["mensagem"];

            //Mensagem = "Ocorreu um erro interno no servidor, por favor entre em contato com a equipe de desenvolvimento.";
            Icone = "fa-warning text-info error-icon-shadow";

        }
    }
}