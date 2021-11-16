using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sistema.Api.dll.Repositorio.Util;

namespace Sistema.Web.UI.PortalSistema.Util
{
    public class Util
    {
        public static string ImportarMasterJsCss()
        {
            //CSS
            return @" <link rel='stylesheet' type='text/css' media='screen' href='../css/bootstrap.min.css' />
                      <link rel='stylesheet' type='text/css' media='screen' href='../Css/font-awesome.min.css' />
                      <link rel='stylesheet' type='text/css' media='screen' href='../Css/demo.min.css' />    
                      <link rel='shortcut icon' href='../Img/favicon/favicon.png' type='image/x-icon' />
                      <link rel='icon' href='../Img/favicon/favicon.png' type='image/x-icon' />" +
                       Funcoes.InvocarTagArquivo("View/Css/sweet-alert.css") +
                       //Funcoes.InvocarTagArquivo("View/select2/select2.css") +
                       Funcoes.InvocarTagArquivo("View/select2/select2-bootstrap.css") +
                       Funcoes.InvocarTagArquivo("View/Css/smartadmin-production.min.css", true) +
                       Funcoes.InvocarTagArquivo("View/Css/smartadmin-skins.min.css", true) +
                       Funcoes.InvocarTagArquivo("View/Css/smartadmin-rtl.min.css", true) +
                       Funcoes.InvocarTagArquivo("View/select2/select2-bootstrap.css") +
                       Funcoes.InvocarTagArquivo("View/Css/sistemamaster.css", true) +
                       Funcoes.InvocarTagArquivo("View/Css/animate.css") +
                       Funcoes.InvocarTagArquivo("View/Css/pnotify.custom.min.css") +
                       Funcoes.InvocarTagArquivo("View/Css/jquery.resizableColumns.css") +
                       Funcoes.InvocarTagArquivo("View/Css/plugin/datepicker.css", true) +
                       Funcoes.InvocarTagArquivo("View/Css/plugin/datetimepicker.css", true) +

                       //JAVASCRIPT
                       Funcoes.InvocarTagArquivo("View/Js/jquery.min.js") +
                       Funcoes.InvocarTagArquivo("View/Js/ajaxhandler.js") +
                       Funcoes.InvocarTagArquivo("View/Js/webstorage.js") +
                       Funcoes.InvocarTagArquivo("View/Js/pnotify.custom.min.js") +
                       @"<script src='../Js/app.config.js'></script>
                       <script src='../Js/bootstrap/bootstrap.min.js'></script>
                       <script src='../Js/notification/SmartNotification.min.js'></script>
                       <script src='../Js/smartwidgets/jarvis.widget.min.js'></script>
                       <script src='../Js/plugin/easy-pie-chart/jquery.easy-pie-chart.min.js'></script>
                       <script src='../Js/plugin/sparkline/jquery.sparkline.min.js'></script>" +
                       Funcoes.InvocarTagArquivo("View/Js/validate.js") +
                       @" <script src='../Js/plugin/masked-input/jquery.maskedinput.min.js'></script>
                       <script src='../Js/plugin/bootstrap-slider/bootstrap-slider.min.js'></script>
                       <script src='../Js/demo.min.js'></script>
                       <script src='../Js/plugin/flot/jquery.flot.cust.min.js'></script>
                       <script src='../Js/plugin/flot/jquery.flot.tooltip.min.js'></script>
                       <script src='../Js/plugin/fullcalendar/jquery.fullcalendar.min.js'></script>" +
                       Funcoes.InvocarTagArquivo("View/SmartAdmin/Js/smart-util.js") +
                       Funcoes.InvocarTagArquivo("View/SmartAdmin/Js/plugin/datatables/jquery.dataTables.min.js") +
                       Funcoes.InvocarTagArquivo("View/SmartAdmin/Js/plugin/datatables/dataTables.colVis.min.js") +
                       Funcoes.InvocarTagArquivo("View/SmartAdmin/Js/plugin/datatables/dataTables.tableTools.min.js") +
                       Funcoes.InvocarTagArquivo("View/SmartAdmin/Js/plugin/datatables/dataTables.bootstrap.min.js") +
                       Funcoes.InvocarTagArquivo("View/SmartAdmin/Js/plugin/datatable-responsive/datatables.responsive.min.js") +
                       Funcoes.InvocarTagArquivo("View/Js/jquery.cookie.js") +
                       Funcoes.InvocarTagArquivo("View/Js/extention.js") +
                       Funcoes.InvocarTagArquivo("View/Js/jquery.price_format.2.0.min.js", true) +
                       Funcoes.InvocarTagArquivo("View/select2/select2.min.js") +
                       Funcoes.InvocarTagArquivo("View/Js/sweet-alert.min.js") +
                       Funcoes.InvocarTagArquivo("View/Js/moment.js") +
                       Funcoes.InvocarTagArquivo("View/Js/lib.js") +
                       Funcoes.InvocarTagArquivo("View/Js/jquery.mask.min.js") +
                       Funcoes.InvocarTagArquivo("View/Js/serializeObject.js") +
                       Funcoes.InvocarTagArquivo("View/Js/jquery.resizableColumns.min.js") +
                       Funcoes.InvocarTagArquivo("View/Js/app.config.js", true) +
                       Funcoes.InvocarTagArquivo("View/Js/app.min.js", true) +
                       Funcoes.InvocarTagArquivo("View/Js/main.js", true) +
                       Funcoes.InvocarTagArquivo("View/Js/plugin/bootstrap-datepicker/bootstrap-datepicker.js", true) +
                       Funcoes.InvocarTagArquivo("View/Js/plugin/bootstrap-timepicker/bootstrap-datetimepicker.js", true);
        }
    }
}