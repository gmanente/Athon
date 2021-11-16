<%@ Page Title="" Language="C#" MasterPageFile="~/View/Master/Submodulo.master" AutoEventWireup="true" CodeBehind="Parametros.aspx.cs" Inherits="Sistema.Web.UI.Sistema.View.Page.Parametros" %>

<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!--INÍCIO CSS-->
    <%= Funcoes.InvocarTagArquivo("View/Css/parametros.css" , true) %>
    <!--FIM CSS-->

    <!--INÍCIO JAVASCRIPT-->
    <%= Funcoes.InvocarTagArquivo("View/Js/parametros.js" , true) %>
    <!--FIM JAVASCRIPT-->

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ol class="breadcrumb">
        <li>
            <a href="../Page/Parametros.aspx" title="Voltar Parâmetros" target="_self">Parâmetros</a>

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

    <input type="hidden" id="IdParametroCampus" name="parametroCampus" />
    <input type="hidden" id="IdParametro" name="parametro" />

    <!-- INICIO MODAL INSERIR/ALTERAR -->
    <div class="modal fade" id="modal-geral" style="display: none">
        <div class="modal-dialog" style="width: 750px; height: auto">
            <div class="modal-content">
                <div class="modal-header cinza-padrao" id="head-inserir">
                    <h3 class="modal-title">Inserir Parâmetros</h3>
                    <p>Preencha as informações referente ao Parâmetros</p>
                </div>
                <div class="modal-header cinza-padrao" id="head-alterar" style="display: none">
                    <h3 class="modal-title">Alterar Parâmetros</h3>
                    <p>Preencha as informações referente ao Parâmetros</p>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12" id="tipo-param" style="padding-bottom: 30px">
                            <div class="btn-checkbox-radio rd btn-group" data-toggle="buttons" style="margin: auto;">
                                <a class="btn icon-btn btn-default active" href="#" tabindex="1"><span class="fa btn-fa icon-del img-circle text-muted"></span>Novo
                                     <input type="checkbox" name="NovoParametro" checked="checked" />
                                </a>
                                <a class="btn icon-btn btn-default" href="#" tabindex="2"><span class="fa btn-fa icon-del img-circle text-muted"></span>Existente
                                     <input type="checkbox" name="Existente" />
                                </a>
                            </div>
                        </div>
                        <div class="col-md-12" id="div-parametros" style="display: none">
                            <div class="form-group">
                                <label>Parametro</label>
                                <select id="cbParametro" name="cbParametro" class="form-control select2" tabindex="3">
                                    <option value="">Selecione</option>
                                    <% foreach (var parametro in LstParametroVO)
                                       { %>
                                    <option value="<%= parametro.Id %>" data-idmodulo="<%= parametro.IdModulo %>"><%= parametro.Nome %></option>
                                    <% } %>
                                </select>
                            </div>
                        </div>
                        <div id="div-itens-param">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>Nome</label>
                                    <input name="nome" type="text" class="form-control required" placeholder="Informe o Nome" tabindex="4"/>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>Descricao</label>
                                    <input name="descricao" type="text" class="form-control required" placeholder="Informe a Descricao" tabindex="5" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Valor</label>
                                <textarea rows="3" name="valor" class="form-control required" placeholder="Informe ao Valor" tabindex="6"></textarea>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Modulo</label>
                                <select name="modulo" class="form-control required select2" tabindex="7">
                                    <option value="">Selecione</option>
                                    <% foreach (var modulo in LstModuloVO)
                                       { %>
                                    <option value="<%= modulo.Id %>"><%= modulo.Nome %></option>
                                    <% } %>
                                </select>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Campus</label>
                                <select name="campus" class="form-control required select2" tabindex="8">
                                    <option value="">Selecione</option>
                                    <% foreach (var Campus in LstCampusVO)
                                       { %>
                                    <option value="<%= Campus.Id %>"><%= Campus.Nome %></option>
                                    <% } %>
                                </select>
                            </div>
                        </div>
                        <div class="col-md-2" style="margin-top: 21px">
                            <div class="btn-checkbox-radio ck btn-group" data-toggle="buttons" style="margin: auto;">
                                <a class="btn icon-btn btn-default" href="#" tabindex="9"><span class="fa btn-fa icon-del img-circle text-muted"></span>Ativo
                                     <input type="checkbox" name="ativo" />
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer cinza-padrao">
                    <button type="button" class="btn btn-primary" id="btn-confirmar" tabindex="10"><span class="fa fa-check-circle-o">&nbsp</span>Confirmar</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal" tabindex="11"><span class="fa fa-caret-square-o-down">&nbsp</span>Fechar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- FIM MODAL INSERIR/ALTERAR -->

    <!-- INICIO MODAL CONSULTAR -->
    <div class="modal fade" id="modal-consultar" style="display: none">
        <div class="modal-dialog" style="width: 750px; height: auto">
            <div class="modal-content">
                <div class="modal-header cinza-padrao" id="head-consultar">
                    <h3 class="modal-title">Consultar Parâmetros</h3>
                    <p>Preencha as informações referente ao Parâmetros</p>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-8">
                            <div class="form-group">
                                <label>Nome</label>
                                <input name="Nome" type="text" class="form-control" placeholder="Informe o Nome" tabindex="1"/>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Parâmetro do filtro</label>
                                <select class="form-control" name="FiltroNome" tabindex="2">
                                    <option value="4" selected="selected">Exato</option>
                                    <option value="1">Contém</option>
                                    <option value="2">Parcial início</option>
                                    <option value="3">Parcial fim</option>
                                </select>
                            </div>
                        </div>
                        <div class="col-md-8">
                            <div class="form-group">
                                <label>Descricao</label>
                                <input name="Descricao" type="text" class="form-control" placeholder="Informe a Descricao" tabindex="3" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Parâmetro do filtro</label>
                                <select class="form-control" name="FiltroDescricao" tabindex="4">
                                    <option value="4" selected="selected">Exato</option>
                                    <option value="1">Contém</option>
                                    <option value="2">Parcial início</option>
                                    <option value="3">Parcial fim</option>
                                </select>
                            </div>
                        </div>
                        <div class="col-md-8">
                            <div class="form-group">
                                <label>Modulo</label>
                                <select name="Modulo" class="form-control select2" tabindex="5">
                                    <option value="">Selecione</option>
                                    <% foreach (var modulo in LstModuloVO)
                                       { %>
                                    <option value="<%= modulo.Id %>"><%= modulo.Nome %></option>
                                    <% } %>
                                </select>
                            </div>
                        </div>
                        <div class="col-md-8">
                            <div class="form-group">
                                <label>Campus</label>
                                <select name="Campus" class="form-control select2" tabindex="6">
                                    <option value="">Selecione</option>
                                    <% foreach (var Campus in LstCampusVO)
                                       { %>
                                    <option value="<%= Campus.Id %>"><%= Campus.Nome %></option>
                                    <% } %>
                                </select>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer cinza-padrao">
                    <button type="button" class="btn btn-primary" id="btn-confirmar-consultar" tabindex="7"><span class="fa fa-check-circle-o">&nbsp</span>Confirmar</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal" tabindex="8"><span class="fa fa-caret-square-o-down">&nbsp</span>Fechar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- FIM MODAL CONSULTAR -->

</asp:Content>
