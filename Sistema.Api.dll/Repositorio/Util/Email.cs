using Sistema.Api.dll.Src.Repositorio.BE;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

#pragma warning disable CS0168
namespace Sistema.Api.dll.Repositorio.Util
{
    /// <summary>
    ///  Author......: Michael S. Lopes , Leandro Curioso
    ///  Date........: 08/01/2014
    ///  Description.: Classe resposavel pelo envio de e-mails.
    /// </summary>
    public class Email
    {
        private bool estado;
        private string mensagem;
        private string remetente;
        private string responder;
        private string destinatario;
        private string copia_oculto;
        private MailPriority prioridade;
        private Encoding codificacao_assunto;
        private Encoding codificacao_corpo;
        private string assunto;
        private bool corpo_html;
        private string corpo;
        private string smtp_servidor;
        private string smtp_usuario;
        private string smtp_senha;
        private int smtp_port;
        private EmailExceptionBE EmailExceptionBe;

        public Email()
        {
        }

        public Boolean Estado
        {
            get { return this.estado; }
            set { this.estado = value; }
        }

        public String Mensagem
        {
            get { return this.mensagem; }
            set { this.mensagem = value; }
        }

        public String Remetente
        {
            get { return this.remetente; }
            set { this.remetente = value; }
        }

        public String Responder
        {
            get { return this.responder; }
            set { this.responder = value; }
        }

        public String Destinatario
        {
            get { return this.destinatario; }
            set { this.destinatario = value; }
        }

        public String Copia_Oculto
        {
            get { return this.copia_oculto; }
            set { this.copia_oculto = value; }
        }

        public MailPriority Prioridade
        {
            get { return this.prioridade; }
            set { this.prioridade = value; }
        }

        public Encoding Codificacao_Assunto
        {
            get { return this.codificacao_assunto; }
            set { this.codificacao_assunto = value; }
        }

        public Encoding Codificacao_Corpo
        {
            get { return this.codificacao_corpo; }
            set { this.codificacao_corpo = value; }
        }

        public String Assunto
        {
            get { return this.assunto; }
            set { this.assunto = value; }
        }

        public Boolean Corpo_Html
        {
            get { return this.corpo_html; }
            set { this.corpo_html = value; }
        }

        public String Corpo
        {
            get { return this.corpo; }
            set { this.corpo = value; }
        }

        public String Smtp_Servidor
        {
            get { return this.smtp_servidor; }
            set { this.smtp_servidor = value; }
        }

        public String Smtp_Usuario
        {
            get { return this.smtp_usuario; }
            set { this.smtp_usuario = value; }
        }

        public Int32 Smtp_Port
        {
            get { return this.smtp_port; }
            set { this.smtp_port = value; }
        }

        public string Smtp_Senha
        {
            get { return this.smtp_senha; }
            set { this.smtp_senha = value; }
        }


        /// <summary>
        /// Enviar email sem anexo
        /// </summary>
        /// <param name="pMail"></param>
        public void Send(Email pMail)
        {
            try
            {
                pMail.Estado = true;

                // Define o Mail
                var Mail = new MailMessage
                {
                    From = new MailAddress(pMail.Remetente),
                    Priority = pMail.Prioridade,
                    IsBodyHtml = pMail.Corpo_Html,
                    Subject = pMail.Assunto,
                    Body = pMail.Corpo,
                    SubjectEncoding = pMail.Codificacao_Assunto,
                    BodyEncoding = pMail.Codificacao_Corpo
                };


                // Define os destinatarios do Mail
                string[] vDestinatario = pMail.Destinatario.Split(new char[] { ',', ';' });

                int vQuantidade_Destinatario = vDestinatario.Length;

                for (int i = 0; i < vQuantidade_Destinatario; i++)
                {
                    Mail.To.Add(vDestinatario[i].ToString());
                }


                // Instancia o SmtpClient
                var Smtp = new SmtpClient
                {
                    Port = Smtp_Port,
                    EnableSsl = true,
                    Host = pMail.Smtp_Servidor,
                    Credentials = new NetworkCredential(pMail.Smtp_Usuario, pMail.Smtp_Senha)
                };

                
                // Envia
                Smtp.Send(Mail);
            }
            catch (Exception ex)
            {
                pMail.Estado = false;
                pMail.Mensagem = ex.Message;

                EmailExceptionBe = new EmailExceptionBE();

                EmailExceptionBe.GravarErroEmail(remetente, destinatario, ex.Message);
            }
            finally
            {
                if (EmailExceptionBe != null)
                    EmailExceptionBe.FecharConexao();
            }
        }


        /// <summary>
        /// Enviar email com anexo
        /// </summary>
        /// <param name="pMail"></param>
        /// <param name="Anexos"></param>
        public void Send(Email pMail, List<Attachment> Anexos)
        {
            int vQuantidade_Destinatario;
            int vQuantidade_Copia_Oculto;
            string[] vDestinatario;
            string[] vCopia_Oculto;

            try
            {
                MailMessage Mail = new MailMessage();
                SmtpClient Smtp = new SmtpClient();

                Mail.From = new MailAddress(pMail.Remetente);
                //Mail.ReplyTo             = new MailAddress(pMail.Responder);
                vDestinatario = pMail.Destinatario.Split(new char[] { ',', ';' });
                vQuantidade_Destinatario = vDestinatario.Length;

                for (int i = 0; i < vQuantidade_Destinatario; i++)
                {
                    Mail.To.Add(vDestinatario[i].ToString());
                }


                Mail.Priority = pMail.Prioridade;
                Mail.IsBodyHtml = pMail.Corpo_Html;
                Mail.Subject = pMail.Assunto;
                Mail.Body = pMail.Corpo;
                Mail.SubjectEncoding = pMail.Codificacao_Assunto;
                Mail.BodyEncoding = pMail.Codificacao_Corpo;

                for (int i = 0; i < Anexos.Count; i++)
                {
                    Mail.Attachments.Add(Anexos[i]);
                }

                Smtp.Port = Smtp_Port;
                Smtp.EnableSsl = true;
                Smtp.Host = pMail.Smtp_Servidor;
                Smtp.Credentials = new NetworkCredential(pMail.Smtp_Usuario, pMail.Smtp_Senha);
                pMail.Estado = true;
                Smtp.Send(Mail);

            }
            catch (Exception exError)
            {
                pMail.Estado = false;
                pMail.Mensagem = exError.Message;
                EmailExceptionBe = new EmailExceptionBE();
                EmailExceptionBe.GravarErroEmail(remetente, destinatario, exError.Message);

            }
            finally
            {
                if (EmailExceptionBe != null)
                    EmailExceptionBe.FecharConexao();
            }
        }
   }       
}