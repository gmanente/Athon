<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage/ProfessorJanelas.Master" AutoEventWireup="true" CodeBehind="PlanoEnsino.aspx.cs" Inherits="Sistema.Web.UI.PortalProfessor.View.Page.PlanoEnsino" %>

<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%= Funcoes.InvocarTagArquivo("View/Js/PlanoEnsino.js" , true) %>
    <%= Funcoes.InvocarTagArquivo("View/Css/planoensino.css" , true) %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div id="content">

       <%-- <div class="row">
            <div class="col-xs-12 col-sm-7 col-md-7 col-lg-4">
                <h1 class="page-title txt-color-blueDark"><i class="fa-fw fa fa-edit"></i>Plano de Ensino <span>> Lançamento</span></h1>
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
                        <div class="col-md-4" style="padding-top: 24px;">
                            <button type="button" class="btn btn-default" id="btnConsultar" disabled="disabled" title="Consultar Horários das Disciplinas para as Turma(s)">
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

        <div id="checkout-form" class="">
            <!-- INÍCIO CONSOLE MODAL -->
            <div class="modal fade" id="modal-registro" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog" style="width: 100%; max-width: 980px;">
                    <div class="modal-content">
                        <div class="modal-header" style="background:#EEE;">
                            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                            <h3 class="modal-title" id="ModalTitulo">Plano de Ensino da Disciplina - </h3>
                        </div>
                        <div class="modal-body">

                            <div class="row" style="" id="gridRegistroLancados">

                                <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4" style="padding-right: 0; padding-left: 2px;">
                                    <div class="tabs-left">
                                        <ul class="nav nav-tabs tabs-left">
                                            <li class="active" id="lilinkEmenta" style="cursor:pointer;">
                                                <a id="linkEmenta" data-toggle="tab" aria-expanded="false"><span class="badge bg-color-blue txt-color-white"><i class='fa fa-file-o'></i></span>Ementa </a>
                                            </li>
                                            <li class="" id="lilinkObjGeral" style="cursor:pointer;">
                                                <a id="linkObjGeral" data-toggle="tab" aria-expanded="false"><span class="badge bg-color-blue txt-color-white"><i class='fa fa-bullseye'></i></span>Objetivo Geral</a>
                                            </li>
                                            <li class="" id="lilinkObjEspecifico" style="cursor:pointer;">
                                                <a id="linkObjEspecifico" data-toggle="tab" aria-expanded="false"><span class="badge bg-color-blue txt-color-white"><i class='fa fa-road'></i></span>Objetivos Específicos</a>
                                            </li>
                                            <li class="" id="lilinkUnidadeDidatica" style="cursor:pointer;">
                                                <a id="linkUnidadeDidatica" data-toggle="tab" aria-expanded="false"><span class="badge bg-color-blue txt-color-white"><i class='fa fa-institution'></i></span>Unidade Didática</a>
                                            </li>
                                            <li class="" id="lilinkCronogramaExecucao" style="cursor:pointer;">
                                                <a id="linkCronogramaExecucao" data-toggle="tab" aria-expanded="false"><span class="badge bg-color-blue txt-color-white"><i class='fa fa-list'></i></span>Cronograma Execução da Carga Horária no Semestre</a>
                                            </li>
                                            <li class="" id="lilinkAvaliacao" style="cursor:pointer;">
                                                <a id="linkAvaliacao" data-toggle="tab" aria-expanded="false"><span class="badge bg-color-blue txt-color-white"><i class='fa fa-inbox'></i></span>Processo de Avaliação da Aprendizagem</a>
                                            </li>
                                            <li class="" id="liBibliografiaBasica" style="cursor:pointer;">
                                                <a id="linkBibliografiaBasica" data-toggle="tab" aria-expanded="false"><span class="badge bg-color-blue txt-color-white"><i class='fa fa-ticket'></i></span>Bibliografia Básica (títulos, períodos, etc.)</a>
                                            </li>
                                            <li class="" id="liBibliografiaComplementar" style="cursor:pointer;">
                                                <a id="linkBibliografiaComplementar" data-toggle="tab" aria-expanded="false"><span class="badge bg-color-blue txt-color-white"><i class='fa fa-language'></i></span>Bibliografia Complementar</a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="col-xs-8 col-sm-8 col-md-8 col-lg-8" style="/*text-align: left; padding-left: 2px; padding-right: 10px;*/">
                                    <div class="tab-pane active" id="tbEmenta" style="margin-bottom: 4px;">
                                        <h4 class="corLancamento">Ementa</h4><br/>
                                        <textarea rows="24" class="form-control required" id="areaEmenta" disabled="disabled" data-msg-required="Por favor informe a Ementa da Disciplina" style="width: 100%;"></textarea>
                                        <input type="hidden" id="idPlanoEnsinotbEmenta" value="0" />
                                    </div>
                                    <div class="tab-pane active" id="tbObjGeral" style="margin-bottom: 4px;">
                                        <h4 class="corLancamento">Objetivo Geral</h4><br/>
                                        <textarea rows="24" class="form-control required" id="areaObjGeral" data-msg-required="Por favor informe o Objetivo Geral da Disciplina" style="width: 100%;"></textarea>
                                        <input type="hidden" id="idPlanoEnsinotbObjGeral" value="0" />
                                    </div>
                                    <div class="tab-pane active" id="tbObjEspecifico" style="margin-bottom: 4px;">
                                        <h4 class="corLancamento">Objetivos Específicos</h4><br/>
                                        <textarea rows="24" class="form-control required" id="areaObjespecifico" data-msg-required="Por favor informe os Objetivos Específicos da Disciplina" style="width: 100%;"></textarea>
                                        <input type="hidden" id="idPlanoEnsinotbObjEspecifico" value="0" />
                                    </div>
                                    <div class="tab-pane active" id="tbUnidadeDidatica" style="margin-bottom: 4px;">
                                        <h4 class="corLancamento">Unidade Didática</h4><br/>
                                        <textarea rows="24" class="form-control required" id="areaUnidadeDidatica" data-msg-required="Por favor informe a Unidade Didática da Disciplina" style="width: 100%;"></textarea>
                                        <input type="hidden" id="idPlanoEnsinotbUnidadeDidatica" value="0" />
                                    </div>
                                    <div class="tab-pane active" id="tbCronogramaExecucao" style="margin-bottom: 4px;">
                                        <h4 class="corLancamento">Cronograma Execução da Carga Horária no Semestre</h4>
                                        <br/>
                                        <div class="col-xs-9 col-sm-9 col-md-9 col-lg-9" id="divtblconteudo" style="text-align: left; padding-left: 0; padding-right: 0;">
                                      
                                        </div>
                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3" style="text-align: left; padding-left: 0; padding-right: 0;">
                                            <div class="tabs-right pull-left">
                                                <ul class="nav nav-tabs" id="lstlinksemana">
                                                </ul>
                                            </div>
                                        </div>

