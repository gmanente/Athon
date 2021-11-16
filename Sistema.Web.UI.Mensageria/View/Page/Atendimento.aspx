<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage/Submodulo.Master" AutoEventWireup="true" CodeBehind="Atendimento.aspx.cs" Inherits="Sistema.Web.UI.Mensageria.View.Page.Atendimento" %>

<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <!--INÍCIO CSS-->
    <%= Funcoes.InvocarTagArquivo("View/Css/atendimento.css" , true) %>
    <!--FIM CSS-->

    <!--INÍCIO JAVASCRIPT-->
    <%= Funcoes.InvocarTagArquivo("View/Js/atendimento.js" , true) %>
    <!--FIM JAVASCRIPT-->

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
     <%
        GetGridTemplate().Render();
     %>

</asp:Content>
