<%@ Page Title="" Language="C#" MasterPageFile="~/View/Master/SubModulo.Master" AutoEventWireup="true" CodeBehind="MenuRapido.aspx.cs" Inherits="Sistema.Web.UI.Sistema.View.Page.MenuRapido" %>

<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!--INÍCIO CSS-->
    <%= Funcoes.InvocarTagArquivo("View/Css/menurapido.css" , true) %>
    <%= Funcoes.InvocarTagArquivo("View/Css/fontawesome-iconpicker.css" , true) %>
    <!--FIM CSS-->

    <!--INÍCIO JAVASCRIPT-->
    <%= Funcoes.InvocarTagArquivo("View/Js/fontawesome-iconpicker.js" , true) %>
    <%= Funcoes.InvocarTagArquivo("View/Js/menurapido.js" , true) %>
    <%= Funcoes.InvocarTagArquivo("View/Js/jquery.floatThead.min.js" , true) %>
    <!--FIM JAVASCRIPT-->

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>
        <ol class="breadcrumb">
            <li>
                <a href="../Page/MenuRapudo.aspx" title="Menu Rápido" target="_self">Menu Rápido</a>
            </li>
            <li class="active">Manutenção</li>

        </ol>
        <div style="width: 100% !important; margin: auto !important;" id="console" class="col-md-12"></div>
    </div>
    
    <!-- INÍCIO FILTROS -->
    <div class="col-md-12 well" style="margin-left: 15px; width: 98.5% !important;">
        <div class="row">
            <div class="col-md-4">
                <label for="Campus"><b>Campus</b></label>
                <select class="form-control select2" id="Campus" name="Campus">
                </select>
            </div>
            <div class="col-md-4">
                <label for="Descricao"><b>Descrição</b></label>
                <input type="text" id="Descricao" class="form-control" maxlength="20" placeholder="Digite uma descrição" />
            </div>
            <div class="col-md-4" style="padding-top: 24px;">
                <% if (Autenticar("RF001"))
                   { %>
                <button type="button" class="btn btn-default" id="btnConsultar"  title="Consultar" disabled="disabled">
                    <i class="fa fa-search"></i>&nbsp;Consultar
                </button>
                <% } %>
            </div>
        </div>
    </div>
    <!-- FIM FILTROS -->

    <div class="col-md-12" style="margin-bottom: 20px;">
        <% if (Autenticar("RF002")) { %>
        <button type="button" class="btn btn-primary" id="btnModalAdicionarMenu" title="Novo Menu Rápido"><i class="fa fa-plus"></i>&nbsp;Novo Menu Rápido</button>
        <% } %>                       
        <input type="hidden" id="idUsuario" value="<%= GetSessao().IdUsuario %>" />
        <input type="hidden" id="idMenuRapido" value="0" />
        <input type="hidden" id="idMenuRapidoItem" value="0" />
    </div>

    <!-- DATA GRID -->
    <div class="col-md-12" id="grid-container">
        <section id="widget-grid">
            <div class="jarviswidget jarviswidget-sortable jarviswidget-color-blue">
                <header role="heading">
                    <h2><i class="fa fa-arrow-circle-right" aria-hidden="true"></i>&nbsp;Lista de Menu Rápido</h2>
                    <%--<span id="loader-interval"><i class="fa fa-spinner fa-spin icon-default"></i></span>--%>
                </header>

                <!-- widget div-->
                <div role="content" class="no-padding">

                    <!-- widget edit box -->
                    <div class="jarviswidget-editbox">
                        <!-- This area used as dropdown edit box -->

                    </div>
                    <!-- end widget edit box -->

                    <!-- widget content -->
                    <div class="widget-body">
                        <div class="rows">
                            <div class="table-responsive no-margin" id="table-container">

                                <table id="grid" data-resizable-columns-id="grid-a" class="table table-hover table-striped table-bordered table-sortable">
                                    <thead>
                                        <tr>
                                            <th data-resizable-column-id="grid-a-1" class="headerSortable" style="text-align: center; width: 5%; vertical-align: middle;">Funções</th>    
                                            <th data-resizable-column-id="grid-a-2" class="headerSortable" style="text-align: right; width: 5%; vertical-align: middle;" data-original-title="Código" data-container="body" data-toggle="tooltip" data-placement="top" title="">Cód.</th>
                                            <th data-resizable-column-id="grid-a-3" class="headerSortable" style="text-align: left; width: 10%; vertical-align: middle;" data-original-title="Nome do Campus" data-container="body" data-toggle="tooltip" data-placement="top" title="">Campus</th>
                                            <th data-resizable-column-id="grid-a-4" class="headerSortable" style="text-align: left; width: 10%; vertical-align: middle;" data-original-title="Descrição Menu Rápido" data-container="body" data-toggle="tooltip" data-placement="top" title="">Descrição</th>
                                            <th data-resizable-column-id="grid-a-5" class="headerSortable" style="text-align: center; width: 8%; vertical-align: middle;" data-original-title="Cor Borda" data-container="body" data-toggle="tooltip" data-placement="top" title="">Cor Borda</th>
                                            <th data-resizable-column-id="grid-a-6" class="headerSortable" style="text-align: center; width: 8%; vertical-align: middle;" data-original-title="Cor Fundo" data-container="body" data-toggle="tooltip" data-placement="top" title="">Cor Fundo</th>
                                            <th data-resizable-column-id="grid-a-7" class="headerSortable" style="text-align: center; width: 8%; vertical-align: middle;" data-original-title="Ordem" data-container="body" data-toggle="tooltip" data-placement="top" title="">Ordem</th>
                                            <th data-resizable-column-id="grid-a-8" class="headerSortable" style="text-align: center; width: 8%; vertical-align: middle;" data-original-title="Ícone Padrão Item" data-container="body" data-toggle="tooltip" data-placement="top" title="">Ícone Item</th>
                                            <th data-resizable-column-id="grid-a-9" class="headerSortable" style="text-align: center; width: 8%; vertical-align: middle;" data-original-title="Cor Padrão Ícone Item" data-container="body" data-toggle="tooltip" data-placement="top" title="">Cor Ícone Item</th>
                                            <th data-resizable-column-id="grid-a-10" class="headerSortable" style="text-align: center; width: 8%; vertical-align: middle;" data-original-title="Cor Padrão Fundo Item " data-container="body" data-toggle="tooltip" data-placement="top" title="">Cor Fundo Item</th>
                                            <th data-resizable-column-id="grid-a-11" class="headerSortable" style="text-align: center; width: 8%; vertical-align: middle;" data-original-title="Preview Item Padrão" data-container="body" data-toggle="tooltip" data-placement="top" title="">Preview Item</th>
                                            <th data-resizable-column-id="grid-a-12" class="headerSortable" style="text-align: center; width: 8%; vertical-align: middle;" data-original-title="Ativo" data-container="body" data-toggle="tooltip" data-placement="top" title="">Ativo</th>
                                        </tr>
                                    </thead>

                                    <tbody id="grid-data-result" class="table-stats">
                                        <tr id="grid-start">
                                            <td colspan="20" class="center" style="padding: 20px !important; text-align: center;">
                                                <i class="fa fa-info-circle"></i>&nbsp;Por favor selecione os filtros acima para consultar.
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <!-- FIM DATA GRID -->

    <!-- INICIO MODAL ADICIONAR ALTERAR  -->
    <div class="modal fade" id="modal-adicionar-alterar" style="display: none; height: auto">
        <div class="modal-dialog" style="width: 95%; height: auto">
            <div class="modal-content">
                <div class="modal-header cinza-padrao">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h3 class="modal-title" id="TituloModalAdicionarAlterar">Alterar Menu Rápido</h3>
                    <p id="SubTituloModalAdicionarAlterar">Alterar informações do menu rápido</p>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-color-blue">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><i class="fa fa-info-circle"></i>&nbsp;&nbsp;Informações do Menu Rápido</h3>
                                </div>
                                <div class="panel-body" style="margin: 0" id="body-dados-menu-rapido">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Descrição</label>
                                                <input type="text" id="DescricaoMenuRapido" class="form-control required" maxlength="200" />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Campus</label>
                                                <select class="form-control select2 required" id="CampusM" name="CampusM"></select> 
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Cor Borda</label>
                                                <input type="text" id="CorBorda" class="form-control colorpicker required" maxlength="50" />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Cor Fundo</label>
                                                <input type="text" id="CorFundo" class="form-control colorpicker required" maxlength="50" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Ícone Padrão Item</label>
                                                <input type="text" id="IconeItem" class="form-control iconpicker required" maxlength="100" />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Cor Padrão Ícone Item</label>
                                                <input type="text" id="CorIconeItem" class="form-control colorpicker required" maxlength="50" />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Cor Padrão Fundo Ícone Item</label>
                                                <input type="text" id="CorFundoItem" class="form-control colorpicker required" maxlength="50" />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Ordem</label>
                                                <input type="text" id="Ordem" class="form-control required" maxlength="4" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3 smart-form">
                                            <label style="color:#000; padding-bottom: 3px;">Ativo</label>
                                            <label class="checkbox">
                                                <input name="checkbox-inline" type="checkbox" id="Ativo" style=""/>
                                                <i></i>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer cinza-padrao">
                    <div class="row">
                        <div class="col-md-6 text-left">
                            <button type="button" class="btn btn-default" data-dismiss="modal"><i class="fa fa-caret-square-o-down"></i>&nbsp;Fechar</button>
                        </div>
                        <div class="col-md-6">
                           <button type="button" class="btn btn-success" id="btnConfirmar" title="Confirmar"><i class="fa fa-check"></i>&nbsp;Confirmar</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>    
    <!-- FIM MODAL ADICIONAR ALTERAR  -->

    <!-- INICIO MODAL ADICIONAR ALTERAR ITEM -->
    <div class="modal fade" id="modal-adicionar-alterar-item" style="display: none; height: auto">
        <div class="modal-dialog" style="width: 95%; height: auto">
            <div class="modal-content">
                <div class="modal-header cinza-padrao">
                    <button type="button" class="close btnFecharModalAlterarItem" data-dismiss="modal" aria-hidden="true">×</button>
                    <h3 class="modal-title" id="TituloModalAlterarAdicionarItem">Alterar Item do Menu Rápido</h3>
                    <p id="SubTituloModalAlterarAdicionarItem">Alterar informações do item do menu rápido</p>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-color-blue">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><i class="fa fa-info-circle"></i>&nbsp;&nbsp;Informações do Item</h3>
                                </div>
                                <div class="panel-body" style="margin: 0" id="body-dados-menu-rapido-item">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Descrição</label>
                                                <input type="text" class="form-control required" id="ItemDescricao" maxlength="200" />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Módulo</label>
                                                <select class="form-control select2 required" id="ItemModulo" name="ItemModulo"></select> 
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>SubMódulo</label>
                                                <select class="form-control select2 required" id="ItemSubModulo" name="ItemSubModulo"></select> 
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Funcionalidade</label>
                                                <select class="form-control select2 required" id="ItemFuncionalidade" name="ItemFuncionalidade"></select> 
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Ícone</label>
                                                <input type="text" class="form-control iconpicker required" id="ItemIcone" maxlength="100" />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Cor Ícone</label>
                                                <input type="text" class="form-control colorpicker required" id="ItemCorIcone" maxlength="50" />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Cor Fundo</label>
                                                <input type="text" class="form-control colorpicker required" id="ItemCorFundo" maxlength="50" />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Ordem</label>
                                                <input type="text" class="form-control required"  id="ItemOrdem" maxlength="4" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Link ou Ação</label>
                                                <input type="text" class="form-control" id="ItemLink" maxlength="255" placeholder="Ex: /View/Page/Pagina.aspx ou ?estornar=ok" />
                                            </div>
                                        </div>
                                        <div class="col-md-3 smart-form">
                                            <label style="color:#000; padding-bottom: 3px;">Ativo</label>
                                            <label class="checkbox">
                                                <input name="checkbox-inline" type="checkbox" id="ItemAtivo" style=""/>
                                                <i></i>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer cinza-padrao">
                    <div class="row">
                        <div class="col-md-6 text-left">
                            <button type="button" class="btn btn-default btnFecharModalAlterarItem"><i class="fa fa-caret-square-o-down"></i>&nbsp;Fechar</button>
                        </div>
                        <div class="col-md-6">
                           <button type="button" class="btn btn-success" id="btnConfirmarItem" title="Confirmar"><i class="fa fa-check"></i>&nbsp;Confirmar</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>    
    <!-- FIM MODAL ADICIONAR ALTERAR ITEM -->

    <!-- INICIO MODAL GERENCIAR ITENS -->
    <div class="modal fade" id="modal-gerenciar-itens" style="display: none; height: auto">
        <div class="modal-dialog" style="width: 95%; height: auto">
            <div class="modal-content">
                <div class="modal-header cinza-padrao">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h3 class="modal-title" id="TituloModalGerenciar">Gerenciar Itens Menu Rápido</h3>
                    <p id="SubTituloModalGerenciar">Gerenciar itens do menu rápido</p>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row" style="margin-bottom: 20px;">
                                <div class="col-md-12">
                                    <button type="button" class="btn btn-primary" id="btnModalAdicionarItem" title="Adicionar Menu Item"><i class="fa fa-plus"></i>&nbsp;Adicionar Item</button>
                                </div>
                            </div>
                            <div class="row" id="divMenuItem" style="display:none;">
                                <!-- DATA GRID -->
                                <div class="col-md-12" id="grid-container-item">
                                    <section id="widget-grid-item">
                                        <div class="jarviswidget jarviswidget-sortable jarviswidget-color-blue">
                                            <header role="heading">
                                                <h2><i class="fa fa-arrow-circle-right" aria-hidden="true"></i>&nbsp;Itens do Menu Rápido</h2>
                                                <%--<span id="loader-interval"><i class="fa fa-spinner fa-spin icon-default"></i></span>--%>
                                            </header>

                                            <!-- widget div-->
                                            <div role="content" class="no-padding">

                                                <!-- widget edit box -->
                                                <div class="jarviswidget-editbox">
                                                    <!-- This area used as dropdown edit box -->

                                                </div>
                                                <!-- end widget edit box -->

                                                <!-- widget content -->
                                                <div class="widget-body">
                                                    <div class="rows">
                                                        <div class="table-responsive no-margin" id="table-container-menu-item">

                                                            <table id="gridMenuItem" data-resizable-columns-id="grid-b" class="table table-hover table-striped table-bordered table-sortable"> 
                                                                <thead>
                                                                    <tr>
                                                                        <th data-resizable-column-id="grid-b-1" class="headerSortable" style="text-align: center; width: 5%; vertical-align: middle;">Funções</th>    
                                                                        <th data-resizable-column-id="grid-b-2" class="headerSortable" style="text-align: right; width: 5%; vertical-align: middle;" data-original-title="Código" data-container="body" data-toggle="tooltip" data-placement="top" title="">Cód.</th>
                                                                        <th data-resizable-column-id="grid-b-3" class="headerSortable" style="text-align: left; width: 10%; vertical-align: middle;" data-original-title="Descrição Menu Rápido Item" data-container="body" data-toggle="tooltip" data-placement="top" title="">Descrição</th>
                                                                        <th data-resizable-column-id="grid-b-4" class="headerSortable" style="text-align: center; width: 5%; vertical-align: middle;" data-original-title="Ordem" data-container="body" data-toggle="tooltip" data-placement="top" title="">Ordem</th>
                                                                        <th data-resizable-column-id="grid-b-5" class="headerSortable" style="text-align: center; width: 5%; vertical-align: middle;" data-original-title="Ícone" data-container="body" data-toggle="tooltip" data-placement="top" title="">Ícone</th>
                                                                        <th data-resizable-column-id="grid-b-6" class="headerSortable" style="text-align: center; width: 5%; vertical-align: middle;" data-original-title="Cor Ícone" data-container="body" data-toggle="tooltip" data-placement="top" title="">Cor Ícone</th>
                                                                        <th data-resizable-column-id="grid-b-7" class="headerSortable" style="text-align: center; width: 5%; vertical-align: middle;" data-original-title="Cor Fundo" data-container="body" data-toggle="tooltip" data-placement="top" title="">Cor Fundo</th>
                                                                        <th data-resizable-column-id="grid-b-8" class="headerSortable" style="text-align: center; width: 5%; vertical-align: middle;" data-original-title="Preview" data-container="body" data-toggle="tooltip" data-placement="top" title="">Preview</th>
                                                                        <th data-resizable-column-id="grid-b-9" class="headerSortable" style="text-align: center; width: 10%; vertical-align: middle;" data-original-title="Módulo" data-container="body" data-toggle="tooltip" data-placement="top" title="">Módulo</th>
                                                                        <th data-resizable-column-id="grid-b-10" class="headerSortable" style="text-align: center; width: 10%; vertical-align: middle;" data-original-title="SubMódulo" data-container="body" data-toggle="tooltip" data-placement="top" title="">SubMódulo</th>
                                                                        <th data-resizable-column-id="grid-b-11" class="headerSortable" style="text-align: center; width: 10%; vertical-align: middle;" data-original-title="Funcionalidade" data-container="body" data-toggle="tooltip" data-placement="top" title="">Funcionalidade</th>
                                                                        <th data-resizable-column-id="grid-b-12" class="headerSortable" style="text-align: center; width: 10%; vertical-align: middle;" data-original-title="Link" data-container="body" data-toggle="tooltip" data-placement="top" title="">Link</th>
                                                                        <th data-resizable-column-id="grid-b-13" class="headerSortable" style="text-align: center; width: 5%; vertical-align: middle;" data-original-title="Ativo" data-container="body" data-toggle="tooltip" data-placement="top" title="">Ativo</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody id="gridMenuItem-data-result" class="table-stats">
                                                                    <tr>
                                                                        <td colspan="20" class="center" style="padding: 20px !important; text-align: center;">
                                                                            <i class="fa fa-info-circle"></i>&nbsp;Nenhum item foi encontrado.
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </section>
                                </div>
                                <!-- FIM DATA GRID -->
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer cinza-padrao">
                    <div class="row">
                        <div class="col-md-6 text-left">
                            <button type="button" class="btn btn-default" data-dismiss="modal"><i class="fa fa-caret-square-o-down"></i>&nbsp;Fechar</button>
                        </div>
                        <div class="col-md-6">
                            &nbsp;
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>    
    <!-- FIM MODAL GERENCIAR ITENS  -->


    <input type="hidden" id="authRf001" value="<%=Autenticar("RF001") %>" />
    <input type="hidden" id="authRf002" value="<%=Autenticar("RF002") %>" />
    <input type="hidden" id="authRf003" value="<%=Autenticar("RF003") %>" />
    <input type="hidden" id="authRf004" value="<%=Autenticar("RF004") %>" />
    <input type="hidden" id="authRf005" value="<%=Autenticar("RF005") %>" />
    <input type="hidden" id="authRf006" value="<%=Autenticar("RF006") %>" />
    <input type="hidden" id="authRf007" value="<%=Autenticar("RF007") %>" />
    <input type="hidden" id="authRf008" value="<%=Autenticar("RF008") %>" />
    <input type="hidden" id="authRf009" value="<%=Autenticar("RF009") %>" />

</asp:Content>
