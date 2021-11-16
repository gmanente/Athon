<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage/Submodulo.Master" AutoEventWireup="true" CodeBehind="Regime.aspx.cs" Inherits="Sistema.Web.UI.GerenciaFinanceira.View.Page.Regime" %>

<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">  
    
    <!--INÍCIO CSS-->
    <%= Funcoes.InvocarTagArquivo("View/Css/regime.css" , true) %>
    <!--FIM CSS-->

    <!--INÍCIO JAVASCRIPT-->
    <%= Funcoes.InvocarTagArquivo("View/Js/regime.js" , true) %>
    <!--FIM JAVASCRIPT-->

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
     <%
        GetGridTemplate().Render();
     %>

</asp:Content>
