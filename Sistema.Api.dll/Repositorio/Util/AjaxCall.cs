using System.Text;

namespace Sistema.Api.dll.Repositorio.Util
{
    public class AjaxCall
    {
        public StringBuilder JsString { get; set; }
        public string Arr { get; set; }
        public string EventFunction { get; set; }
        public string ElementSelector { get; set; }
        public string FormId { get; set; }
        public string Debug { get; set; }
        public string Button { get; set; }
        public string ForceSubmit { get; set; }
        public string ValidationRules { get; set; }
        public string RequestUrl { get; set; }
        public string WebMethod { get; set; }
        public string RequestMethod { get; set; }
        public string RequestAsynchronous { get; set; }
        public string Callback { get; set; }
        public string CleanForm { get; set; }
        public string PreContentCode { get; set; }
        public string ContentCode { get; set; }
        public string PreCodeContent { get; set; }
        public string PosCodeContent { get; set; }
        public string AjaxHandlerMethod { get; set; }
        public string ControllerName { get; set; }
        public string ForceAjaxSubmit { get; set; }
        public bool BeforeMessage { get; set; }
        public string BeforeTextMessage { get; set; }
        public string BeforeSuccessTextMessage { get; set; }
        public string BeforeTextTitle { get; set; }

        public AjaxCall(string ajaxHandlerMethod = null)
        {
            AjaxHandlerMethod = ajaxHandlerMethod ?? "submitHandler";
            ControllerName = "consoleController";
            ForceAjaxSubmit = "true";
        }

        //CallEvent
        public string Create()
        {
            JsString = new StringBuilder();

            JsString.AppendLine("<script type='text/javascript'>");
            JsString.AppendLine(PreCodeContent);
            JsString.AppendLine("$(document).ready(function () {");
            JsString.AppendLine(PreContentCode);
            JsString.AppendLine("$(" + ElementSelector + ").on(\"" + EventFunction + "\" , function (e) {");
            JsString.AppendLine("e.preventDefault();");

            if (BeforeMessage)
            {
                JsString.AppendLine(@"swal({
                                                 title: '" + BeforeTextTitle + @"',
                                                 text: '" + BeforeTextMessage + @"',
                                                 type: 'warning',
                                                 showCancelButton: true,
                                                 confirmButtonColor: '#DD6B55',
                                                 confirmButtonText: 'Confirmar',
                                                 closeOnConfirm: false
                                            },
                                             function(){

                                   ");
            }


            JsString.AppendLine(ContentCode);
            JsString.AppendLine("objOptions = {");
            JsString.AppendLine("\"formId\":" + FormId + ",");

            //Button
            if (Button != null)
            {
                JsString.AppendLine("\"button\":" + Button + ",");
            }

            //Frce submit
            if (ForceSubmit == "" || ForceSubmit == "true" || ForceSubmit == null)
            {
                JsString.AppendLine("\"forceSubmit\":true,");
            }
            else if (ForceSubmit == "false")
            {
                JsString.AppendLine("\"forceSubmit\":false,");
            }

            //Debug
            if (Debug != null)
            {
                JsString.AppendLine("\"debug\":" + Debug + ",");
            }
            else
            {
                JsString.AppendLine("\"debug\":false,");
            }
            JsString.AppendLine("\"validationRules\":" + ValidationRules + ",");
            JsString.AppendLine("\"requestURL\":" + RequestUrl + ",");
            JsString.AppendLine("\"requestMethod\":" + RequestMethod + ",");
            JsString.AppendLine("\"webMethod\":" + WebMethod + ",");
            JsString.AppendLine("\"requestAsynchronous\":" + RequestAsynchronous + ",");
            JsString.AppendLine("\"requestData\":" + Arr + ",");
            JsString.AppendLine("\"forceAjaxSubmit\":" + ForceAjaxSubmit + ",");
            JsString.AppendLine("\"callback\": function () {");
            JsString.AppendLine("if (httpRequest.readyState == 4) {");
            JsString.AppendLine("if (httpRequest.status == 200) {");
            JsString.AppendLine("var json = eval('(' + httpRequest.responseText + ')');");
            JsString.AppendLine("var objJson  = "+ ControllerName +"( $(" + FormId + "), json.d, " + CleanForm + " );");

            JsString.AppendLine(Callback);
            if (BeforeMessage)
            {
                JsString.AppendLine(@"if(objJson.StatusOperacao == true){swal('Confirmado!', '" + BeforeSuccessTextMessage + @"', 'success');}");
            }

            JsString.AppendLine("}}}");
            JsString.AppendLine("};");

            JsString.AppendLine(AjaxHandlerMethod + "(objOptions);");

            if (BeforeMessage)
            {
                JsString.AppendLine("});");
            }

            JsString.AppendLine("});");
            JsString.AppendLine(@" $('input,select').on('focusin focusout', function (event) {
                                    var eventType = event.type;

                                    if ($(this).prop('disabled') != true && $(this).prop('readonly') != true) {
                                        //Focusin
                                        if (eventType == 'focusin') {
                                            $(this).css('background-color', '#FFFFDD');
                                        }

                                        //Focusout
                                        if (eventType == 'focusout') {
                                            $(this).css('background-color', '#FFFFFF');
                                        }
                                    }
                                });");

            JsString.AppendLine("});");

            JsString.AppendLine(PosCodeContent);
            JsString.AppendLine("</script>");

            return JsString.ToString();
        }
    }
}

