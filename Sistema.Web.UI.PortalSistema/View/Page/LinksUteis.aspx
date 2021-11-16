<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage/ProfessorJanelas.Master" AutoEventWireup="true" CodeBehind="LinksUteis.aspx.cs" Inherits="Sistema.Web.UI.PortalProfessor.View.Page.LinksUteis" %>

<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%= Funcoes.InvocarTagArquivo("View/Js/LinksUteis.js", true) %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content">

        <% if (Autenticar("RF001"))
            { %>
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-12">
                <!--INÍCIO PAINEL TÍTULOS -->
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title" style="font-weight: bold;">
                            <i class="fa fa-list-alt" aria-hidden="true"></i>&nbsp;Associação Brasileira de Normas Técnicas
                        </h3>
                    </div>
                    <div class="panel-body" style="padding-bottom: 20px;">
                        <p>A ABNT é a Associação Brasileira de Normas Técnicas que define a base normativa para o desenvolvimento tecnológico do país.</p>
                        <p>Portanto, ela cria regras para tudo e essas regras devem ser respeitadas, para que haja uma padronização.</p>
                        <p>Sendo assim, há também regras para os trabalhos acadêmicos.</p>
                        <p>Essas regras definem como o trabalho deve ser realizado, o que deve ou não constar nele, como esses elementos devem ser dispostos no trabalho, etc.</p>

                        <strong>Clique no link abaixo para Consultar as Normas da ABNT :</strong>
                        <p>
                            <a href=" http://www.gedweb.com.br/univag" target="_blank" title="Consulta Normas ABNT">
                                <img src="../Img/ABNT.png" width="220" alt="ABNT" /></a>

                            <a href=" http://www.gedweb.com.br/univag" target="_blank">Consulta Normas da ABNT</a>
                        </p>
                    </div>
                </div>
            </div>
        </div>
        <% } %>
    </div>
</asp:Content>
