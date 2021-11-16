<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage/Submodulo.Master" AutoEventWireup="true" CodeBehind="ChequeTerceiro.aspx.cs" Inherits="Sistema.Web.UI.Relatorio.View.Page.ChequeTerceiro" %>

<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <!--INÍCIO CSS-->
    <%= Funcoes.InvocarTagArquivo("View/Css/chequeterceiro.css" , true) %>
    <!--FIM CSS-->

    <!--INÍCIO JAVASCRIPT-->
    <%= Funcoes.InvocarTagArquivo("View/Js/chequeterceiro.js" , true) %>
    <!--FIM JAVASCRIPT-->


</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <ol class="breadcrumb">
        <li>
            <a href="../Page/ChequeTerceiro.aspx?idSubModulo=<%= GetIdSubModulo() %>" title="" target="_self">Relatórios - Cheque de Terceiros</a>
        </li>
        <li class="active">Manutenção
        </li>
    </ol>

    <!--INÍCIO ROW-->
    <div class="col-md-4">
        <ul class="nav nav-pills nav-stacked">
            <li><a id="box-cheque" title="Relatório de Cheques de Terceiros">Relatório de Cheques de Terceiros</a></li>
        </ul>

        <%--<button id="box-cheque"> relatorio cheque</button>--%>
    </div>
    <!--FIM ROW-->

    <!-- INÍCIO MODAL -->
    <div class="modal fade in" id="modal-cheque-terceiro" tabindex="-1" role="dialog" aria-hidden="false" aria-labelledby="Relatorio" style="display: none;">
        <div class="modal-dialog" style="">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #444; color: #666; background: #EEE">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h3 class="modal-title">Relatório Cheque de Terceiros
                    </h3>
                    <p>
                        Preencha as informações abaixo para emitir o relatório.
                    </p>
                </div>
                <div class="modal-body" style="">

                    <%--<div class="form-group w3" id="Div1">
                        <div class="btn-group" data-toggle="buttons">
                            <label class="btn btn-primary">
                                <input name="options" id="radio-analitico" autocomplete="off" checked="checked" type="radio">
                                Analítico
                            </label>
                            <label class="btn btn-primary">
                                <input name="options" id="radio-sintetico" autocomplete="off" type="radio">
                                Sintético
                            </label>
                            <label class="btn btn-primary">
                                <input name="options" id="radio-grafico" autocomplete="off" type="radio">
                                Gráfico
                            </label>
                        </div>
                    </div>--%>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label for="situacao_cheque">Situação do Cheque</label>
                                <select class="form-control required" data-type="combo" id="situacao-cheque">
                                    <option value="">Selecione uma opção</option>
                                    <option value="0">Todas as Situações</option>
                                    <%foreach (var lst in LstChequeSituacao)
                                      {
                                    %><option value="<%=lst.Id %>"><%=lst.Descricao %></option>
                                    <%
                                    }%>
                                </select>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label for="rdb_tipo_data_consulta">Selecione o tipo de data para consulta</label>
                                <select class="form-control required" id="tipo-data" disabled>
                                    <option class="esconder-pendente esconder-movimento" value="">Selecione uma opção</option>
                                    <option class="esconder-pendente" value="0">Data de Movimento</option>
                                    <option class="esconder-movimento" value="1">Data de Emissão</option>
                                    <option class="esconder-movimento" value="2">Data de Vencimento</option>
                                    <option class="esconder-movimento esconder-pendente" value="3">Data de Depósito</option>
                                    <option class="esconder-movimento esconder-pendente" value="4">Data de Compensação</option>
                                    <option class="esconder-movimento esconder-pendente" value="5">Data de 1º Devolução</option>
                                    <option class="esconder-movimento esconder-pendente" value="6">Data de 2º Devolução</option>
                                    <option class="esconder-movimento esconder-pendente" value="7">Data de Liquidação</option>
                                    <option class="esconder-movimento esconder-pendente" value="8">Data de Cobrança</option>
                                    <option class="esconder-movimento esconder-pendente" value="9">Data de Resgate</option>
                                    <option class="esconder-pendente esconder-pendente" value="10">Data de Cancelamento</option>
                                    <option class="esconder-pendente esconder-pendente" value="11">Data Recebimento Cobrança</option>
                                </select>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group" id="Div3" style="">
                                <label for="data_inicial">Data da Consulta - Inicial</label>
                                <input type="text" class="form-control datepicker dateBR required" id="data-inicial" maxlength="10" minlength="10" placeholder="Digite a Data Inicial" />
                            </div>

                        </div>

                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Data da Consulta - Final</label>
                                <input type="text" class="form-control w5 datepicker dateBR required" maxlength="10" minlength="10" id="data-final" placeholder="Digite a Data Final" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="modal-footer" style="background-color: #444; color: #666; background: #EDEDED">
                    <button type="button" class="botao-acao  btn btn-primary " id="btn-relatorio-cheque">
                        <span class="fa fa-file-text"></span>
                        Emitir Relatório
                    </button>

                    <button type="reset" class="  btn btn-warning ">
                        <span class="fa fa-eraser"></span>
                        Limpar formulário
                    </button>

                    <button type="" class="fechar-modal  btn btn-default " data-dismiss="modal">
                        <span class="fa fa-caret-square-o-down"></span>
                        Fechar
                    </button>
                </div>
            </div>
        </div>
    </div>
    <!-- FIM MODAL -->


</asp:Content>

