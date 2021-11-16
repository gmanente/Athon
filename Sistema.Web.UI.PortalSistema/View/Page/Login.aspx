<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Sistema.Web.UI.PortalProfessor.View.Page.Login" %>

<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>
<%@ Import Namespace="Sistema.Web.UI.PortalProfessor.Util" %>
<%@ Import Namespace="Sistema.Web.UI.PortalProfessor" %>


<!DOCTYPE html>
<html lang="en-us" id="extr-page">
<head>

    <!--INÍCIO CHAR SET-->
    <meta charset="utf-8">
    <!--FINAL CHAR SET-->

    <!--INÍCIO TÍTULO DA APLICAÇÃO-->
    <title>Portal Professor Univag - Login</title>
    <!--FIM TÍTULO DA APLICAÇÃO-->

    <!--INÍCIO AUTOR & DIREITOS AUTORAIS & DISTRIBUIÇÃO DA APLICAÇÃO-->
    <meta name="author" content="Univag - Centro Universitário (NPD - Núcleo de Processamento de Dados)">
    <meta name="copyright" content="© <% DateTime ano = DateTime.Now; Response.Write(ano.ToString("yyyy")); %> | Todos os direitos reservados">
    <meta name="document-type" content="Private">
    <meta name="document-distribution" content="IU">


    <!--Elimina Cache da Página-->
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="0" />
    <!--Elimina Cache da Página-->

    <!--FIM AUTOR & DIREITOS AUTORAIS & DISTRIBUIÇÃO DA APLICAÇÃO-->

    <!--INÍCIO META TAG DE COMPATIBILIDADE CROSS-BROWSER-->
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <!--FIM META TAG DE COMPATIBILIDADE CROSS-BROWSER-->

    <!--INÍCIO VIEWPORT PARA RESPONSIVIDADE EM DIFERENTES RESOLUÇÕES DE TELA-->
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!--FIM VIEWPORT PARA RESPONSIVIDADE EM DIFERENTES RESOLUÇÕES DE TELA-->

    <!--INÍCIO FAVICON-->
    <link rel="icon" href="../Img/favicon.png" type="image/x-icon" />
    <link rel="shortcut icon" href="../Img/favicon.png" type="image/x-icon" />
    <link rel="shortcut icon" href="../Img/favicon.png" type="image/vnd.microsoft.icon" />
    <!--FIM FAVICON-->

    <!-- #CSS Links -->
    <!-- Basic Styles -->
    <%= Funcoes.InvocarTagArquivo("View/Css/bootstrap.min.css", true) %>
    <%--<link rel="stylesheet" type="text/css" media="screen" href="../css/bootstrap.min.css">--%>

    <%= Funcoes.InvocarTagArquivo("View/Css/font-awesome.min.css", true) %>
    <%--<link rel="stylesheet" type="text/css" media="screen" href="../css/font-awesome.min.css">--%>
    <!-- SmartAdmin Styles : Please note (smartadmin-production.css) was created using LESS variables -->

    <%= Funcoes.InvocarTagArquivo("View/Css/smartadmin-production.min.css", true) %>
    <%--<link rel="stylesheet" type="text/css" href="../css/smartadmin-production.min.css">--%>
    <%--    <link rel="stylesheet" type="text/css" media="screen" href="../css/smartadmin-skins.min.css">--%>

    <%= Funcoes.InvocarTagArquivo("View/Css/jumbotron-narrow.css", true) %>
    <%--<link href="../Css/jumbotron-narrow.css" rel="stylesheet" />--%>

    <%= Funcoes.InvocarTagArquivo("View/Css/sweet-alert.css") %>
    <%= Funcoes.InvocarTagArquivo("View/Js/sweet-alert.min.js") %>

    <!-- SmartAdmin RTL Support is under construction
			 This RTL CSS will be released in version 1.5
		<link rel="stylesheet" type="text/css" media="screen" href="css/smartadmin-rtl.min.css"> -->

    <!-- We recommend you use "your_style.css" to override SmartAdmin
		     specific styles this will also ensure you retrain your customization with each SmartAdmin update.
		<link rel="stylesheet" type="text/css" media="screen" href="css/your_style.css"> -->

    <!-- Demo purpose only: goes with demo.js, you can delete this css when designing your own WebApp -->
    <%--    <link rel="stylesheet" type="text/css" media="screen" href="../css/demo.min.css">--%>

    <!-- #FAVICONS -->
    <link rel="shortcut icon" href="../Img/favicon/icon.jpg" type="image/x-jpg">
    <link rel="icon" href="../Img/favicon/icon.jpg" type="image/x-jpg">

    <!-- #GOOGLE FONT -->
    <%--<link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Open+Sans:400italic,700italic,300,400,700">--%>

    <!-- #APP SCREEN / ICONS -->
    <!-- Specifying a Webpage Icon for Web Clip 
			 Ref: https://developer.apple.com/library/ios/documentation/AppleApplications/Reference/SafariWebContent/ConfiguringWebApplications/ConfiguringWebApplications.html -->
    <link rel="apple-touch-icon" href="../Img/logo.png">
    <link rel="apple-touch-icon" sizes="76x76" href="../Img/logo.png">
    <link rel="apple-touch-icon" sizes="120x120" href="../Img/logo.png">
    <link rel="apple-touch-icon" sizes="152x152" href="../Img/logo.png">

    <!-- iOS web-app metas : hides Safari UI Components and Changes Status Bar Appearance -->
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">

    <!-- Startup image for web apps -->
    <link rel="apple-touch-startup-image" href="img/splash/ipad-landscape.png" media="screen and (min-device-width: 481px) and (max-device-width: 1024px) and (orientation:landscape)">
    <link rel="apple-touch-startup-image" href="img/splash/ipad-portrait.png" media="screen and (min-device-width: 481px) and (max-device-width: 1024px) and (orientation:portrait)">
    <link rel="apple-touch-startup-image" href="img/splash/iphone.png" media="screen and (max-device-width: 320px)">
