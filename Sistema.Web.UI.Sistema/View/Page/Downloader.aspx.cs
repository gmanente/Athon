using System;
using System.Web;

namespace Sistema.Web.UI.Sistema.View.Page
{
    public partial class Downloader : System.Web.UI.Page
    {
        public string Arquivo { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Arquivo = Request.QueryString["arquivo"];
            string path = Request.Url.Host.ToLower();
            var caminhoFull = "http://" + path + Arquivo;
            var posicoesUrl = caminhoFull.Split(new Char[] { '/' });
            var quantidadePosicoes = posicoesUrl.Length;
            var nomeArquivo = posicoesUrl[(quantidadePosicoes - 1)];

            //Force download
            HttpContext.Current.Response.ContentType = "application/text";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + nomeArquivo);
            HttpContext.Current.Response.WriteFile(Server.MapPath(Arquivo));
            HttpContext.Current.Response.End();
            HttpContext.Current.Response.Flush();


        }
    }
}