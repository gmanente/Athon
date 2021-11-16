using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using Sistema.Api.dll.Repositorio.Util;

namespace Sistema.Web.UI.Mensageria
{
    public class UtilMensageria : System.Web.UI.Page
    {
        //Enviar email
        public string Remetente { get; set; }
        public string Smtp { get; set; }
        public int SmtpPorta { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }        


        public bool EnviarEmail(string email, string texto, string assunto)
        {
            Email Mandar_Mail = null;
            try
            {
                Mandar_Mail = new Email();
                Mandar_Mail.Remetente = Remetente;
                Mandar_Mail.Destinatario = email;
                Mandar_Mail.Codificacao_Assunto = Encoding.GetEncoding("ISO-8859-1");
                Mandar_Mail.Codificacao_Corpo = Encoding.GetEncoding("ISO-8859-1");
                Mandar_Mail.Assunto = assunto;
                Mandar_Mail.Corpo_Html = true;
                Mandar_Mail.Corpo = texto;
                Mandar_Mail.Smtp_Port = SmtpPorta;
                Mandar_Mail.Smtp_Servidor = Smtp;
                Mandar_Mail.Smtp_Usuario = Usuario;
                Mandar_Mail.Smtp_Senha = Senha;                

                Mandar_Mail.Send(Mandar_Mail);
                bool teste = Mandar_Mail.Estado;
                string msg = Mandar_Mail.Mensagem;


                return teste;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Ler template
        public string LerTemplateLocal(string caminhoArquivoTemplate)
        {
            return System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath(caminhoArquivoTemplate));
        }

        public string LerTemplate(string caminhoArquivoTemplate)
        {
            var webRequest = WebRequest.Create(caminhoArquivoTemplate);
            var strContent = "";
            using (var response = webRequest.GetResponse())
            using (var content = response.GetResponseStream())
            using (var reader = new StreamReader(content))
            {
                strContent = reader.ReadToEnd();
            }

            return strContent;
        }

    }
}