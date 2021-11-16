using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Services;
using Sistema.Api.dll.Repositorio.Util.Componentes;
using Sistema.Api.dll.Repositorio.Util.Componentes.Templates;
using Sistema.Api.dll.Src;
using Sistema.Api.dll.Src.Comum.BE;
using Sistema.Api.dll.Src.Comum.VO;
using Sistema.Api.dll.Src.Repositorio.BE;

namespace Sistema.Api.dll.Repositorio.Util
{
    public abstract class CommonPage : System.Web.UI.Page
    {
        protected static HttpContext _context;


        // Nome do Perfil Ativo
        public static string NomePerfilAcesso { get; set; }


        public void Page_Load(object sender, EventArgs e)
        {
            RenovarChecarSessao();
        }


        /// <summary>
        /// GetSessao
        /// Autor: Leandro Curioso, Michael Lopes
        /// Data: 04/08/2014
        /// Descrição: Responsavel por trazer a sessao atual do sistema
        /// </summary>
        /// <returns></returns>
        public static SessaoSistema GetSessao()
        {
            var sessaoSistema = SessionHandler.GetSession<SessaoSistema>("Session");

            if (sessaoSistema == null)
                HttpContext.Current.Response.Redirect("/View/Page/Erro.aspx?s=sessao-expirada", true);

            return sessaoSistema;
        }


