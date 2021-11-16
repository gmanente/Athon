<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EsqueciMinhaSenha.aspx.cs" Inherits="Sistema.Web.UI.Sistema.EsqueciMinhaSenha" %>
<%@ Import Namespace="Sistema.Api.dll.Src" %>
<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>

<!DOCTYPE html>
<!--INÍCIO DA EMULAÇÃO CROSS-BROWSER-->
<!--[if lt IE 7]>  <html class="no-js lt-ie9 lt-ie8 lt-ie7"> <![endif]-->
<!--[if IE 7]>     <html class="no-js lt-ie9 lt-ie8"> <![endif]-->
<!--[if IE 8]>     <html class="no-js lt-ie9"> <![endif]-->
<!--[if gt IE 8]><!-->
<html xmlns="http://www.w3.org/1999/xhtml" lang="pt-br" class="no-js">
<!--<![endif]-->
<!--FIM DA EMULAÇÃO CROSS-BROWSER-->

<head id="Head1" runat="server">

    <!--INÍCIO CHAR SET-->
    <meta charset="utf-8" />
    <!--FINAL CHAR SET-->

    <!--INÍCIO TÍTULO DA APLICAÇÃO-->
    <title>Sistema - Recuperar Senha</title>
    <!--FIM TÍTULO DA APLICAÇÃO-->

    <!--INÍCIO AUTOR & DIREITOS AUTORAIS & DISTRIBUIÇÃO DA APLICAÇÃO-->
    <meta name="author" content="Athon Sistemas" />
    <meta name="copyright" content="© <% DateTime ano = DateTime.Now; Response.Write(ano.ToString("yyyy")); %> | Todos os direitos reservados" />
    <meta name="document-type" content="Private" />
    <meta name="document-distribution" content="IU" />
    <!--FIM AUTOR & DIREITOS AUTORAIS & DISTRIBUIÇÃO DA APLICAÇÃO-->

    <!--INÍCIO META TAG DE COMPATIBILIDADE CROSS-BROWSER-->
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <!--FIM META TAG DE COMPATIBILIDADE CROSS-BROWSER-->

    <!--INÍCIO VIEWPORT PARA RESPONSIVIDADE EM DIFERENTES RESOLUÇÕES DE TELA-->
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!--FIM VIEWPORT PARA RESPONSIVIDADE EM DIFERENTES RESOLUÇÕES DE TELA-->

    <!--INÍCIO META TAGS PARA NÃO INDEXAÇÃO DO SISTEMA COM MOTORES DE BUSCA-->
    <meta name="robots" content="noindex" />
    <meta name="googlebot" content="noindex" />
    <!--FIM META TAGS PARA NÃO INDEXAÇÃO DO SISTEMA COM MOTORES DE BUSCA-->
    
    <!--INÍCIO FAVICON-->
    <link rel="icon" href="img/favicon.png" type="image/x-icon" />
    <link rel="shortcut icon" href="img/favicon.png" type="image/x-icon" />
    <link rel="shortcut icon" href="img/favicon.png" type="image/vnd.microsoft.icon" />
    <!--FIM FAVICON-->


    <!--INÍCIO CSS-->
    <%= Funcoes.InvocarTagArquivo("View/Css/normalize.css") %>
    <%= Funcoes.InvocarTagArquivo("View/Css/bootstrap.css") %>
    <%= Funcoes.InvocarTagArquivo("css/font-awesome.min.css" , true) %>
    <%= Funcoes.InvocarTagArquivo("View/Css/jquery.resizableColumns.css") %>
    <%= Funcoes.InvocarTagArquivo("View/Css/bootstrap-theme.css") %>
    <%= Funcoes.InvocarTagArquivo("View/Css/pre-sets.css") %>
    <%= Funcoes.InvocarTagArquivo("css/esqueciminhasenha.css" , true) %>
    <!--FIM CSS-->
    
    
        <!--INÍCIO JAVASCRIPT-->
    <%= Funcoes.InvocarTagArquivo("View/Js/jquery.min.js") %>
    <%= Funcoes.InvocarTagArquivo("View/Js/bootstrap.js") %>
    <%= Funcoes.InvocarTagArquivo("View/Js/lib.js") %>
    <%= Funcoes.InvocarTagArquivo("View/Js/validate.js") %>
    <%= Funcoes.InvocarTagArquivo("View/Js/ajaxhandler.js") %>
    <%= Funcoes.InvocarTagArquivo("View/Js/serializeObject.js") %>
    <%= Funcoes.InvocarTagArquivo("Js/esqueciminhasenha.js" , true) %>
    <%= Funcoes.InvocarTagArquivo("View/Js/jquery.cookie.js") %>
    <!--FIM JAVASCRIPT-->
    
