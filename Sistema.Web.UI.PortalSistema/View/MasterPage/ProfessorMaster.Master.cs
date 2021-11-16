using Newtonsoft.Json;
using Sistema.Api.dll.Src.Biblioteca.BE;
using Sistema.Api.dll.Src.Biblioteca.VO;
using Sistema.Api.dll.Src.Coordenacao.BE;
using Sistema.Api.dll.Src.Comum.BE;
using Sistema.Api.dll.Src.Comum.VO;
using Sistema.Api.dll.Src.Repositorio.BE;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using Sistema.Web.UI.PortalProfessor.Util;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI.WebControls;
using Sistema.Api.dll.Src.Coordenacao.VO;
using Sistema.Api.dll.Src;

namespace Sistema.Web.UI.PortalProfessor.View.MasterPage
{
    public partial class ProfessorMaster : System.Web.UI.MasterPage
    {
        // Controle
        private static HttpContext _context;

        public string UrlCompleta { get; set; }
        public string UrlSemQueryString { get; set; }
        public string[] Urls { get; set; }
        private const int ConsPosicaoPaginalAtual = 3;
        public string PaginaAtual { get; set; }
        public List<UsuarioModuloVO> lstUsuarioModuloVo { get; set; }
        public List<UsuarioSubModuloVO> lstUsuarioSubModuloVo { get; set; }
        public static UsuarioCampusVO UsuarioCampusVo { get; set; }
        public int TempoRestanteSessao { get; set; }

        public static List<UsuarioFuncionalidadeVO> lstUsuarioFuncionalidade { get; set; }



