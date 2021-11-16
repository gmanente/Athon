using Sistema.Api.dll.Src;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace Sistema.Api.dll.Repositorio.Util
{
    public class UtilMensageria : System.Web.UI.Page
    {
        public string Remetente { get; set; }
        public string Smtp { get; set; }
        public int SmtpPorta { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public List<EmailConfig> lstEmailConfig { get; set; }


        public class EmailConfig
        {
            public string Usuario { get; set; }
            public string Senha { get; set; }
            public string Stmp { get; set; }
            public int SmtpPort { get; set; }
        }


        /// <summary>
        /// EnviarEmail
        /// </summary>
        /// <param name="email"></param>
        /// <param name="texto"></param>
        /// <param name="assunto"></param>
        /// <param name="enviarEmailTest"></param>
        /// <returns></returns>
        public bool EnviarEmail(string email, string texto, string assunto, string enviarEmailTest = "")
        {
            Email Mandar_Mail = null;
            bool emailEnviado = false;
            int tentativas = 0;

            if (Dominio.AppState != Dominio.ApplicationState.Producao)
                email = "univagnpd@gmail.com";

            EmbaralharListaEmail(lstEmailConfig);

            foreach (var item in lstEmailConfig)
            {
                // Email reserva de teste
                if (!string.IsNullOrEmpty(enviarEmailTest))
                    email = enviarEmailTest;

                try
                {
                    Mandar_Mail = new Email();

                    Usuario = item.Usuario;
                    Senha = item.Senha;
                    Smtp = "smtp.gmail.com";
                    SmtpPorta = 587;

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

                    emailEnviado = Mandar_Mail.Estado;
                    string msg = Mandar_Mail.Mensagem;

                    tentativas++;
                }
                catch (Exception e)
                {
                    if (tentativas == lstEmailConfig.Count)
                        throw e;
                }

                if (emailEnviado)
                    break;
            }

            return emailEnviado;
        }


        /// <summary>
        /// EnviarEmailAnexo
        /// </summary>
        /// <param name="email"></param>
        /// <param name="texto"></param>
        /// <param name="assunto"></param>
        /// <param name="lstAnexos"></param>
        /// <returns></returns>
        public bool EnviarEmailAnexo(string email, string texto, string assunto, List<Attachment> lstAnexos)
        {
            Email Mandar_Mail = null;
            bool emailEnviado = false;
            int tentativas = 0;

            if (Dominio.AppState != Dominio.ApplicationState.Producao)
                email = "univagnpd@gmail.com";

            EmbaralharListaEmail(lstEmailConfig);

            foreach (var item in lstEmailConfig)
            {
                try
                {
                    Mandar_Mail = new Email();

                    Usuario = item.Usuario;
                    Senha = item.Senha;
                    Smtp = "smtp.gmail.com";
                    SmtpPorta = 587;

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

                    Mandar_Mail.Send(Mandar_Mail, lstAnexos);


                    emailEnviado = Mandar_Mail.Estado;
                    string msg = Mandar_Mail.Mensagem;

                    tentativas++;

                }
                catch (Exception e)
                {
                    if (tentativas == lstEmailConfig.Count)
                        throw e;
                }

                if (emailEnviado)
                    break;

            }

            if (lstAnexos != null && lstAnexos.Any())
            {
                foreach (var anexo in lstAnexos)
                {
                    anexo.Dispose();
                }
            }

            return emailEnviado;
        }


        /// <summary>
        /// EnviarEmailString
        /// </summary>
        /// <param name="email"></param>
        /// <param name="texto"></param>
        /// <param name="assunto"></param>
        /// <returns></returns>
        public string[] EnviarEmailString(string email, string texto, string assunto)
        {
            Email Mandar_Mail = null;
            bool emailEnviado = false;
            int tentativas = 0;
            string msg = "[SUCESSO]";
            string emailRemetente = "";
            string textoEnviado = "";

            if (Dominio.AppState != Dominio.ApplicationState.Producao)
                email = "univagnpd@gmail.com";

            EmbaralharListaEmail(lstEmailConfig);

            foreach (var item in lstEmailConfig)
            {
                try
                {
                    Mandar_Mail = new Email();

                    Usuario = item.Usuario;
                    Senha = item.Senha;
                    Smtp = "smtp.gmail.com";
                    SmtpPorta = 587;

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

                    emailEnviado = Mandar_Mail.Estado;
                    msg += Mandar_Mail.Mensagem;

                    emailRemetente += item.Usuario;
                    textoEnviado = texto;

                    tentativas++;
                }
                catch (Exception e)
                {
                    msg = e.Message;
                    if (tentativas == lstEmailConfig.Count)
                        throw e;
                }

                if (emailEnviado)
                    break;

            }

            return new string[3] { msg, emailRemetente, textoEnviado };
        }


        /// <summary>
        /// EnviarEmailAnexoString
        /// </summary>
        /// <param name="email"></param>
        /// <param name="texto"></param>
        /// <param name="assunto"></param>
        /// <param name="lstAnexos"></param>
        /// <returns></returns>
        public string[] EnviarEmailAnexoString(string email, string texto, string assunto, List<Attachment> lstAnexos)
        {
            Email Mandar_Mail = null;
            bool emailEnviado = false;
            int tentativas = 0;
            string msg = "[SUCESSO]";
            string emailRemetente = "";
            string textoEnviado = "";

            if (Dominio.AppState != Dominio.ApplicationState.Producao)
                email = "univagnpd@gmail.com";

            EmbaralharListaEmail(lstEmailConfig);

            foreach (var item in lstEmailConfig)
            {
                try
                {
                    Mandar_Mail = new Email();

                    Usuario = item.Usuario;
                    Senha = item.Senha;
                    Smtp = "smtp.gmail.com";
                    SmtpPorta = 587;

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

                    Mandar_Mail.Send(Mandar_Mail, lstAnexos);

                    emailEnviado = Mandar_Mail.Estado;
                    msg += Mandar_Mail.Mensagem;

                    emailRemetente += item.Usuario;
                    textoEnviado = texto;

                    tentativas++;
                }
                catch (Exception e)
                {
                    msg = e.Message;
                    if (tentativas == lstEmailConfig.Count)
                        throw e;
                }

                if (emailEnviado)
                    break;

            }

            if (lstAnexos != null && lstAnexos.Any())
            {
                foreach (var anexo in lstAnexos)
                {
                    anexo.Dispose();
                }
            }

            return new string[3] { msg, emailRemetente, textoEnviado };
        }


        /// <summary>
        /// EnviarEmailRematricula
        /// </summary>
        /// <param name="email"></param>
        /// <param name="texto"></param>
        /// <param name="assunto"></param>
        /// <returns></returns>
        public Email EnviarEmailRematricula(string email, string texto, string assunto)
        {
            Email Mandar_Mail = null;
            bool emailEnviado = false;
            int tentativas = 0;

            email = email.ToLower().Trim();

            if (Dominio.AppState != Dominio.ApplicationState.Producao)
                email = "univagnpd@gmail.com";

            EmbaralharListaEmail(lstEmailConfig);

            foreach (var item in lstEmailConfig)
            {
                try
                {
                    Mandar_Mail = new Email();

                    Usuario = item.Usuario;
                    Senha = item.Senha;
                    Smtp = "smtp.gmail.com";
                    SmtpPorta = 587;

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

                    emailEnviado = Mandar_Mail.Estado;
                    string msg = Mandar_Mail.Mensagem;

                    tentativas++;

                }
                catch (Exception e)
                {
                    if (tentativas == lstEmailConfig.Count)
                        throw e;
                }

                if (emailEnviado)
                    break;
            }

            return Mandar_Mail;
        }


        /// <summary>
        /// EmbaralharListaEmail
        /// </summary>
        /// <param name="lista"></param>
        /// Método que embaralha a ordem dos elementos da lista
        void EmbaralharListaEmail(List<EmailConfig> lista)
        {
            // cria um objeto da classe Random
            Random rnd = new Random();

            //embaralha
            for (int i = 0; i < lista.Count; i++)
            {
                int a = rnd.Next(lista.Count);
                EmailConfig temp = lista[i];
                lista[i] = lista[a];
                lista[a] = temp;
            }
        }


        /// <summary>
        /// LerTemplateLocal
        /// </summary>
        /// <param name="caminhoArquivoTemplate"></param>
        /// <returns></returns>
        public string LerTemplateLocal(string caminhoArquivoTemplate)
        {
            var path = HttpContext.Current.Server.MapPath(caminhoArquivoTemplate);

            return File.ReadAllText(path);
        }


        /// <summary>
        /// LerTemplateLocalReal
        /// </summary>
        /// <param name="caminhoArquivoTemplate"></param>
        /// <returns></returns>
        public string LerTemplateLocalReal(string caminhoArquivoTemplate)
        {
            return File.ReadAllText(caminhoArquivoTemplate);
        }


        /// <summary>
        /// UtilMensageria
        /// </summary>
        public UtilMensageria()
        {
            lstEmailConfig = new List<EmailConfig>();

            lstEmailConfig.Add(new EmailConfig() { Usuario = "univagnpd@gmail.com", Senha = "@npd2015" });
            lstEmailConfig.Add(new EmailConfig() { Usuario = "sisunivag@univag.edu.br", Senha = "#NTIC2016sis!" });
            lstEmailConfig.Add(new EmailConfig() { Usuario = "sisunivag01@univag.edu.br", Senha = "#NTIC2016sis!" });
            lstEmailConfig.Add(new EmailConfig() { Usuario = "sisunivag02@univag.edu.br", Senha = "#NTIC2016sis!" });
            lstEmailConfig.Add(new EmailConfig() { Usuario = "sisunivag03@univag.edu.br", Senha = "#NTIC2016sis!" });
            lstEmailConfig.Add(new EmailConfig() { Usuario = "sisunivag04@univag.edu.br", Senha = "#NTIC2016sis!" });
            lstEmailConfig.Add(new EmailConfig() { Usuario = "sisunivag05@univag.edu.br", Senha = "#NTIC2016sis!" });
            lstEmailConfig.Add(new EmailConfig() { Usuario = "sisunivag06@univag.edu.br", Senha = "#NTIC2016sis!" });
            lstEmailConfig.Add(new EmailConfig() { Usuario = "sisunivag07@univag.edu.br", Senha = "#NTIC2016sis!" });
            lstEmailConfig.Add(new EmailConfig() { Usuario = "sisunivag08@univag.edu.br", Senha = "#NTIC2016sis!" });
            lstEmailConfig.Add(new EmailConfig() { Usuario = "sisunivag09@univag.edu.br", Senha = "#NTIC2016sis!" });
            lstEmailConfig.Add(new EmailConfig() { Usuario = "sisunivag10@univag.edu.br", Senha = "#NTIC2016sis!" });
        }

    }
}