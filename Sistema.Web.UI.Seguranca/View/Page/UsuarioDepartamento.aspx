<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage/Submodulo.master" AutoEventWireup="true" CodeBehind="UsuarioDepartamento.aspx.cs" Inherits="Sistema.Web.UI.Seguranca.View.Page.UsuarioDepartamento" %>

<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!--INÍCIO CSS-->
    <%= Funcoes.InvocarTagArquivo("View/Css/modulo.css" , true) %>
    <!--FIM CSS-->

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <!--INÍCIO NAVEGAÇÃO-->
    <div>
        <ol class="breadcrumb">
            <li><a href="../Page/Usuario.aspx" title="Voltar para página Usuário" target="_self">Usuário</a></li>
            <li class="active current">Vincular Departamento</li>
        </ol>
    </div>

    <div class="col-md-12">
        <div class="alert alert-info" id="box-informacoes-pessoa">

            <h4>Informações do Usuário</h4>

            <div class="row" style="margin-top: 15px;">
                <div class="col-md-2">
                    <p style="font-weight: bold;">
                        Cod
                    </p>

                    <p id="Id">
                        <% = UsuarioVO.Id %>
                    </p>
                </div>

                <div class="col-md-3">
                    <p style="font-weight: bold;">
                        CPF
                    </p>

                    <p id="Cpf">
                        <% = UsuarioVO.Cpf %>
                    </p>
                </div>

                <div class="col-md-6" id="divMatricula">
                    <p style="font-weight: bold;">
                        Nome
                    </p>

                    <p id="Nome">
                        <% = UsuarioVO.Nome %>
                    </p>
                </div>
            </div>
        </div>
 
    </div>
   

    <%
        GetGridTemplate().Render();        
    %>


    <div class="modal fade" id="modal-alternativo" style="display: none">
        <div class="modal-dialog" style="width: 35%">
            <input type="hidden" id="modal-tipo" value="">
            <div class="modal-content">

                <div class="modal-header" style="background-color: #444; color: #666; background: #EEE">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h3 class="modal-title">Inserir Novo Departamento ao Usuario</h3>
                    <p class="p-info">
                        Preenchar as informações para realizar a inserção do novo departamento.
                    </p>
                </div>

                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-9">
                            <label>Departamento</label>
                            <select id="combo-dpt" class="form-control required">
                                <option value="">Selecione</option>
                                <% foreach (var Dept in lstDepartamentoVO)
                                   {%>
                                <option value="<% = Dept.Id %>"><% = Dept.Nome %></option>
                                <%} %>
                            </select>
                            <script type="text/javascript">
                                $('#combo-dpt').select2();
                            </script>
                        </div>
                        <div class="col-md-3" style="position: relative; padding-top: 25px">
                            <div class="btn-group" data-toggle="buttons">
                                <label id="btn-ativo" class="btn btn-primary" style="height: 28px; padding-top: 3px">
                                    <object style="width: 0px; height: 0px; border: 0px;" id="txt">Ativar</object>
                                    <span id="icone" class="fa fa-question-circle"></span>
                                    <input type="checkbox" id="ck-ativo" autocomplete="off" />
                                </label>
                            </div>
                            <script type="text/javascript">
                                $("#btn-ativo").on("click", function () {
                                    if ($("#ck-ativo").is(":checked")) {
                                        checkPrimary();
                                    }
                                    else {
                                        checkSuccess();
                                    }
                                });

                                function checkSuccess() {
                                    $("#ck-ativo").prop("checked", true);
                                    $("#btn-ativo").removeClass("btn-primary");
                                    $("#btn-ativo").addClass("btn-success");
                                    $("#icone").removeClass("fa-question-circle");
                                    $("#icone").addClass("fa-check-square-o");
                                    $("#txt").text("Ativo");
                                }

                                function checkPrimary() {
                                    $("#ck-ativo").prop("checked", false);
                                    $("#btn-ativo").removeClass("btn-success");
                                    $("#btn-ativo").addClass("btn-primary");
                                    $("#icone").removeClass("fa-check-square-o");
                                    $("#icone").addClass("fa-question-circle");
                                    $("#txt").text("Ativar");
                                }

                                function noChecked() {
                                    $("#btn-ativo").removeClass("active");
                                    checkPrimary();
                                }

                                function Checked() {
                                    $("#btn-ativo").addClass("active");
                                    checkSuccess();
                                }

                            </script>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="background-color: #444; color: #666; background: #EDEDED">

                    <a href="#" class="botao-acao-confirmar  btn btn-primary">
                        <span class="fa fa-check-circle-o"></span>
                        Confirmar
                    </a>

                    <a class="fechar-modal  btn btn-default" data-dismiss="modal">
                        <span class="fa fa-caret-square-o-down"></span>
                        Fechar
                    </a>

                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modal-excluir" style="display: none">
        <div class="modal-dialog" style="width: 30%">
            <input type="hidden" id="idUsuariodpt" value="">
            <div class="modal-content">

                <div class="modal-header" style="background-color: #444; color: #666; background: #EEE">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h3 class="modal-title">Excluir Departamento do Usuario</h3>
                    <p>
                        Selecione o botão confirmar para realizar a exclusão.
                    </p>
                </div>

                <div class="modal-body">
                    <p>Caso confirme a exclusão do departamento do usuario, o mesmo será totalmente removido do sistema.</p>
                </div>
                <div class="modal-footer" style="background-color: #444; color: #666; background: #EDEDED">

                    <a href="#" class="botao-acao  btn btn-danger" id="btn-excluir">
                        <span class="fa fa-check-circle-o"></span>
                        Confirmar
                    </a>

                    <a class="fechar-modal  btn btn-default" data-dismiss="modal">
                        <span class="fa fa-caret-square-o-down"></span>
                        Fechar
                    </a>

                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">

        $("#btn-inserir").on("click", function () {
            $(".modal-title").text("Inserir Novo Departamento ao Usuario");
            $(".p-info").text("Preenchar as informações para realizar a inserção do novo departamento.");
            $("#modal-tipo").val("inserir");
            $("#combo-dpt").select2("val", "");
            noChecked();
            $("#modal-alternativo").modal("show");
        });

    </script>

    <!--INÍCIO JAVASCRIPT-->
    <%= Funcoes.InvocarTagArquivo("View/Js/usuariodepartamento.js" , true) %>
    <!--FIM JAVASCRIPT-->

</asp:Content>
