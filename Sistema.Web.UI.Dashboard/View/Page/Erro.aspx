<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Erro.aspx.cs" Inherits="Sistema.Web.UI.Dashboard.View.Page.Erro" %>
<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title><%=Title %> - SISUNIVAG</title>

    <!--INÍCIO CHARSET -->
    <meta charset="utf-8">
    <!--FIM CHARSET -->

    <!--INÍCIO META TAGS PARA NÃO INDEXAÇÃO DO SISTEMA COM MOTORES DE BUSCA-->
    <meta name="robots" content="noindex">
    <meta name="googlebot" content="noindex">
    <!--FIM META TAGS PARA NÃO INDEXAÇÃO DO SISTEMA COM MOTORES DE BUSCA-->
    
    <!--INÍCIO AUTOR & DIREITOS AUTORAIS & DISTRIBUIÇÃO DA APLICAÇÃO-->
    <meta name="author" content="Univag - Centro Universitário (NPD - Núcleo de Tecnologia da Informação)">
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

    <!-- INÍCIO CSS -->
    <%= Funcoes.InvocarTagArquivo("View/Css/normalize.css") %>
    <%= Funcoes.InvocarTagArquivo("View/Css/bootstrap.css") %>
    <%= Funcoes.InvocarTagArquivo("View/Css/font-awesome.min.css") %>
    <%= Funcoes.InvocarTagArquivo("View/Css/bootstrap-theme.css") %>
    <%= Funcoes.InvocarTagArquivo("View/Css/erro.css") %>
    <!-- FIM CSS -->

</head>
<body>

    <div class="container" style="padding-top:100px;"> 
        <form id="form" runat="server">
       
           <div class="panel">
                <h1 class="error-text">
                    <i class="fa <%=Icone %>"></i> <%=Titulo %>
                </h1>

                <div class="row">
                   <div class="col-md-12">
                        <h2 class="error-msg"><%=Mensagem %></h2>
                   </div>
                </div>               
                <div class="row">
                   <div class="col-md-12">
                       <ul class="error-links">
                           <%
                            string url = "//sistema.univag.edu.br";
                            url = HttpContext.Current.Request.Url.Host;
                               
                            if (Sistema.Api.dll.Src.Dominio.AppState == Sistema.Api.dll.Src.Dominio.ApplicationState.Teste)
                            {
                                url = "//sistema.univag.teste.edu.br";
                            }
                            else if (Sistema.Api.dll.Src.Dominio.AppState == Sistema.Api.dll.Src.Dominio.ApplicationState.Homologacao)
                            {
                                url = "//sistema.univaglabs.edu.br";
                            }
                            else if (Sistema.Api.dll.Src.Dominio.AppState == Sistema.Api.dll.Src.Dominio.ApplicationState.Debug)
                            {
                                url = HttpContext.Current.Request.Url.Authority;
                            }
                            %>

                           <% if(Request.QueryString["s"] != null && Request.QueryString["s"].ToString() == "sessao-expirada") { %>
                           <li><a target="_parent" href="<%=url %>/Login.aspx">Fazer novo Login</a></li>
                           <% } else { %>
                           <li><a href="javascript:history.back();">Voltar Página</a></li>
                           <% } %>
                       </ul>                        
                   </div>
               </div>
            </div>
        
        </form>
    </div>

    <!-- INÍCIO LOGO RODAPÉ -->
    <img src="/View/Img/logo_branco_300x100.png" id="logo-login" alt="Univag" title="Univag - Centro Universitário" />
    <!-- FIM LOGO RODAPÉ -->

    <!-- INÍCIO RODAPÉ -->
    <footer id="footer">
        <p>© <%= DateTime.Now.Year %> SISUNIVAG - Sistemas Univag. Todos os direitos reservados.</p>
    </footer>
    <!-- FIM RODAPÉ -->
</body>
</html>