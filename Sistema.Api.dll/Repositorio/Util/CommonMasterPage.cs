using Sistema.Api.dll.Src;
using System;
using System.Web;

namespace Sistema.Api.dll.Repositorio.Util
{
    public class CommonMasterPage : System.Web.UI.MasterPage
    {
        //Seg
        public string UrlCompleta { get; set; }
        public string UrlSemQueryString { get; set; }
        public string[] Urls { get; set; }
        private const int ConsPosicaoPaginalAtual = 5;
        public string PaginaAtual { get; set; }
        public SessaoSistema Sessao { get; set; }

        public void Page_Load(object sender, EventArgs e)
        {
            var sessao = SessionHandler.GetSession<SessaoSistema>("Session");
            if (sessao == null && Dominio.AppState != Dominio.ApplicationState.Debug)
            {
                HttpContext.Current.Response.Redirect("../Page/Erro.aspx?s=sessao-expirada");
            }
            RefreshSesion(sessao);
            GetUrlCompleta();
            GetUrlSemQueryString();
            GetUrlsArray();
            GetPaginaAtual();
        }

        private void RefreshSesion(SessaoSistema sessao)
        {
            var idModulo = Request.QueryString["idModulo"];
            if (idModulo != null)
            {
                sessao.IdModulo = Convert.ToInt64(Request.QueryString["idModulo"]);
                var s = new SessionHandler()
                {
                    Objeto = sessao
                };
                s.NewSession("Session");

            }
            Sessao = sessao;
        }

        //Get a url atual
        public void GetUrlCompleta()
        {
            UrlCompleta = Request.Url.ToString();
        }

        //Get url sem query string
        public void GetUrlSemQueryString()
        {
            UrlSemQueryString = new Uri(UrlCompleta).GetLeftPart(UriPartial.Path);
        }

        //Get url array
        public string GetUrlModulo()
        {
            return Urls[0] + "//" + Urls[2];
        }

        //Get url array
        public void GetUrlsArray()
        {
            Urls = UrlSemQueryString.Split(new Char[] { '/' });
        }

        //Get página atual
        public void GetPaginaAtual()
        {
            PaginaAtual = Urls[ConsPosicaoPaginalAtual];
        }
    }
}

