<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage/Submodulo.master" AutoEventWireup="true" CodeBehind="UltimosAcessos.aspx.cs" Inherits="Sistema.Web.UI.Seguranca.View.Page.UltimosAcessos" %>

<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
    <!--INÍCIO CSS-->
    <%= Funcoes.InvocarTagArquivo("View/Css/ultimosacessos.css" , true) %>
    <!--FIM CSS-->

    <!--INÍCIO JAVASCRIPT-->
    <%= Funcoes.InvocarTagArquivo("View/Js/ultimosacessos.js" , true) %>
    <!--FIM JAVASCRIPT-->
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


        <%  //Erro parametro
        if (Request.QueryString["status"] == "erro-parametro")
        { %>
    <div class='alert alert-dismissable alert-danger'>
        <button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button>
        <strong>Status: </strong>Ocorreu um erro interno no servidor.<br />
        Por favor entre em contato com a equipe de desenvolvimento.
    </div>
    <% }  %>
    
     <!--INÍCIO ÚLTIMOS ACESSOS-->
    <% SetTemplate().Render(); %>
    <!--FIM ÚLTIMOS ACESSOS-->

</asp:Content>
