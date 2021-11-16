using System.Text;

namespace Sistema.Api.dll.Repositorio.Util
{
    public class JsInjector
    {
        private StringBuilder JsString { get; set; }
        public string PreContentCode { get; set; }
        public string ContentCode { get; set; }
        public string PosContentCode { get; set; }

        public JsInjector(string preContentCode = "", string contentCode = "", string posContentCode = "")
        {
            PreContentCode = preContentCode;
            ContentCode = contentCode;
            PosContentCode = posContentCode;
        }

         //CallEvent
        public string Create()
        {
            JsString = new StringBuilder();

            JsString.AppendLine("<script type='text/javascript'>");
            JsString.AppendLine(PreContentCode);
            JsString.AppendLine("$(document).ready(function () {");
            JsString.AppendLine(ContentCode);
            JsString.AppendLine("});");
            JsString.AppendLine(PosContentCode);
            JsString.AppendLine("</script>");

            return JsString.ToString();
        }
    }
}
