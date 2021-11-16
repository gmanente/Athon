using System;
using System.IO;
using System.Web;
using System.Web.Services;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;

namespace Sistema.Web.UI.Sistema
{
    public partial class MeuPerfil : CommonPage
    {
        // Método Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // Método SelecionaUsuario
        // Seleciona as informações do usuário
        public UsuarioVO SelecionaUsuario()
        {
            UsuarioBE usuarioBE = null;
            try
            {
                usuarioBE = new UsuarioBE();
                var usuarioVO =  usuarioBE.Consultar(new UsuarioVO()
                {
                    Id = GetSessao().IdUsuario
                });

                return usuarioVO;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (usuarioBE != null)
                    usuarioBE.FecharConexao();
            }

        }


        // Método AvatarUsuario
        // Retorna a imagem do usuário
        public string AvatarUsuario()
        {
            var imgPath = "";

            string nomeLogin = SelecionaUsuario().NomeLogin;

            string imageFile = "img/avatars/" + nomeLogin + ".jpg";

            if (File.Exists(HttpContext.Current.Server.MapPath(imageFile)))
            {
                imgPath = imageFile;
            }
            else
            {
                imgPath = "img/avatars/avatar.svg"; ;
            }

            return imgPath;
        }


        // WebMétodo EditarInformacoesUsuario
        // Edita as Informacoes do Usuario
        [WebMethod]
        public static string EditarInformacoesUsuario(
            string dataNascimento,
            string telefone,
            string celular,
            string emailAtual,
            string email,
            string senhaAtual = "",
            string senha = "",
            string senhaR = "")
        {
            RenovarChecarSessao();

            var ajax = new Ajax();

            UsuarioSenhaVO usuarioSenhaVo = null;
            UsuarioBE usuarioBE = null;
            UsuarioSenhaBE usuarioSenhaBE = null;

            try
            {
                usuarioBE = new UsuarioBE();
                usuarioSenhaBE = new UsuarioSenhaBE(usuarioBE.GetSqlCommand());

                // Consulta o usuário
                usuarioSenhaVo = usuarioSenhaBE.Consultar(new UsuarioSenhaVO()
                {
                    IdUsuario = GetSessao().IdUsuario
                });

                // Se a senha atual não conferir
                //if (usuarioSenhaVo == null || usuarioSenhaVo.Senha.Equals(Criptografia.MD5(senhaAtual)) == false)
                if (usuarioSenhaVo == null || usuarioSenhaVo.Senha.Equals(senhaAtual) == false)
                {
                    throw new Exception("A senha não confere. Por favor informe novamente a sua senha atual.");
                }

                // Verifica se o email informado é diferente do atual
                if (email != emailAtual)
                {
                    // Verifica o novo e-mail do usuário
                    if (email != null)
                    {
                        usuarioBE.VerificarEmail(email, GetSessao().IdUsuario);
                    }
                }

                // Se foi solicitado nova senha
                if (senha != "")
                {
                    // Se a nova senha é diferente da senha repetida
                    if (senha != senhaR)
                    {
                        throw new Exception("A senha de repetição é diferente da nova senha. Por favor informe senhas iguais.");
                    }

                    // Seleciona os dados do usuário
                    UsuarioVO usuarioVo = usuarioBE.Consultar(new UsuarioVO() { Id = GetSessao().IdUsuario });

                    // Verifica as ultimas senhas gravadas
                    usuarioSenhaBE.VerificarUltimasSenhas(
                        new UsuarioSenhaVO()
                        {
                            IdUsuario = GetSessao().IdUsuario
                        },
                        senha,
                        usuarioVo.Nome,
                        usuarioVo.NomeLogin,
                        usuarioVo.Email
                    );
                }

                // Se foi solicitado alteração de outros dados
                if (dataNascimento != null || telefone != null || celular != null || email != null)
                {
                    UsuarioVO usuarioVO = new UsuarioVO();

                    usuarioVO.Id = GetSessao().IdUsuario;
                    usuarioVO.Telefone = telefone;
                    usuarioVO.Celular = celular;
                    usuarioVO.Email = email;
                    if (dataNascimento != "") usuarioVO.DataNascimento = Convert.ToDateTime(dataNascimento);

                    usuarioBE.AlterarCompleto(usuarioVO);
                }

                // Sucesso
                ajax.StatusOperacao = true;
                ajax.ObjMensagem = "Informações do usuário atualizadas com sucesso!";
            }
            catch (Exception ex)
            {
                // Erro
                ajax.StatusOperacao = false;
                ajax.ObjMensagem = ex.Message;
            }
            finally
            {
                if (usuarioBE != null)
                    usuarioBE.FecharConexao();
            }

            System.Threading.Thread.Sleep(1000);

            return ajax.GetAjaxJson();
        }

    }
}