</head>

<body class="animated fadeInDown">

    <!-- INÍCIO CAMPOS OCULTOS -->
    <input type="hidden" id="SessionId" value="<%=Request.Params["ASP.NET_SessionId"] %>" />
    <input type="hidden" id="ServerName" value="<%=Request.ServerVariables["SERVER_NAME"] %>" />
    <input type="hidden" id="EnderecoIp" value="<%=Request.UserHostAddress %>" />
    <input type="hidden" id="BrowserVersao" value="<%=Request.Browser.Version.ToString() %>" />
    <input type="hidden" id="BrowserTipo" value="<%=Request.Browser.Win32.ToString() %>" />
    <input type="hidden" id="BrowserNome" value="<%=Request.Browser.Browser.ToString() %>" />
    <!-- FIM CAMPOS OCULTOS -->

    <!--INÍCIO NOSCRIPT-->
    <!--[if lt IE 7]>
    <p class="chromeframe">Você está utilizando um browser <strong>desatualizado!</strong> Por favor <a href="http://browsehappy.com/">atualize seu broswe</a> ou <a href="http://www.google.com/chromeframe/?redirect=true">ative o Google Frame</a> para melhorar sua experiência.</p>
    <![endif]-->
    <noscript>
        <h2>Javascript desabilitado em seu navegador! Acesso negado.<br />
            <a href="http://www.enable-javascript.com/pt/" target="_blank" title="Como habilitar o javascript?">Clique aqui</a> e aprenda como habilita-lo para utilizar o Sistema de Gerenciamento SISGER.</h2>
    </noscript>
    <!--FIM NOSCRIPT-->
    <!--INÍCIO LOADING-->
    <header id="header">
        <div id="logo-group">
            <span id="logo">
                <img src="../Img/logo.png" alt="Portal Professor">
            </span>
        </div>
        <div id="help-tutorial">
            <a href="docs/guia-de-acesso-ao-sistema-univag.pdf" target="_blank" class="btn btn-info i-btn" id="tutorial">
                <i class="fa fa-lg fa-fw fa-question-circle"></i>&nbsp;Tutorial
            </a>
        </div>

    <!--INICIO APPLICATION INSIGHTS-->
    <script type="text/javascript">
        var appInsights = window.appInsights || function (a) {
            function b(a) { c[a] = function () { var b = arguments; c.queue.push(function () { c[a].apply(c, b) }) } } var c = { config: a }, d = document, e = window; setTimeout(function () { var b = d.createElement("script"); b.src = a.url || "https://az416426.vo.msecnd.net/scripts/a/ai.0.js", d.getElementsByTagName("script")[0].parentNode.appendChild(b) }); try { c.cookie = d.cookie } catch (a) { } c.queue = []; for (var f = ["Event", "Exception", "Metric", "PageView", "Trace", "Dependency"]; f.length;)b("track" + f.pop()); if (b("setAuthenticatedUserContext"), b("clearAuthenticatedUserContext"), b("startTrackEvent"), b("stopTrackEvent"), b("startTrackPage"), b("stopTrackPage"), b("flush"), !a.disableExceptionTracking) { f = "onerror", b("_" + f); var g = e[f]; e[f] = function (a, b, d, e, h) { var i = g && g(a, b, d, e, h); return !0 !== i && c["_" + f](a, b, d, e, h), i } } return c
        }({
            instrumentationKey: "b10dd8ec-729f-4662-8718-3a4a6a4c66ca"
        });

        window.appInsights = appInsights, appInsights.queue && 0 === appInsights.queue.length && appInsights.trackPageView();
    </script>
     <!--FIM APPLICATION INSIGHTS-->

        <%--		<span id="extr-page-header-space"> <span class="hidden-mobile">Need an account?</span> <a href="register.html" class="btn btn-danger">Create account</a> </span>--%>
    </header>

    <div id="container">

        <!-- MAIN CONTENT -->
        <div id="content" class="container">
            <div class="jumbotron">

                <form id="form" class="smart-form client-form">
                    <div id="console-login">
                        <% // Sessão expirada
                            if (Request.QueryString["status"] == "sessao-expirada")
                            { %>
                        <div class='alert alert-dismissable alert-warning'>
                            <button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button>
                            <strong>Status: </strong>Sessão expirada!<br />
                            Por favor faça login novamente.
                        </div>
                        <%  }
                            // Senha alterada
                            if (Request.QueryString["senhaAlterada"] != null && Request.QueryString["senhaAlterada"] == "true")
                            { %>
                        <div class='alert alert-dismissable alert-info'>
                            <button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button>
                            <strong>Status: </strong>Usuário deslogado.<br />
                            Por favor faça login novamente utilizando a nova senha cadastrada.
                        </div>
                        <% } // Usuário deslogado
                            else if (Request.QueryString["status"] == "logoff")
                            { %>
                        <div class='alert alert-dismissable alert-info'>
                            <button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button>
                            <strong>Status: </strong>Usuário deslogado.<br />
                            Para realizar login novamente preencha os campos abaixo.
                        </div>
                        <% } %>
                    </div>


                    <div id="foto-aluno">
                        <i class="fa fa-user fa-5x"></i>
                        <img src="/View/Img/avatars/male.png" id="Img1" width="130" height="130" style="display: none" class="img-circle">
                    </div>

                    <header>
                        <h3>Acesso ao Portal do Professor</h3>

                        <%=Ambiente != "" ? "<p style=\"color: #7517d3; font-size: 1.4rem;\">" + Ambiente + "<br>Senha: "+ SenhaDesenvolvimento +"</p>" : "" %>
                    </header>

                    <fieldset>
                        <section>
                            <label class="input">
                                <i class="icon-append fa fa-user"></i>
                                <input type="text" class="required" name="usuario" id="usuario" placeholder="Usuário (CPF)" title="Informe o CPF do usuário">
                                <b class="tooltip tooltip-top-right">
                                    <i class="fa fa-user txt-color-teal"></i> Informe seu usuário (CPF)
                                </b>
                            </label>
                        </section>

                        <section>
                            <label class="input">
                                <i class="icon-append fa fa-lock"></i>
                                <input type="password" class="form-control required" name="senha" id="senha" placeholder="Senha" value="<%=Ambiente != "" ? SenhaDesenvolvimento : "" %>">
                                <b class="tooltip tooltip-top-right">
                                    <i class="fa fa-lock txt-color-teal"></i> Informe sua senha
                                </b>
                            </label>
                        </section>

                        <div id="box-captcha" style="display: none">
                            <img src="#" class="imagem-captcha" alt="Captcha" title="Clique para alterar a imagem" width="300" height="75" style="margin-bottom: 10px;" />

                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-shield"></i></span>
                                    <input type="text" class="form-control required number captcha" id="captcha" name="captcha" maxlength="5" placeholder=" Digite os dígitos da imagem" data-msg-required="Por favor informe os dígitos da imagem" />
                                </div>
                            </div>
                            <span for="captcha" class="error"></span>
                        </div>

                        <%--   <section>
                            <label class="checkbox">
                                <input type="checkbox" name="remember" checked="">
                                <i></i>Stay signed in</label>
                        </section>--%>
                    </fieldset>
                    <footer>
                        <div class="btn-group">
                            <a href="#" class="btn btn-primary" id="btn-entrar"><span class="fa fa-sign-in"></span>&nbsp;Entrar</a>
                        </div>
                        <div class="btn-group">
                            <a class="btn btn-default i-btn" data-window="RecuperarSenha" id="recuperarSenha">
                                <i class="fa fa-unlock-alt"></i>&nbsp;Recuperar Senha
                            </a>
                        </div>
                    </footer>
                </form>
            </div>
        </div>
    </div>


    <!-- MODAL SOLICITAÇÃO -->
    <div class="modal fade" id="modal-solicitacao" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog" style="width: 60%; max-width: 480px;">

            <div class="modal-content">
                <div class="modal-header" style="background: #033649; color: white;">
                    <button type="button" class="close" data-dismiss="modal">
                        <span aria-hidden="true" style="color: white;">&times;</span>
                        <span class="sr-only">Close</span></button>
                    Recuperar senha Portal Professor - UNIVAG
                </div>
                <div class="modal-body">
                    <div id="alerta-recuperar-senha" style="display: none">
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <label>Digite seu usuário para Recuperar a Senha:</label>
                            <div class="input-group i-recupera">
                                <span class="input-group-addon addon-tam"><span class="fa fa-user"></span></span>
                                <input type="text" class="form-control required" id="usuario2" name="usuario2" placeholder="Digite seu usuário" data-msg-required="Por favor informe o usuário" />
                            </div>
                            <div class="input-group" style="padding: 0px; margin: 0px; height: 75px;">
                                <!-- INÍCIO CAMPO CAPTCHA -->
                                <img class="imagem-captcha img-responsive" id="img-captcha2" alt="Captcha" title="Clique para alterar a imagem" />&nbsp;
                                    <%--<button type="button" class="btn btn-default btn-play-audio" title="Clique para ouvir os dígitos da imagem"><i class="fa fa-spinner fa-spin"></i></button>                                --%>
                            </div>
                            <div class="input-group i-recupera">
                                <span class="input-group-addon addon-tam"><span class="fa fa-shield"></span></span>
                                <input type="text" class="form-control required number captcha" id="captcha2" name="captcha2" maxlength="5" placeholder="Digite os dígitos da imagem" data-msg-required="Por favor informe os dígitos da imagem" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="background: #EDEDED">
                    <button type="button" class="btn btn-default pull-left" data-dismiss="modal">
                        <i class="fa fa-reply"></i>&nbsp;voltar para o Login
                    </button>
                    <button type="button" class="btn btn-primary" title="Gravar os dados" id="btn-confirmar">
                        <i class="fa fa-retweet"></i>&nbsp;Recuperar Senha
                    </button>

                </div>
            </div>
        </div>
    </div>


    <div id="footer">
        <p>© <%= DateTime.Now.Year %> Portal Professor UNIVAG - Sistemas Univag. Todos os direitos reservados.</p>
    </div>
    <!--================================================== -->
    <img src="../Img/logo_branco_300x100.png" id="logo-login" alt="Univag" title="Univag - Centro Universitário">
    <!-- PACE LOADER - turn this on if you want ajax loading to show (caution: uses lots of memory on iDevices)-->
    <script src="../js/plugin/pace/pace.min.js"></script>

    <!-- Link to Google CDN's jQuery + jQueryUI; fall back to local -->
    <!--<script src="//ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js"></script>-->
    <script> if (!window.jQuery) { document.write('<script src="/View/Js/libs/jquery-2.0.2.min.js"><\/script>'); } </script>

    <!--<script src="//ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/jquery-ui.min.js"></script>-->
    <script> if (!window.jQuery.ui) { document.write('<script src="/View/Js/libs/jquery-ui-1.10.3.min.js"><\/script>'); } </script>

    <!-- JS TOUCH : include this plugin for mobile drag / drop touch events 		
		<script src="js/plugin/jquery-touch/jquery.ui.touch-punch.min.js"></script> -->

    <!-- BOOTSTRAP JS -->
    <%--<script src="../js/bootstrap/bootstrap.min.js"></script>--%>
    <%= Funcoes.InvocarTagArquivo("View/Js/bootstrap/bootstrap.min.js", true) %>

    <!-- JQUERY VALIDATE -->
    <%--<script src="../js/plugin/jquery-validate/jquery.validate.min.js"></script>--%>

    <!-- JQUERY MASKED INPUT -->
    <%--<script src="../js/plugin/masked-input/jquery.maskedinput.min.js"></script>--%>
    
    <%= Funcoes.InvocarTagArquivo("View/Js/plugin/masked-input/jquery.maskedinput.min.js", true) %>
    <%= Funcoes.InvocarTagArquivo("View/Js/webstorage.js") %>
    <%= Funcoes.InvocarTagArquivo("View/Js/lib.js") %>
    <%= Funcoes.InvocarTagArquivo("View/Js/ajaxhandler.js") %>

    <%= Funcoes.InvocarTagArquivo("View/Js/login.js", true) %>
    <%= Funcoes.InvocarTagArquivo("View/Js/validate.js") %>
    <!--[if IE 8]>
			
			<h1>Your browser is out of date, please update your browser by going to www.microsoft.com/download</h1>
			
		<![endif]-->

    <!-- MAIN APP JS FILE -->
    <%--  <script src="../Js/app.min.js"></script>--%>
</body>
</html>
