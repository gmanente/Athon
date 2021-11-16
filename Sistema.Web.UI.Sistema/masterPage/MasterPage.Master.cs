using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src;
using Sistema.Api.dll.Src.Comum.BE;
using Sistema.Api.dll.Src.Comum.VO;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;


namespace Sistema.Web.UI.Sistema.masterPage
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        public bool Login { get; set; }
        public string UrlCompleta { get; set; }
        public string UrlSemQueryString { get; set; }
        public string[] Urls { get; set; }
        public string PaginaAtual { get; set; }
        public UsuarioCampusVO UsuarioCampusVo { get; set; }
        public List<UsuarioModuloVO> lstUsuarioModuloVo { get; set; }
        public List<UsuarioSubModuloVO> lstUsuarioSubModuloVo { get; set; }
        public List<UsuarioCampusVO> LstUsuarioCampusVO { get; set; }
        public bool erroInicial { get; set; }
        public string erroMensagem { get; set; }


        //Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            UsuarioCampusBE usuarioCampusBE = null;

            try
            {
                usuarioCampusBE = new UsuarioCampusBE();


                // --------------- Verifica e recupera a sessão do usuário no sistema
                var sessaoSistema = VerificarRenovarSessao(usuarioCampusBE.GetSqlCommand());


                // --------------- Verifica se o usuário possui a Senha Padrão
                VerificarSenhaPadrao(sessaoSistema);


                // --------------- Recupera a foto do usuário
                string fotoUsuario = RecuperarFotoUsuario(sessaoSistema.LoginNome);


                // --------------- Define o UsuarioCampusVo de acrodo com a sessão sessaoSistema
                UsuarioCampusVo = new UsuarioCampusVO() {
                    Id = sessaoSistema.IdUsuarioCampus,
                    Usuario = {
                        Id = sessaoSistema.IdUsuario,
                        Nome = sessaoSistema.NomeUsuario,
                        Foto = fotoUsuario
                    },
                    Campus =
                    {
                        Id = sessaoSistema.IdCampus,
                        Nome = sessaoSistema.NomeCampus
                    },
                    AcessoExterno = sessaoSistema.AcessoExterno
                };

                sessaoSistema.AcessoExterno = false;

                // --------------- Recupera e Define os Urls e a página atual
                GetPagina();


                // --------------- Monta o menu do sistema para o usuário
                MontarMenu(sessaoSistema, usuarioCampusBE);

            }
            catch(Exception ex)
            {
                erroInicial = true;

                erroMensagem = ex.Message;
            }
            finally
            {
                if (usuarioCampusBE != null)
                    usuarioCampusBE.FecharConexao();
            }
        }


        /// <summary>
        /// VerificarRenovarSessao
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public SessaoSistema VerificarRenovarSessao(dynamic conn)
        {
            try
            {
                var auditoriaBE = new AuditoriaBE(conn);


                // --------------- Recupera e define o appState
                string appState = Dominio.AppState.ToString();


                // --------------- Recupera e Define a sessaoSistema
                var sessaoSistema = SessionHandler.GetSession<SessaoSistema>("Session");


                // --------------- Se não encontrar a sessaoSistema
                if (sessaoSistema == null)
                {
                    // ----- Redireciona para o login
                    Response.Redirect("Login.aspx?status=sessao-expirada", true);
                }


                // --------------- Define o chaveUnica
                Guid chaveUnica = Guid.NewGuid();


                // --------------- Recupera o chaveUnicaCookie
                var chaveUnicaCookie = Request.Cookies["chaveUnica"];


                // --------------- Se o chaveUnicaCookie for nulo
                if (chaveUnicaCookie == null)
                {
                    // ----- Redireciona para o login
                    Response.Redirect("Login.aspx?status=sessao-expirada&error=chave-ausente", true);
                }


                chaveUnica = Guid.Parse(chaveUnicaCookie.Value);


                // --------------- Se o AppState for Debug ou Teste
                if (appState == "Debug" || appState == "Teste")
                {
                    return sessaoSistema;
                }


                // --------------- Define o idUsuario
                long idUsuario = sessaoSistema.IdUsuario;


                // --------------- Consulta o AuditoriaVO
                var auditoriaVO = auditoriaBE.Consultar(new AuditoriaVO() { IdUsuario = idUsuario });


                // --------------- Se não encontrar o AuditoriaVO
                if (auditoriaVO == null)
                {
                    throw new Exception("Não foi possível recuperar a auditoria.");
                }


                // --------------- Verifica se a sessão é válida
                bool sessaoValida = auditoriaBE.SessaoValida(auditoriaVO.DataWho);


                // --------------- Se a sessão não for válida
                if (sessaoValida == false)
                {
                    // ----- Redireciona para o login
                    Response.Redirect("Login.aspx?status=sessao-expirada&error=tempo-sessao", true);
                }

                // --------------- Se a chave única for diferente
                if (Dominio.UnicaSessao && chaveUnica != auditoriaVO.ChaveUnica)
                {
                    // ----- Redireciona para o login
                    Response.Redirect("Login.aspx?status=sessao-expirada&error=sessao-concorrente", true);
                }


                return sessaoSistema;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// VerificarSenhaPadrao
        /// </summary>
        /// <param name="sessaoSistema"></param>
        private void VerificarSenhaPadrao(SessaoSistema sessaoSistema)
        {
            bool trocarSenhaPadrao = sessaoSistema.TrocarSenhaPadrao;

            string status = Request.QueryString["status"];


            // ---------- Se foi utilizado a senha padrão, redireciona para forçar a mudança da senha
            if (trocarSenhaPadrao == true && status != "trocarSenhaPadrao")
            {
                Response.Redirect("Principal.aspx?status=trocarSenhaPadrao" , true);
            }
        }



        /// <summary>
        /// GetPagina
        /// </summary>
        public void GetPagina()
        {
            UrlCompleta = Request.Url.ToString();

            UrlSemQueryString = new Uri(UrlCompleta).GetLeftPart(UriPartial.Path);

            Urls = UrlSemQueryString.Split(new Char[] { '/' });

            PaginaAtual = Urls[3];

            Login = Request.QueryString["login"] != null ? true : false;
        }


        /// <summary>
        /// MontarMenu
        ///  Monta o menu do sistema
        /// </summary>
        private void MontarMenu(SessaoSistema sessaoSistema, dynamic conn)
        {
            try
            {
                var usuarioModuloBE = new UsuarioModuloBE(conn.GetSqlCommand());

                // ---------- Autentica os Modulos do usuario
                lstUsuarioModuloVo = usuarioModuloBE.AutenticarModulos(sessaoSistema.IdUsuario, 0, sessaoSistema.AcessoExterno, false);
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
            var imgPath = "";

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
    }
}