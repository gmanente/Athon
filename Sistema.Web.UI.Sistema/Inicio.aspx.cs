using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistema.Web.UI.Sistema
{
    public partial class Inicio : System.Web.UI.Page
    {
        public UsuarioCampusVO UsuarioCampusVo { get; set; }
        public bool ErroInicial { get; set; }
        public string ErroMensagem { get; set; }
        public string Ambiente { get; set; }
        public List<UsuarioModuloVO> LstMenuVo { get; set; }
        public List<MenuRapidoVO> LstMenuRapidoVO { get; set; }
        public List<MenuRapidoItemVO> LstMenuRapidoItemVO { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoginBE be = null;

            try
            {
                be = new LoginBE();

                var conn = be.GetSqlCommand();


                // --------------- Verifica se foi solicitado o Logout do usuário no sistema
                ChecarLogout(be);


                // --------------- Verifica e recupera a Sessão do usuário no sistema
                var sessaoSistema = be.VerificarRenovarSessaoSistema();


                // --------------- Verifica a Senha padrão do usuário no sistema
                ChecarSenhaPadrao(sessaoSistema.TrocarSenhaPadrao);


                Ambiente = Dominio.AppState.ToString();


                // --------------- Define o UsuarioCampusVo de acordo com a sessão sessaoSistema
                UsuarioCampusVo = new UsuarioCampusVO()
                {
                    Id = sessaoSistema.IdUsuarioCampus,
                    Usuario = {
                        Id = sessaoSistema.IdUsuario,
                        Nome = sessaoSistema.NomeUsuario
                    },
                    Campus =
                    {
                        Id = sessaoSistema.IdCampus,
                        Nome = sessaoSistema.NomeCampus
                    },
                    AcessoExterno = sessaoSistema.AcessoExterno
                };


                // --------------- Recupera a foto do usuário
                UsuarioCampusVo.Usuario.Foto = RecuperarFotoUsuario(sessaoSistema.LoginNome);


                // --------------- Monta o Menu Lateral do sistema para o usuário
                MontarMenu(sessaoSistema.IdUsuario, conn);


                // --------------- Monta o Menu Rápido do sistema para o usuário
                //MontarMenuRapido(sessaoSistema.IdUsuario, conn);

            }
            catch (Exception ex)
            {
                ErroInicial = true;

                ErroMensagem = ex.Message;
            }
            finally
            {
                if (be != null)
                    be.FecharConexao();
            }
        }


        /// <summary>
        /// ChecarSenhaPadrao
        /// </summary>
        /// <param name="trocarSenhaPadrao"></param>
        private void ChecarSenhaPadrao(bool trocarSenhaPadrao)
        {
            // ---------- Se foi utilizado a senha padrão, redireciona para forçar a mudança da senha
            if (trocarSenhaPadrao == true &&  Request.QueryString["acao"] != "trocarSenhaPadrao")
            {
                Response.Redirect("Principal.aspx?acao=trocarSenhaPadrao", true);
            }
        }


        /// <summary>
        /// MontarMenu
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="conn"></param>
        private void MontarMenu(long idUsuario, dynamic conn)
        {
            try
            {
                var be = new UsuarioModuloBE(conn);

                // ---------- Autentica os Modulos do usuario
                LstMenuVo = be.AutenticarModulos(idUsuario, 0, false, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// MontarMenuRapido
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="conn"></param>
        private void MontarMenuRapido(long idUsuario, dynamic conn)
        {
            try
            {
                var be = new MenuRapidoItemBE(conn);

                LstMenuRapidoVO = new List<MenuRapidoVO>();

                if (idUsuario == 0) return;

                LstMenuRapidoItemVO = be.AutenticarMenuRapido(idUsuario, 0, false, false);

                if (LstMenuRapidoItemVO != null)
                {
                    foreach (var menu in LstMenuRapidoItemVO)
                    {
                        bool jaExiste = false;

                        if (LstMenuRapidoVO.Count > 0)
                        {
                            jaExiste = LstMenuRapidoVO.Any(m => m.Id == menu.MenuRapido.Id);

                            if (!jaExiste)
                                LstMenuRapidoVO.Add(menu.MenuRapido);
                        }
                        else
                        {
                            LstMenuRapidoVO.Add(menu.MenuRapido);
                        }
                    }

                    LstMenuRapidoVO = LstMenuRapidoVO.OrderBy(x => x.Ordem).ThenBy(x => x.Descricao).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// ChecarLogout
        /// </summary>
        /// <param name="be"></param>
        private void ChecarLogout(LoginBE be)
        {
            try
            {
                if (Request.QueryString["acao"] == "logout")
                {
                    be.RemoverCookieSessaoSistema();


                    Response.Redirect("LoginSistema.aspx?status=logout", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// RecuperarFotoUsuario
        /// </summary>
        /// <returns></returns>
        public string RecuperarFotoUsuario(string nomeLogin)
        {
            var imgPath = "img/avatars/avatar.svg";

            string imageFile = "img/avatars/" + nomeLogin + ".jpg";

            if (File.Exists(Server.MapPath(imageFile)))
            {
                imgPath = imageFile;
            }


            return imgPath;
        }
    }
}