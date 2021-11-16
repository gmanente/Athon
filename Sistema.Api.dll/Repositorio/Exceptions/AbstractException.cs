using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Comum.BE.LogExceptionBE;
using Sistema.Api.dll.Src.Comum.VO.LogErroSistemaVO;
using System;
using System.Data.SqlClient;

#pragma warning disable CS0168, CS0649
namespace Sistema.Api.dll.Repositorio.Exceptions
{
    public abstract class AbstractException : ApplicationException
    {
        public const int Verde = 1;
        public const int Amarelo = 2;
        public const int Vermelho = 3;
        private LogErroSistemaVO InsLogErroSistema;
        private string NomeSistema;

        protected AbstractException()
            : base("Ocorreu um erro no sistema. Por favor procurar o setor NPD.")
        { }

        protected AbstractException(string mensagem)
            : base(mensagem)
        { }

        protected AbstractException(string mensagem, Exception exception, int idLotacao)
            : base(mensagem, exception)
        {
            CarregaLogErro(idLotacao, exception);
            InserirErroLog();
            //EnviarEmail();
        }

        private void CarregaLogErro(int idLotacao, Exception exception)
        {
            ModelException insModelException = null;
            try
            {
                insModelException = new ModelException(exception);
                InsLogErroSistema = new LogErroSistemaVO
                {
                    IdLotacao = idLotacao,
                    Gravidade = Vermelho,
                    IpMaquina = Ip.GetIp(),
                    NomeClasse = insModelException.Arquivo,
                    NomeMetodo = insModelException.Metodo,
                    Linha = insModelException.Linha,
                    CaminhoArquivo = insModelException.Caminho,
                    Status = 1,
                    Menssagem = insModelException.Mensagem,
                    DataHoraCadastro = DateTime.Now
                };
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        private void InserirErroLog()
        {
            LogErroSistemaBE lBe = null;

            try
            {
                lBe = new LogErroSistemaBE();
                lBe.InserirInserirLogErro(InsLogErroSistema);
            }
            catch (SqlException e)
            {

            }
            finally
            {
                lBe?.FecharConexao();
            }

        }

        private void EnviarEmail()
        {
            Email insEmail = null;
            try
            {
                insEmail = new Email();
                string emailStr = ManipulaArquivo.ArquivoToString("Template/Email/LogErro.html");
                string email = emailStr.Replace("%Sistema%", NomeSistema)
                    .Replace("%Gravidade%", Convert.ToString(InsLogErroSistema.Gravidade))
                    .Replace("%Ip%", InsLogErroSistema.IpMaquina)
                    .Replace("%NomeClasse%", InsLogErroSistema.NomeClasse)
                    .Replace("%NomeClasse%", InsLogErroSistema.NomeClasse)
                    .Replace("%NomeMetodo%", InsLogErroSistema.NomeMetodo)
                    .Replace("%Linha%", InsLogErroSistema.Linha)
                    .Replace("%CaminhoArquivo%", InsLogErroSistema.CaminhoArquivo)
                    .Replace("%DataHora%", InsLogErroSistema.DataHoraCadastro.ToString("G"))
                    .Replace("%Mensagem%", InsLogErroSistema.Menssagem);

                //insEmail.Assunto = "Erro no sistema " + NomeSistema;
                //insEmail.CodificacaoAssunto = new UTF8Encoding();
                //insEmail.CodificacaoCorpo = new UTF8Encoding();
                //insEmail.CorpoHtml = true;
                //insEmail.Corpo = email;
                //insEmail.Destinatario = "michael.s.lopes92@gmail.com";
                //insEmail.SmtpUsuario = "rematricula@univag.edu.br";
                //insEmail.Remetente = "rematricula@univag.edu.br";
                //insEmail.SmtpSenha = "!@univagweb@!";
                //insEmail.SmtpPort = 587;
                //insEmail.SmtpServidor = "smtp.gmail.com";

                insEmail.Send(insEmail);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}