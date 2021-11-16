using Sistema.Api.dll.Src;
using System;
using System.Web;

namespace Sistema.Api.dll.Repositorio.Util
{
    public class SessionHandler
    {
        public object Objeto { set; get; }
        public bool Portal = false;


        /// <summary>
        /// NewSession
        /// </summary>
        /// <param name="identificador"></param>
        /// <param name="cookieExpiration"></param>
        public void NewSession(string identificador, int tempoExpiracao = 0)
        {
            var request = HttpContext.Current.Request;
            var response = HttpContext.Current.Response;


            // --------------- Recupera o dominio da aplicação
            string dominioAplicacao = Dominio.GetDominioAplicacao();


            // --------------- Definições para a Sessão do Cookie

            // Define a dataExpiracaoPadrao: 1 dia - a data de expiracao do cookie
            DateTime dataExpiracaoPadrao =  (tempoExpiracao == 0) ? DateTime.Today.AddDays(1) : DateTime.Now.AddMinutes(tempoExpiracao);

            // Define o cookieNome - nome do cookie
            var cookieNome = Funcoes.RemoveCaracteresEspeciais(Criptografia.CifrarCesar(identificador, identificador.Length), false, false);

            // Define o cookieValor - valor do cookie
            var cookieValor = HttpUtility.UrlEncode(Criptografia.Base64Encode(Json.Serialize(Objeto)));


            // --------------- Define o cookie
            var cookie = new HttpCookie(cookieNome)
            {
                Value = cookieValor,
                Expires = dataExpiracaoPadrao
            };


            // --------------- Define o cookieTime
            var cookieTime = new HttpCookie("CT")
            {
                Value = dataExpiracaoPadrao.ToString(),
                Expires = dataExpiracaoPadrao
            };


            // -------------- Se não for Portal define os dominios do cookie
            if (!Portal)
            {
                cookie.Domain = dominioAplicacao == "localhost" ? "" : dominioAplicacao;
                cookieTime.Domain = dominioAplicacao == "localhost" ? "" : dominioAplicacao;
            }


            // --------------- Recupera os Cookies cookie e cookieTime
            var c1 = request.Cookies[cookie.Name];
            var c2 = request.Cookies[cookieTime.Name];


            // -------------- Redefine os Cookies cookie e cookieTime
            if (c1 == null)
            {
                response.Cookies.Add(cookie);
            }
            else
            {
                response.SetCookie(cookie);
            }

            if (c2 == null)
            {
                response.Cookies.Add(cookieTime);
            }
            else
            {
                response.SetCookie(cookieTime);
            }
        }


