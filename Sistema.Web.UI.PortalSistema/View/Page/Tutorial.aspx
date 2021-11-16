<%@ Page Language="C#" MasterPageFile="~/View/MasterPage/ProfessorJanelas.Master" AutoEventWireup="true" CodeBehind="Tutorial.aspx.cs" Inherits="Sistema.Web.UI.PortalProfessor.View.Page.Tutorial" %>

<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%= Funcoes.InvocarTagArquivo("View/Js/Tutorial.js", true) %>
</asp:Content>

<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="content">
        <div class="row">
            <div class="col-md-12">
                <div class="alert alert-info">
                    <p><i class="fa fa-info-circle"></i>&nbsp; Para acompanhar um tutorial clique em uma das opções abaixo:</p>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="well">
                    <div class="row">
                        <h6>Tutoriais</h6>
                        <div class="dd" id="nestable">
                            <ol class="dd-list"></ol>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
