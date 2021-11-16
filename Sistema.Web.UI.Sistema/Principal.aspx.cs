using System;
using System.Collections.Generic;
using System.Linq;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src;
using Sistema.Api.dll.Src.Comum.BE;
using Sistema.Api.dll.Src.Comum.VO;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;

namespace Sistema.Web.UI.Sistema
{
    public partial class Principal : CommonPage
    {
        public bool Login { get; set; }
        public string UrlCompleta { get; set; }
        public string UrlSemQueryString { get; set; }
        public string[] Urls { get; set; }
        public string PaginaAtual { get; set; }
        public UsuarioCampusVO UsuarioCampusVo { get; set; }
        public List<MenuRapidoVO> lstMenuRapidoVO { get; set; }
        public List<MenuRapidoItemVO> lstMenuRapidoItemVO { get; set; }
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

                // --------------- Define o UsuarioCampusVo de acrodo com a sessão sessaoSistema
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

                sessaoSistema.AcessoExterno = false;

                // --------------- Monta o menu do sistema para o usuário
                //MontarMenuRapido(sessaoSistema, usuarioCampusBE);

            }
            catch (Exception ex)
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
        /// MontarMenu
        ///  Monta o menu do sistema
        /// </summary>
        private void MontarMenuRapido(SessaoSistema sessaoSistema, dynamic conn)
        {
            try
            {
                var menuRapidoItemBE = new MenuRapidoItemBE(conn.GetSqlCommand());

                lstMenuRapidoItemVO = menuRapidoItemBE.AutenticarMenuRapido(sessaoSistema.IdUsuario, 0, sessaoSistema.AcessoExterno, false);

                if (lstMenuRapidoItemVO != null)
                {
                    lstMenuRapidoVO = new List<MenuRapidoVO>();             

                    foreach (var menu in lstMenuRapidoItemVO)
                    {
                        bool jaExiste = false;

                        if (lstMenuRapidoVO.Count > 0)
                        {
                            jaExiste = lstMenuRapidoVO.Any(m => m.Id == menu.MenuRapido.Id);

                            if (!jaExiste)
                                lstMenuRapidoVO.Add(menu.MenuRapido);
                        }
                        else
                        {
                            lstMenuRapidoVO.Add(menu.MenuRapido);
                        }
                    }

                    lstMenuRapidoVO = lstMenuRapidoVO.OrderBy(x => x.Ordem).ThenBy(x => x.Descricao).ToList(); ;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}