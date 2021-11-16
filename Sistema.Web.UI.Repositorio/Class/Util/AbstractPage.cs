using System;
using System.IO;
using System.Timers;
using System.Web;

namespace Sistema.Web.UI.Repositorio.Class.Util
{
    public class AbstractPage : System.Web.UI.Page
    {
        private static System.Timers.Timer aTimer = new System.Timers.Timer(10000);
        private static bool StartTimer = false;
        private static string caminho = HttpContext.Current.Server.MapPath("~/Uploads/temp/");


        //public static AutencicacaoUsuarioVO GetAutentica()
        //{
        //    if (HttpContext.Current.Session["AutencicacaoUsuario"] != null)
        //    {
        //        return (AutencicacaoUsuarioVO)HttpContext.Current.Session["AutencicacaoUsuario"];
        //    }
        //    else
        //    {
        //        return new AutencicacaoUsuarioVO();
        //    }

        //}

        private void timerHandler()
        {

            Console.WriteLine("deu certo");
        }

        public static void LimparTemp(bool enabled)
        {
            if (enabled == false && StartTimer == true)
            {
                StartTimer = false;

            } else if(enabled = true ){
                StartTimer = false;
            }
           

            if (StartTimer == false)
            {
                StartTimer = true;
                if (enabled == false)
                {
                    aTimer.Stop();
                }
                else
                {
                    aTimer.Start();
                }
                aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                aTimer.Interval = 60000;
                aTimer.Enabled = enabled;
            }

        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            DirectoryInfo dr = new DirectoryInfo(caminho);
            try
            {
                foreach (FileInfo file in dr.GetFiles())
                {

                    file.IsReadOnly = false;
                    file.Delete();
                    dr.Refresh();
                }

                foreach (DirectoryInfo dir in dr.GetDirectories())
                {
                    dir.Delete(true);
                    dr.Refresh();
                }
            }
            catch (Exception)
            {
                dr.Refresh();
            }
            finally
            {
                if (dr != null)
                {
                    dr = null;
                }
            }
        }


        //Controlador de ordenação na paginação
        public string ColunaOrdem(string colunaOrdenacao)
        {
            string pag = Request.QueryString["pag"];
            string ordem = Request.QueryString["ordem"];
            string consulta = Request.QueryString["c"];
            string c = consulta == null ? "" : "&c=" + consulta;
            string url = "";

            if (pag == null)
            {

                if (ordem != null && ordem.IndexOf('-') == 0)
                {
                    url = GetUrlSemQueryString(Request.Url.ToString()) + "?ordem=" + colunaOrdenacao + c;
                }
                else
                {
                    url = GetUrlSemQueryString(Request.Url.ToString()) + "?ordem=-" + colunaOrdenacao + c;

                }


            }
            else
            {
                if (ordem != null && ordem.IndexOf('-') == 0)
                {
                    url = GetUrlSemQueryString(Request.Url.ToString()) + "?pag=" + pag + "&ordem=" + colunaOrdenacao + c;
                }
                else
                {
                    url = GetUrlSemQueryString(Request.Url.ToString()) + "?pag=" + pag + "&ordem=-" + colunaOrdenacao + c;
                }

            }

            return url;
        }

        //Get url sem query string
        public string GetUrlSemQueryString(string urlCompleta)
        {
            var uri = new Uri(urlCompleta);
            return uri.GetLeftPart(UriPartial.Path);
        }

    }
}