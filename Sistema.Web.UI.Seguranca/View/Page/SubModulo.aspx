<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage/Submodulo.master" AutoEventWireup="true" CodeBehind="SubModulo.aspx.cs" Inherits="Sistema.Web.UI.Seguranca.View.Page.SubModulo" %>

<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!--INÍCIO CSS-->
    <%= Funcoes.InvocarTagArquivo("View/Css/modulo.css" , true) %>
    <%= Funcoes.InvocarTagArquivo("View/Css/fontawesome-iconpicker.min.css") %>
    <!--FIM CSS-->

    <!--INÍCIO JAVASCRIPT-->
    <%= Funcoes.InvocarTagArquivo("View/Js/submodulo.js" , true) %>
    <%= Funcoes.InvocarTagArquivo("View/Js/fontawesome-iconpicker.min.js") %> 
    <!--FIM JAVASCRIPT-->

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!--INÍCIO NAVEGAÇÃO-->
    <div>
        <ol class="breadcrumb">
            <li><a href="../Page/Modulo.aspx?SubModulo=<%= Request.QueryString["idSubModulo"] %>" title="Voltar para página Módulo" target="_self">Módulo</a></li>
            <li class="active current">Submódulo</li>
        </ol>
    </div>

    <%
        GetGridTemplate().Render();
    %>
</asp:Content>