        /// <summary>
        /// GetSession
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="identificador"></param>
        /// <returns></returns>
        public static T GetSession<T>(string identificador) where T : class
        {
            try
            {
                // --------------- Define o request
                var request = HttpContext.Current.Request;
                var session = HttpContext.Current.Session;
                long idModulo = 0;
                long idSubModulo = 0;

                // --------------- Recupera o appState
                var appState = Dominio.AppState.ToString();


                // --------------- Define o objeto
                T objeto = null;


                // --------------- Recupera o e define cookieName
                var cookieName = Funcoes.RemoveCaracteresEspeciais(Criptografia.CifrarCesar(identificador, identificador.Length), false, false);


                // --------------- Recupera e define o cookie
                var cookie = request.Cookies[cookieName];


                // --------------- Se o identificador for Session
                if (identificador == "Session" && (session == null || session["SessaoSistema"] == null) )
                {
                    // ---------- Recupera o idModulo
                    idModulo = CommonPage.GetIdModulo();


                    // ---------- Recupera o idSubModulo
                    idSubModulo = CommonPage.GetIdSubModulo();


                    // ---------- Se o ambiente da aplicação for Debug
                    if (appState == "Debug")
                    {
                        var sessaoSistema = new SessaoSistema();

                        // ---------- Atualiza a sessão sessaoSistema
                        sessaoSistema.IdUsuario = Dominio.IdUsuarioDebug;
                        sessaoSistema.IdUsuarioCampus = Dominio.IdUsuarioCampusDebug;
                        sessaoSistema.NomeUsuario = Dominio.NomeUsuarioDebug;
                        sessaoSistema.IdCampus = Dominio.IdCampusDebug;
                        sessaoSistema.NomeCampus = Dominio.NomeCampusDebug;

                        sessaoSistema.IdModulo = idModulo;
                        sessaoSistema.IdSubModulo = idSubModulo;


                        // ---------- Cria o cookie sessaoSistema
                        var sessionHandler = new SessionHandler()
                        {
                            Objeto = sessaoSistema
                        };

                        sessionHandler.NewSession("Session");


                        // ------- Converte e retorna SessaoSistema
                        objeto = (T)Convert.ChangeType(sessaoSistema, typeof(T));

                        return objeto;
                    }
                }


                // --------------- Se o cookie for nulo
                if (cookie == null)
                {
                    return null;
                }


                // --------------- Recupera e define o cookieValue
                var cookieValue = cookie.Value;


                // --------------- Se o valor do cookie for nulo
                if (string.IsNullOrEmpty(cookieValue))
                {
                    return null;
                }


                // --------------- Recupera o cookieValor e os dados
                string cookieValor = HttpUtility.UrlDecode(cookieValue);

                string dados = Criptografia.Base64Decode(cookieValor);


                // --------------- Recupera o objeto
                try
                {
                    objeto = Json.DeSerialize<T>(dados);


                    // ---------- Se objto for nulo
                    if (objeto == null)
                        return null;


                    // ---------- Se objeto for sessão do sistema
                    if (objeto.GetType().Name == "SessaoSistema")
                    {
                        // ---------- Recupera o idUsuario
                        long idUsuario = (long)objeto.GetType().GetProperty("IdUsuario").GetValue(objeto);


                        // ---------- Se Não encontrar o idUsuario
                        if (idUsuario < 1)
                            return null;

                        // ---------- Guarda o idModulo
                        if (idModulo > 0)
                        {
                            objeto.GetType().GetProperty("IdModulo").SetValue(objeto, idModulo);
                        }

                        // ---------- Gurada o IdSubModulo
                        if (idSubModulo > 0)
                        {
                            objeto.GetType().GetProperty("IdSubModulo").SetValue(objeto, idSubModulo);
                        }
                    }
                }
                catch
                {
                    return null;
                }


                return objeto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// GetIdSubModuloSession
        /// </summary>
        /// <returns></returns>
        public static long GetIdSubModuloSession()
        {
            long idSubModulo = 0;

            try
            {
                var cookieIdSubmodulo = HttpContext.Current.Request.Cookies["IdSubModulo"];

                idSubModulo = cookieIdSubmodulo != null ? Convert.ToInt64(cookieIdSubmodulo.Value) : 0;
            }
            catch
            {
            }

            return idSubModulo;
        }



        /// <summary>
        /// RefreshSession
        /// </summary>
        /// <param name="identificador"></param>
        /// <param name="cookieExpiration"></param>
        public static void RefreshSession(string identificador, int tempoExpiracao = 0)
        {
            var cookieName = Funcoes.RemoveCaracteresEspeciais(Criptografia.CifrarCesar(identificador, identificador.Length), false, false);


            int minutosExpiracaoPadrao = Dominio.MinutosExpiracaoCookieSessao;


            var cookie = HttpContext.Current.Request.Cookies[cookieName];
            var cookieTime = HttpContext.Current.Request.Cookies["CT"];


            if (cookie != null || cookieTime == null) return;


            cookie.Expires = (tempoExpiracao == 0) ?
                DateTime.Now.AddMinutes(minutosExpiracaoPadrao) : DateTime.Now.AddMinutes(tempoExpiracao);


            cookieTime.Value = (tempoExpiracao == 0) ?
                DateTime.Now.AddMinutes(minutosExpiracaoPadrao).ToString() : DateTime.Now.AddMinutes(tempoExpiracao).ToString();

            cookieTime.Expires = (tempoExpiracao == 0)
                ? DateTime.Now.AddMinutes(Dominio.MinutosExpiracaoCookieSessao + 1) : DateTime.Now.AddMinutes(tempoExpiracao + 1);


            HttpContext.Current.Response.Cookies.Add(cookie);
            HttpContext.Current.Response.Cookies.Add(cookieTime);
        }


        /// <summary>
        /// RemoveSession
        /// </summary>
        /// <param name="identificador"></param>
        /// <param name="domain"></param>
        public static void RemoveSession(string identificador, string domain = null)
        {
            try
            {
                // --------------- Recupera o dominio da aplicação
                string dominioAplicacao = Dominio.GetDominioAplicacao();

                var cookieName = Funcoes.RemoveCaracteresEspeciais(Criptografia.CifrarCesar(identificador, identificador.Length), false, false);

                var cookie = HttpContext.Current.Request.Cookies[cookieName];
                var cookieTime = HttpContext.Current.Request.Cookies["CT"];


                if (cookie == null || cookieTime == null) return;


                cookie.Value = null;
                cookie.Expires = DateTime.Now.AddDays(-2d);

                cookieTime.Value = null;
                cookieTime.Expires = DateTime.Now.AddDays(-2d);


                HttpContext.Current.Response.SetCookie(cookie);
                HttpContext.Current.Response.SetCookie(cookieTime);

                HttpContext.Current.Response.Cookies.Add(cookie);
                HttpContext.Current.Response.Cookies.Add(cookieTime);

                HttpContext.Current.Response.Cookies.Clear();

                HttpContext.Current.Session.Abandon();

                // Forçando a expiração do Cookie
                var expiredCookie = new HttpCookie(cookieName) {
                    Expires = DateTime.Now.AddDays(-3),
                    Domain = dominioAplicacao == "localhost" ? "" : dominioAplicacao
                };

                HttpContext.Current.Response.Cookies.Add(expiredCookie);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Fim dos métodos
    }
}