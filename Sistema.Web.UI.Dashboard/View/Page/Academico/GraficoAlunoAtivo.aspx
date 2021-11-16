<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage/Submodulo.Master" AutoEventWireup="true" CodeBehind="GraficoAlunoAtivo.aspx.cs" Inherits="Sistema.Web.UI.Dashboard.View.Page.Academico.GraficoAlunoAtivo" %>

<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%= ImportComponents() %>
    <%= Funcoes.InvocarTagArquivo("View/js/chartjs.plugins.js", true) %>

    <%= Funcoes.InvocarTagArquivo("View/Css/highcharts.css", true) %>
    <%= Funcoes.InvocarTagArquivo("View/js/highcharts.src.js", true) %>
    <%= Funcoes.InvocarTagArquivo("View/js/exporting.js", true) %>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server" ng-cloack="">
        <div id="content" style="opacity: 1;" ng-controller="GraficoAlunoAtivoController">
            <div class="row">
                <div class="col-md-6">
                    <h1 class="page-title txt-color-blueDark">
                        <i class="fa fa-bar-chart-o fa-fw "></i>Gráfico <span>&gt; Quantitativo de Alunos&nbsp;</span><span>-&nbsp;Matrícula e Rematrícula</span>
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
                            <a class="btn btn-default" target="_blank" href="<%= Request.Url %>" title="Abrir em outra aba"><i class="fa fa-external-link-square" aria-hidden="true"></i></a>
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
                                ng-change="ChangePeriodoLetivo();">
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
                    

                    <div class="row">
                        <!--Chart Periodo Letivo-->
                        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6" ng-show="IdPeriodoLetivo > 0">
                            <div class="col-md-12 well well-sm well-light">
                                <div class="row">
                                    <div class="col-md-8">
                                    </div>
                                    <div class="col-md-4">
                                        <div class="widget-toolbar">
                                            <a class="pull-right" ng-disabled="loadChartPeriodoLetivo" ng-click="GetDadosAreaConhecimento()">
                                                <i class="fa fa-refresh"></i>
                                            </a>
                                        </div>
                                        <div class="widget-toolbar">
                                            <div class="smart-form">
                                                <label class="toggle">
                                                    <input type="checkbox" id="demo-switch-to-pills" name="checkbox-toggle" ng-model="realTimePeriodoLetivo" />
                                                    <i data-swchon-text="SIM" data-swchoff-text="NÃO"></i>Tempo real
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>                               

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="text-center" ng-show="!loadChartPeriodoLetivo">
                                            <div id="chartPeriodoLetivo" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
                                        </div>
                                        <div ng-show="loadChartGpa">
                                            <div class="loading-chart"></div>
                                            <div class="loading-chart-text">Carregando...</div>
                                        </div>
                                    </div>
                                </div>                                
                            </div>
                        </div>
                        <!--Chart Periodo-->                        
                    </div>
                </div>
            </div>          
        </div>
    </form>
</asp:Content>