        /// <summary>
        /// IsAjaxRequest
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
        /// RenovarChecarSessao
        /// </summary>
        public static SessaoSistema RenovarChecarSessao()
        {
            AuditoriaBE auditoriaBE = null;
            SessaoSistema sessaoSistema = null;
            bool redirecionaLogin = false;

            var context = HttpContext.Current;
            var request = context.Request;
            var response = context.Response;
            var session = context.Session;

            try
            {
                auditoriaBE = new AuditoriaBE();
                var parametroBE = new ParametroBE(auditoriaBE.GetSqlCommand());

                // --------------- Recupera e define o appState
                string appState = Dominio.AppState.ToString();


                // --------------- Recupera a sessão sessaoSistema
                sessaoSistema = SessionHandler.GetSession<SessaoSistema>("Session");


                // --------------- Se não encontrou a Sessão sessaoSistema
                if (sessaoSistema == null)
                {
                    // ---------- Se for requisição Ajax
                    if (IsAjaxRequest(request))
                    {
                        response.ClearHeaders();
                        response.ClearContent();
                        response.Status = "401 Unauthorized access";
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        response.StatusDescription = "Sessão não encontrada";
                        response.Headers.Add("AuthenticationStatus", "NotAuthorized");

                        return null;
                    }

                    redirecionaLogin = true;

                    // ---------- Redireciona para o Erro
                    response.Redirect("/View/Page/Erro.aspx?s=sessao-expirada&error=1", true);
                }


                // --------------- Se o AppState for Debug ou Teste
                // Finaliza as validações da sessão
                if (appState == "Debug")
                {
                    return sessaoSistema;
                }


                // --------------- Recuper e  Define o idUsuario, idModulo e idSubModulo
                long idUsuario = sessaoSistema.IdUsuario;
                long idModulo = sessaoSistema.IdModulo;
                long idSubModulo = sessaoSistema.IdSubModulo;



                // --------------- Consulta o AuditoriaVO
                var auditoriaVO = auditoriaBE.Consultar(new AuditoriaVO() { IdUsuario = idUsuario });


                // --------------- Se não encontrar o AuditoriaVO
                if (auditoriaVO == null)
                    throw new Exception("Não foi possível recuperar a auditoria.");


                // --------------- Verifica se a sessão é válida
                bool sessaoValida = auditoriaBE.SessaoValida(auditoriaVO.DataWho);


                // --------------- Se a sessão Não for válida
                if (sessaoValida == false)
                {
                    SessionHandler.RemoveSession("Session");

                    if (IsAjaxRequest(request))
                    {
                        response.ClearHeaders();
                        response.ClearContent();
                        response.Status = "401 Unauthorized access";
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        response.StatusDescription = "Sessão data expirada";
                        response.Headers.Add("AuthenticationStatus", "NotAuthorized");

                        return null;
                    }

                    redirecionaLogin = true;

                    // ---------- Redireciona para o Erro
                    response.Redirect("/View/Page/Erro.aspx?s=sessao-expirada&error=2", true);
                }


                // --------------- Se a auditoria possui ChaveUnica
                if (auditoriaVO.ChaveUnica != null)
                {
                    // ---------- Define a chaveUnica - chave única de acesso ao sistema
                    Guid? chaveUnica = null;

                    // ---------- Recupera o chaveUnicaCookie
                    var chaveUnicaCookie = request.Cookies["chaveUnica"];


                    // ---------- Se o chaveUnicaCookie for nulo
                    if (chaveUnicaCookie == null)
                    {
                        SessionHandler.RemoveSession("Session");

                        if (IsAjaxRequest(request))
                        {
                            response.ClearHeaders();
                            response.ClearContent();
                            response.Status = "401 Unauthorized access";
                            response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            response.StatusDescription = "Sessão expirada";
                            response.Headers.Add("AuthenticationStatus", "NotAuthorized");

                            return null;
                        }

                        redirecionaLogin = true;

                        // ---------- Redireciona para o Erro
                        response.Redirect("/View/Page/Erro.aspx?s=sessao-expirada&error=3", true);
                    }


                    // ---------- Recupera a chaveUnica
                    chaveUnica = Guid.Parse(chaveUnicaCookie.Value);


                    // ---------- Se a chave única não confere
                    if (Dominio.UnicaSessao && chaveUnica != auditoriaVO.ChaveUnica)
                    {
                        SessionHandler.RemoveSession("Session");

                        if (IsAjaxRequest(request))
                        {
                            response.ClearHeaders();
                            response.ClearContent();
                            response.Status = "401 Unauthorized access";
                            response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            response.StatusDescription = "Sessão expirada por outro acesso";
                            response.Headers.Add("AuthenticationStatus", "NotAuthorized");

                            return null;
                        }

                        redirecionaLogin = true;

                        // ---------- Redireciona para o Login
                        response.Redirect("/View/Page/Erro.aspx?s=sessao-expirada&error=4", true);
                    }
                }


                // --------------- Se a requisição poussui idModulo e idSubModulo
                if (idModulo > 0 && idSubModulo > 0)
                {
                    var auditoriaNovoVO = new AuditoriaVO()
                    {
                        Id = auditoriaVO.Id,
                        DataLogin = auditoriaVO.DataLogin,
                        Login = auditoriaVO.Login,
                        Senha = auditoriaVO.Senha,
                        IdCampus = auditoriaVO.IdCampus,
                        IdUsuario = idUsuario,
                        IdModulo = idModulo,
                        IdSubmodulo = idSubModulo,
                        SessionId = session.SessionID,
                        ServerName = request.ServerVariables["SERVER_NAME"],
                        EnderecoIp = request.UserHostAddress,
                        BrowserNome = request.Browser.Browser,
                        BrowserVersao = request.Browser.Version,
                        BrowserTipo = request.Browser.Win32 ? "Win32" : "Win64",
                        HostName = WindowsIdentity.GetCurrent().Name.ToString(),
                        ChaveUnica = auditoriaVO.ChaveUnica
                    };


                    // ---------- Seta novas configurações para o AuditoriaNovoVO
                    auditoriaBE.SetAuditoria(auditoriaNovoVO);

                  
                    var valorParametro = parametroBE.Consultar(new ParametroCampusVO()
                    {
                        Id = 2429,
                    }).Valor;

                    if (Convert.ToBoolean(valorParametro))
                    {
                        auditoriaBE.Auditar(auditoriaNovoVO);
                    }
                    else
                    {
                        // ---------- Verifica se o usuario mantem a sessão no mesmo Modulo (Atualizando somente as Datas e Horas de Ação)
                        if (auditoriaVO.IdModulo == idModulo && auditoriaVO.IdSubmodulo == idSubModulo)
                            auditoriaBE.AuditarAcao(auditoriaNovoVO);
                        else
                             auditoriaBE.Auditar(auditoriaNovoVO);
                    }
                }
            }
            catch (Exception ex)
            {
                if (IsAjaxRequest(request))
                {
                    response.ClearHeaders();
                    response.ClearContent();
                    response.Status = "503 ServiceUnavailable";
                    response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                    response.StatusDescription = "Acesso indisponível. Erro Interno na Consulta. " + ex.Message;
                }
                else if (!redirecionaLogin)
                    response.Redirect("/View/Page/Erro.aspx?s=503", true);
            }
            finally
            {
                if (auditoriaBE != null)
                    auditoriaBE.FecharConexao();
            }

            return sessaoSistema;
        }


