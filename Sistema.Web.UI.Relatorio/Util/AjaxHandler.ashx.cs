using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Comum.BE;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Linq;

namespace Sistema.Web.UI.Relatorio.Util
{
    /// <summary>
    /// Autor: Giovanni Ramos
    /// Data: 21.10.2015
    /// Descrição: Classe AjaxHandler para centralizar as chamadas de consulta via combobox
    /// </summary>
    public class AjaxHandler : CommonPage, IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        public int idCampus { get; set; }
        public int idPeriodoLetivo { get; set; }
        public int idCurso { get; set; }
        public int idGpa { get; set; }
        public int idUsuario { get; set; }
        public int idAvaliacao { get; set; }
        public bool acessoCompleto { get; set; }


        // ProcessRequest
        public override void ProcessRequest(HttpContext context)
        {
            // Cabeçalho
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;

            // Parâmetros enviados via Ajax
            var jsonString = string.Empty;

            context.Request.InputStream.Position = 0;
            using (var inputStream = new StreamReader(context.Request.InputStream))
            {
                jsonString = inputStream.ReadToEnd();
            }

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            dynamic data = jsSerializer.Deserialize<AjaxHandler>(jsonString);

            try
            {
                // Variável de retorno
                var jsonReturn = "";

                // Nome do método chamado via Ajax
                switch (context.Request["MethodName"])
                {
                    default:
                        throw new ArgumentException("O nome do método não foi informado ou é desconhecido.");
                }

                // Retorna a requisição em Json
                context.Response.Write(jsonReturn);
            }
            catch (Exception ex)
            {
                // Mensagem de Erro
                string mensagem = "{ 'Error': '" + ex.Message + "' }";

                // Retorna a requisição em Json
                context.Response.Write(Ajax.JsonToDynamic(mensagem));
            }
        }

    }
}