(function () {
    'use strict';

    // Defining angularjs Module
    var app = angular.module($ngSession.ModuleName)

    app.controller('GraficoAlunoAtivoController', GraficoAlunoAtivoController);

    GraficoAlunoAtivoController.$inject = ['$scope', 'growl', '$filter', '$cookies', 'graficoAlunoAtivoFactory', '$popover', 'segurancaFactory'];
    function GraficoAlunoAtivoController($scope, growl, $filter, $cookies, graficoAlunoAtivoFactory, $popover, segurancaFactory) {
        var vm = $scope;

        vm.realTimePeriodoLetivo = false;


        vm.ColorsChart = ColorsChart;
        var ColorsChart = [
            "#FF8E98",
            "#45CAEB",
            "#FFCE56",
            "#10B2A3",
            "#FF97E7",
            "#9B8EFF",
            "#43CF93",
            "#C0A0FF",
            "#5FFFB6",
            "#C26CA7",
            "#FFCAF2",
            "#A7C7C5",
            "#09504F",
            "#172A40",
            "#FFF7DC",
            "#D9383A"
        ];

        vm.ColorsChart2 = ColorsChart2;
        var ColorsChart2 = [
            "#FF7995",
            "#7CDBFF"
        ];


        var colors = {
            green: {
                fill: '#e0eadf',
                stroke: '#5eb84d',
            },
            lightBlue: {
                stroke: '#6fccdd',
            },
            darkBlue: {
                fill: '#92bed2',
                stroke: '#3282bf',
            },
            purple: {
                fill: '#8fa8c8',
                stroke: '#75539e',
            },
            black: {
                fill: '#1e1d1d33',
                stroke: '#333'
            }
        };

        GetCampusUsuario();
        vm.GetCampusUsuario = GetCampusUsuario;
        function GetCampusUsuario() {
            graficoAlunoAtivoFactory.GetCampusUsuario().success(function (data) {
                vm.LstCampusUsuario = data;
            }).error(function (data) {
                growl.error("Ocorreu uma falha ao carregar o Campus/Polo", { title: 'Atenção' });
            });
        }
        
        
        GetPeriodoLetivo();
        function GetPeriodoLetivo() {
            graficoAlunoAtivoFactory.GetPeriodoLetivo().success(function (data) {
                vm.LstPeriodoLetivo = data;
            }).error(function (data) {
                growl.error("Ocorreu uma falha ao carregar o Periodo Letivo", { title: 'Atenção' });
            });
        }

        vm.GetDadosPorPeriodoLetivo = GetDadosPorPeriodoLetivo;
        function GetDadosPorPeriodoLetivo() {

            vm.LstChartDadosPeriodoLetivoSigla = [];
            SetCookiesConsulta();
            LoadChartPeriodoLetivo();
            

            var idPeriodoletivo = (vm.IdPeriodoLetivo || 0),
                idCampus = (vm.IdCampus || 0);

            if (idPeriodoletivo < 1)
                return false;

            vm.loadChartPeriodoLetivo = true;
            vm.TotalAtual = 0;
            vm.TotalAnterior = 0;
            graficoAlunoAtivoFactory.GetDadosPorPeriodoLetivo(idCampus, idPeriodoletivo, true).success(function (data) {
                vm.loadChartPeriodoLetivo = false;
                vm.LstYName = [];               
                
                angular.forEach(data, function (value, key) {
                    vm.LstChartDadosPeriodoLetivoSigla.push(value.PeriodoLetivoSigla);
                    vm.LstYName.push({
                        name: value.PeriodoLetivoSigla,
                        y: value.Total,
                        total: value.Total,
                        key: value.IdPeriodoLetivo
                    });
                });

                LoadChartPeriodoLetivo();


            }).error(function (data) {
                vm.loadChartPeriodoLetivo = false;
                growl.error("Falha ao carregar o gráfico do Período Letivo", { title: 'Atenção' });
            });
        }

        //Quando seleciona o campo periodo letivo
        vm.ChangePeriodoLetivo = ChangePeriodoLetivo;
        function ChangePeriodoLetivo() {
            GetDadosPorPeriodoLetivo();
        }

        LoadChartPeriodoLetivo();

        //Function init chart PeriodoLetivo
        function LoadChartPeriodoLetivo() {            
            vm.chartPeriodoLetivo = Highcharts.chart('chartPeriodoLetivo', {
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    type: 'column',
                },
                credits:
                {
                    enabled: false
                },
                title:
                {
                    text: 'Total por Período Letivo'
                },
                lang:
                {
                    contextButtonTitle: "Menu de Formatos de arquivo",
                    downloadPDF: "<span class='fa fa-file-pdf'></span>&nbsp;Baixar arquivo PDF",
                    downloadJPEG: "<span class='fa fa-file-image'></span>&nbsp;Baixar arquivo JPEG",
                    downloadPNG: "<span class='fa fa-file-image'></span>&nbsp;Baixar arquivo PNG",
                    downloadSVG: "<span class='fa fa-file-image'></span>&nbsp;Baixar arquivo SVG",
                    printChart: "<span class='fa fa-print'></span>&nbsp;Imprimir Chart"
                },
                xAxis: {
                    categories: vm.LstChartDadosPeriodoLetivoSigla
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Total de Alunos por Perído Letivo'
                    },
                    stackLabels: {
                        enabled: true,
                        style: {
                            fontWeight: 'bold',
                            color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                        }
                    }
                },
                //legend: {
                //    align: 'right',
                //    x: -30,
                //    verticalAlign: 'top',
                //    y: 25,
                //    floating: true,
                //    backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white',
                //    borderColor: '#CCC',
                //    borderWidth: 1,
                //    shadow: true
                //},
                tooltip: {
                    headerFormat: '<b>{point.x}</b><br/>',
                    pointFormat: '{series.name}: {point.y}<br/>Total: {point.stackTotal}'
                },
                plotOptions: {
                    column: {
                        stacking: 'normal',
                        //dataLabels: {
                        //    enabled: true,
                        //    color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white'
                        //}
                    },
                    series: {
                        point: {
                            events: {
                                click: vm.ClickChartPeriodoLetivo
                            }
                        }
                    }
                },
                series: [{
                    name: 'Total',
                    colorByPoint: true,
                    data: vm.LstYName
                }]
            });
        }
        //default
        vm.timeRefresh = 10;

        //var timeout = setInterval(RefreshAll, vm.timeRefresh * 1000);

        vm.changeTime = function () {
            console.log(timeout);
            console.log(vm.timeRefresh);
            clearTimeout(timeout);
            timeout = setInterval(RefreshAll, vm.timeRefresh * 1000);
        }

        //Colocar a tela em fullscreen
        vm.SetFullScreen = function () {
            vm.IsFullscreen = true;
        }

        //esconder filtros
        vm.SetHideFilter = function () {
            if (!vm.HideFilter)
                vm.HideFilter = true;
            else vm.HideFilter = false;
        }

        //Salva no cookie a consulta
        function SetCookiesConsulta() {
            var idPeriodoLetivo = vm.IdPeriodoLetivo,
                idCampus = vm.IdCampus;

            $cookies.put('GraficoAlunoAtivoIdPeriodoLetivo', idPeriodoLetivo),
            $cookies.put('GraficoAlunoAtivoIdCampus', idCampus);
        }
    }
})();