<%--                                        <textarea rows="30" class="form-control required" id="areaCronogramaExecucao" data-msg-required="Por favor informe o Cronograma Execução da Disciplina" style="width: 100%;"></textarea>
                                        <input type="hidden" id="idPlanoEnsinotbCronogramaExecucao" value="0" />--%>
                                    </div>
                                    <div class="tab-pane active" id="tbAvaliacao" style="margin-bottom: 4px;">
                                        <h4 class="corLancamento">Processo de Avaliação da Aprendizagem</h4><br/>
                                        <textarea rows="24" class="form-control required" id="areaAvaliacao" data-msg-required="Por favor informe o Processo de Avaliação da Aprendizagem da Disciplina" style="width: 100%;"></textarea>
                                        <input type="hidden" id="idPlanoEnsinotbAvaliacao" value="0" />
                                    </div>
                                    <div class="tab-pane active" id="tbBibliografiaBasica" style="margin-bottom: 4px;">
                                        <h4 class="corLancamento">Bibliografia Básica (títulos, periódicos, etc.)</h4><br/>
                                        <textarea rows="24" class="form-control required" id="areaBibliografiaBasica" disabled="disabled" data-msg-required="Por favor informe a Bibliografia Básica (títulos, períodos, etc.) da Disciplina" style="width: 100%;"></textarea>
                                        <input type="hidden" id="idPlanoEnsinotbBibliografiaBasica" value="0" />
                                    </div>
                                    <div class="tab-pane active" id="tbBibliografiaComplementar" style="margin-bottom: 4px;">
                                        <h4 class="corLancamento">Bibliografia Complementar</h4><br/>
                                        <textarea rows="24" class="form-control required" id="areabibliografiaComplementar" data-msg-required="Por favor informe a Bibliografia Complementar da Disciplina" style="width: 100%;"></textarea>
                                        <input type="hidden" id="idPlanoEnsinotbBibliografiaComplementar" value="0" />
                                    </div>
                              <%--      <button type="button" class="btn btn-success pull-right" id="btnSalvarSemana">
                                        <i class="fa fa-save"></i>&nbsp;Gravar
                                    </button>--%>
                                </div>
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
        </div>
        
            <!-- INÍCIO MODAL SELEÇÃO DA DISCIPLINA PARA CÓPIA -->
            <div class="modal fade" id="modal-disciplina-destino" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog" style="width: 100%; max-width: 980px;">
                    <div class="modal-content">
                        <div class="modal-header" style="background:#EEE;">
                            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                            <h3 class="modal-title" id="ModalTituloDisciplinaDestino">Selecione a Disciplina de Destino</h3>
                        </div>
                        <div class="modal-body">
                            <div class="widget-body no-padding" style="min-height: 130px;">
                                <!-- INÍCIO DATA GRID -->
                                <div class="table-responsive">

                                    <table id="grid-disciplinas-destino" class="table table-hover table-striped table-stats table-bordered table-sortable">
                                        <thead>
                                            <tr>
                                                <th style="width: 80px;">Ação</th>
                                                <th data-resizable-column-id="rescol1">Nome da Disciplina</th>
                                                <th data-resizable-column-id="rescol2">Turma</th>
                                                <th data-resizable-column-id="rescol3">Curso</th>
                                                <th data-resizable-column-id="rescol3">Dia Semana</th>
                                            </tr>
                                        </thead>
                                        <tbody id="grid-data-result-disciplinas-destino">
                                            <tr id="grid-data-loading-disciplinas-destino">
                                                <td colspan="6" style="text-align: center; background-color: #d9edf7; padding: 20px !important; font-size: 13px">
                                                    <i class="fa fa-circle-o-notch fa-spin"></i>&nbsp;Consultando Disciplinas ...
                                                </td>
                                            </tr>
                                            <tr id="grid-data-not-found-disciplinas-destino" style="display: none;">
                                                <td colspan="6" class="center" style="background-color: #FFF8DC; padding: 20px !important; font-size: 13px">
                                                    <i class="fa fa-info-circle"></i>&nbsp;Nenhuma disciplina encontrada.<br />
                                                    Por favor considere outros filtros para uma nova consulta.
                                                </td>
                                            </tr>
                                            <tr id="grid-data-error-disciplinas-destino" style="display: none;">
                                                <td colspan="6" class="center danger" style="background-image: linear-gradient(to bottom, #f2dede 0%, #e7c3c3 100%); padding: 20px !important; font-size: 13px;">
                                                    <i class="fa fa-times"></i>&nbsp;<span id="grid-data-error-text-aluno-pesquisado"></span>
                                                </td>
                                            </tr>

                                        </tbody>
                                    </table>

                                </div>
                            </div>
                        </div>
                        <div class="modal-footer" style="background: #EDEDED" id="Div31">
                            <button type="button" class="btn btn-default pull-right" data-dismiss="modal">
                                <i class="fa fa-times"></i>&nbsp;Fechar
                            </button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- FIM MODAL SELEÇÃO DA DISCIPLINA PARA CÓPIA -->
    </div>

    <!-- FIM FILTROS -->
    <input type="hidden" id="authRf001" value="<%=Autenticar("RF001") %>" />
    <input type="hidden" id="authRf002" value="<%=Autenticar("RF002") %>" />
    <input type="hidden" id="authRf003" value="<%=Autenticar("RF003") %>" />
    <input type="hidden" id="authRf004" value="<%=Autenticar("RF004") %>" />
    <input type="hidden" id="hAbaSelecionada" value="" />
    <input type="hidden" id="hConteudoAbaSelecionada" value="" />
    <input type="hidden" id="hAreaSelecionada" value="" />
    <input type="hidden" id="liSelecionado" value="" />
    <input type="hidden" id="hMensagemtab" value="" />    
    <input type="hidden" id="hcampusUsuario" value="<%=campusUsuario %>" />
    <input type="hidden" id="hperiodoLetivoCorrente" value="<%=periodoLetivoCorrente %>" />
    <input type="hidden" id="hidDisciplinaOfertaProfessor" value="" /> 
    <input type="hidden" id="hidDisciplinaOferta" value="" /> 
    <input type="hidden" id="hAbaSemanaSelecionada" value="" /> 
    <input type="hidden" id="hValorSemanaSelecionadaConteudo" value="" /> 
    <input type="hidden" id="hValorSemanaSelecionadaMetodologia" value="" /> 
    <input type="hidden" id="hValorSemanaSelecionadaRecurso" value="" /> 
       
    

</asp:Content>