<%@ Page Title="" Language="C#" MasterPageFile="~/masterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="Sistema.Web.UI.Sistema.Principal" %>
<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Athon Sistemas </title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<div class="row" id="divMenuRapido" style="display:none;">--%>

    <%--<% if (lstMenuRapidoVO.Count() > 0) { %>

        <div class="col-md-12">
            <div class="row masonry">

                <% foreach (var menu in lstMenuRapidoVO)
                   {
                %>

                    <!-- Menu Rapido (Bloco) -->
                    <div class="masonry-panel">
                        <div class="masonry-panel__content">

                            <div class="panel panel-default panel-acesso-rapido" style="border:2px solid <%= menu.CorBorda %>; background-color:<%= menu.CorFundo %>">
                               <div class="panel-body">
                                   <div class="row">
                                       <div class="col-md-12">
                                            <h5><strong><%= menu.Descricao %></strong></h5>
                                       </div>
                                   </div>
                                   <div class="row display-flex">

                                   <% 
                                       var lstFilhos = lstMenuRapidoItemVO.Where(x => x.MenuRapido.Id == menu.Id).OrderBy(x => x.Ordem).ThenBy(x => x.Descricao).ToList();

                                       foreach (var item in lstFilhos)
                                       {
                                           var ambiente = Sistema.Api.dll.Src.Dominio.AppState;

                                           string linkModulo = "//url-modulo-nao-definido";

                                           if (ambiente == Sistema.Api.dll.Src.Dominio.ApplicationState.Producao && item.Funcionalidade.SubModulo.Modulo.Link != null)
                                           {
                                               linkModulo = item.Funcionalidade.SubModulo.Modulo.Link;
                                           }
                                           else if (ambiente == Sistema.Api.dll.Src.Dominio.ApplicationState.Debug && item.Funcionalidade.SubModulo.Modulo.LinkDebug != null)
                                           {
                                               linkModulo = item.Funcionalidade.SubModulo.Modulo.LinkDebug;
                                           }
                                           else if (ambiente == Sistema.Api.dll.Src.Dominio.ApplicationState.Teste && item.Funcionalidade.SubModulo.Modulo.LinkTeste != null)
                                           {
                                               linkModulo = item.Funcionalidade.SubModulo.Modulo.LinkTeste;
                                           }
                                           else if (ambiente == Sistema.Api.dll.Src.Dominio.ApplicationState.Homologacao && item.Funcionalidade.SubModulo.Modulo.LinkHomologacao != null)
                                           {
                                               linkModulo = item.Funcionalidade.SubModulo.Modulo.LinkHomologacao;
                                           }

                                           string menuItemLinkAcao = item.Link;
                                           string linkSubModulo = linkModulo.Trim() + "/" + item.Funcionalidade.SubModulo.Link.Trim();
                                           string linkSubModuloOk = linkSubModulo;

                                           if (menuItemLinkAcao != "")
                                               linkSubModuloOk = menuItemLinkAcao.StartsWith("?") ? linkSubModulo + menuItemLinkAcao : linkModulo.Trim() + menuItemLinkAcao;

                                           %>

                                           <!-- Menu Item  -->
                                           <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4 text-center">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <a href="javascript:void(0);" class="real-dialog-open-dialog"
                                                            id="submod-<%= item.Funcionalidade.SubModulo.Id %>" 
                                                            data-href="<%= linkSubModuloOk %>" 
                                                            data-id-modulo="<%= item.Funcionalidade.SubModulo.Modulo.Id %>" 
                                                            data-id-submodulo="<%= item.Funcionalidade.SubModulo.Id %>"
                                                            data-id-funcionalidade="<%= item.Funcionalidade.Id %>"
                                                            data-title-content="<i class='fa fa-<%= item.Funcionalidade.SubModulo.Modulo.Icone %>'></i> <%= item.Funcionalidade.SubModulo.Nome %> - <%= item.Funcionalidade.SubModulo.Modulo.Nome %>"
                                                            data-title-color="#fff" 
                                                            data-title-backgroundcolor="<%= item.Funcionalidade.SubModulo.Modulo.Cor %>" 
                                                            data-position-top="140" 
                                                            data-position-left="center" 
                                                            data-dimension-height="75%">
                                                            <div class="panel panel-default panel-acesso-rapido-item">
                                                                <div class="panel-heading" style="background-color:<%= item.CorFundo %>">
                                                                    <i style="color:<%= item.CorIcone %>" class="<%= item.Icone %> fa-2x"></i>
                                                                </div>
                                                                <div class="panel-body" data-original-title="<%= item.Descricao %>" data-container="body" data-toggle="tooltip" data-placement="bottom" style="background-color:<%= menu.CorFundo %>">
                                                                   <span><%= item.Descricao %></span>
                                                                </div>
                                                            </div>
                                                        </a>
                                                    </div>
                                                </div>
                                           </div>

                                       <% } %>

                                   </div>
                               </div>
                           </div>

                        </div>
                    </div>
                    <!-- Fim Menu Rapido (Bloco) -->

                <% } %>

            </div>
        </div>

    <% } %>--%>

  <%--  </div>--%>

</asp:Content>