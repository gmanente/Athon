<%@ Page Title="" Language="C#" MasterPageFile="~/View/Master/Submodulo.master" AutoEventWireup="true" CodeBehind="Modulo.aspx.cs" Inherits="Sistema.Web.UI.Sistema.View.Page.Modulo" %>

<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!--INÍCIO CSS-->
    <%= Funcoes.InvocarTagArquivo("View/Css/modulo.css" , true) %>
    <%= Funcoes.InvocarTagArquivo("View/Css/fontawesome-iconpicker.min.css") %>
    <!--FIM CSS-->

    <!--INÍCIO JAVASCRIPT-->
    <%= Funcoes.InvocarTagArquivo("View/Js/modulo.js" , true) %>
    <%= Funcoes.InvocarTagArquivo("View/Js/fontawesome-iconpicker.min.js") %> 
    <!--FIM JAVASCRIPT-->

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>
        <ol class="breadcrumb">
            <li class="active current"><a href="../Page/Modulo.aspx" title="Voltar para página Módulo" target="_self">Módulo</a></li>
        </ol>
    </div>

    <%
        GetGridTemplate().Render();
    %>

    <input type="hidden" id="SaveGridHidden" value="<%=Criptografia.Base64Encode(GetUrlSubModulo()).Substring(0, 10)  %>" />
    <input type="hidden" id="IdUsuarioHidden" value="<%=GetSessao().IdUsuario %>" />
    <input type="hidden" id="IdSubModuloHidden" value="<%=GetIdSubModulo() %>" />
    <input type="hidden" id="SubModuloUrlHidden" value="<%=GetUrlSubModulo() %>" />

</asp:Content>
