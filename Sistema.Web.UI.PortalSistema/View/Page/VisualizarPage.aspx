<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisualizarPage.aspx.cs" Inherits="Sistema.Web.UI.PortalProfessor.View.Page.VisualizarPage" %>

<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        html, body {
            height: 100%;
        }

        #geral {
            min-height: 100%;
        }

        * html #geral {
            /*Hack IE6*/
            height: 100%;
        }
    </style>

    <%= Funcoes.InvocarTagArquivo("View/Js/jquery.min.js") %>
    <%= Funcoes.InvocarTagArquivo("View/Js/ajaxhandler.js") %>


    <script>
        $(document).ready(function () {


            var idPage = $('#idPage').val();
            var tipo = $('#Tipo').val();


            var jqxhr = $.ajax({
                type: 'POST',
                url: '/View/Page/VisualizarPage.aspx/GetAutenticacao',
                data: JSON.stringify({ id: idPage, tipo: tipo }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            })
             .done(function (data, textStatus, jqXHR) {
                 var response = JSON.parse(data.d);

                 if (!response.StatusOperacao) {

                     $('#frameBibliotecaVirtual').attr('src', '/View/Page/Erro.aspx?s=token-webservice&m=' + response.Variante);

                     return false;
                 }
                 else {
                     var obj = JSON.parse(response.Variante);

                     if (obj.WebService) {
                         $('#frameBibliotecaVirtual').attr('src', obj.UrlToken);
                     } else {
                         $('#frameBibliotecaVirtual').attr('src', obj.Url);
                     }
                 }
             })
             .fail(function (jqXHR, textStatus, errorThrown) {

             })
             .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {

             });
        });
    </script>
</head>
<body>
    <input type="hidden" id="idPage" value="<%= Request.QueryString["Id"] == null ? 0 : Convert.ToInt32(Request.QueryString["Id"]) %>" />
    <input type="hidden" id="Tipo" value="<%= Request.QueryString["Tipo"] == null ? 0 : Convert.ToInt32(Request.QueryString["Tipo"]) %>" />

    <iframe style="width: 100%; height: 100%; position: absolute; border: 0px" id="frameBibliotecaVirtual" scrolling="auto"></iframe>
</body>
</html>
