<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage/Submodulo.master" AutoEventWireup="true" CodeBehind="Filtro.aspx.cs" Inherits="Sistema.Web.UI.Seguranca.View.Page.Filtro" %>


<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!--INÍCIO CSS-->
    <%= Funcoes.InvocarTagArquivo("View/Css/filtro.css" , true) %>
    <!--FIM CSS-->

    <!--INÍCIO JAVASCRIPT-->
    <%= Funcoes.InvocarTagArquivo("View/Js/filtro.js" , true) %>
    <!--FIM JAVASCRIPT-->

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <ol class="breadcrumb">
        <li>
            <a href="../Page/Filtro.aspx" title="Voltar Filtro" target="_self">Filtro</a>

        </li>

        <li class="active">Manutenção
        </li>

    </ol>

    <div class="col-md-12 row">
        <div class="col-md-6">
            <% if (Autenticar("RF001"))
               { %>
            <a href="#" class="btn btn-primary" data-toggle="modal" data-target="#modal-geral" id="btn-inserir" onclick="ConfigModal('#head-inserir');"><span class="fa fa-plus">&nbsp</span>Inserir</a>
            <% } %>
            <% if (Autenticar("RF002"))
               { %>
            <a href="#" class="btn btn-info" data-toggle="modal" data-target="#modal-consultar" onclick="ConfigModal('#head-consultar');"><span class="fa fa-search">&nbsp</span>Consultar</a>
            <% } %>            
        </div>
    </div>

    <div class="col-md-12" id="grid-container">
        <% = RecarregarGridConsultaExistente() %>
    </div>


    <input type="hidden" id="IdFiltro" name="Filtro" />

    <!-- INICIO MODAL INSERIR/ALTERAR -->
    <div class="modal fade" id="modal-geral" style="display: none">
        <div class="modal-dialog" style="">
            <div class="modal-content">
                <div class="modal-header cinza-padrao" id="head-inserir">
                    <h3 class="modal-title">Inserir Filtro</h3>
                    <p>Preencha as informações referente ao Filtro</p>
                </div>
                <div class="modal-header cinza-padrao" id="head-alterar" style="display: none">
                    <h3 class="modal-title">Alterar Filtro</h3>
                    <p>Preencha as informações referente ao Filtro</p>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Nome</label>
                                <input name="nome" type="text" class="form-control required" placeholder="Informe o Nome" tabindex="1" />
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>SubModulo</label>
                                <select name="submodulo" class="form-control required select2" tabindex="2">
                                    <option value="">Selecione</option>
                                    <% foreach (var submodulo in LstSubModuloVO)
                                       { %>
                                    <option value="<%= submodulo.Id %>"><%= submodulo.Modulo.Nome  + " - " + submodulo.Nome %></option>
                                    <% } %>
                                </select>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer cinza-padrao">
                    <button type="button" class="btn btn-primary" id="btn-confirmar" tabindex="3" ><span class="fa fa-check-circle-o">&nbsp</span>Confirmar</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal" tabindex="4" ><span class="fa fa-caret-square-o-down">&nbsp</span>Fechar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- FIM MODAL INSERIR/ALTERAR -->


    <!-- INICIO MODAL INSERIR/ALTERAR INSTRUCAO SQL -->
    <div class="modal fade" id="modal-sql" style="display: none">
        <div class="modal-dialog" style="width: 80%; height: auto">
            <div class="modal-content">
                <div class="modal-header cinza-padrao">
                    <h3 class="modal-title">Instrucao SQL</h3>
                    <p>Preencha a informação referente a Instrucao SQL</p>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Instrucao SQL</label>
                                <textarea autoresize rows="3" name="InstrucaoSQL" class="form-control required" placeholder=" FROM Tabela" tabindex="1" ></textarea>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer cinza-padrao">
                    <button type="button" class="btn btn-primary" id="btn-confirmar-sql"><span class="fa fa-check-circle-o">&nbsp</span>Confirmar</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal"><span class="fa fa-caret-square-o-down">&nbsp</span>Fechar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- FIM MODAL INSERIR/ALTERAR INSTRUCAO SQL -->

     <!-- INICIO MODAL QUERY SQL -->
    <div class="modal fade" id="modal-query" style="display: none">
        <div class="modal-dialog" style="width: 80%; height: auto">
            <div class="modal-content">
                <div class="modal-header cinza-padrao">
                    <h3 class="modal-title">Query SQL</h3>
                    <p>Vizualize abaixo a Query SQL completa</p>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Query SQL</label>
                                <textarea autoresize rows="3" name="QuerySQL" class="form-control required"></textarea>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer cinza-padrao">
                    <button type="button" class="btn btn-default" data-dismiss="modal"><span class="fa fa-caret-square-o-down">&nbsp</span>Fechar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- FIM MODAL COMPLETE SQL -->
    
    <!-- INICIO MODAL CONSULTAR -->
    <div class="modal fade" id="modal-consultar" style="display: none">
        <div class="modal-dialog" style="">
            <div class="modal-content">
                <div class="modal-header cinza-padrao" id="head-consultar">
                    <h3 class="modal-title">Consultar Filtro</h3>
                    <p>Preencha as informações referente ao Filtro</p>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-8">
                            <div class="form-group">
                                <label>Nome</label>
                                <input name="Nome" type="text" class="form-control" placeholder="Informe o Nome" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Parâmetro do filtro</label>
                                <select class="form-control" name="FiltroNome">
                                    <option value="4" selected="selected">Exato</option>
                                    <option value="1">Contém</option>
                                    <option value="2">Parcial início</option>
                                    <option value="3">Parcial fim</option>
                                </select>
                            </div>
                        </div>
                        <div class="col-md-8">
                            <div class="form-group">
                                <label>SubModulo</label>
                                <select name="SubModulo" class="form-control select2">
                                    <option value="">Selecione</option>
                                    <option value="0">Não Identificado</option>
                                    <% foreach (var submodulo in LstSubModuloVO)
                                       { %>
                                    <option value="<%= submodulo.Id %>"><%= submodulo.Modulo.Nome  + " - " + submodulo.Nome %></option>
                                    <% } %>
                                </select>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer cinza-padrao">
                    <button type="button" class="btn btn-primary" id="btn-confirmar-consultar"><span class="fa fa-check-circle-o">&nbsp</span>Confirmar</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal"><span class="fa fa-caret-square-o-down">&nbsp</span>Fechar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- FIM MODAL CONSULTAR -->

</asp:Content>

