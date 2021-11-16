<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tour.aspx.cs" Inherits="Sistema.Web.UI.Sistema.Tour" %>

<%@ Import Namespace="Sistema.Api.dll.Src" %>
<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>

<html>
<head>
    <!--INÍCIO TITULO -->
    <title>Tour - Athon Sistemas </title>
    <!--FIM TITULO -->

    <!--INÍCIO CHARSET -->
    <meta charset="utf-8">
    <!--FIM CHARSET -->

    <!--INÍCIO META TAGS PARA NÃO INDEXAÇÃO DO SISTEMA COM MOTORES DE BUSCA-->
    <meta name="robots" content="noindex">
    <meta name="googlebot" content="noindex">
    <!--FIM META TAGS PARA NÃO INDEXAÇÃO DO SISTEMA COM MOTORES DE BUSCA-->

    <!--INÍCIO AUTOR & DIREITOS AUTORAIS & DISTRIBUIÇÃO DA APLICAÇÃO-->
    <meta name="author" content="Athon Sistemas">
    <meta name="copyright" content="© <% DateTime ano = DateTime.Now; Response.Write(ano.ToString("yyyy")); %> | Todos os direitos reservados">
    <meta name="document-type" content="Private">
    <meta name="document-distribution" content="IU">
    <!--FIM AUTOR & DIREITOS AUTORAIS & DISTRIBUIÇÃO DA APLICAÇÃO-->

    <!--INÍCIO META TAG DE COMPATIBILIDADE CROSS-BROWSER-->
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <!--FIM META TAG DE COMPATIBILIDADE CROSS-BROWSER-->

    <!--INÍCIO FAVICON-->
    <link rel="icon" href="../img/favicon.png" />
    <!--FIM FAVICON-->

    <!--INÍCIO CSS-->
    <%= Funcoes.InvocarTagArquivo("css/font-awesome.min.css", true) %>
    <%= Funcoes.InvocarTagArquivo("View/Css/bootstrap.css") %>
    <%= Funcoes.InvocarTagArquivo("css/tour.css", true) %>
    <!--FIM CSS-->
</head>
<body>

    <div class="container-fluid">

        <div class="row">
            <div class="col-sm-3 col-md-2 sidebar">

                <ul class="nav nav-sidebar">
                    <li class="active" id="btn-inicio"><a href="#inicio"><i class="fa fa-home"></i>&nbsp;Início Apresentação</a></li>
                </ul>

                <ul class="nav nav-sidebar" id="links-navegacao"></ul>

                <ul class="nav nav-sidebar">
                    <li id="btn-sobre" data-titulo="Sobre o sistema" data-descricao="Software de Gestão.<br><br>Desenvolvimento: Germano Manente Neto <br>Versão: Beta 1.0<br><br>Copyright © 2021. Athon<br>Todos os direitos reservados.">
                        <a href="#sobre"><i class="fa fa-info-circle"></i>&nbsp;Sobre o Sistema</a>
                    </li>
                    <li id="btn-voltar" style="display:none;"><a href="#"><i class="fa fa-arrow-left"></i>&nbsp;Voltar</a></li>
                </ul>
            </div>
            <div class="col-sm-9 col-sm-offset-3 col-md-10 col-md-offset-2 main">
                <div id="box-grafico" style="width:800px; height:800px; position:relative;">

                    <div id="botoes-tour">

                        <div class="front"> 
                            <div id="btn-iniciar-apresentacao" class="clicar">
                                <i class="fa fa-caret-right"></i><br />
                                <span>Iniciar Tour</span>
                            </div>
                        </div>
                        <div class="back">
                            <div style="width:230px; height:230px; top: 304px; left: 286px; position:absolute; background: url('img/logo_tour_sis_univag.png');"></div>
                        </div>
                    </div>

                    <!-- Grafico -->
                    <div id="pieChart"></div>
                </div>
            </div>

        </div>
    </div>


    <div class="modal fade" id="box-modal">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color:#204a93; color: #ffffff">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title"></h4>
                </div>
                <div class="modal-body" style="min-height:200px;"></div>
                <div class="modal-footer" style="background-color:#224a8e">
                    <button type="button" class="btn btn-default pull-left" id="btn-fechar" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Fechar</button>
                    <button type="button" class="btn btn-primary pull-right" id="btn-avancar" style="display:none;">Avançar&nbsp;<i class="fa fa fa-arrow-right"></i></button>
                </div>
            </div>
        </div>
    </div>


<!-- INÍCIO JAVASCRIPT -->
<%= Funcoes.InvocarTagArquivo("View/Js/jquery.min.js") %>
<%= Funcoes.InvocarTagArquivo("View/Js/bootstrap.js") %>
<%= Funcoes.InvocarTagArquivo("js/flip.js", true) %>
<%= Funcoes.InvocarTagArquivo("js/d3.min.js", true) %>
<%= Funcoes.InvocarTagArquivo("js/d3pie.min.js", true) %>
<%= Funcoes.InvocarTagArquivo("js/tour.js", true) %>
<!-- FIM JAVASCRIPT -->

</body>
</html>