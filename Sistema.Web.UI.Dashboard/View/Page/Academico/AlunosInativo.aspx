﻿<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage/Submodulo.Master" AutoEventWireup="true" CodeBehind="AlunosInativo.aspx.cs" Inherits="Sistema.Web.UI.Dashboard.View.Page.Academico.AlunosInativo" %>

<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%= ImportComponents() %>
    <%= Funcoes.InvocarTagArquivo("View/js/chartjs.plugins.js", true) %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server" ng-cloack="">
        <div id="content" style="opacity: 1;" ng-controller="AlunosInativoController">
            <div class="row">
                <div class="col-md-6">
                    <h1 class="page-title txt-color-blueDark">
                        <i class="fa fa-bar-chart-o fa-fw "></i>Resumo <span>&gt; Quantitativo de Alunos&nbsp;</span><span>-&nbsp;<strong></strong> Inativos</span>
                    </h1>
                </div>
                <div class="col-md-6">

                    <div class="row">
                        <div class="col-md-3 pull-right">

                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-refresh"></i></span>
                                    <select ng-model="timeRefresh" ng-change="changeTime()" class="form-control">
                                        <option ng-selected="timeRefresh == 10" ng-value="10">10 s</option>
                                        <option ng-selected="timeRefresh == 20" ng-value="20">20 s</option>
                                        <option ng-selected="timeRefresh == 30" ng-value="30">30 s</option>
                                        <option ng-selected="timeRefresh == 40" ng-value="40">40 s</option>
                                        <option ng-selected="timeRefresh == 50" ng-value="50">50 s</option>
                                        <option ng-selected="timeRefresh == 60" ng-value="60">60 s</option>
                                    </select>

                                </div>
                            </div>


                        </div>

                        <div class="pull-right">

                            <a class="btn btn-default" ng-click="SetHideFilter()" href="javascript:void(0);" title="Esconder Cabeçalho"><i ng-class="{'fa fa-chevron-circle-up' : !HideFilter, 'fa fa-chevron-circle-down' : HideFilter}" class="fa fa-chevron-up" aria-hidden="true"></i></a>
                            <!--<a class="btn btn-default" ng-click="SetFullScreen()"  title="Full Screen"><i class="fa fa-arrows-alt"></i></a>-->
                        </div>
                    </div>
                </div>
            </div>
            <div>
                <div class="col-md-12 well well-sm well-light" style="width: 100% !important; margin-left: 0px !important" ng-show="!HideFilter">
                    <div class="row">

                        <div class="col-md-3">
                            <label>Campus</label>
                            <select name="IdCampus"
                                ng-model="IdCampus"
                                ng-options="v.Campus.Id as v.Campus.Nome for v in LstCampusUsuario" class="form-control"
                                ng-change="GetDadosAreaConhecimento();">
                                <option value="">-- Todos os Campus --</option>
                            </select>
                        </div>

                        <div class="col-md-4">
                            <label>Periodo Letivo</label>
                            <select name="IdPeriodoLetivo"
                                ng-model="IdPeriodoLetivo"
                                ng-options="v.Id as v.Descricao for v in LstPeriodoLetivo" class="form-control"
                                ng-change="ChangePeriodoLetivo();">
                                <option value="">-- Selecione um Periodo Letivo --</option>
                            </select>
                        </div>


                    </div>
                </div>
                <div fullscreen="IsFullscreen">

                    <style>
                        canvas {
                            -moz-user-select: none;
                            -webkit-user-select: none;
                            -ms-user-select: none;
                        }
                    </style>
                    <div class="row" ng-show="IdPeriodoLetivo > 0">
                        <!--Chart Resumo de Matricula e Rematricula no Periodo-->
                        <!--<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <div class="col-md-12 well well-sm well-light">
                        <div class="row">
                            <div class="col-md-8">
                                <h4 class="txt-color-blue">
                                    <span class="semi-bold">Resumo de Matrículas e Rematrículas nos períodos </span>
                                </h4>
                            </div>

                            <div class="col-md-4">
                                <div class="widget-toolbar">
                                    <a class="pull-right"><i class="fa fa-refresh" ng-click="RefreshChartResumoPorPeriodo()"></i></a>
                                </div>

                                <div class="widget-toolbar">
                                    <div class="smart-form">
                                        <label class="toggle">
                                            <input type="checkbox" id="demo-switch-to-pills" name="checkbox-toggle" ng-model="realTimeResumoPorPeriodo" />
                                            <i data-swchon-text="SIM" data-swchoff-text="NÃO"></i>Tempo real
                                        </label>
                                    </div>
                                </div>
                            </div>


                        </div>

                        <div class="text-center" ng-show="loadChartGpa == false">
                            <canvas id="chartResumoPeriodo" tc-chartjs-line chart-data="DadosCharResumoPeriodo"
                                    style="display: block !important; width: 100% !important; height: 250px !important"
                                    chart-options="OptionsResumoPeriodo"></canvas>
                        </div>
                        <div ng-show="loadChartResumoPorPeriodo == true">
                            <div class="loading-chart"></div>
                            <div class="loading-chart-text">Carregando...</div>
                        </div>
                    </div>
                </div>-->
                        <!--Chart Resumo de Matricula e Rematricula no Periodo-->
                    </div>

                    <div class="row">
                        <!--Chart GPA-->
                        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6" ng-show="IdPeriodoLetivo > 0">
                            <div class="col-md-12 well well-sm well-light">
                                <div class="row">
                                    <div class="col-md-8">
                                        <h4 class="txt-color-blue">
                                            <span class="semi-bold">Quantitativo por </span>Área de Conhecimento
                                        </h4>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="widget-toolbar">
                                            <a class="pull-right" ng-disabled="loadChartGpa" ng-click="GetDadosAreaConhecimento()">
                                                <i class="fa fa-refresh"></i>
                                            </a>
                                        </div>

                                        <div class="widget-toolbar">
                                            <div class="smart-form">
                                                <label class="toggle">
                                                    <input type="checkbox" id="demo-switch-to-pills" name="checkbox-toggle" ng-model="realTimeGPA" />
                                                    <i data-swchon-text="SIM" data-swchoff-text="NÃO"></i>Tempo real
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="text-center" ng-show="!loadChartGpa">
                                            <canvas id="chartGpa" tc-chartjs-doughnut chart-data="DadosChartsGpa" style="display: block; height: 299px;"
                                                chart-options="OptionsChartGpa"
                                                chart-click="ClickChartGpa(event)"></canvas>

                                        </div>
                                        <div ng-show="loadChartGpa">
                                            <div class="loading-chart"></div>
                                            <div class="loading-chart-text">Carregando...</div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row text-center" style="padding-top: 5%;">
                                    <div class="col-md-12">
                                        <label>
                                            Total:&nbsp;<small class="txt-color-blue">{{TotalGeralGpa}}</small>
                                        </label>
                                    </div>
                                    <!--<div class="col-md-4">
                                <label>
                                    Total Matrícula:&nbsp;<small class="txt-color-blue">{{TotalMatriculaGpa}}</small>
                                </label>
                            </div>

                            <div class="col-md-4">
                                <label>
                                    Total Rematrícula:&nbsp;<small class="txt-color-blue">{{TotalRematriculaGpa}}</small>
                                </label>
                            </div>-->
                                </div>

                            </div>
                        </div>
                        <!--Chart GPA-->
                        <!--Chart Tipo de Ingresso-->
                        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6" ng-show="IdGpaSelecionado > 0">
                            <div class="col-md-12 well well-sm well-light">
                                <div class="row">
                                    <div class="col-md-6">
                                        <h4 ng-style="{ 'color' : Gpa.Color }"><i class="fa fa-filter" aria-hidden="true"></i>&nbsp;{{ Gpa.Nome }}<span class="semi-bold"></span></h4>
                                    </div>

                                    <div class="col-md-6">

                                        <div class="widget-toolbar" ng-show="IdGpaSelecionado > 0">
                                            <a class="pull-right" ng-click="RefreshChartSituacaoAcademica()"><i class="fa fa-refresh"></i></a>
                                        </div>

                                        <div class="widget-toolbar">
                                            <div class="smart-form">
                                                <label class="toggle">
                                                    <input type="checkbox" id="demo-switch-to-pills" name="checkbox-toggle" ng-model="realTimeSituacaoAcademica" />
                                                    <i data-swchon-text="SIM" data-swchoff-text="NÃO"></i>Tempo real
                                                </label>
                                            </div>
                                        </div>

                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="text-center" ng-show="!loadChartSituacaoAcademica">
                                            <canvas id="chartSituacaoAcademica" tc-chartjs-pie chart-data="DadosChartsSituacaoAcademica" style="display: block; height: 399px;"
                                                chart-options="OptionsChartSituacaoAcademica"></canvas>
                                        </div>

                                        <div class="text-center" ng-show="loadChartSituacaoAcademica">
                                            <div class="loading-chart"></div>
                                            <div class="loading-chart-text">Carregando...</div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row text-center" style="padding-top: 5%;">
                                    <div class="col-md-12">
                                        <label>
                                            Total:&nbsp;<small class="txt-color-blue">{{TotalGeralTipoAcesso}}</small>
                                        </label>
                                    </div>
                                    <!--<div class="col-md-6">
                                <label>
                                    Total Matrícula:&nbsp;<small class="txt-color-blue">{{TotalMatriculaTipoAcesso}}</small>
                                </label>
                            </div>-->
                                    <!--<div class="col-md-4">
                                <label>
                                    Total Rematrícula:&nbsp;<small class="txt-color-blue">{{TotalRematriculaTipoAcesso}}</small>
                                </label>
                            </div>-->
                                </div>
                            </div>
                        </div>
                        <!--Chart Tipo de Ingresso-->
                    </div>
                    <div class="row">
                        <!--Chart Cursos-->
                        <div ng-class="{ 'col-xs-12 col-sm-12 col-md-12 col-lg-12' : DadosChartsCursoTurma.labels.length >= 20, 'col-xs-6 col-sm-6 col-md-6 col-lg-6' : (DadosChartsCursoTurma.labels.length < 20 || !DadosChartsCursoTurma.labels) }" ng-show="IdGpaSelecionado > 0">
                            <div class="col-md-12 well well-sm well-light">
                                <div class="row">
                                    <div class="col-md-6">
                                        <h4 ng-style="{ 'color' : Gpa.Color }"><i class="fa fa-filter" aria-hidden="true"></i>&nbsp;{{ Gpa.Nome }}<span class="semi-bold"></span></h4>
                                    </div>

                                    <div class="col-md-6">

                                        <div class="widget-toolbar" ng-show="IdGpaSelecionado > 0">
                                            <a class="pull-right" ng-click="RefreshChartCurso()"><i class="fa fa-refresh"></i></a>
                                        </div>

                                        <div class="widget-toolbar">
                                            <div class="smart-form">
                                                <label class="toggle">
                                                    <input type="checkbox" id="demo-switch-to-pills" name="checkbox-toggle" ng-model="realTimeCurso" />
                                                    <i data-swchon-text="SIM" data-swchoff-text="NÃO"></i>Tempo real
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="text-center" ng-show="!loadChartCurso">
                                            <canvas id="chartCursos" tc-chartjs-horizontalbar chart-data="DadosChartsCurso" style="display: block; height: 588px;"
                                                chart-options="OptionsChartCurso"
                                                chart-click="ClickChartCurso(event)"></canvas>
                                        </div>
                                        <div class="text-center" ng-show="loadChartCurso">
                                            <div class="loading-chart"></div>
                                            <div class="loading-chart-text">Carregando...</div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row text-center" style="padding-top: 5%;">
                                    <div class="col-md-12">
                                        <label>
                                            Total:&nbsp;<small class="txt-color-blue">{{TotalGeralCursos}}</small>
                                        </label>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <!--Chart Cursos-->
                        <!--Chart Tumas-->
                        <div ng-class="{ 'col-xs-12 col-sm-12 col-md-12 col-lg-12' : DadosChartsCursoTurma.labels.length >= 20, 'col-xs-6 col-sm-6 col-md-6 col-lg-6' : DadosChartsCursoTurma.labels.length < 20}" ng-show="IdCursoSelecionado > 0">
                            <div class="col-md-12 well well-sm well-light">
                                <div class="row">
                                    <div class="col-md-8">
                                        <h4 class="txt-color-blue">
                                            <span class="semi-bold">Quantitativo por </span>Turmas
                                        </h4>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="widget-toolbar">
                                            <a class="pull-right"><i class="fa fa-refresh" ng-click="RefreshChartTurma()"></i></a>
                                        </div>

                                        <div class="widget-toolbar">
                                            <div class="smart-form">
                                                <label class="toggle">
                                                    <input type="checkbox" id="demo-switch-to-pills" name="checkbox-toggle" ng-model="realTimeTurma" />
                                                    <i data-swchon-text="SIM" data-swchoff-text="NÃO"></i>Tempo real
                                                </label>
                                            </div>
                                        </div>
                                    </div>


                                </div>


                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="text-center" ng-show="!loadChartCursoTurma">
                                            <canvas id="chartCursoTurma" tc-chartjs-bar chart-data="DadosChartsCursoTurma" style="display: block; height: 588px;"
                                                chart-options="OptionsChartCursoTurma" chart-click="ClickChartCursoTurmas(event)"></canvas>
                                        </div>
                                        <div ng-show="loadChartCursoTurma">
                                            <div class="loading-chart"></div>
                                            <div class="loading-chart-text">Carregando...</div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row text-center" style="padding-top: 5%;">
                                    <div class="col-md-12">
                                        <label>
                                            Total:&nbsp;<small class="txt-color-blue">{{TotalGeralCursoTurma}}</small>
                                        </label>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <!--Chart Tumas-->
                    </div>






                    <div class="row">
                        <!--Relação de Alunos da turma selecionada-->
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" ng-show="IdGradeLetivaTurma > 0">
                            <div class="col-md-12 well well-sm well-light">
                                <div class="row">
                                    <div class="col-md-8">
                                        <h4 class="txt-color-blue">
                                            <span class="semi-bold">Relação de Alunos da Turma&nbsp;</span>-&nbsp;{{ Turma.Nome }}
                                        </h4>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="widget-toolbar">
                                            <a ng-click="CloseListaTurmas()" class="pull-right"><i class="fa fa-times"></i></a>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <table class="table table-bordered">
                                                <thead>
                                                    <tr>
                                                        <th>#</th>
                                                        <th>Matricula</th>
                                                        <th>Cpf</th>
                                                        <th style="text-align: left">Nome</th>
                                                        <th style="text-align: left">Situação Acadêmica</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr ng-show="loadListaAlunosTurma">
                                                        <td colspan="8">
                                                            <div>
                                                                <div class="loading-chart"></div>
                                                                <div class="loading-chart-text">Carregando...</div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr ng-show="!loadListaAlunosTurma && FilterItems.length === 0">
                                                        <td colspan="8">
                                                            <div class="alert alert-warning">
                                                                <div>
                                                                    Não foi encontrado registros. Verifique os filtros selecionados
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr ng-repeat="row in LstAlunosTurma | filter: FilterListaAluno() as FilterItems">
                                                        <th class="text-center">{{ $index + 1 }}</th>
                                                        <td>{{ row.Matricula }}</td>
                                                        <td>{{ row.Cpf }}</td>
                                                        <td style="text-align: left">{{ row.Nome }}</td>
                                                        <td style="text-align: left">{{ row.SituacaoAcademica }}</td>
                                                    </tr>
                                                </tbody>
                                            </table>

                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>
                        <!--Relação de Alunos da turma selecionada-->
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
