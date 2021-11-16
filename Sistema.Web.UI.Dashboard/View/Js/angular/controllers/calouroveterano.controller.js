(function () {
    'use strict';

    // Defining angularjs Module
    var app = angular.module($ngSession.ModuleName)


    app.controller('CalouroVeteranoController', CalouroVeteranoController);

    CalouroVeteranoController.$inject = ['$scope', 'growl', '$filter', '$cookies', 'calouroVeteranoFactory', '$popover', 'segurancaFactory'];

    function CalouroVeteranoController($scope, growl, $filter, $cookies, calouroVeteranoFactory, $popover, segurancaFactory) {
        var vm = $scope;

        $scope.popover = {
            title: "Outros tipo acesso IES",
            content: ""
        };

        //Veriaveis relatime
        vm.realTimeGPA = false;
        vm.realTimeCurso = false;
        vm.realTimeTipoAcessoIES = false;
        vm.realTimeTurma = false;


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


        var MONTHS = ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"];

        /*Carrega compos filtro*/
        //GetCampus
        vm.GetCampusUsuario = GetCampusUsuario;
        function GetCampusUsuario() {
            calouroVeteranoFactory.GetCampusUsuario().success(function (data) {
                vm.LstCampusUsuario = data;
            }).error(function (data) {
                growl.error("Falha ao carregar o campo Campus", { title: 'Atenção' });
            });
        }
        GetCampusUsuario();

        //Consulta Periodo Letivo e Carrega o combo
        function GetPeriodoLetivo() {
            calouroVeteranoFactory.GetPeriodoLetivo().success(function (data) {
                vm.LstPeriodoLetivo = data;
            }).error(function (data) {
                growl.error("Falha ao carregar o campo Periodo Letivo", { title: 'Atenção' });
            });
        }

        vm.GetPeriodoLetivoSuggest = GetPeriodoLetivoSuggest;
        function GetPeriodoLetivoSuggest(strConsulta) {
            if (strConsulta !== undefined && strConsulta !== '' && strConsulta.length >= 3) {
                return calouroVeteranoFactory.GetPeriodoLetivo(0, strConsulta, vm.IdPeriodoLetivo);
            }
        };


        //Controle de Cookies de Consultas
        function GetCookieConsulta() {
            var idPeriodoLetivo = parseInt($cookies.get('MatriculaRematriculaIdPeriodoLetivo'), 10),
                idCampus = parseInt($cookies.get('MatriculaRematriculaIdCampus'), 10);

            vm.IdPeriodoLetivo = idPeriodoLetivo;
            vm.IdCampus = idCampus;

         


        }

        //Salva no cookie a consulta
        function SetCookiesConsulta() {
            var idPeriodoLetivo = vm.IdPeriodoLetivo,
                idCampus = vm.IdCampus;

            $cookies.put('MatriculaRematriculaIdPeriodoLetivo', idPeriodoLetivo),
            $cookies.put('MatriculaRematriculaIdCampus', idCampus);
        }


        /*Carrega compos filtro*/

        //Consultar no Load
        GetPeriodoLetivo();
        GetCookieConsulta();


        //Quando seleciona o campo periodo letivo
        vm.ChangePeriodoLetivo = ChangePeriodoLetivo;
        function ChangePeriodoLetivo() {
            delete vm.IdGpaSelecionado;
            delete vm.IdCursoSelecionado;
            CloseListaTurmas();
            GetDadosAreaConhecimento();
            GetDadosResumoPorPeriodo();
        }

        //Quando seleciona o campo campus
        vm.ChangeCampus = ChangeCampus;
        function ChangeCampus() {
            GetDadosAreaConhecimento();
            CloseListaTurmas();
            delete vm.IdGpaSelecionado;
            delete vm.IdCursoSelecionado;
        }



        /*Charts Área de Conhecimento*/
        vm.GetDadosAreaConhecimento = GetDadosAreaConhecimento;
        function GetDadosAreaConhecimento() {

            SetCookiesConsulta();
            InitChartGpa();


            var idPeriodoletivo = (vm.IdPeriodoLetivo || 0),
                idCampus = (vm.IdCampus || 0);

            if (idPeriodoletivo < 1)
                return false;

            vm.loadChartGpa = true;
            vm.TotalGeralGpa = 0;
            vm.TotalMatriculaGpa = 0;
            vm.TotalRematriculaGpa = 0;
            calouroVeteranoFactory.GetDadosAreaConhecimento(idPeriodoletivo, idCampus, true).success(function (data) {
                vm.loadChartGpa = false;

                angular.forEach(data, function (value, key) {

                    vm.DadosChartsGpa.labels.push(value.Sigla + ' (' + value.Total + ')');

                    vm.DadosChartsGpa.datasets[0].data.push(value.Total);

                    vm.DadosChartsGpa.datasets[0].dataId.push(value.IdGPA);

                    vm.DadosChartsGpa.datasets[0].label.push(value.NomeGPA);
                    vm.DadosChartsGpa.datasets[0].backgroundColor.push(ColorsChart[key]);


                    vm.DadosChartsGpa.datasets[0].TotalMatricula = value.TotalMatricula;
                    vm.DadosChartsGpa.datasets[0].TotalRematricula = value.TotalRematricula;


                    vm.TotalGeralGpa += value.Total;
                    vm.TotalMatriculaGpa += value.TotalMatricula;
                    vm.TotalRematriculaGpa += value.TotalRematricula;

                });

            }).error(function (data) {
                vm.loadChartGpa = false;
                growl.error("Falha ao carregar o gráfico do Gpa", { title: 'Atenção' });
            });
        }

        //Init all Charts and Configs
        InitChartGpa();
        //InitChartCurso();

        //Function init chart GPA
        function InitChartGpa() {

            //Datasets
            vm.DadosChartsGpa = {
                labels: [],
                datasets: [{
                    data: [],
                    dataId: [],
                    label: [],
                    backgroundColor: [],
                    hoverBackgroundColor: []
                },
                ]
            };

            //Config
            vm.OptionsChartGpa = {
                legend: {
                    display: true,
                    position: "left",
                    labels: {
                        fontColor: 'rgb(255, 99, 132)',
                        padding: 20
                    }
                },
                title: {
                    display: true,
                    text: 'Quantitativo de Alunos'
                },
                tooltips: {
                    callbacks: {
                        label: function (tooltipItem, data) {

                            var index = tooltipItem.index;
                            var datasetIndex = tooltipItem.datasetIndex;
                            var dataset = data.datasets[datasetIndex];

                            return (dataset.label[index] + ': ' + dataset.data[index]);
                        }
                    }
                }
            };
        }
        vm.ClickChartGpa = function (event) {


            if (event.element[0] == undefined)
                return false;

            var index = event.element[0]._index;
            var element = event.element[0];


            var dataset = element._chart.config.data.datasets[0];

            var dataChart = dataset.data[index];
            var IdGpa = dataset.dataId[index];
            var totalMatricula = dataset.TotalMatricula;
            var totalRematricula = dataset.TotalRematricula;

            var label = dataset.label[index];
            var color = dataset.backgroundColor[index];


            vm.Gpa = {
                Nome: label,
                Color: color,
                TotalMatricula: totalMatricula,
                TotalRematricula: totalRematricula
            };

            var idPeriodoletivo = (vm.IdPeriodoLetivo || 0),
              idCampus = (vm.IdCampus || 0);

            GetDadosCursos(idCampus, IdGpa, idPeriodoletivo);
            GetDadosTipoAcessoIES(idCampus, IdGpa, idPeriodoletivo);
            CloseListaTurmas();

            delete vm.IdCursoSelecionado;


        };




        //default
        vm.timeRefresh = 10;
        var timeout = setInterval(RefreshAll, vm.timeRefresh * 1000);

        vm.changeTime = function () {
            console.log(timeout);
            console.log(vm.timeRefresh);
            clearTimeout(timeout);
            timeout = setInterval(RefreshAll, vm.timeRefresh * 1000);
        }


        //Function de atualizar os charts conforme o intervalo configurado
        function RefreshAll() {
            if (vm.realTimeGPA) {
                GetDadosAreaConhecimento();
            }
            if (vm.realTimeTipoAcessoIES) {
                RefreshChartTipoAcessoIES();
            }
            if (vm.realTimeCurso) {
                RefreshChartCurso();
            }
            if (vm.realTimeTurma) {
                RefreshChartTurma();
            }

            if (vm.realTimeResumoPorPeriodo) {
                RefreshChartResumoPorPeriodo();
            }

        }

        //esconder filtros
        vm.SetHideFilter = function () {
            if (!vm.HideFilter)
                vm.HideFilter = true;
            else vm.HideFilter = false;
        }
    }



})();

