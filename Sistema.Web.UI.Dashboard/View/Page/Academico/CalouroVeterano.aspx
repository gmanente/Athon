<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage/Submodulo.Master" AutoEventWireup="true" CodeBehind="CalouroVeterano.aspx.cs" Inherits="Sistema.Web.UI.Dashboard.View.Page.Academico.CalouroVeterano" %>

<%@ Import Namespace="Sistema.Api.dll.Repositorio.Util" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%= Funcoes.InvocarTagArquivo("View/Css/fab-forms.css") %>

    <%= ImportComponents() %>
    <%= Funcoes.InvocarTagArquivo("View/js/chartjs.plugins.js", true) %>

    <%= Funcoes.InvocarTagArquivo("View/lib/ng-tags-input.min.js", true) %>
    <%= Funcoes.InvocarTagArquivo("View/lib/ng-tags-input.min.css", true) %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server" ng-cloack="">
        <div id="content" style="opacity: 1;" ng-controller="CalouroVeteranoController">
            <div class="row">
                <div class="col-md-6">
                    <h1 class="page-title txt-color-blueDark">
                        <i class="fa fa-bar-chart-o fa-fw "></i>Resumo <span>&gt; Quantitativo de Alunos&nbsp;</span><span>-&nbsp;Calouros e Veteranos</span>
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

                        <div class="col-md-2">
                            <label>Campus</label>
                            <select name="IdCampus"
                                ng-model="IdCampus"
                                ng-options="v.Campus.Id as v.Campus.Nome for v in LstCampusUsuario" class="form-control"
                                ng-change="GetDadosAreaConhecimento();">
                                <option value="">-- Todos os Campus --</option>
                            </select>
                        </div>

                        <div class="col-md-3">
                            <label>Periodo Letivo Atual</label>
                            <select name="IdPeriodoLetivo"
                                ng-model="IdPeriodoLetivo"
                                ng-options="v.Id as v.Descricao for v in LstPeriodoLetivo" class="form-control"
                                ng-change="ChangePeriodoLetivo();">
                                <option value="">-- Selecione um Periodo Letivo --</option>
                            </select>
                        </div>

                        <div class="col-md-4">
                            <label>Períodos Letivos (à comparar) <i class="fa-exclamation-circle fa" aria-hidden="true"></i></label>
                            <tags-input ng-model="LstPeriodoLetivoComparar" display-property="Descricao"
                                add-on-comma="true" replace-spaces-with-dashes="false" placeholder="Adicionar um Período Letivo" enable-editing-last-tag="false"
                                max-tags="3">
                                            <auto-complete source="GetPeriodoLetivoSuggest($query)"></auto-complete>
                            </tags-input>
                        </div>
                        <div class="col-md-3" style="padding-top: 25px">
                            <a class="btn btn-info" ng-click="ProcessarDash()"><i class="fa fa-gear"></i> Processar</a>
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




                </div>
            </div>
        </div>
    </form>
</asp:Content>
