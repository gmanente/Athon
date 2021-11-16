<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage/ProfessorJanelas.Master" AutoEventWireup="true" CodeBehind="RegimeDomiciliar.aspx.cs" Inherits="Sistema.Web.UI.PortalProfessor.View.Page.RegimeDomiciliar" %>

<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%= Funcoes.InvocarTagArquivo("View/Js/RegimeDomiciliar.js" , true) %>  
    <%= Funcoes.InvocarTagArquivo("View/Css/regimedomiciliar.css" , true) %> 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content">

     <%--   <div class="row">
            <div class="col-xs-12 col-sm-7 col-md-7 col-lg-4">
                <h1 class="page-title txt-color-blueDark"><i class="fa-fw fa fa-home"></i>Regime Domiciliar <span>> Lançamento</span></h1>
            </div>
            <div class="col-xs-12 col-sm-5 col-md-5 col-lg-8">
            </div>
        </div>--%>

        <div class="row">
            <div class="col-md-12">
                <div class="well">
                    <!-- INÍCIO FILTROS -->
                    <div class="row">
                        <div class="col-md-4">
                            <label for="Campus"><b>Campus</b></label>
                            <select class="form-control" id="Campus">
                                <option value="">Selecione o Campus</option>
                                <% foreach (var campus in ListarCampus())
                                   { %>
                                <option value="<%=campus.OptionValue %>"><%=campus.OptionText %></option>
                                <% } %>
                            </select>
                        </div>
                        <div class="col-md-4">
                            <label for="PeriodoLetivo"><b>Período Letivo</b></label>
                            <select class="form-control" id="PeriodoLetivo" disabled="disabled">
                                <option value="">Selecione o Período Letivo</option>
                            </select>
                        </div>
                        <div class="col-md-4" style="padding-top: 24px; ">
                            <button type="button" class="btn btn-primary" id="btnConsultar" title="Consultar Horários das Disciplinas para as Turma(s)">
                                <i class="fa fa-search"></i>&nbsp;Consultar
                            </button>
                            <span id="loading-filtros" style="display: none"><i class="fa fa-circle-o-notch fa-spin"></i>&nbsp;Carregando...</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <section id="widget-grid" class="">
            <div class="row">

                <!-- NEW WIDGET START -->
                <article class="col-sm-12">

                    <!-- Widget ID (each widget will need unique ID)-->
                    <div class="jarviswidget jarviswidget-color-blueDark " id="wid-id-0"
                        data-widget-colorbutton="false"
                        data-widget-editbutton="false"
                        data-widget-togglebutton="false"
                        data-widget-deletebutton="false"
                        data-widget-custombutton="false"
                        data-widget-collapsed="false"
                        data-widget-fullscreenbutton="false"
                        data-widget-sortable="false">
                        <header id="campusPediodoEscolhido">
                            <span class="widget-icon"><i class="fa fa-list"></i></span>
                            <h2 id="campusPediodoEscolhido2">Disciplinas</h2>

                        </header>
                        <div>

                            <div class="widget-body no-padding" style="min-height: 130px;">
                                <!-- INÍCIO DATA GRID -->
                                <div class="table-responsive">

                                    <table id="grid" class="table table-hover table-striped table-stats table-bordered table-sortable">
                                        <thead>
                                            <tr>
                                                <th style="width: 80px;">Ações</th>
                                                <th data-resizable-column-id="rescol1">Aluno</th>
                                                <th data-resizable-column-id="rescol2">Turma</th>
                                                <th data-resizable-column-id="rescol1">Nome da Disciplina</th>                                                
                                                <th data-resizable-column-id="rescol3">Carga Horária</th>
                                                <th data-resizable-column-id="rescol3">Vizualizado</th>
                                                <th data-resizable-column-id="rescol3">Nº Atividade</th>
                                                <th data-resizable-column-id="rescol3">Média</th>
                                            </tr>
                                        </thead>

                                        <tbody id="grid-data-result">
                                            <tr id="grid-start">
                                                <td colspan="8" class="center" style="padding: 20px !important; text-align: center;">
                                                    <i class="fa fa-info-circle"></i>&nbsp;Por favor selecione os filtros acima para consultar as disciplinas.
                                                </td>
                                            </tr>

                                        </tbody>
                                    </table>

                                </div>
                            </div>
                        </div>
                    </div>
                </article>
            </div>
        </section>
          
          <!-- Modal info do Regime -->
            <div id="tela-regime">
                
            </div>
            <!-- FIM Modal info do Regime -->

                 <!-- Modal info do Regime -->
            <div id="tela-atividade">

            </div>
            <!-- FIM Modal info do Regime -->
    </div>

    <!-- FIM FILTROS -->
    <input type="hidden" id="authRf002" value="<%=Autenticar("RF002") %>" />
    <input type="hidden" id="authRf003" value="<%=Autenticar("RF003") %>" />
    <input type="hidden" id="hidDisciplinaOfertaProfessor" value="" />
    <input type="hidden" id="hidGradeLetivaDisciplina" value="" />
    <input type="hidden" id="hacao" value="" />
    <input type="hidden" id="hidRegistroMateria" value="" />
    <input type="hidden" id="hcampusUsuario" value="<%=campusUsuario %>" />
    <input type="hidden" id="hperiodoLetivoCorrente" value="<%=periodoLetivoCorrente %>" />
    
</asp:Content>
