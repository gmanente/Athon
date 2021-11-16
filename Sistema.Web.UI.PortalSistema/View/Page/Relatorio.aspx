<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage/ProfessorJanelas.Master" AutoEventWireup="true" CodeBehind="Relatorio.aspx.cs" Inherits="Sistema.Web.UI.PortalProfessor.View.Page.Relatorio" %>

<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!--INÍCIO CSS-->
    <%= Funcoes.InvocarTagArquivo("View/Css/relatorio.css" , true) %>
    <%= Funcoes.InvocarTagArquivo("View/Js/relatorio.js" , true) %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="content">
        <div class="row">
            <div class="col-md-12">
                <div class="well">
                    <!-- INÍCIO FILTROS -->
                    <div class="row">
                        <div class="col-md-2">
                            <label for="Campus"><b>Campus</b></label>
                            <select class="form-control" id="Campus" name="Campus">
                                <option value="">Selecione o Campus</option>
                                <% foreach (var campus in ListarCampus())
                                   { %>
                                <option value="<%=campus.OptionValue %>"><%=campus.OptionText %></option>
                                <% } %>
                            </select>
                        </div>
                        <div class="col-md-2">
                            <label for="PeriodoLetivo"><b>Período Letivo</b></label>
                            <select class="form-control" id="PeriodoLetivo" name="PeriodoLetivo" disabled="disabled">
                                <option value="0">Selecione o Campus</option>
                            </select>
                        </div>
                        <div class="col-md-3">
                            <label for="Disciplina"><b>Disciplina</b></label>
                            <select class="form-control" id="Disciplina" name="Disciplina" disabled="disabled">
                                <option value="0">Selecione o Período Letivo</option>
                            </select>
                        </div>
                        <div class="col-md-2" style="padding-top: 24px;">
                            <%--<button type="button" class="btn btn-default" id="btnConsultar" disabled="disabled" title="Consultar">
                                <i class="fa fa-search"></i>&nbsp;Consultar
                            </button>--%>
                            <span id="loading-filtros" style="display: none"><i class="fa fa-circle-o-notch fa-spin"></i>&nbsp;Carregando...</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="alert alert-info">
                    <p><i class="fa fa-info-circle"></i>&nbsp; Para imprimir um relatório selecione os filtros acima.</p>
                </div>
            </div>
        </div>


        <div class="row">
            <div class="col-md-12">
                <div class="well">
                    <div class="row">
                        <h6>Relatórios </h6>
                        <div class="dd" id="nestable">
                            <ol class="dd-list">

                                <% if (Autenticar("RF002")) { %>
                                <li class="dd-item" data-id="1">
                                    <div class="dd-handle">
                                        <strong>Lista de Chamada</strong>
                                        <button type="button" class="btn btn-primary btn-xs" id="btn-lista-chamada" style="float: right">Imprimir</button>
                                    </div>
                                </li>
                                <% } %>

                                <% if (Autenticar("RF005")) { %>
                                <li class="dd-item" data-id="1">
                                    <div class="dd-handle">
                                        <strong>Lista de Presença 1ºB</strong>
                                        <button type="button" class="btn btn-primary btn-xs" id="btn-lista-presenca-1b" style="float: right">Imprimir</button>
                                    </div>
                                </li>
                                <% } %>

                                <% if (Autenticar("RF006")) { %>
                                <li class="dd-item" data-id="1">
                                    <div class="dd-handle">
                                        <strong>Lista de Presença 2ºB</strong>
                                        <button type="button" class="btn btn-primary btn-xs" id="btn-lista-presenca-2b" style="float: right">Imprimir</button>
                                    </div>
                                </li>
                                <% } %>

                                <% if (Autenticar("RF004")) { %>
                                <li class="dd-item" data-id="1">
                                    <div class="dd-handle">
                                        <strong>Lista de Presença PF</strong>
                                        <button type="button" class="btn btn-primary btn-xs" id="btn-lista-presenca-pf" style="float: right">Imprimir</button>
                                    </div>
                                </li>
                                <% } %>

                                <% if (Autenticar("RF003")) { %>
                                <li class="dd-item" data-id="1">
                                    <div class="dd-handle">
                                        <strong>Lista de Conferência</strong>
                                        <button type="button" class="btn btn-primary btn-xs" id="btn-lista-conferencia" style="float: right">Imprimir</button>
                                    </div>
                                </li>
                                <% } %>

                                <% if (Autenticar("RF001")) { %>
                                <li class="dd-item" data-id="1">
                                    <div class="dd-handle">
                                        <strong>Planilha de Notas e Faltas</strong>
                                        <button type="button" class="btn btn-primary btn-xs" id="btn-planilha-nota-falta" style="float: right">Imprimir</button>
                                    </div>
                                </li>
                                <% } %>
                                
                                 <% if (Autenticar("RF008")) { %>
                                <li class="dd-item" data-id="1">
                                    <div class="dd-handle">
                                        <strong>Registro de Matéria</strong>
                                        <button type="button" class="btn btn-primary btn-xs" id="btn-registro-materia" style="float: right">Imprimir</button>
                                    </div>
                                </li>
                                <% } %>
                                
                                 <% if (Autenticar("RF007")) { %>
                                <li class="dd-item" data-id="1">
                                    <div class="dd-handle">
                                        <strong>Plano de Ensino</strong>
                                        <button type="button" class="btn btn-primary btn-xs" id="btn-plano-ensino" style="float: right">Imprimir</button>
                                    </div>
                                </li>
                                <% } %>

                            </ol>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>


    <!-- FIM FILTROS -->
    <%--<input type="hidden" id="authRf001" value="<%=Autenticar("RF001") %>" />
    <input type="hidden" id="authRf002" value="<%=Autenticar("RF002") %>" />
    <input type="hidden" id="authRf003" value="<%=Autenticar("RF003") %>" />
    <input type="hidden" id="idLancamentoFalta" value="0" />
    <input type="hidden" id="idLancamentoFaltaNaoFinalizado" value="0" />
    <input type="hidden" id="idAulaNaoFinalizado" value="0" />
    <input type="hidden" id="erroGravaLancamento" value="0" />
    <input type="hidden" id="statusLancamentoFalta" value="0" />
    <input type="hidden" id="hidDisciplinaOfertaProfessor" value="" />
    <input type="hidden" id="hidGradeLetivaDisciplina" value="" />
    <input type="hidden" id="hacao" value="" />
    <input type="hidden" id="hidRegistroMateria" value="" />
    <input type="hidden" id="hcampusUsuario" value="<%=campusUsuario %>" />
    <input type="hidden" id="hperiodoLetivoCorrente" value="<%=periodoLetivoCorrente %>" />--%>
</asp:Content>
