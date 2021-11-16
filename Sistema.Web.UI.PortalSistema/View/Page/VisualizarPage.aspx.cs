using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistema.Web.UI.PortalProfessor.View.MasterPage;
using System.Web.Services;
using Sistema.Api.dll.Repositorio.Util;
using RestSharp;
using System.Net;
using System.Dynamic;
using Newtonsoft.Json;
using Sistema.Api.dll.Src.Biblioteca.BE;
using Sistema.Api.dll.Src.Biblioteca.VO;

namespace Sistema.Web.UI.PortalProfessor.View.Page
{
    public partial class VisualizarPage : System.Web.UI.Page
    {
        public string UrlPage { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ProfessorMaster.RenovarChecarSessao();


            string qPageRetorno = "~/View/Page/Principal.aspx";
            int id = 0;
            try
            {



                qPageRetorno = (Request.QueryString["UrlReturn"] == null ? "~/View/Page/Principal.aspx" : Request.QueryString["UrlReturn"]);
                id = (Request.QueryString["Id"] == null ? 0 : Convert.ToInt32(Request.QueryString["Id"]));



                if (id == 0)
                    throw new Exception("Biblioteca não econtrada!");
                //Response.Redirect(qPageRetorno);


                var qPage = Request.QueryString["PageUrl"];

                UrlPage = Criptografia.Base64Decode(qPage);
            }
            catch (Exception)
            {
                Response.Redirect(qPageRetorno);
            }


        }


        [WebMethod]
        public static string GetAutenticacao(int id, int tipo)
        {
            Ajax ajax = null;
            BibliotecaVirtualBE bibliotecaVirtualBE = null;
            PeriodicoOnlineBE periodicoOnlineBE = null;

            try
            {
                ajax = new Ajax();
                bibliotecaVirtualBE = new BibliotecaVirtualBE();
                periodicoOnlineBE = new PeriodicoOnlineBE(bibliotecaVirtualBE.GetSqlCommand());


                if (tipo == 1)
                {

                    var bibliotecaVirtual = bibliotecaVirtualBE.Consultar(new BibliotecaVirtualVO() { Id = id });


                    var urlBase = bibliotecaVirtual.Url;
                    bool webService = bibliotecaVirtual.WebService == null ? false : Convert.ToBoolean(bibliotecaVirtual.WebService);

                    IRestResponse response;

                    if (webService)
                    {

                        List<KeyValuePair<string, string>> LstKeyHeader = new List<KeyValuePair<string, string>>();
                        dynamic jsonHeader = null;
                        if (bibliotecaVirtual.Header != null && bibliotecaVirtual.Header != "[]")
                            jsonHeader = Json.Serialize<dynamic>(bibliotecaVirtual.Header);

                        if (jsonHeader != null)
                        {
                            foreach (dynamic item in jsonHeader)
                            {
                                LstKeyHeader.Add(new KeyValuePair<string, string>(item.Chave, item.Valor));
                            }
                        }


                        //Types
                        //1 = Text
                        //2 = JSON
                        //3 = XML

                        var client = new RestClient(urlBase);
                        client.Proxy = WebRequest.DefaultWebProxy;


                        Method method = bibliotecaVirtual.MethodSend == 1 ? Method.GET : Method.POST;

                        var request = new RestRequest(method);


                        if (bibliotecaVirtual.BodyType == 1)
                        {
                            request.AddBody(bibliotecaVirtual.Body);
                        }
                        if (bibliotecaVirtual.BodyType == 2)
                        {
                            request.AddJsonBody(bibliotecaVirtual.Body);
                        }
                        if (bibliotecaVirtual.BodyType == 3)
                        {
                            request.AddXmlBody(bibliotecaVirtual.Body);
                        }


                        Uri uri = new Uri(bibliotecaVirtual.UrlToken);
                        string strParametros = "";

                        response = client.Execute(request);

                        if (response.StatusCode == HttpStatusCode.OK)
                        {

                            string strURL = uri.AbsoluteUri;

                            int urlIndex = strURL.LastIndexOf('?');
                            //this returns the value :- Name=nagarjuna
                            string strValue = strURL.Substring(urlIndex + 1);
                            //if u want the value from that then use
                            //this returns 'nagarjuna'
                            string strUrlValue = strValue.Substring(strValue.IndexOf('=') + 1);

                            string[] parametros = strValue.Split(new char[] { '&' });

                            //KeyValuePair<string, dynamic> k = new KeyValuePair<string, dynamic>();
                            var dict = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(response.Content);


                            foreach (var item in parametros)
                            {
                                string valueParam = item.Substring(item.IndexOf('=') + 1);
                                string param = item.Substring(0, item.IndexOf('='));

                                string value = "";
                                if (dict.Where(x => x.Key == valueParam).Any())
                                {
                                    value = dict[valueParam];
                                    valueParam = value;
                                }
                                else
                                {
                                    param = string.Empty;
                                    valueParam = string.Empty;
                                }

                                if (!string.IsNullOrEmpty(param) && !string.IsNullOrEmpty(valueParam))
                                {
                                    if (string.IsNullOrEmpty(strParametros))
                                    {
                                        strParametros = string.Concat("?", param, "=", valueParam);
                                    }
                                    else
                                    {
                                        strParametros += string.Concat("&", param, "=", valueParam);
                                    }
                                }
                            }
                        }
                        else
                        {

                            throw new Exception(string.Concat("Falha na requisição do WebService: Verifique as configurações do Serviço!", " Status Code: ", response.StatusDescription));
                        }

                        string urlAcesso = string.Concat(uri.Scheme, "://", uri.Host, uri.AbsolutePath, strParametros);
                        bibliotecaVirtual.UrlToken = urlAcesso;
                        bibliotecaVirtual.Url = null;
                    }




                    ajax.StatusOperacao = true;
                    ajax.Variante = Json.Serialize(bibliotecaVirtual);
                }


                if (tipo == 2)
                {
                    var periodicoOnlineVo = periodicoOnlineBE.Consultar(new PeriodicoOnlineVO() { Id = id });

                    ajax.StatusOperacao = true;
                    ajax.Variante = Json.Serialize(periodicoOnlineVo);
                }



            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.Variante = ex.Message;

            }
            finally
            {
                if (bibliotecaVirtualBE != null)
                    bibliotecaVirtualBE.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }


    }
}