        //Page load
        protected void Page_Load(object sender, EventArgs e)
        {
            ChecarSessao();


            GetUrlCompleta();
            GetUrlSemQueryString();
            GetUrlsArray();
            GetPaginaAtual();
            RenovarChecarSessao();
            //VerificarSenhaPadrao();
            MontarMenu();


            UsuarioFuncionalidadeBE usuarioFuncionalidadeBE = null;
            try
            {
                usuarioFuncionalidadeBE = new UsuarioFuncionalidadeBE();
                var idsCampusUsuario = ProfessorMaster.GetSession().IdsCampus.Split(',');
                lstUsuarioFuncionalidade = new List<UsuarioFuncionalidadeVO>();
                foreach (var idCampus in idsCampusUsuario)
                {
                    if (idCampus != "")
                    {
                        var lst = usuarioFuncionalidadeBE.AutenticarFuncionalidades(ProfessorMaster.GetUrlSubModulo(), ProfessorMaster.GetSession().IdUsuario,
                        Convert.ToInt32(idCampus), ProfessorMaster.GetSession().AcessoExterno);

                        lstUsuarioFuncionalidade.AddRange(lst);
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                if (usuarioFuncionalidadeBE != null)
                    usuarioFuncionalidadeBE.FecharConexao();

            }
           
          
        }


        // ChecarSessao
        /// <summary>
        /// Autor: Leandro Curioso
        /// Data: 07.06.2015
        /// Descrição: Responsável pelo carregamento no início da página
        /// </summary>
        /// <returns>Não há retorno</returns>
        public static void ChecarSessao()
        {
            try
            {
                if (HasSession())
                {
                    // Atualiza a sessão a cada page load
                    SessionHandler.RefreshSession("SessionPortalProfessor", Global.SessionCookieTimeout);
                }
                else
                {
                    // Redireciona para a página de Login
                    HttpContext.Current.Response.Redirect("~/View/Page/Login.aspx?status=sessao-expirada");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // HasSession
        /// <summary>
        /// Autor: Giovanni Ramos
        /// Data: 19.12.2017
        /// Descrição: Responsável por verificar a existência da sessão
        /// </summary>
        public static bool HasSession()
        {
            try
            {
                var hasSession = GetSession();

                return hasSession == null ? false : true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GetSession
        /// <summary>
        /// Autor: Giovanni Ramos
        /// Data: 19.12.2017
        /// Descrição: Responsável por retornar a sessão
        /// </summary>
        /// <returns>Retorno dinâmico retornar a sessão</returns>
        public static SessionPortalProfessor GetSession()
        {
            try
            {
                return SessionHandler.GetSession<SessionPortalProfessor>("SessionPortalProfessor");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        //Verifica se é requisição Ajax
        /// <summary>
        /// Autor: Giovanni Ramos
        /// Data: 28/04/2016
        /// Descrição: Responsável em verificar se a requisição é do tipo Ajax
        /// </summary>
        /// <returns>bool</returns>
        public static bool IsAjaxRequest(HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            _context = HttpContext.Current;

            var isCallbackRequest = false; // Callbacks são solicitações Ajax 

            if (_context != null && _context.CurrentHandler != null && _context.CurrentHandler is System.Web.UI.Page)
                isCallbackRequest = ((System.Web.UI.Page)_context.CurrentHandler).IsCallback;

            return isCallbackRequest || (request["X-Requested-With"] == "XMLHttpRequest") || (request.Headers["X-Requested-With"] == "XMLHttpRequest");
        }


        /// <summary>
        /// Método responsável por checar e renovar a sessão;
        /// Autor: Carlos Cortez
        /// Date: 24/08/2015
        /// </summary>
        public static void RenovarChecarSessao()
        {
            AuditoriaBE AuditoriaBe = null;

            try
            {
                AuditoriaBe = new AuditoriaBE();
                var ConsultaBE = new ConsultaBE(AuditoriaBe.GetSqlCommand());


                _context = HttpContext.Current;
                var request = _context.Request;
                var response = _context.Response;
                var session = _context.Session;


                var sessao = SessionHandler.GetSession<SessionPortalProfessor>("SessionPortalProfessor");


                string SessionID = session.SessionID;
                long IdModulo = GetIdModulo();
                long IdSubModulo = GetIdSubModulo();
                long IdUsuario = 0;


                if (IsAjaxRequest(request))
                {
                    if (sessao == null)
                    {
                        response.Status = "401 Unauthorized access";
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        response.StatusDescription = "An error has occurred";
                        response.Headers.Add("AuthenticationStatus", "NotAuthorized");
                    }
                }


                if (sessao == null)
                {    
                    response.Redirect("~/View/Page/Login.aspx?status=sessao-expirada", true);
                }


                IdUsuario = sessao.IdUsuario;


                if (Dominio.UnicaSessao && !AuditoriaBe.SessaoAtiva(IdUsuario, request.UserHostAddress))
                {
                    SessionHandler.RemoveSession("SessionPortalProfessor");

                    response.Redirect("~/View/Page/Login.aspx?status=sessao-expirada", true);
                }


                SessionHandler.RefreshSession("SessionPortalProfessor", Global.SessionCookieTimeout);

                    
                if (IdModulo > 0 && IdSubModulo > 0)
                {
                    // --------------- Consulta o AuditoriaVO
                    var AuditoriaAtual = AuditoriaBe.Consultar(new AuditoriaVO() { IdUsuario = IdUsuario });


                    var AuditoriaVo = new AuditoriaVO()
                    {
                        Id = sessao.IdAuditoria,
                        DataLogin = AuditoriaAtual.DataLogin,
                        Login = AuditoriaAtual.Login,
                        Senha = AuditoriaAtual.Senha,
                        SessionId = SessionID,
                        ServerName = request.ServerVariables["SERVER_NAME"],
                        IdUsuario = IdUsuario,
                        EnderecoIp = request.UserHostAddress,
                        IdCampus = sessao.IdCampus,
                        BrowserVersao = request.Browser.Version.ToString(),
                        BrowserTipo = Convert.ToBoolean(request.Browser.Win32.ToString()) ? "Win32" : "Win64",
                        BrowserNome = request.Browser.Browser.ToString(),
                        IdModulo = IdModulo,
                        IdSubmodulo = IdSubModulo,
                        HostName = sessao.HostName,
                        ChaveUnica = AuditoriaAtual.ChaveUnica
                    };


                    // Setando o AuditoriaVO
                    AuditoriaBe.SetAuditoria(AuditoriaVo);


                    // Verifica se o usuario mantem a sessão no mesmo Modulo (Atualizando somente as Datas e Horas de Ação)
                    if (AuditoriaAtual.IdModulo == AuditoriaVo.IdModulo && AuditoriaAtual.IdSubmodulo == AuditoriaVo.IdSubmodulo)
                        AuditoriaBe.AuditarAcao(AuditoriaVo);
                    else
                        AuditoriaBe.Auditar(AuditoriaVo);
                }

            }
            catch (Exception ex)
            {
                //throw new Exception("~/View/Page/Login.aspx?status=sessao-expirada");
            }
            finally
            {
                if (AuditoriaBe != null)
                    AuditoriaBe.FecharConexao();
            }
        }

        
        //Get a url atual
        public void GetUrlCompleta()
        {
            UrlCompleta = Request.Url.ToString();
        }

        //Get url sem query string
        public void GetUrlSemQueryString()
        {
            UrlSemQueryString = new Uri(UrlCompleta).GetLeftPart(UriPartial.Path);
        }

        //Get url array
        public void GetUrlsArray()
        {
            Urls = UrlSemQueryString.Split(new Char[] { '/' });
        }

        //Get página atual
        public void GetPaginaAtual()
        {
            PaginaAtual = Urls[ConsPosicaoPaginalAtual];
        }


        //VerificarSenhaPadrao
        private void VerificarSenhaPadrao()
        {
            string status = Request.QueryString["status"];

            if (HttpContext.Current.Session["useDefaultPassword"] != null && HttpContext.Current.Session["useDefaultPassword"].ToString() == "s" && status != "trocarSenhaPadrao")
            {
                HttpContext.Current.Response.Redirect("Principal.aspx?status=trocarSenhaPadrao");
            }
        }

        public static string GetUrlSubModulo()
        {
            string url = HttpContext.Current.Request.Url.ToString();
            var UrlSemQueryString = new Uri(url).GetLeftPart(UriPartial.Path);
            var arrUrl = UrlSemQueryString.Split('/');
            string newUrl = "";

            for (var i = 0; i < arrUrl.Length; i++)
            {
                var urlUnit = arrUrl[i];

                if (i == 0)
                    newUrl += arrUrl[i] + "//";
                else if (i > 1)
                    newUrl += arrUrl[i] + "/";
            }
            var urlSubModulo = newUrl.Substring(0, newUrl.IndexOf("aspx") + 4);

            // Altera a urlSubModulo funcionais para atender multiplos protocolos
            // Data alteração: 20/07/2015
            // Alterado por: Evander
            urlSubModulo = urlSubModulo.Replace("https:", "").Replace("http:", "");

            return urlSubModulo;
        }


        /// <summary>
        /// GetIdModulo
        /// </summary>
        /// <returns></returns>
        public static long GetIdModulo()
        {
            ConsultaBE consultaBE = null;

            try
            {
                consultaBE = new ConsultaBE();

                return consultaBE.GetIdModulo(GetIdSubModulo());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (consultaBE != null)
                    consultaBE.FecharConexao();
            }
        }


        /// <summary>
        /// GetIdSubModulo
        /// </summary>
        /// <returns></returns>
        public static long GetIdSubModulo()
        {
            ConsultaBE consultaBE = null;

            try
            {
                consultaBE = new ConsultaBE();

                return consultaBE.GetIdSubModulo(GetUrlSubModulo());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (consultaBE != null)
                    consultaBE.FecharConexao();
            }
        }


        /// <summary>
        /// MontarMenu
        /// </summary>
        private void MontarMenu()
        {
            UsuarioModuloBE usuarioModuloBe = null;

            try
            {
                var sessao = GetSession();

                long idModulo = sessao.IdModuloLogado; //sessao.IdModulo; //27; //Portal Professor

                if (sessao != null)
                {
                    usuarioModuloBe = new UsuarioModuloBE();
                    var usuarioCampusBe = new UsuarioCampusBE(usuarioModuloBe.GetSqlCommand());


                    UsuarioCampusVo = usuarioCampusBe.Consultar(new UsuarioCampusVO() { Usuario = { Id = sessao.IdUsuario } });


                    lstUsuarioModuloVo = usuarioModuloBe
                        .AutenticarModulos(sessao.IdUsuario, UsuarioCampusVo.Campus.Id, sessao.AcessoExterno, sessao.Portal, idModulo)
                        .OrderBy(x=>x.Modulo.Nome).ToList();


                    foreach (var item in lstUsuarioModuloVo)
                    {
                        item.ListUsuarioSubModuloVO = item.ListUsuarioSubModuloVO
                            .Where(x => x.SubModulo.Modulo.Id == item.Modulo.Id)
                            .OrderBy(x=>x.SubModulo.Ordem).ThenBy(x=>x.SubModulo.Nome)
                            .ToList();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (usuarioModuloBe != null)
                    usuarioModuloBe.FecharConexao();
            }
        }


        // GetPortalBibliotecaAutenticacao
        public static string GetPortalBibliotecaAutenticacao()
        {
            string ret = string.Empty;
            PessoaBE pessoaBE = null;
            PessoaVO pessoaVO = null;

            try
            {
                var sessaoPortalProfessor = GetSession();

                pessoaBE = new PessoaBE();


                UsuarioBE usuarioBe = new UsuarioBE(pessoaBE.GetSqlCommand());
                var usuarioVO = usuarioBe.Consultar(new UsuarioVO() { Id = sessaoPortalProfessor.IdUsuario });
                if (usuarioVO == null)
                    throw new Exception("Usuário não encontrado!");

                ProfessorBE professorBe = new ProfessorBE(pessoaBE.GetSqlCommand());
                var professor = professorBe.Consultar(new ProfessorVO() { Usuario = { Id = usuarioVO.Id } });
                if (professor == null)
                    throw new Exception("Professor não encontrado!");


                // Busca os dados da pessoa na Biblioteca
                pessoaVO = pessoaBE.Consultar(new PessoaVO()
                {
                    Cpf = usuarioVO.Cpf,
                    Matricula = professor.Matricula
                });

                if (pessoaVO == null)
                {
                    pessoaVO = pessoaBE.Consultar(new PessoaVO()
                    {
                        Cpf = usuarioVO.Cpf,
                    });
                }

                if (pessoaVO != null)
                {
                    dynamic dadosPessoais = new ExpandoObject();
                    dadosPessoais.IdPessoa = pessoaVO.Id;
                    dadosPessoais.Cpf = pessoaVO.Cpf;
                    dadosPessoais.DTNow = DateTime.Now;

                    // AUTH-TOKEN
                    string dadosPessoaisJson = JsonConvert.SerializeObject(dadosPessoais);
                    string hash = Criptografia.CifrarCesar(dadosPessoaisJson, 10);
                    string hash64 = Criptografia.Base64Encode(hash);


                    // Busca o caminho do portal
                    var scheme = HttpContext.Current.Request.Url.Scheme;

                    if (Dominio.AppState == Dominio.ApplicationState.Debug)
                        ret = scheme + "://localhost:27832/portal/";

                    if (Dominio.AppState == Dominio.ApplicationState.Homologacao)
                        ret = scheme + "://portalbiblioteca.univaglabs.edu.br/portal/";

                    if (Dominio.AppState == Dominio.ApplicationState.Teste)
                        ret = scheme + "://portalbiblioteca.univag.teste.edu.br/portal/";

                    if (Dominio.AppState == Dominio.ApplicationState.Producao)
                        ret = scheme + "://portalbiblioteca.univag.edu.br/portal/";

                    ret = string.Concat(ret, "?authtoken=", hash64);
                }
            }
            catch
            {
                if (ret == string.Empty)
                    ret = "http://portalbiblioteca.univag.edu.br/portal/";
            }
            finally
            {
                if (pessoaBE != null)
                    pessoaBE.FecharConexao();
            }

            return ret;
        }
        
    }
}