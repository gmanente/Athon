using System;
using System.IO;
using System.Web;
using Sistema.Api.dll.Repositorio.Util;

namespace Sistema.Web.UI.Mensageria.Util
{
    /// <summary>
    /// Summary description for AjaxFileUploader
    /// </summary>
    public class AjaxFileUploader : IHttpHandler
    {
        public string Hashname = string.Empty;
        public string DiretorioTemporario = "~/Uploads/";
        public string Estado;
        public Ajax AjaxHandler;

        //Construtor
        public AjaxFileUploader()
        {
            AjaxHandler = new Ajax();
        }

        //FIltrar as permissões
        public Boolean FiltrarExtensao(string extensoesPermitidas, string extensaoArquivo)
        {
            int pos = Array.IndexOf(extensoesPermitidas.Split(new Char[] { '-' }), extensaoArquivo.Replace(".", ""));
            if (!(pos > -1))
            {
                AjaxHandler.StatusOperacao = false;
                AjaxHandler.AddMessage("A extensão do arquivo não é permitida.<br/>Utilize as extenções: " + extensoesPermitidas, Mensagem.Erro);
                return false;
            }
            else
            {
                return true;
            }
        }

        //Checa se o arquivo foi enviado
        public Boolean ChecarEnvio(HttpContext context)
        {
            //Caso o arquivo tenha sido enviado
            if (File.Exists(context.Server.MapPath(DiretorioTemporario + Hashname)))
            {
                AjaxHandler.StatusOperacao = true;
                AjaxHandler.AddMessage("Arquivo enviado com sucesso", Mensagem.Sucesso);
                AjaxHandler.Variante = DiretorioTemporario + Hashname;
                return true;
            }
            else
            {
                AjaxHandler.StatusOperacao = false;
                AjaxHandler.AddMessage("Ocorreu um erro ao enviar o arquivo, por favor tente novamente.", Mensagem.Erro);
                return false;
            }

        }

        //Processar a requisição
        public void ProcessRequest(HttpContext context)
        {
            //Montar diretório de upload
            DiretorioTemporario = DiretorioTemporario + context.Request.QueryString["folder"] + "/";

            if (context.Request.Files.Count > 0)
            {
                HttpFileCollection files = context.Request.Files;
                context.Response.ContentType = "text/plain";
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile file = files[i];

                    //Extensão do arquivo
                    string extension = Path.GetExtension(file.FileName);

                    //Checa extensões permitidas
                    if (FiltrarExtensao(context.Request.QueryString["extensao"], extension))
                    {
                        Hashname = Criptografia.MD5(DateTime.Now.ToString("yyyyMMddHHmmss") + file.FileName) + extension;
                        string teste = DiretorioTemporario + Hashname;
                        string fullName = context.Server.MapPath(DiretorioTemporario + Hashname);
                        file.SaveAs(fullName);
                        //Checar se o aruqivo foi enviado
                        ChecarEnvio(context);
                    }
                }
            }

            //Retorno da classe
            context.Response.Write(AjaxHandler.GetAjaxJson());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}