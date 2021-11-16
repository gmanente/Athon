<%@ Page Title="" Language="C#" MasterPageFile="~/View/Master/Submodulo.master" AutoEventWireup="true" CodeBehind="Funcionalidade.aspx.cs" Inherits="Sistema.Web.UI.Sistema.View.Page.Funcionalidade" %>

<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!--INÍCIO CSS-->
    <%= Funcoes.InvocarTagArquivo("View/Css/modulo.css" , true) %>
    <!--FIM CSS-->

    <!--INÍCIO JAVASCRIPT-->
    <%= Funcoes.InvocarTagArquivo("View/Js/funcionalidade.js" , true) %>
    <!--FIM JAVASCRIPT-->

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!--INÍCIO NAVEGAÇÃO-->
    <div>
        <ol class="breadcrumb">
            <li><a href="../Page/Modulo.aspx?SubModulo=<%= Request.QueryString["idSubModuloSis"] %>" title="Voltar para página Módulo" target="_self">Módulo</a></li>
            <li><a href="../Page/SubModulo.aspx?idModulo=<%= Request.QueryString["idModulo"] %>" title="Voltar para página Submódulo" target="_self">Submódulo</a></li>
            <li class="active current">Funcionalidades</li>
        </ol>
    </div>

    <%
        GetGridTemplate().Render();
    %>
</asp:Content>
