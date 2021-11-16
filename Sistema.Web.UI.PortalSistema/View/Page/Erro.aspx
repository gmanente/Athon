<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Erro.aspx.cs" Inherits="Sistema.Web.UI.PortalProfessor.View.Page.Erro" %>
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
    <%= Funcoes.InvocarTagArquivo("css/font-awesome.min.css", true) %>
    <%= Funcoes.InvocarTagArquivo("View/Css/bootstrap-theme.css") %>
   
    <!-- FIM CSS -->


    <style>
        /*
    ERRO CSS
    AUTOR: Evander Emanuel da Silva Costa
    ORGANIZAÇÃO: Univag - Centro Universitário (NTI - Núcleo de Tecnologia da Informação)
*/
@charset "utf-8";

body {
    background: url('../img/background_login.png') #142752;
}

.panel {
    padding: 40px;
}

.error-text {
    text-align:center;
    font-weight: 400;
    color: #fff;
    letter-spacing: -4px;
    font-size: 700%;
    margin-bottom: 30px;
    text-shadow: 0 1px 0 #ccc,0 2px 0 #bfbfbf,0 3px 0 #bbb,0 4px 0 #b9b9b9,0 5px 0 #aaa,0 6px 1px rgba(0,0,0,.1),0 0 5px rgba(0,0,0,.1),0 1px 3px rgba(0,0,0,.3),0 3px 5px rgba(0,0,0,.2),0 5px 10px rgba(0,0,0,.25),0 10px 10px rgba(0,0,0,.2),0 20px 20px rgba(0,0,0,.15);
}

.error-msg {
    text-align:center;
    color:#666;
}
.error-links {
    width: 220px;
    margin: 20px auto;
    font-size: 1.2em;
}

#form {
    max-width: 960px;
    padding: 15px;
    margin: 0 auto;
}


#footer
{
    position: fixed;
    bottom: 0;
    width: 100%;
    /* Set the fixed height of the footer here */
    height: 3em;
    background: linear-gradient(#f5f5f5, #c4c4c4);
    text-align: center;
    padding-top: 1em;
    box-shadow: 0px 0px 15px 7px rgba(0,0,0,0.4);
}

#logo-login {
    position:fixed;
    bottom: 100px;
    right: 40px;
    width: 300px;
    height: 100px;
}

@media screen and (max-width: 1366px) {
    #logo-login {
        bottom: 60px;
        right: 30px;
        width: 250px;
        height: 83px;
    }
}

@media screen and (max-width: 1280px) {
    #logo-login {
        bottom: 60px;
        right: 30px;
        width: 220px;
        height: 73px;
    }
}
@media screen and (max-width: 800px) {
    #footer {
        display:none;
    }
    #logo-login {
        display:none;
    }
}

    </style>

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
                           <% if(Request.QueryString["s"] != null && Request.QueryString["s"].ToString() == "sessao-expirada") { %>
                           <li><a target="_parent" href="/Login.aspx">Fazer novo Login</a></li>
                           <% } else { %>
                           <%--<li><a href="/Principal.aspx">Acessar Página Inicial</a></li>--%>
                           <%--<li><a href="javascript:history.back();">Voltar Página</a></li>--%>
                           <% } %>
                       </ul>                        
                   </div>
               </div>
            </div>
        
        </form>
    </div>

    <!-- INÍCIO LOGO RODAPÉ -->
    <img src="/img/logo_branco_300x100.png" id="logo-login" alt="Univag" title="Univag - Centro Universitário" />
    <!-- FIM LOGO RODAPÉ -->

    <!-- INÍCIO RODAPÉ -->
    <footer id="footer">
        <p>© <%= DateTime.Now.Year %> SISUNIVAG - Sistemas Univag. Todos os direitos reservados.</p>
    </footer>
    <!-- FIM RODAPÉ -->
</body>
</html>
