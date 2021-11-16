<%@ Page Title="" Language="C#" MasterPageFile="~/View/Master/Submodulo.master" AutoEventWireup="true" CodeBehind="UsuarioCampus.aspx.cs" Inherits="Sistema.Web.UI.Sistema.View.Page.UsuarioCampus" %>

<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!--INÍCIO CSS-->
    <%= Funcoes.InvocarTagArquivo("View/Css/usuario.css" , true) %>
    <!--FIM CSS-->

    <!--INÍCIO JAVASCRIPT-->
    <%= Funcoes.InvocarTagArquivo("View/Js/usuariocampus.js" , true) %>
    <!--FIM JAVASCRIPT-->

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!--INÍCIO NAVEGAÇÃO-->
    <div>
        <ol class="breadcrumb">
            <li><a href="../Page/Usuario.aspx" title="Voltar para página Usuário" target="_self">Usuário</a></li>
            <li class="active current">Vincular Campus</li>
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