</head>
<body>    
    <!--INÍCIO NOSCRIPT-->
    <!--[if lt IE 7]>
         <p class="chromeframe">Você está utilizando um browser <strong>desatualizado!</strong> Por favor <a href="http://browsehappy.com/">atualize seu broswe</a> ou <a href="http://www.google.com/chromeframe/?redirect=true">ative o Google Frame</a> para melhorar sua experiência.</p>
        <![endif]-->
    <noscript>
        <h2>Javascript desabilitado em seu navegador! Acesso negado.<br/><a href="http://www.enable-javascript.com/pt/" target="_blank" title="Como habilitar o javascript?">Clique aqui</a> e aprenda como habilita-lo para utilizar o Sistema de Gerenciamento SISGER.</h2>
    </noscript>
    <!--FIM NOSCRIPT-->

    <!--INÍCIO LOADING-->
    <div id="loader"></div>
    <!--FIM LOADING-->

    <!--INÍCIO CONTAINER-->
    <div class="container">

        <!--INÍCIO FORM-->
        <form id="form" runat="server" class="form-signin">
          
            <!--INÍCIO SCRIPT MANAGER-->
            <asp:ScriptManager ID="scm" runat="server" EnablePageMethods="true" />
            <!--FIM SCRIPT MANAGER-->

            <a href="Login.aspx" id="login_logo" title="Athon Sistemas">
                <img src="../img/capa_login_422x185.png" alt="Athon Sistemas" />
            </a>

            <div class="form-content">
                <h4 class="form-signin-heading">Para realizar a recuperação de senha por favor digite seu usuário de login abaixo:</h4>

                <!--INÍCIO CONSOLE-->
                <div id="console"></div>
                <!--FIM CONSOLE-->

                 <div class="form-group">
                    <div class="input-group" style="margin-bottom:10px;">
                        <span class="input-group-addon"><i class="fa fa-user" style="width:16px;"></i></span>
                        <input type="text" class="form-control required" id="login" name="login" placeholder="Digite seu usuário de rede" data-msg-required="Por favor informe o usuário" />
                    </div>
                </div>

                <img src="CImage.aspx" id="ImageCaptcha" title="Clique para alterar a imagem" style="margin-bottom:10px;" />

                <div id="audio-player" style="display:none"></div>

                <button type="button" id="btn-audio" class="btn btn-default" title="Clique para ouvir os dígitos da imagem"><i class="fa fa-spinner fa-spin"></i></button>

                <div class="form-group">
                    <input type="text" class="form-control required number" id="captcha" name="captcha" maxlength="5" placeholder="Digite os dígitos da imagem acima" data-msg-required="Por favor informe os dígitos da imagem" />
                </div>               

                <button class="btn btn-lg btn-primary btn-block" disabled="disabled" id="btn-recuperar-senha" type="submit" data-acao="recuperar-senha"><i class="fa fa-retweet"></i> Recuperar Senha</button>

                <a class="btn btn-lg btn-default btn-block" href="Login.aspx" title="Voltar para a página de login"><i class="fa fa-reply"></i> Voltar para o Login</a>

            </div>

        </form>
        <!--FIM FORM-->

    </div>
    <!--FIM CONTAINER-->

    <img src="img/logo_branco_300x100.png" id="logo-login" alt="Athon" title="Athon Sistemas" />

    <!--INÍCIO FOOTER-->
    <footer id="footer">
        <p>© <%= DateTime.Now.Year %> Athon Sistemas.  Todos os direitos reservados.</p>
    </footer>
    <!--INÍCIO FOOTER-->

    <script>
        // jQuery
        $(document).ready(function () {

        });
    </script>

</body>

</html>