using Sistema.Api.dll.Repositorio.Util;
using System;
using System.Web;
using static Sistema.Web.RepositorioWebAPI.Tools.AngularTools;

namespace Sistema.Web.UI.Datanorte.View.Page
{
    public partial class Erro : System.Web.UI.Page
    {
        public string Title { get; set; }
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
        public string Icone { get; set; }
        public string Status { get; set; }
        public string SubModuloUrl { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Get query string com o erro
            Status = Request.QueryString["s"];
            SubModuloUrl = GetSubModuloUrl();

            Title = "Erro Interno";
            Titulo = "Erro Interno";
            Mensagem = Request.QueryString["msg"] ?? "";
            Icone = "fa-times-circle text-danger error-icon-shadow";

            //Erro 401
            if (Status == "401")
            {
                Title = "Erro 401 - Não Autorizado";
                Titulo = "Erro 401 <br> Não Autorizado";
                Mensagem = "Acesso negado devido a credenciais inválidas.";
                Icone = "fa-ban text-danger error-icon-shadow";
            }
            //Erro 403
            if (Status == "403")
            {
                Title = "Erro 403 - Proibido";
                Titulo = "Erro 403 <br> Proibido";
                Mensagem = "Você não tem permissão de acesso.";
                Icone = "fa-ban text-danger error-icon-shadow";
            }
            //Erro 404
            if (Status == "404")
            {
                Title = "Erro 404";
                Titulo = "Erro 404";
                Mensagem = "A página que você acessou pode ter sido removida ou renomeada ou o link que você seguiu está incorreto.";
                Icone = "fa-warning text-info error-icon-shadow";
            }
            //Erro 500
            else if (Status == "500")
            {
                Title = "Erro 500";
                Titulo = "Erro 500";
                Mensagem = "Ocorreu um erro interno no servidor. Por favor tente refazer o processo. Persistindo o erro, entre em contato com a equipe de desenvolvimento. Detalhe do erro abaixo: <br><br> <span style='color: #a94442;'><i class='fa fa-bullhorn'></i> " + Mensagem + "</span>";
                Icone = "fa-warning text-danger error-icon-shadow";
            }
            // Custom
            else if (Status == "custom")
            {
                Title = Request.QueryString["ct"];
                Titulo = Request.QueryString["ct"];
                Mensagem = Request.QueryString["cm"] + ". Detalhe do erro abaixo: <br><br> <span style='color: #42a990;'>" + Request.QueryString["cd"] + "</span>";
                Icone = "fa-warning text-danger error-icon-shadow";
            }
            //Token inválido
            else if (Status == "token-invalido")
            {
                Title = "Token Inválido";
                Titulo = "Token Inválido";
                Mensagem = "Ocorreu um erro ao validar token no servidor, por favor selecione novamente o módulo no menu para renovar sessão.";
            }
            //Sessão expirada
            else if (Status == "sessao-expirada")
            {
                Title = "Sessão Expirada";
                Titulo = "Sessão Expirada";
                Mensagem = "Sua sessão expirou no servidor, por favor faça o processo de login novamente.";
                Icone = "fa-warning text-warning error-icon-shadow";
            }
            //Web storage não suportado
            else if (Status == "web-storage")
            {
                Titulo = "Navegador Obsoleto";
                Mensagem = "Desculpe mas o seu navegador não tem suporte para uma tecnologia fundamental utilizada neste sistema. [WebStorage]";
            }
        }

        private string GetSubModuloUrl()
        {
            var cookieAngularSession = HttpContext.Current.Request.Cookies[HttpUtility.UrlEncode(Criptografia.MD5("AngularSession"))];
            if (cookieAngularSession != null)
            {
                var SessionAngular = Json.DeSerialize<AngularSession>(Criptografia.Base64Decode(cookieAngularSession.Value));
                return SessionAngular.SubModuloUrl;
            }
            else return "";
        }
    }
}