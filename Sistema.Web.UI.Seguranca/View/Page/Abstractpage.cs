using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using Sistema.Api.dll.Src.Comum.BE;
using Sistema.Api.dll.Src.Comum.VO;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Repositorio.Util.Componentes;
using Sistema.Api.dll.Repositorio.Util.Componentes.Templates;
using Sistema.Api.dll.Src;

namespace Sistema.Web.UI.Seguranca.View.Page
{
    public abstract class AbstractPage : System.Web.UI.Page
    {
        public void Page_Load(object sender, EventArgs e)
        {
            RenovarChecarSessao();
        }

        public static SessaoModulo GetSessao()
        {
            RenovarChecarSessao();
            return (SessaoModulo)HttpContext.Current.Session["Sessao"];
        }

        public static string Criptografar(string str)
        {
            return Criptografia.CifrarCesar(str, DateTime.Now.Day);
        }

        public static string Decriptografar(string str)
        {
            return Criptografia.DecifraCesar(str, DateTime.Now.Day);
        }

        public static string GetCamposSelect(List<ConsultaCampoVO> lst)
        {
            string campos = "";
            foreach (var filtroCampoVo in lst)
            {
                campos = campos + filtroCampoVo.NomeCampo + ",";
            }
            return campos.Substring(0, campos.Length - 1);
        }

        //Renovar e checagem de sessão
        public static void RenovarChecarSessao()
        {
            var sessao = (SessaoModulo)HttpContext.Current.Session["Sessao"];
            if (!Dominio.Debug)
            {
                if (sessao == null)
                {
                    HttpContext.Current.Response.Redirect("Erro.aspx?s=sessao-expirada");
                }
                else
                {
                    string id = HttpContext.Current.Request.QueryString["idSubModulo"];
                    var s = new SessaoModulo()
                    {
                        IdCampus = sessao.IdCampus,
                        IdUsuario = sessao.IdUsuario,
                        IdModulo = sessao.IdModulo,
                        IdSubModulo = id != null ? Convert.ToInt32(id) : sessao.IdSubModulo,

                    };
                    HttpContext.Current.Session["Sessao"] = s;
                }
            }
            else
            {

                string id = HttpContext.Current.Request.QueryString["idSubModulo"];
                var s = new SessaoModulo()
                {
                    IdCampus = Dominio.IdCampusDebug,
                    IdUsuario = Dominio.IdUsuarioDebug,
                    IdModulo = Dominio.IdModuloDebug,
                    IdSubModulo = id != null ? Convert.ToInt32(id) : sessao.IdSubModulo,
                };
                HttpContext.Current.Session["Sessao"] = s;

            }
        }

        //Montar modal consultar
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
                long t = GetSessao().IdSubModulo;
                filtroBE = new FiltroBE();
                filtroVO = filtroBE.Consultar(new ConsultaVO() { IdSubModulo = GetSessao().IdSubModulo });
                filtroTemplate.CheckFieldType(filtroVO.LstFiltroCampos);

                //Instrução sql primeira parte
                var instrucaoSql = new Hidden()
                {
                    Id = "instrucaoSql",
                    Name = "instrucaoSql",
                    Value = Criptografia.CifrarCesar(filtroVO.InstrucaoSQL, DateTime.Now.Day).Replace("'", "|")
                };

                //Campos instrução sql
                var camposInstrucaoSql = new Hidden()
                {
                    Id = "camposInstrucaoSql",
                    Name = "camposInstrucaoSql",
                    Value = Criptografia.CifrarCesar(GetCamposSelect(filtroVO.LstFiltroCampos), DateTime.Now.Day)
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
                    ContentCode = "removeSessionStorage('form'); $('#grid-container').html(''); if($('#form').valid() == true) { $('.ImageLoading').clone().show().prependTo('#grid-container'); }",
                    FilterOptions = true,
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
                                                                   addSessionStorage('wsql{2}',$('#sqlWhereContainer').val()); consultarCallback(objJson);",
                                           GetSessao().IdSubModulo + "-"
                                           + GetSessao().IdUsuario,
                                           GetSessao().IdSubModulo + "-"
                                           + GetSessao().IdUsuario,
                                           GetSessao().IdSubModulo + "-"
                                           + GetSessao().IdUsuario)
                };

                ajax.Variante = filtroTemplate + chamadaAjaxBotaoFiltrarPersistencia.Create() + whereHidden + instrucaoSql + camposInstrucaoSql;

            }
            catch (Exception e)
            {
                ajax.StatusOperacao = false;
                ajax.UrlRetorno = "../Page/UltimosAcessos.aspx?status=erro-parametro";
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