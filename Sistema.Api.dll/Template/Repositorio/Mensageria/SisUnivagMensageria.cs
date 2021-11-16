using System;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src;

namespace Sistema.Api.dll.Template.Repositorio.Mensageria
{
    public class SisUnivagMensageria
    {
        public UtilMensageria UtilMensageriaInsc { get; set; }

        public SisUnivagMensageria()
        {
            UtilMensageriaInsc = new UtilMensageria
            {
                Remetente = "gmanente@gmail.com",
                Smtp = "smtp.gmail.com",
                Usuario = Dominio.EmailSistema,
                Senha = Dominio.SenhaEmailSistema,
                SmtpPorta = 587
            };
        }


        //Enviar email recuperação de senha
        public bool EnviarEmailRecuperacaoSenha(string email, string diasExpiracaoSenha, string nome, string login, string novaSenha)
        {
            var textoBruto = UtilMensageriaInsc.LerTemplateLocal(@"Template/RecuperacaoSenhaTemplate.html");

            textoBruto = textoBruto.Replace("#{NOME}", nome.Trim());
            textoBruto = textoBruto.Replace("#{DATA}", DateTime.Now.ToString());
            textoBruto = textoBruto.Replace("#{SISTEMA}", "SISUnivag");
            textoBruto = textoBruto.Replace("#{DIASEXPIRACAOSENHA}", diasExpiracaoSenha);
            textoBruto = textoBruto.Replace("#{LOGIN}", login);
            textoBruto = textoBruto.Replace("#{NOVASENHA}", novaSenha);
            textoBruto = textoBruto.Replace("#{ANO}", DateTime.Now.Year.ToString());


            return UtilMensageriaInsc.EnviarEmail(email, textoBruto, "Solicitação de Recuperação de Senha");
        }


        //Enviar email alteração de senha
        public bool EnviarEmailAlteracaoSenha(string email, string nome, string login, string novaSenha)
        {
            var textoBruto = UtilMensageriaInsc.LerTemplateLocal(@"Template/AlteracaoSenhaTemplate.html");

            textoBruto = textoBruto.Replace("#{NOME}", nome);
            textoBruto = textoBruto.Replace("#{DATA}", DateTime.Now.ToString());
            textoBruto = textoBruto.Replace("#{SISTEMA}", "SISUnivag");
            textoBruto = textoBruto.Replace("#{LOGIN}", login);
            textoBruto = textoBruto.Replace("#{NOVASENHA}", novaSenha);
            textoBruto = textoBruto.Replace("#{ANO}", DateTime.Now.Year.ToString());

            return UtilMensageriaInsc.EnviarEmail(email, textoBruto, "Alteração de Senha");
        }

    }
}