<%@ Page Title="" Language="C#" MasterPageFile="~/View/Master/Submodulo.master" AutoEventWireup="true" CodeBehind="FiltroCampo.aspx.cs" Inherits="Sistema.Web.UI.Sistema.View.Page.FiltroCampo" %>

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

        <li class="active">Filtro Campo
        </li>

        <li class="active">Manutenção
        </li>
    </ol>

        <div class="col-md-12">
            <div class="alert alert-info" style="clear: both; padding: 20px">
                <h4>Informações do Usuário</h4>

                <div class="row">
                    <div class="col-md-2">
                        <p>
                            Código
                        </p>

                        <p>
                            <% = FiltroSession.Id %>
                        </p>

                    </div>

                    <div class="col-md-2">
                        <p>
                            Nome do Filtro
                        </p>

                        <p>
                            <% = FiltroSession.Nome %>
                        </p>

                    </div>

                    <div class="col-md-2">
                        <p>
                            SubModulo
                        </p>

                        <p>
                            <% = FiltroSession.NomeSubModulo %>
                        </p>

                    </div>

                </div>
            </div>
        </div>

        <div class="col-md-12 row">
            <div class="col-md-6">
                <% if (Autenticar("RF005"))
                   { %>
                <a href="#" class="btn btn-primary" data-toggle="modal" data-target="#modal-geral" id="btn-inserir-campo" onclick="ConfigModal('#head-inserir-campo');"><span class="fa fa-plus">&nbsp</span>Inserir</a>
                <% } %>

                <a href="#" class="btn btn-info" onclick="GerarScript();"><span class="fa fa-download">&nbsp</span>Gerar Script</a>
                <a href="#" class="btn btn-default" onclick="AtualizarSequence();"><span class="fa fa-refresh">&nbsp</span>Atualizar Sequence</a>
            </div>
        </div>

        <div class="col-md-12" id="grid-container" style="margin-bottom: 30px">
            <% = GetGrid() %>
        </div>

    <input type="hidden" id="IdFiltroCampo" name="FiltroCampo" />

    <!-- INICIO MODAL INSERIR/ALTERAR -->
    <div class="modal fade" id="modal-geral" style="display: none">
        <div class="modal-dialog" style="width: 750px; height: auto">
            <div class="modal-content">
                <div class="modal-header cinza-padrao" id="head-inserir-campo">
                    <h3 class="modal-title">Inserir Filtro</h3>
                    <p>Preencha as informações referente ao Filtro Campo</p>
                </div>
                <div class="modal-header cinza-padrao" id="head-alterar-campo" style="display: none">
                    <h3 class="modal-title">Alterar Filtro</h3>
                    <p>Preencha as informações referente ao Filtro Campo</p>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Nome</label>
                                <input name="nomeCampo" type="text" class="form-control required" placeholder="Informe o Nome" tabindex="1" />
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Descricao</label>
                                <input name="descricaoCampo" type="text" class="form-control required" placeholder="Informe a Descricao" tabindex="2" />
                            </div>
                        </div>
                        <div class="col-md-10">
                            <div class="form-group">
                                <label>Tipo Campo</label>
                                <input name="tipoCampo" type="text" class="form-control required" placeholder="Informe o Tipo" tabindex="3" />
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Tamanho</label>
                                <input name="tamanhoCampo" type="number" class="form-control digits required" placeholder="Tamanho" tabindex="4"  />
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Ordem</label>
                                <input name="ordem" type="number" class="form-control digits" placeholder="Ordem" tabindex="5" />
                            </div>
                        </div>

                        <div class="col-md-2" style="margin-top: 21px">
                            <div class="btn-checkbox-radio ck btn-group" data-toggle="buttons" style="margin: auto;">
                                <a class="btn icon-btn btn-default" href="#" tabindex="6" ><span class="fa btn-fa icon-del img-circle text-muted"></span>Ativar
                                     <input type="checkbox" name="ativar" />
                                </a>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="modal-footer cinza-padrao">
                    <button type="button" class="btn btn-primary" id="btn-confirmar-campo" tabindex="7" ><span class="fa fa-check-circle-o">&nbsp</span>Confirmar</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal" tabindex="8" ><span class="fa fa-caret-square-o-down">&nbsp</span>Fechar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- FIM MODAL INSERIR/ALTERAR -->

</asp:Content>
