<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage/Submodulo.master" AutoEventWireup="true" CodeBehind="UsuarioPerfil.aspx.cs" Inherits="Sistema.Web.UI.Seguranca.View.Page.UsuarioPerfil" %>

<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!--INÍCIO CSS-->
    <%= Funcoes.InvocarTagArquivo("View/Css/usuario.css" , true) %>
    <!--FIM CSS-->

    <!--INÍCIO JAVASCRIPT-->
    <%= Funcoes.InvocarTagArquivo("View/Js/usuarioperfil.js" , true) %>
    <!--FIM JAVASCRIPT-->

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!--INÍCIO NAVEGAÇÃO-->
    <div>
        <ol class="breadcrumb">
            <li><a href="../Page/Usuario.aspx" title="Voltar para página Usuário" target="_self">Usuário</a></li>
            <li><a href="../Page/UsuarioCampus.aspx?idUsuario=<%= Request.QueryString["IdUsuario"] %>" title="Voltar para página Vincular Empresa" target="_self">Vincular Empresa</a></li>
            <li class="active current">Vincular Perfil</li>
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