        /// <summary>
        /// AtualizaSessao
        /// </summary>
        /// <param name="idSubModulo"></param>
        [WebMethod]
        public static void AtualizaSessao(int idSubModulo)
        {
            var sessao = SessionHandler.GetSession<SessaoSistema>("Session");

            int id = idSubModulo;
            if (id > 0 && sessao != null)
            {
                var s = new SessaoSistema()
                {
                    IdCampus = sessao.IdCampus,
                    NomeCampus = sessao.NomeCampus,
                    IdUsuario = sessao.IdUsuario,
                    IdUsuarioCampus = sessao.IdUsuarioCampus,
                    LoginNome = sessao.LoginNome,
                    NomeUsuario = sessao.NomeUsuario,
                    EmailUsuario = sessao.EmailUsuario,
                    IdModulo = sessao.IdModulo,
                    IdAuditoria = sessao.IdAuditoria,
                    IdsCampus = sessao.IdsCampus,
                    IdSubModulo = id
                };

                var sessionHandler = new SessionHandler()
                {
                    Objeto = s
                };

                sessionHandler.NewSession("Session");
            }
        }


        // Criptografar
        /// <summary>
        /// Autor: Leandro Curioso, Michael Lopes
        /// Data: 01/02/2014
        /// Descrição: Responsavel por cripitogafar saidas para View
        /// </summary>
        /// <returns></returns>
        public static string Criptografar(string str)
        {
            return Criptografia.CifrarCesar(str, DateTime.Now.Day);
        }


        // Decriptografar
        /// <summary>
        /// Autor: Leandro Curioso, Michael Lopes
        /// Data: 01/02/2014
        /// Descrição: Responsavel por decripitogafar entradas da View
        /// </summary>
        /// <returns></returns>
        public static string Decriptografar(string str)
        {
            return Criptografia.DecifraCesar(str, DateTime.Now.Day);
        }


        // GetIdModulo
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

                long idSubModulo = GetIdSubModulo();

                long idModulo = consultaBE.GetIdModulo(idSubModulo);

