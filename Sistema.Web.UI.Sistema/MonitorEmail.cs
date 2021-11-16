using Sistema.Api.dll.Src.Comum.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema.Web.UI.Sistema
{
    public class MonitorEmail : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            AuditoriaBE be = null;

            // ---------- Validações
            try
            {
                long id = Convert.ToInt64(context.Request.QueryString["id"]);
                string referencia = context.Request.QueryString["ref"];
                string token = context.Request.QueryString["token"];
                bool debug = context.Request.QueryString["debug"] != null ? true : false;

                if (id == 0 || string.IsNullOrEmpty(referencia) || string.IsNullOrEmpty(token))
                    throw new Exception();

                if (!debug)
                {
                    be = new AuditoriaBE();

                    be.ChecarAbrirEmail(id, referencia, token);
                }
            }
            catch
            {
            }
            finally
            {
                if (be != null)
                    be.FecharConexao();

                string path = context.Server.MapPath("/img/recebeu_email.png");

                context.Response.WriteFile(path);

                context.Response.ContentType = "image/png";
            }
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}