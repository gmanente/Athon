<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage/Submodulo.master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="Sistema.Web.UI.Seguranca.View.Page.Perfil" %>

<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!--INÍCIO CSS-->
    <%= Funcoes.InvocarTagArquivo("View/Css/modulo.css" , true) %>
    <!--FIM CSS-->

    <!--INÍCIO JAVASCRIPT-->
    <%= Funcoes.InvocarTagArquivo("View/Js/perfil.js" , true) %>
    <!--FIM JAVASCRIPT-->

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>
        <ol class="breadcrumb">
            <li class="active current"><a href="../Page/Perfil.aspx" title="Voltar para página Perfil" target="_self">Perfil</a></li>
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
