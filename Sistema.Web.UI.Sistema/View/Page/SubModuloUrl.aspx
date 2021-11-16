<%@ Page Title="" Language="C#" MasterPageFile="~/View/Master/Submodulo.master" AutoEventWireup="true" CodeBehind="SubModuloUrl.aspx.cs" Inherits="Sistema.Web.UI.Sistema.View.Page.SubModuloUrl" %>

<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!--INÍCIO CSS-->
    <%= Funcoes.InvocarTagArquivo("View/Css/submodulourl.css" , true) %>
    <!--FIM CSS-->

    <!--INÍCIO JAVASCRIPT-->
    <%= Funcoes.InvocarTagArquivo("View/Js/submodulourl.js" , true) %>
    <!--FIM JAVASCRIPT-->

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <ol class="breadcrumb">
        <li class="active current"><a href="../Page/SubModuloUrl.aspx" title="Voltar SubModuloUrl" target="_self">SubModuloUrl</a></li>
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


    <input type="hidden" id="IdSubmoduloUrl" name="SubmoduloUrl" />

    <!-- INICIO MODAL INSERIR/ALTERAR -->
    <div class="modal fade" id="modal-geral" style="display: none">
        <div class="modal-dialog" style="width: 750px; height: auto">
            <div class="modal-content">
                <div class="modal-header cinza-padrao" id="head-inserir">
                    <h3 class="modal-title">Inserir SubmoduloUrl</h3>
                    <p>Preencha as informações referente ao SubmoduloUrl</p>
                </div>
                <div class="modal-header cinza-padrao" id="head-alterar" style="display: none">
                    <h3 class="modal-title">Alterar SubmoduloUrl</h3>
                    <p>Preencha as informações referente ao SubmoduloUrl</p>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>SubModulo</label>
                                <select name="submodulo" class="form-control required select2">
                                    <option value="">Selecione</option>
                                    <% foreach (var submodulo in LstSubModuloVO)
                                       { %>
                                    <option value="<%= submodulo.Id %>"><%= submodulo.Modulo.Nome  + " - " + submodulo.Nome %></option>
                                    <% } %>
                                </select>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Url</label>
                                <input name="url" type="text" class="form-control required" placeholder="Informe a url" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer cinza-padrao">
                    <button type="button" class="btn btn-primary" id="btn-confirmar"><span class="fa fa-check-circle-o">&nbsp</span>Confirmar</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal"><span class="fa fa-caret-square-o-down">&nbsp</span>Fechar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- FIM MODAL INSERIR/ALTERAR -->

    <!-- INICIO MODAL CONSULTAR -->
    <div class="modal fade" id="modal-consultar" style="display: none">
        <div class="modal-dialog" style="">
            <div class="modal-content">
                <div class="modal-header cinza-padrao" id="head-consultar">
                    <h3 class="modal-title">Consultar SubmoduloUrl</h3>
                    <p>Preencha as informações referente ao SubmoduloUrl</p>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-8">
                            <div class="form-group">
                                <label>Url</label>
                                <input name="Url" type="text" class="form-control" placeholder="Informe a Url" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Parâmetro do filtro</label>
                                <select class="form-control" name="FiltroUrl">
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
