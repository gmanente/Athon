<%@ Page Title="" Language="C#" MasterPageFile="~/View/masterPage/Submodulo.Master" AutoEventWireup="true" CodeBehind="Contato.aspx.cs" Inherits="Sistema.Web.UI.Datanorte.View.Page.Contato" %>
<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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

        <div growl></div>

        <!-- FILTROS -->
    <div class="col-md-12">
        <section>
            <div class="jarviswidget jarviswidget-no-header jarviswidget-sortable">
                <div role="content" class="border-content bg-color-gray pt20">
                    <div class="widget-body mh80">
                        <form name="consultaForm">
                            <div class="row">                       
                                <div class="smart-form">
                                    <%-- ESTADO --%>
                                    <section class="col col-1">
                                        <label>Estado</label>
                                        <label class="input">
                                            <ui-select ng-model="ContatoConsulta.Estado" ng-change="GetCidade()" ng-required="true">
                                                <ui-select-match  placeholder="Selecione o Estado">
                                                    <span ng-bind="$select.selected.Sigla"></span>
                                                </ui-select-match>
                                                <ui-select-choices 
                                                    refresh="GetEstado()"
                                                    refresh-delay="150"
                                                    repeat="item in (rowlistEstado | filter: $select.search)">
                                                    <span ng-bind="item.Sigla"></span>
                                                </ui-select-choices>
                                            </ui-select>
                                        </label>
                                    </section>

                                    <%-- CIDADE --%>
                                    <section class="col col-2">
                                        <label>Cidade</label>
                                        <label class="input">
                                            <ui-select ng-model="ContatoConsulta.Cidade" ng-change="GetBairro()" ng-required="true">
                                                <ui-select-match  placeholder="Selecione o Cidade">
                                                    <span ng-bind="$select.selected.Nome"></span>
                                                </ui-select-match>
                                                <ui-select-choices 
                                                    <%--refresh="GetCidade()"--%>
                                                    refresh-delay="150"
                                                    repeat="item in (rowlistCidade | filter: $select.search)">
                                                    <span ng-bind="item.Nome"></span>
                                                </ui-select-choices>
                                            </ui-select>
                                        </label>
                                    </section>

                                    <%-- BAIRRO --%>
                                    <section class="col col-2">
                                        <label>Bairro</label>
                                        <label class="input">
                                            <ui-select ng-model="ContatoConsulta.Bairro"  ng-required="true">
                                                <ui-select-match  placeholder="Selecione o Bairro">
                                                    <span ng-bind="$select.selected.Bairro"></span>
                                                </ui-select-match>
                                                <ui-select-choices 
                                                    <%--refresh="GetBairro()"--%>
                                                    refresh-delay="150"
                                                    repeat="item in (rowlistBairro | filter: $select.search)">
                                                    <span ng-bind="item.Bairro"></span>
                                                </ui-select-choices>
                                            </ui-select>
                                        </label>
                                    </section>



                                   <%-- <section class="col col-2">
                                        <label>Bairro</label>
                                        <label class="input">
                                            <i class="icon-prepend fa fa-newspaper-o"></i>
                                            <input placeholder="Bairro" ng-model="ContatoConsulta.Bairro" type="text" class="ng-pristine ng-valid ng-empty ng-touched">   
                                        </label>
                                    </section>--%>
                                    
                                    <%-- ENDEREÇO --%>
                                    <section class="col col-4">
                                        <label>Endereço</label>
                                        <label class="input">
                                            <i class="icon-prepend fa fa-newspaper-o"></i>
                                            <input placeholder="Endereço" ng-model="ContatoConsulta.Logradouro" type="text" class="ng-pristine ng-valid ng-empty ng-touched">   
                                        </label>
                                    </section>

                                    <%-- NUMERO --%>
                                    <section class="col col-2">
                                        <label>Nº </label>
                                        <label class="input">
                                            <i class="icon-prepend fa fa-newspaper-o"></i>
                                            <input placeholder="Nº" ng-model="ContatoConsulta.LogradouroNumero" type="text" class="ng-pristine ng-valid ng-empty ng-touched">                        
                                        </label>
                                    </section>
                                    
                                    <%-- CEP --%>
                                    <section class="col col-2">
                                        <label>CEP </label>
                                        <label class="input">
                                            <i class="icon-prepend fa fa-newspaper-o"></i>
                                            <input placeholder="CEP (Somente Nº)" ng-model="ContatoConsulta.Cep" type="text" class="ng-pristine ng-valid ng-empty ng-touched">                        
                                        </label>
                                    </section>
                                    
                                    <%-- DDD INICIAL --%>
                                    <section class="col col-2">
                                        <label>DDD Inicial</label>
                                        <label class="input">
                                            <i class="icon-prepend fa fa-newspaper-o"></i>
                                            <input placeholder="DDD Inicial" ng-model="ContatoConsulta.DDDInicial" type="text" class="ng-pristine ng-valid ng-empty ng-touched">                        
                                        </label>
                                    </section>
                                    
                                    <%-- DDD FINAL --%>
                                    <section class="col col-2">
                                        <label>DDD Final</label>
                                        <label class="input">
                                            <i class="icon-prepend fa fa-newspaper-o"></i>
                                            <input placeholder="DDD Final" ng-model="ContatoConsulta.DDDFinal" type="text" class="ng-pristine ng-valid ng-empty ng-touched">                        
                                        </label>
                                    </section>
                                    
                                    <%-- TELEFONE --%>
                                    <section class="col col-2">
                                        <label>Telefone </label>
                                        <label class="input">
                                            <i class="icon-prepend fa fa-newspaper-o"></i>
                                            <input placeholder="Nº Telefone" ng-model="ContatoConsulta.Telefone" type="text" class="ng-pristine ng-valid ng-empty ng-touched">                        
                                        </label>
                                    </section>
                                    
                                    <%-- CPF --%>
                                    <section class="col col-2">
                                        <label>CPF </label>
                                        <label class="input">
                                            <i class="icon-prepend fa fa-newspaper-o"></i>
                                            <input placeholder="CPF (Apenas Nº)" ng-model="ContatoConsulta.Cpf" type="text" class="ng-pristine ng-valid ng-empty ng-touched">                        
                                        </label>
                                    </section>
                                    
                                    <%-- Nome --%>
                                    <section class="col col-4">
                                        <label>Nome</label>
                                        <label class="input">
                                            <i class="icon-prepend fa fa-newspaper-o"></i>
                                            <input placeholder="Nome da Pessoa" ng-model="ContatoConsulta.Nome" type="text" class="ng-pristine ng-valid ng-empty ng-touched">                        
                                        </label>
                                    </section>                

                                    <%-- CONSULTAR --%>                                    
                                    <section class="col col-2">
                                        <div style="display: inline-block">
                                            <button type="button" ng-if="IsAuthorized('RF001')" class="btn btn-info btn-smart-form" id="btnConsultar" title="Consultar" ng-disabled="consultaForm.$invalid" ng-click="ConsultarContato()">
                                                <i class="fa fa-search"></i>&nbsp;Consultar
                                            </button>
                                        </div>
                                    </section>
                                    
                                    <%-- IMPRIMIR --%>                                    
                                    <section class="col col-2">
                                        <div style="display: inline-block">
                                            <button type="button" ng-if="IsAuthorized('RF002')" class="btn btn-warning btn-smart-form" id="btnImprimir" title="Imprimir" data-toggle="dropdown" ng-disabled="consultaForm.$invalid">
                                                <i class="fa fa-print"></i>&nbsp;&nbsp;&nbsp;Gerar Arquivo
                                            </button>  
                                             <ul class="dropdown-menu" role="menu">
                                                 <li ng-if="IsAuthorized('RF002')">
                                                    <a style="cursor: pointer;" ng-click="GetImprimir(1)">
                                                        <span class="fa fa-desktop"></span>&nbsp;Visualizar em Tela
                                                    </a>
                                                 </li>
                                                 <li ng-if="IsAuthorized('RF002')">
                                                    <a style="cursor: pointer;" ng-click="GetImprimir(2)">
                                                        <span class="fa fa-file-pdf-o"></span>&nbsp;Formato PDF
                                                    </a>
                                                </li>

                                                 <li ng-if="IsAuthorized('RF002')">
                                                    <a style="cursor: pointer;" ng-click="GetImprimir(3)">
                                                        <span class="fa fa-file-excel-o"></span>&nbsp;Formato Excel
                                                    </a>
                                                </li>
                                                <li ng-if="IsAuthorized('RF002')">
                                                    <a style="cursor: pointer;" ng-click="GetImprimir(4)">
                                                        <span class="fa fa-table"></span>&nbsp;Formato CSV
                                                    </a>
                                                </li>
                                            </ul>

                                        </div>  
                                    </section>
                                </div>  
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </section>
    </div>
    
    <!-- FIM FILTROS -->


        <!-- DATA GRID -->
        <div class="col-md-12" id="grid-container">
            <section id="widget-grid">
                <div class="jarviswidget jarviswidget-sortable jarviswidget-color-blue">
                    <header role="heading">
                        <h2><i class="fa fa-arrow-circle-right" aria-hidden="true"></i>&nbsp;Listagem de Contatos <span class="badge bg-color-red txt-color-white">{{rowCollection.length}}</span></h2>
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
                                <div ng-show="IsAuthorized('RF001')" class="table-responsive no-margin" id="table-container" style="padding-bottom: 100px;">
                                    <table st-table="displayedCollection" st-safe-src="rowCollection" id="grid" class="table table-hover grid- table-stats table-condensed table-bordered table-sortable">
                                        <thead ng-if="rowCollection.length > 0">
                                            <tr>
                                                <th class="headerSortable thgrid" style="text-align: center; width: 5%; vertical-align: middle;">Funções</th>
                                                <th class="headerSortable thgrid" style="text-align: center; width: 20%; vertical-align: middle;" data-original-title="Nome" data-container="body" data-toggle="tooltip" data-placement="bottom" title="" st-sort="Nome">Nome</th>
                                                <th class="headerSortable thgrid" style="text-align: center; width: 8%; vertical-align: middle;" data-original-title="Cpf" data-container="body" data-toggle="tooltip" data-placement="bottom" title="" st-sort="Cpf">CPF</th>
                                                <th class="headerSortable thgrid" style="text-align: center; width: 5%; vertical-align: middle;" data-original-title="Rg" data-container="body" data-toggle="tooltip" data-placement="bottom" title="Ativo" st-sort="Rg">RG</th>
                                                <th class="headerSortable thgrid" style="text-align: center; width: 13%; vertical-align: middle;" data-original-title="Logr_Nome" data-container="body" data-toggle="tooltip" data-placement="bottom" title="Ativo" st-sort="Logr_Nome">Endereço</th>
                                                <th class="headerSortable thgrid" style="text-align: center; width: 5%; vertical-align: middle;" data-original-title="Logr_Numero" data-container="body" data-toggle="tooltip" data-placement="bottom" title="Ativo" st-sort="Logr_Numero">Nº</th>
                                                <th class="headerSortable thgrid" style="text-align: center; width: 10%; vertical-align: middle;" data-original-title="Bairro" data-container="body" data-toggle="tooltip" data-placement="bottom" title="Ativo" st-sort="Bairro">Bairro</th>
                                                <th class="headerSortable thgrid" style="text-align: center; width: 5%; vertical-align: middle;" data-original-title="Cep" data-container="body" data-toggle="tooltip" data-placement="bottom" title="Ativo" st-sort="Cep">CEP</th>
                                                <th class="headerSortable thgrid" style="text-align: center; width: 10%; vertical-align: middle;" data-original-title="Cidade" data-container="body" data-toggle="tooltip" data-placement="bottom" title="Ativo" st-sort="Cidade">Cidade</th>
                                                <th class="headerSortable thgrid" style="text-align: center; width: 5%; vertical-align: middle;" data-original-title="Uf" data-container="body" data-toggle="tooltip" data-placement="bottom" title="Ativo" st-sort="UF">UF</th>
                                                <th class="headerSortable thgrid" style="text-align: center; width: 5%; vertical-align: middle;" data-original-title="TelefoneTipo" data-container="body" data-toggle="tooltip" data-placement="bottom" title="Ativo" st-sort="TelefoneTipo">Tipo</th>
                                                <th class="headerSortable thgrid" style="text-align: center; width: 5%; vertical-align: middle;" data-original-title="TelefoneDDD" data-container="body" data-toggle="tooltip" data-placement="bottom" title="Ativo" st-sort="TelefoneDDD">DDD</th>
                                                <th class="headerSortable thgrid" style="text-align: center; width: 10%; vertical-align: middle;" data-original-title="TelefoneNumero" data-container="body" data-toggle="tooltip" data-placement="bottom" title="Ativo" st-sort="TelefoneNumero">Telefone</th>

                                                <th class="headerSortable thgrid" style="text-align: center; width: 10%; vertical-align: middle;" data-original-title="ContatoHistorico.DataOperacao" data-container="body" data-toggle="tooltip" data-placement="bottom" title="Data Atendimento" st-sort="DataOperacao">Data Atend.</th>
                                                <th class="headerSortable thgrid" style="text-align: center; width: 10%; vertical-align: middle;" data-original-title="ContatoHistorico.Usuario.Nome" data-container="body" data-toggle="tooltip" data-placement="bottom" title="Usuário" st-sort="UsuarioNome">Operador</th>
                                                <th class="headerSortable thgrid" style="text-align: center; width: 15%; vertical-align: middle;" data-original-title="ContatoHistorico.Observação" data-container="body" data-toggle="tooltip" data-placement="bottom" title="Observação" st-sort="Observacao">Observação</th>
                                            </tr>

                                            <tr ng-if="rowCollection.length > 0">
                                                <td>&nbsp;</td>
                                                <td><input st-search="Nome" placeholder="Filtro por Nome" class="input-sm form-control" type="search" /></td>
                                                <td><input st-search="Cpf" placeholder="Filtro por Cpf" class="input-sm form-control" type="search" /></td>
                                                <td><input st-search="Rg" placeholder="Filtro por Rg" class="input-sm form-control" type="search" /></td>
                                                <td><input st-search="Logr_Nome" placeholder="Filtro por Logradouro" class="input-sm form-control" type="search" /></td>
                                                <td><input st-search="Logr_Numero" placeholder="Filtro por Nº Endereço" class="input-sm form-control" type="search" /></td>
                                                <td><input st-search="Bairro" placeholder="Filtro por Bairro" class="input-sm form-control" type="search" /></td>
                                                <td><input st-search="Cep" placeholder="Filtro por Cep" class="input-sm form-control" type="search" /></td>
                                                <td><input st-search="Cidade" placeholder="Filtro por Cidade" class="input-sm form-control" type="search" /></td>
                                                <td><input st-search="Uf" placeholder="Filtro por UF" class="input-sm form-control" type="search" /></td>
                                                <td><input st-search="TelefoneTipo" placeholder="Filtro por Tipo do Telefone" class="input-sm form-control" type="search" /></td>
                                                <td><input st-search="TelefoneDDD" placeholder="Filtro por DDD" class="input-sm form-control" type="search" /></td>
                                                <td><input st-search="TelefoneNumero" placeholder="Filtro por Nº Telefone" class="input-sm form-control" type="search" /></td>
                                                <td><input st-search="ContatoHistorico.DataOperacaoString" placeholder="Filtro por Data" class="input-sm form-control" type="search" /></td>
                                                <td><input st-search="ContatoHistorico.Usuario.Nome" placeholder="Filtro por Usuário" class="input-sm form-control" type="search" /></td>
                                                <td><input st-search="ContatoHistorico.Observacao" placeholder="Filtro por Observação" class="input-sm form-control" type="search" /></td>                                                            
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr ng-if="!rowCollection">
                                                <td colspan="16" style="padding: 20px !important"><i class="fa fa-info-circle"></i>&nbsp;Clique no botão Consultar para filtrar os resultados.</td>
                                            </tr>
                                            <tr class="warning" ng-if="!rowCollection && startPage">
                                                <td colspan="16" style="padding: 20px !important"><i class="fa fa-spinner fa-spin fa-fw"></i>&nbsp;Carregando Informações</td>
                                            </tr>
                                            <tr class="warning" ng-if="rowCollection.length == 0">
                                                <td colspan="16" style="padding: 20px !important"><i class="fa fa-warning"></i>&nbsp;Não foram encontrados resultados para esta consulta</td>
                                            </tr>

                                            <tr ng-repeat="row in displayedCollection" ng-if="rowCollection.length > 0">
                                                <td>
                                                    <div class='btn-group'>
                                                        <button type="button" class="dropdown-toggle btn btn-default btn-xs" data-toggle="dropdown">
                                                            <span class="fa fa-share"></span>Ações <span class="fa fa-caret-down"></span>
                                                        </button>
                                                        <ul class="dropdown-menu" role="menu">                                                            
                                                            <li title="Permite inserir um atendimento."
                                                                ng-if="IsAuthorized('RF003')">
                                                                <a style="cursor: pointer;"
                                                                    ng-click="ModalInserir(row)">
                                                                    <span class="fa fa-edit"></span>&nbsp;Registrar Atendimento
                                                                </a>
                                                            </li>

                                                            <li title="Exibe o histórico de Atendimentos." ng-if="IsAuthorized('RF004')">
                                                                <a style="cursor: pointer;"
                                                                    ng-click="ModalHistorico(row)">
                                                                    <span class="fa fa-list-alt"></span>&nbsp;Histórico de Atendimentos              
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
                                                <td style="text-align: center; vertical-align: middle;">{{ row.ContatoHistorico.Dataoperacao  | date: 'dd/MM/yyyy' }}</td>
                                                <td style="text-align: center; vertical-align: middle;">{{ row.ContatoHistorico.Usuario.Nome }}</td>
                                                <td style="text-align: center; vertical-align: middle;">{{ row.ContatoHistorico.Observacao }}</td>
                                                <%--<td style="text-align: center; vertical-align: middle;"><i class="fa fa-square-o" ng-if="!row.Ativo"></i><i class="fa fa-check-square" ng-if="row.Ativo"></i></td>--%>

                                            </tr>
                                            <tr ng-show="rowCollection.length > 0">
                                                <td colspan="16" class="text-center">
                                                    <div id="pagerId" st-pagination="" st-items-by-page="itemsByPage" st-displayed-pages="7"></div>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <div ng-show="!IsAuthorized('RF001')" class="alert alert-danger text-center">
                                    <b>Atenção, você não possui permissão necessária para acessar as informações deste recurso (RF001)</b>
                                </div>
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


        <!--MODAL HISTORICO -->
        <div class="modal fade" id="modal-historico" role="dialog" aria-hidden="true" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog" style="width: 70%; height: auto">
                <div class="modal-content">
                    <div class="modal-header cinza-padrao">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h3 ng-bind="tituloModal" class="modal-title"></h3>
                        
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <!-- GRID HISTÓRICO -->
                            <div class="col-md-12" id="grid-container4" ng-if="status3 == 1 || status3 == 2 && rowCollection3.length >= 0">
                                <br />
                                <section id="widget-grid4">
                                    <div class="jarviswidget jarviswidget-sortable jarviswidget-color-blue">
                                        <header role="heading">
                                            <h2><i class="fa fa-history" aria-hidden="true"></i>&nbsp;Histórico de Situações</h2>
                                        </header>

                                        <div role="content" class="border-content">
                                            <div class="jarviswidget-editbox"></div>
                                            <div class="widget-body no-padding mh20">
                                                <div class="rows">
                                                    <div class="table-responsive no-margin" id="table-container4">

                                                        <table id="grid4" class="table table-hover table-stats table-condensed table-bordered table-sortable table-striped no-column-border">
                                                            <tbody>
                                                                <tr class="warning" ng-if="status3 == 1">
                                                                    <td colspan="10" style="padding: 20px !important;"><i class="fa fa-spinner fa-spin fa-fw"></i>Carregando Informações</td>
                                                                </tr>
                                                                <tr class="warning" ng-if="status3 == 2 && rowCollection3.length == 0">
                                                                    <td class="text-center" colspan="12" style="padding: 20px!important"><i class="fa fa-warning"></i>Não foram encontrados resultados para esta consulta</td>
                                                                </tr>
                                                                <tr ng-if="rowCollection3.length > 0">
                                                                    <td class="headerSortable thgrid" style="text-align: left; width: 20%; vertical-align: middle;" data-original-title="Data da Situação" data-container="body" data-toggle="tooltip" data-placement="bottom" title="">Data</td>
                                                                    <td class="headerSortable thgrid" style="text-align: left; width: 30%; vertical-align: middle;" data-original-title="Nome do Usuário" data-container="body" data-toggle="tooltip" data-placement="bottom" title="">Usuário</td>
                                                                    <td class="headerSortable thgrid" style="text-align: left; width: 50%; vertical-align: middle;" data-original-title="Observacao" data-container="body" data-toggle="tooltip" data-placement="bottom" title="">Observação</td>
                                                                </tr>
                                                                <tr ng-repeat="row in rowCollection3" ng-if="rowCollection3.length > 0">
                                                                    <td style="text-align: left; vertical-align: middle;">{{ row.Dataoperacao | date: 'dd/MM/yyyy - HH:mm' }} </td>                                                                    
                                                                    <td style="text-align: left; vertical-align: middle;">{{ row.Usuario.Nome  }}</td>
                                                                    <td style="text-align: left; vertical-align: middle;">{{ row.Observacao  }}</td>
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
                            <!-- FIM GRID HISTÓRICO -->
                        </div>
                    </div>
                    <div class="modal-footer cinza-padrao">
                        <div class="row">
                            <div class="col-md-6 text-left">
                                <button type="button" class="btn btn-default" data-dismiss="modal"><span class="fa fa-caret-square-o-down">&nbsp</span>Fechar</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--FIM MODAL HISTORICO -->

        <!--MODAL INSERIR-->
        <div class="modal fade" id="modal-inserir" role="dialog" aria-hidden="true" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog" style="width: 80%; height: auto">
                <div class="modal-content">
                    <div class="modal-header cinza-padrao">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h3 ng-bind="tituloModal" class="modal-title"></h3>
                        <p ng-bind="infoModal"></p>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <form name="inserirForm">
                                    <div class="well line">
                                        <div class="form-group row">
                                            <div class="col-md-2 smart-form" data-title="Data Atendimento" bs-tooltip style="padding-bottom: 15px">
                                                <label> Data do Atendimento</label>
                                                    <label class="input">
                                                        <i class="icon-prepend fa fa-calendar"></i>
                                                        <input type="text" data-mask="99/99/9999" autoclose="true" id="DataOperacao" name="DataOperacao" placeholder="Data da Operação" class="form-control calendario" data-date-format="dd/MM/yyyy" ng-model="ContatoHistorico.DataOperacao" bs-datepicker ng-required="true" />
                                                </label>
                                            </div>
                                            <div class="col-md-12 smart-form">
                                                <label>Observação</label>
                                                <label class="input">
                                                    <textarea class="form-control" rows="5" ng-model="ContatoHistorico.Observacao" name="Descricao" required></textarea>
                                                </label>
                                            </div>                                    
                                        </div>
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
                            <div class="col-md-6 text-right" ng-show="btnInserir">
                                <button type="button" class="btn btn-primary" ng-disabled="inserirForm.$invalid" ng-click="Insert();"><span class="fa fa-check-square-o">&nbsp</span>Confirmar</button>
                            </div>
                            <div class="col-md-6" ng-show="!btnInserir">
                                <button type="button" class="btn btn-primary" ng-disabled="inserirForm.$invalid" ng-click="Update();"><span class="fa fa-check-square-o">&nbsp</span>Confirmar Alteração</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--FIM MODAL INSERIR -->

        <!--Modal-->
        <div class="modal" data-keyboard="false" data-backdrop="static"
            id="modal-loading" tabindex="-1" role="dialog" aria-hidden="false"
            style="top: 20%;">
            <div class="modal-dialog" style="width: 400px;" ng-show="IsAuthorized('RF001')">
                <div class="modal-content">
                    <div class="modal-body" style="">
                        <div style="display: contents;">
                            <div style="display: block; width: auto; color: blue; color: lightslategrey;" class="fa fa-spinner fa-pulse fa-5x fa-fw">
                            </div>
                        </div>
                        <div class="row">
                            <h3 style="text-align: center; vertical-align: middle;">Aguarde! Carregando as Informações na Consulta...</h3>
                            <%--<div class="progress progress-striped active page-progress-bar">
                                <div class="progress-bar" style="width: 100%;"></div>
                            </div>--%>
                        </div>
                    </div>

                     <div class="modal-footer cinza-padrao">
                        <div class="row">
                            <div class="col-md-6 text-center">
                                <button type="button" class="btn btn-danger" ng-click="AbortarConsulta();" data-dismiss="modal" ><span class="fa fa-ban">&nbsp</span>Abortar Consulta</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--Modal-->

        <!--Modal-->
        <div class="modal" data-keyboard="false" data-backdrop="static"
            id="modal-cidade" tabindex="-1" role="dialog" aria-hidden="false"
            style="top: 20%;">
            <div class="modal-dialog" style="width: 400px;" ng-show="IsAuthorized('RF001')">
                <div class="modal-content">
                    <div class="modal-body" style="">
                        <div style="display: contents;">
                            <div style="display: block; width: auto; color: blue; color: lightslategrey;" class="fa fa-spinner fa-pulse fa-5x fa-fw">
                            </div>
                        </div>
                        <div class="row">
                            <h3 style="text-align: center; vertical-align: middle;">Aguarde! Carregando as cidades...</h3>
                            <%--<div class="progress progress-striped active page-progress-bar">
                                <div class="progress-bar" style="width: 100%;"></div>
                            </div>--%>
                        </div>
                    </div>                     
                </div>
            </div>
        </div>
        <!--Modal-->

        <!--Modal-->
        <div class="modal" data-keyboard="false" data-backdrop="static"
            id="modal-bairro" tabindex="-1" role="dialog" aria-hidden="false"
            style="top: 20%;">
            <div class="modal-dialog" style="width: 400px;" ng-show="IsAuthorized('RF001')">
                <div class="modal-content">
                    <div class="modal-body" style="">
                        <div style="display: contents;">
                            <div style="display: block; width: auto; color: blue; color: lightslategrey;" class="fa fa-spinner fa-pulse fa-5x fa-fw">
                            </div>
                        </div>
                        <div class="row">
                            <h3 style="text-align: center; vertical-align: middle;">Aguarde! Carregando os bairros da cidade...</h3>
                            <%--<div class="progress progress-striped active page-progress-bar">
                                <div class="progress-bar" style="width: 100%;"></div>
                            </div>--%>
                        </div>
                    </div>                     
                </div>
            </div>
        </div>
        <!--Modal-->


    </div>

</asp:Content>

