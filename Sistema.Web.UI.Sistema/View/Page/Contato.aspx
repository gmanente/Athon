<%@ Page Title="" Language="C#" MasterPageFile="~/View/Master/SubModuloWebAPI.Master" AutoEventWireup="true" CodeBehind="Contato.aspx.cs" Inherits="Sistema.Web.UI.Sistema.View.Page.Contato" %>

<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">

    <!--INÍCIO CSS-->
    <%= Funcoes.InvocarTagArquivo("View/css/fab-forms.css") %>
    <%= Funcoes.InvocarTagArquivo("View/css/ui-select.css") %>
    <%= Funcoes.InvocarTagArquivo("View/css/libs.min.css", true) %>
    <%= Funcoes.InvocarTagArquivo("View/css/contato.css", true) %> 
    <!--FIM CSS-->

    <%= ImportComponents() %>

   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div ng-controller="ContatoController">
         <!--INÍCIO NAVEGAÇÃO-->
        <div>
            <ol class="breadcrumb">
                <li>
                    <a href="./Contato.aspx" title="Contato" target="_self">Contato</a>
                </li>
                <li class="active current">Manutenção
                </li>
                <li class="active current pull-right" title="Usuário">{{ Usuario.Nome }}
                </li>
            </ol>
        </div>
        <!--FIM NAVEGAÇÃO-->

        <!-- INÍCIO FILTROS -->
        <div class="well" <%--ng-show="IsAuthorized('RF001')"--%>>
            <form name="formConsultaContato">
            <div class="row">
                    <%--<div class="col-md-5">
                        <label>Nome do Situação</label>
                        <input type="text"  ng-model="ContatoConsulta.Nome" ng-init="ContatoConsulta.Nome=''" class="form-control" />
                    </div>--%>
                    
                    <div class="col-md-4" style="padding-top: 22px;">
                        <a class="btn btn-info" <%--ng-if="IsAuthorized('RF001')"--%>  ng-click="ModalConsultar()" ng-disabled="formConsultaContato.$invalid">
                             <span class="fa fa-filter"></span>&nbsp;Consultar
                        </a>                        
                    </div>                
                </div>
            </form>
        </div>
        <!-- FIM FILTROS -->

        <%--<div class="col-md-12 form-group" id="btn-container">
            <a class="btn btn-primary" ng-click="ShowModalInsert()" ng-show="IsAuthorized('RF001')">
                <span class="fa fa-plus"></span>&nbsp;Novo
            </a>
        </div>--%>

        <!-- DATA GRID -->
        <div class="col-md-12" id="grid-container">
            <section id="widget-grid">
                <div class="jarviswidget jarviswidget-sortable jarviswidget-color-blue">
                    <header role="heading">
                        <h2><i class="fa fa-arrow-circle-right" aria-hidden="true"></i>&nbsp;Contatos</h2>
                        <div class="dataTables_length pull-right" id="dt_basic_length" style="height: 30px;">
                            <label>
                                <select ng-model="itemsByPage" class="form-control"
                                    ng-options="value.Id as value.Descricao for (key, value) in listItemsByPage">
                                </select>
                            </label>
                        </div>
                    </header>

                    <div role="content" class="no-padding border-content" style="margin-bottom: 40px;">
                        <div class="jarviswidget-editbox">
                        </div>
                        <div class="widget-grid">
                            <div class="rows">
                                <div <%--ng-show="IsAuthorized('RF001')"--%> class="table-responsive no-margin" id="table-container" style="padding-bottom: 100px;">

                                    <table st-table="displayedCollection" st-safe-src="rowCollection" id="grid" class="table table-hover grid- table-stats table-condensed table-bordered table-sortable">
                                        <thead ng-if="rowCollection.length > 0">
                                            <tr>
                                                <th class="headerSortable thgrid" style="text-align: center; width: 5%; vertical-align: middle;">Funções</th>
                                                <th class="headerSortable thgrid" style="text-align: center; width: 25%; vertical-align: middle;" data-original-title="Nome" data-container="body" data-toggle="tooltip" data-placement="bottom" title="" st-sort="Nome">Nome</th>
                                                <th class="headerSortable thgrid" style="text-align: center; width: 10%; vertical-align: middle;" data-original-title="Cpf" data-container="body" data-toggle="tooltip" data-placement="bottom" title="" st-sort="Cpf">CPF</th>
                                                <th class="headerSortable thgrid" style="text-align: center; width: 8%; vertical-align: middle;" data-original-title="Rg" data-container="body" data-toggle="tooltip" data-placement="bottom" title="Ativo" st-sort="Rg">RG</th>
                                                <th class="headerSortable thgrid" style="text-align: center; width: 15%; vertical-align: middle;" data-original-title="Logr_Nome" data-container="body" data-toggle="tooltip" data-placement="bottom" title="Ativo" st-sort="Logr_Nome">Endereço</th>
                                                <th class="headerSortable thgrid" style="text-align: center; width: 5%; vertical-align: middle;" data-original-title="Logr_Numero" data-container="body" data-toggle="tooltip" data-placement="bottom" title="Ativo" st-sort="Logr_Numero">Nº</th>
                                                <th class="headerSortable thgrid" style="text-align: center; width: 10%; vertical-align: middle;" data-original-title="Bairro" data-container="body" data-toggle="tooltip" data-placement="bottom" title="Ativo" st-sort="Bairro">Bairro</th>
                                                <th class="headerSortable thgrid" style="text-align: center; width: 8%; vertical-align: middle;" data-original-title="Cep" data-container="body" data-toggle="tooltip" data-placement="bottom" title="Ativo" st-sort="Cep">CEP</th>
                                                <th class="headerSortable thgrid" style="text-align: center; width: 10%; vertical-align: middle;" data-original-title="Cidade" data-container="body" data-toggle="tooltip" data-placement="bottom" title="Ativo" st-sort="Cidade">Cidade</th>
                                                <th class="headerSortable thgrid" style="text-align: center; width: 5%; vertical-align: middle;" data-original-title="Uf" data-container="body" data-toggle="tooltip" data-placement="bottom" title="Ativo" st-sort="UF">UF</th>
                                                <th class="headerSortable thgrid" style="text-align: center; width: 5%; vertical-align: middle;" data-original-title="TelefoneTipo" data-container="body" data-toggle="tooltip" data-placement="bottom" title="Ativo" st-sort="TelefoneTipo">Tipo</th>
                                                <th class="headerSortable thgrid" style="text-align: center; width: 5%; vertical-align: middle;" data-original-title="TelefoneDDD" data-container="body" data-toggle="tooltip" data-placement="bottom" title="Ativo" st-sort="TelefoneDDD">DDD</th>
                                                <th class="headerSortable thgrid" style="text-align: center; width: 10%; vertical-align: middle;" data-original-title="TelefoneNumero" data-container="body" data-toggle="tooltip" data-placement="bottom" title="Ativo" st-sort="TelefoneNumero">Telefone</th>

                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr ng-if="!rowCollection">
                                                <td colspan="12" style="padding: 20px !important"><i class="fa fa-info-circle"></i>&nbsp;Clique no botão Consultar para filtrar os resultados.</td>
                                            </tr>
                                            <tr class="warning" ng-if="!rowCollection && startPage">
                                                <td colspan="12" style="padding: 20px !important"><i class="fa fa-spinner fa-spin fa-fw"></i>&nbsp;Carregando Informações</td>
                                            </tr>
                                            <tr class="warning" ng-if="rowCollection.length == 0">
                                                <td colspan="12" style="padding: 20px !important"><i class="fa fa-warning"></i>&nbsp;Não foram encontrados resultados para esta consulta</td>
                                            </tr>

                                            <tr ng-repeat="row in displayedCollection" ng-if="rowCollection.length > 0">
                                                <td>
                                                    <div class='btn-group'>
                                                        <button type="button" class="dropdown-toggle btn btn-default btn-xs" data-toggle="dropdown">
                                                            <span class="fa fa-share"></span>Ações <span class="fa fa-caret-down"></span>
                                                        </button>
                                                        <ul class="dropdown-menu" role="menu">                                                            
                                                            <li title="Permite atualizar as informações do Situação."
                                                                ng-if="IsAuthorized('RF003')">
                                                                <a style="cursor: pointer;"
                                                                    ng-click="ModalEdit(row)">
                                                                    <span class="fa fa-edit"></span>&nbsp;Alterar                  
                                                                </a>
                                                            </li>
                                                            <li title="Exclui do sistema o cadastro selecionado." 
                                                                ng-if="IsAuthorized('RF004')">
                                                                <a style="cursor: pointer;"
                                                                    ng-click="ModalDelete(row)">
                                                                    <span class="fa fa-trash"></span>&nbsp;Excluir                  
                                                                </a>
                                                            </li>
                                                            
                                                        </ul>
                                                    </div>
                                                </td>
                                                <td style="text-align: center; vertical-align: middle;">{{ row.Nome }}</td>
                                                <td style="text-align: center; vertical-align: middle;">{{ row.Cpf }}</td>
                                                <td style="text-align: center; vertical-align: middle;">{{ row.Rg }}</td>
                                                <td style="text-align: center; vertical-align: middle;">{{ row.Logr_Nome }}</td>
                                                <td style="text-align: center; vertical-align: middle;">{{ row.Logr_Numero }}</td>
                                                <td style="text-align: center; vertical-align: middle;">{{ row.Bairro }}</td>
                                                <td style="text-align: center; vertical-align: middle;">{{ row.Cep }}</td>
                                                <td style="text-align: center; vertical-align: middle;">{{ row.Cidade }}</td>
                                                <td style="text-align: center; vertical-align: middle;">{{ row.Uf }}</td>
                                                <td style="text-align: center; vertical-align: middle;">{{ row.TelefoneTipo }}</td>
                                                <td style="text-align: center; vertical-align: middle;">{{ row.TelefoneDDD }}</td>
                                                <td style="text-align: center; vertical-align: middle;">{{ row.TelefoneNumero }}</td>
                                                <%--<td style="text-align: center; vertical-align: middle;"><i class="fa fa-square-o" ng-if="!row.Ativo"></i><i class="fa fa-check-square" ng-if="row.Ativo"></i></td>--%>

                                            </tr>
                                            <tr ng-show="rowCollection.length > 0">
                                                <td colspan="13" class="text-center">
                                                    <div id="pagerId" st-pagination="" st-items-by-page="itemsByPage" st-displayed-pages="7"></div>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <%--<div ng-show="!IsAuthorized('RF001')" class="alert alert-danger text-center">
                                    <b>Atenção, você não possui permissão necessária para acessar as informações deste recurso (RF001)</b>
                                </div>--%>
                            </div>
                        </div>
                    </div>
            </section>
        </div>
        <%--FIM DA GRID DE PESQUISA--%>

        <!-- MODAL CONSULTAR -->
        <div class="modal fade" id="modal-consultar" role="dialog" aria-hidden="true" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog" style="width: 70%; height: auto">
                <div class="modal-content">
                    <div class="modal-header cinza-padrao">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h3 class="modal-title">Filtro da Consulta</h3>
                        <p>Preencha as informações abaixo para realizar a consulta</p>
                    </div>
                    <div class="modal-body" ui-placeholder="*">
                        <div class="row">
                            <div class="col-md-12">
                                <form name="consultaForm" class="row well line">
                                    <div class="col-md-6 col-h70 smart-form">
                                        <label>Nome Completo</label>
                                        <label class="input">
                                            <i class="icon-prepend fa fa-pencil-square-o"></i>
                                            <input name="nome" class="form-control" ng-model="ContatoConsulta.Nome" maxlength="100" placeholder="Informe o Nome do Contato" ng-minlength="3" />
                                        </label>
                                    </div>
                                    <div class="col-md-6 col-h70 smart-form">
                                        <label>Nome Social</label>
                                        <label class="input">
                                            <i class="icon-prepend fa fa-pencil-square-o"></i>
                                            <input name="nomeSocial" class="form-control" ng-model="ContatoConsulta.NomeSocial" maxlength="300" placeholder="Informe o Nome Social" ng-minlength="3" />
                                        </label>
                                    </div>
                                    <div class="col-md-6 col-h70 smart-form">
                                        <label>Nº do CPF</label>
                                        <label class="input">
                                            <i class="icon-prepend fa fa-object-group"></i>
                                            <input name="cpf" class="form-control" ng-model="ContatoConsulta.Cpf" maxlength="80" placeholder="Informe o Nº do CPF" ng-minlength="3" />
                                        </label>
                                    </div>
                                    <div class="col-md-6 col-h70 smart-form">
                                        <label>Nº do RG</label>
                                        <label class="input">
                                            <i class="icon-prepend fa fa-object-group"></i>
                                            <input name="rg" class="form-control" ng-model="ContatoConsulta.Rg" maxlength="80" placeholder="Informe o Nº do RG" ng-minlength="3" />
                                        </label>
                                    </div>
                                    <div class="col-md-6 col-h70 smart-form">
                                        <label>Nome da Mãe</label>
                                        <label class="input">
                                            <i class="icon-prepend fa fa-copyright"></i>
                                            <input name="nomeMae" class="form-control" ng-model="ContatoConsulta.NomeMae" maxlength="100" placeholder="Informe o Nome da Mãe" ng-minlength="3" />
                                        </label>
                                    </div>
                                    <div class="col-md-6 col-h70 smart-form">
                                        <label>Nome do Pai</label>
                                        <label class="input">
                                            <i class="icon-prepend fa fa-columns"></i>
                                            <input name="nomePai" class="form-control" ng-model="ContatoConsulta.NomePai" maxlength="80" placeholder="Informe o Nome do Pai" ng-minlength="3" />
                                        </label>
                                    </div>
                                    <div class="col-md-6 col-h70 smart-form">
                                        <label>Cidade</label>
                                        <label class="input">
                                            <i class="icon-prepend fa fa-pencil-square-o"></i>
                                            <input name="cidade" class="form-control" ng-model="ContatoConsulta.Cidade" maxlength="80" placeholder="Informe a Cidade" ng-minlength="3" />
                                        </label>
                                    </div>
                                    <div class="col-md-6 col-h70 smart-form">
                                        <label>UF</label>
                                        <label class="input">
                                            <i class="icon-prepend fa fa-hashtag"></i>
                                            <input name="uf" class="form-control" ng-model="ContatoConsulta.Uf" maxlength="10" placeholder="Informe a UF" ng-minlength="1" />
                                        </label>
                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>

                    <div class="modal-footer cinza-padrao">
                        <div class="row">
                            <div class="col-md-6 text-left">
                                <button type="button" class="btn btn-default" data-dismiss="modal"><span class="fa fa-caret-square-o-down">&nbsp</span>Fechar</button>
                            </div>
                            <div class="col-md-6">
                                <button type="button" class="btn btn-primary" ng-click="GetSearch()"><span class="fa fa-check">&nbsp</span>Confirmar</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- FIM MODAL CONSULTAR -->

    </div>

</asp:Content>

