<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage/ProfessorJanelas.Master" AutoEventWireup="true" CodeBehind="RegistroMateria.aspx.cs" Inherits="Sistema.Web.UI.PortalProfessor.View.Page.RegistroMateria" %>

<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!--INÍCIO CSS-->
    <%= Funcoes.InvocarTagArquivo("View/Css/registromateria.css" , true) %>
    <%= Funcoes.InvocarTagArquivo("View/Js/RegistroMateria.js" , true) %>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
             
    <div id="content">
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
                        <div class="col-md-4" style="padding-top: 24px;">
                            <button type="button" class="btn btn-default" id="btnConsultar" disabled="disabled"  title="Consultar Horários das Disciplinas para as Turma(s)">
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
                                                <th data-resizable-column-id="rescol1">Nome da Disciplina</th>
                                                <th data-resizable-column-id="rescol2">Turma</th>
                                                <th data-resizable-column-id="rescol3">Curso</th>
                                                <th data-resizable-column-id="rescol3">Dia Semana</th>
                                            </tr>
                                        </thead>

                                        <tbody id="grid-data-result">
                                            <tr id="grid-start">
                                                <td colspan="5" class="center" style="padding: 20px !important; text-align: center; color: #DC6363;">
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
          
        <%--<form id="checkout-form" class="smart-form" >--%>
        <!-- INÍCIO CONSOLE MODAL -->
        <div class="modal fade" id="modal-registro" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog" style="width: 100%; max-width: 980px;">
                <div class="modal-content">
                    <div class="modal-header" style="background: #EEE;"> <%--background: #033649; color: white;--%>
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h3 class="modal-title" id="ModalTitulo">Registro de Matéria da Disciplina - </h3>
                    </div>
                    <div class="modal-body">
                        <div class="row" style="padding-bottom: 5px;" id="btnNovoLancamento">
                            <button type="button" class="btn btn-primary" id="btnNovoRegistro" title="Consultar Horários das Disciplinas para as Turma(s)">
                                <i class="fa fa-plus"></i>&nbsp;Novo
                            </button>
                        </div>
                        
                        <div class="row" style="padding-bottom: 5px;" id="camposLancamento">
                             <%--   <div class="" style="background-color: #0B486B; color: white;">Novo Registro de Matéria</div>--%>
                                <div class="col-xs-6 col-sm-6 col-md-7 col-lg-7">
                                    <div class="espacoLancamento">
                                        <h4 class="corLancamento">Selecione a <span id="spanTextoSemanaAula">Semana</span></h4>
                                        <select class="form-control required state-error" data-msg-required="Por favor Informe a Semana"  id="slfDiaAula" style="width:100%;"></select>
                                    </div>
                                </div>
                                <div class="col-xs-6 col-sm-6 col-md-5 col-lg-5">
                                    <div class="espacoLancamento">
                                        <h4 class="corLancamento">&nbsp;</h4>
                                        <button type="button" class="btn btn-success" id="btnCarregarPlanoEnsinoCronograma" disabled="disabled" style="white-space: normal;">
                                            <i class="fa fa-download"></i>&nbsp;Carregar Cronograma do Plano de Ensino
                                        </button>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <br/>
                                    <div class="espacoLancamento">
                                        <h4 class="corLancamento">Conteúdo</h4>
                                        <textarea rows="3" class="form-control required" id="areaConteudo" data-msg-required="Por favor Informe o Conteúdo" style="width: 100%;"></textarea>
                                    </div>
                                    <div class="espacoLancamento">
                                        <h4 class="corLancamento">Metodologia</h4>
                                        <textarea rows="3" class="form-control required" id="areaMetodologia" data-msg-required="Por favor Informe a Métodologia" style="width: 100%;"></textarea>
                                    </div>
                                    <div class="espacoLancamento">
                                        <h4 class="corLancamento">Recurso Previsto</h4>
                                        <textarea rows="3" class="form-control required" data-msg-required="Por favor Informe o Recurso Previsto"  id="areaRecursoPrevisto" style="width: 100%;"></textarea>
                                    </div>
                                    <div class="espacoLancamento">
                                        <button type="button" class="btn btn-default pull-left" id="btnCancelarEdicao">
                                            <i class="fa fa-times"></i>&nbsp;Cancelar
                                        </button>
                                        <button type="button" class="btn btn-success pull-right" id="btnSalvar">
                                            <i class="fa fa-save"></i>&nbsp;Gravar
                                        </button>
                                    </div>
                                </div>
                        </div>
                        
                        <div class="row" style="" id="gridRegistroLancados">
                            <!-- DATA GRID -->
                            <%--<div class="col-md-12" id="grid-container">--%>
                            <div class="table-responsive">
                                <table id="grid-registro" class="table table-hover table-striped table-stats table-bordered table-sortable table-condensed">
                                    <thead>
                                        <tr>
                                            <th data-resizable-column-id="#" style="width: 80px !important; text-align: center;">Ações</th>
                                            <th data-resizable-column-id="rescol4" style="text-align: center;">Aula</th>
<%--                                            <th data-resizable-column-id="rescol5" style="text-align: center;">Data da Aula</th>--%>
                                            <th data-resizable-column-id="rescol6" style="text-align: center;">Dia da Semana</th>
                                            <th data-resizable-column-id="rescol6" style="text-align: center;">Incluído em</th>
                                            <th data-resizable-column-id="rescol7" style="text-align: center;">Alterado em</th>
                                        </tr>
                                    </thead>

                                    <tbody id="gridLancamentosRegistro">
                                    </tbody>

                                </table>

                            </div>
                            <%--</div>--%>
                            <!-- FIM GRID -->
                        </div>
                    </div>
                    <div class="modal-footer" style="background: #EDEDED" id="btnFechar">
                        <button type="button" class="btn btn-default pull-right" data-dismiss="modal">
                            <i class="fa fa-times"></i>&nbsp;Fechar
                        </button>
                    </div>
                </div>
            </div>
        </div>
        <!-- FIM CONSOLE MODAL -->
           <%-- </form>--%>
    </div>

    <!-- FIM FILTROS -->
    <input type="hidden" id="authRf002" value="<%=Autenticar("RF002") %>" />
    <input type="hidden" id="authRf003" value="<%=Autenticar("RF003") %>" />
    <input type="hidden" id="hidDisciplinaOfertaProfessor" value="" />
    <input type="hidden" id="hidDisciplinaOferta" value="" />
    <input type="hidden" id="hidGradeLetivaDisciplina" value="" />
    <input type="hidden" id="hidDiaSemana" value="0" />
    <input type="hidden" id="hacao" value="" />
    <input type="hidden" id="hidRegistroMateria" value="" />
    <input type="hidden" id="hcampusUsuario" value="<%=campusUsuario %>" />
    <input type="hidden" id="hperiodoLetivoCorrente" value="<%=periodoLetivoCorrente %>" />
    
</asp:Content>
