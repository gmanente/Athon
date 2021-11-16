<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Redirecionando.aspx.cs" Inherits="Sistema.Web.UI.PortalProfessor.View.Page.Redirecionando" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <meta name="robots" content="noindex, nofollow" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <title>Aguarde um momento...</title>
    <style type="text/css">
    html, body {width: 100%; height: 100%; margin: 0; padding: 0;}
    body {background-color: #ffffff; font-family: Helvetica, Arial, sans-serif; font-size: 100%;}
    h1 {font-size: 1.5em; color: #404040; text-align: center;}
    p {font-size: 1em; color: #404040; text-align: center; margin: 10px 0 0 0;}
    @-webkit-keyframes bubbles { 33%: { -webkit-transform: translateY(10px); transform: translateY(10px); } 66% { -webkit-transform: translateY(-10px); transform: translateY(-10px); } 100% { -webkit-transform: translateY(0); transform: translateY(0); } }
    @keyframes bubbles { 33%: { -webkit-transform: translateY(10px); transform: translateY(10px); } 66% { -webkit-transform: translateY(-10px); transform: translateY(-10px); } 100% { -webkit-transform: translateY(0); transform: translateY(0); } }
    .bubbles { background-color: #404040; width:15px; height: 15px; margin:2px; border-radius:100%; -webkit-animation:bubbles 0.6s 0.07s infinite ease-in-out; animation:bubbles 0.6s 0.07s infinite ease-in-out; -webkit-animation-fill-mode:both; animation-fill-mode:both; display:inline-block; }
    #bubbles { text-align: center }
    </style>
    <script>function redirectFunction() { document.location.href = '<%= GetUrlTokenPortalBiblioteca() %>'; }</script>
</head>
<body onload="javascript:setTimeout('redirectFunction()', 3000)"> 
    <table width="100%" height="100%">
    <tr>
        <td>
            <div id="bubbles"><div class="bubbles"></div><div class="bubbles"></div><div class="bubbles"></div></div>
            <h1>Autenticando o seu acesso ao Portal da Biblioteca.</h1>
            <p>Este processo é automático. Seu navegador redirecionará rapidamente o seu conteúdo solicitado.</p>
            <p>Aguarde um momento...</p>
        </td>
    </tr>
    </table>
</body>
</html>