                return idModulo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                consultaBE?.FecharConexao();
            }
        }


        // GetIdSubModulo
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

                // ---------- Recupera o urlSubModulo
                string urlSubModulo = GetUrlSubModulo();

                // ---------- Recupera o idSubModulo pelo urlSubModulo
                long idSubModulo = consultaBE.GetIdSubModulo(urlSubModulo);

                return idSubModulo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                consultaBE?.FecharConexao();
            }
        }


        // GetUrlSubModulo
        /// <summary>
        /// GetUrlSubModulo
        /// </summary>
        /// <returns></returns>
        public static string GetUrlSubModulo()
        {
            string url = HttpContext.Current.Request.Url.ToString();

            var urlSemQueryString = new Uri(url).GetLeftPart(UriPartial.Path);

            var arrUrl = urlSemQueryString.Split('/');

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


        // Cria um novo serial do tipo Guid
        /// <summary>
        /// Autor: Giovanni Ramos
        /// Data: 27/05/2019
        /// Descrição: Responsável em criar um serial identificador exclusivo
        /// </summary>
        /// <returns>string</returns>
        public static string GetSerialAutenticador()
        {
            return Guid.NewGuid().ToString().ToUpper();
        }


        // GetCamposSelect
        /// <summary>
        /// Autor: Leandro Curioso, Michael Lopes
        /// Data: 04/03/2014
        /// Descrição: Responsavel por montar campos do select cadastrados no bando de dados
        /// </summary>
        /// <returns></returns>
        public static string GetCamposSelect(List<ConsultaCampoVO> lst)
        {
            string campos = "";

            foreach (var filtroCampoVo in lst)
            {
                campos = campos + filtroCampoVo.NomeCampo + ",";
            }

            return campos.Substring(0, campos.Length - 1);
        }


        // Montar modal consultar
        /// Renovar e checagem de sessão
        /// <summary>
        /// Autor: Leandro Curioso, Michael Lopes
        /// Data: 05/03/2014
        /// Descrição: Responsavel por montar filtro cadastrado no banco de dados
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string MontarModalConsultar(string pagina)
        {
            RenovarChecarSessao();
            var filtroTemplate = new ConsultarModalTemplate();

            var ajax = new Ajax();

            ConsultaVO filtroVO = null;
            FiltroBE filtroBE = null;

            try
            {
                long idSubModulo = GetIdSubModulo();

                filtroBE = new FiltroBE();

                string url = GetUrlSubModulo();

                filtroVO = filtroBE.Consultar(new ConsultaVO() { IdSubModulo = idSubModulo });

                if (filtroVO != null)
                    filtroTemplate.CheckFieldType(filtroVO.LstFiltroCampos);

                DateTime now = DateTime.Now;
                int chave = Convert.ToInt32(now.Day.ToString("00"));

                //Instrução sql primeira parte
                var instrucaoSql = new Hidden()
                {
                    Id = "instrucaoSql",
                    Name = "instrucaoSql",
                    Value = Criptografia.CifrarCesar(filtroVO.InstrucaoSQL, chave).Replace("'", "|")
                };

                //Campos instrução sql
                var camposInstrucaoSql = new Hidden()
                {
                    Id = "camposInstrucaoSql",
                    Name = "camposInstrucaoSql",
                    Value = Criptografia.CifrarCesar(GetCamposSelect(filtroVO.LstFiltroCampos), chave)
                };

                //Campos instrução where
                var whereHidden = new Hidden()
                {
                    Id = "sqlWhereContainer",
                    Name = "sqlWhereContainer"
                };

                //Chamada ajax botão filtrar persistência
                var chamadaAjaxBotaoFiltrarPersistencia = new AjaxCall()
                {
                    ContentCode = "removeSessionStorage('form'); $('#grid-container').html(''); /*$('.ImageLoading').clone().show().prependTo('#grid-container');*/",
                    PreCodeContent = "datePickerWhereSqlCreator();",
                    ElementSelector = "'#botao-acao-confirmar'",
                    EventFunction = "click",
                    CleanForm = "false",
                    FormId = "'#form'",
                    Button = "'#botao-acao-confirmar'",
                    ValidationRules = "{}",
                    RequestUrl = "'" + pagina + "'",
                    WebMethod = "'ConsultarAjax'",
                    RequestMethod = "'POST'",
                    RequestAsynchronous = "true",
                    Arr = "{  instrucaoSql: $('#instrucaoSql').val(), camposInstrucaoSql: $('#camposInstrucaoSql').val(), whereSql:$('#sqlWhereContainer').val()  }",
                    Callback = string.Format(@"addSessionStorage('isql{0}',$('#instrucaoSql').val()); addSessionStorage('csql{1}',$('#camposInstrucaoSql').val());
                                                                   addSessionStorage('wsql{2}',$('#sqlWhereContainer').val()); consultarCallback(objJson); addSessionStorage('SaveGridSubModulo',Base64.encode('" + idSubModulo + "'));",
                                          Criptografia.Base64Encode(url).Substring(0, 10) + "-"
                                           + GetSessao().IdUsuario,
                                         Criptografia.Base64Encode(url).Substring(0, 10) + "-"
                                           + GetSessao().IdUsuario,
                                           Criptografia.Base64Encode(url).Substring(0, 10) + "-"
                                           + GetSessao().IdUsuario)
                };

                ajax.Variante = filtroTemplate + chamadaAjaxBotaoFiltrarPersistencia.Create() + whereHidden + instrucaoSql + camposInstrucaoSql;
            }
            catch (Exception e)
            {
                ajax.ObjMensagem = "Message: " + e.Message + "\n Source: " + e.Source + "\n StackTrace: " + e.StackTrace;
                ajax.StatusOperacao = false;
                ajax.UrlRetorno = "/View/Page/Erro.aspx?status=erro-parametro";
            }
            finally
            {
                if (filtroBE != null)
                    filtroBE.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }

    }
}
