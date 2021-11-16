<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErroRelatorio.aspx.cs" Inherits="Sistema.Web.UI.Repositorio.View.Page.ErroRelatorio" %>

<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title><%=Title %></title>

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
    <link rel="icon" href="/View/img/favicon.png" />
    <!--FIM FAVICON-->

    <!-- INÍCIO CSS -->
    <%= Funcoes.InvocarTagArquivo("View/Css/normalize.css",true) %>
    <%= Funcoes.InvocarTagArquivo("View/Css/bootstrap.css",true) %>
    <%= Funcoes.InvocarTagArquivo("View/Css/font-awesome.min.css", true) %>
    <%= Funcoes.InvocarTagArquivo("View/Css/bootstrap-theme.css",true) %>
    <%= Funcoes.InvocarTagArquivo("View/Css/erro.css", true) %>
    <!-- FIM CSS -->

    <script type="text/javascript">
        //setTimeout("document.location = 'Login.aspx'", 5000);
    </script>
</head>
<body>

    <div class="container" style="padding-top: 100px;">
        <form id="form" runat="server">

            <div class="panel">
                <h1 class="error-text"><i class="fa <%=Icone %>"></i><%=Title %></h1>

                <div class="row">
                    <div class="col-md-12">
                        <h2 class="error-msg"><%=Mensagem %></h2>
                    </div>
                </div>
                <%--<div class="row">
                   <div class="col-md-12">
                       <ul class="error-links">
                           <li><a href="/Principal.aspx">Acessar Página Inicial</a></li>
                           <li><a href="javascript:history.back();">Voltar Página</a></li>
                       </ul>                        
                   </div>
               </div>--%>
            </div>

        </form>
    </div>
    <div class="text-center">
        <!-- INÍCIO LOGO RODAPÉ -->
        <img src="/View/img/logo_branco_300x100.png" id="logo-login" alt="Univag" title="Athon Sistemas" />
        <!-- FIM LOGO RODAPÉ -->
    </div>
    <!-- INÍCIO RODAPÉ -->
    <footer id="footer">
        <p>© <%= DateTime.Now.Year %> Athon Sistemas. Todos os direitos reservados.</p>
    </footer>
    <!-- FIM RODAPÉ -->
</body>
</html>
