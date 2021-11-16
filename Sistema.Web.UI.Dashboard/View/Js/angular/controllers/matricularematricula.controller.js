(function () {
    'use strict';

    // Defining angularjs Module
    var app = angular.module($ngSession.ModuleName)


    app.controller('MatriculaRematriculaController', MatriculaRematriculaController);

    MatriculaRematriculaController.$inject = ['$scope', 'growl', '$filter', '$cookies', 'matriculaRematriculaFactory', '$popover', 'segurancaFactory'];

    function MatriculaRematriculaController($scope, growl, $filter, $cookies, matriculaRematriculaFactory, $popover, segurancaFactory) {
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

        vm.CloseListaTurmas = CloseListaTurmas;

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

        /*Carrega compos filtro*/
        //GetCampus
        vm.GetCampusUsuario = GetCampusUsuario;
        function GetCampusUsuario() {
            matriculaRematriculaFactory.GetCampusUsuario().success(function (data) {
                vm.LstCampusUsuario = data;
            }).error(function (data) {
                growl.error("Falha ao carregar o campo Campus", { title: 'Atenção' });
            });
        }
        GetCampusUsuario();

        //Consulta Periodo Letivo e Carrega o combo
        function GetPeriodoLetivo() {
            matriculaRematriculaFactory.GetPeriodoLetivo().success(function (data) {
                vm.LstPeriodoLetivo = data;
            }).error(function (data) {
                growl.error("Falha ao carregar o campo Periodo Letivo", { title: 'Atenção' });
            });
        }

        //Controle de Cookies de Consultas
        function GetCookieConsulta() {
            var idPeriodoLetivo = parseInt($cookies.get('MatriculaRematriculaIdPeriodoLetivo'), 10),
                idCampus = parseInt($cookies.get('MatriculaRematriculaIdCampus'), 10);

            vm.IdPeriodoLetivo = idPeriodoLetivo;
            vm.IdCampus = idCampus;

            GetDadosAreaConhecimento();
            GetDadosResumoPorPeriodo();


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
            LoadChartGpa();


            var idPeriodoletivo = (vm.IdPeriodoLetivo || 0),
                idCampus = (vm.IdCampus || 0);

            if (idPeriodoletivo < 1)
                return false;

            vm.loadChartGpa = true;
            vm.TotalGeralGpa = 0;
            vm.TotalMatriculaGpa = 0;
            vm.TotalRematriculaGpa = 0;
            matriculaRematriculaFactory.GetDadosAreaConhecimento(idPeriodoletivo, idCampus, true).success(function (data) {
                vm.loadChartGpa = false;
                vm.LstYName = [];
                angular.forEach(data, function (value, key) {

                    //vm.DadosChartsGpa.labels.push(value.Sigla + ' (' + value.Total + ')');

                    //vm.DadosChartsGpa.datasets[0].data.push(value.Total);

                    //vm.DadosChartsGpa.datasets[0].dataId.push(value.IdGPA);

                    //vm.DadosChartsGpa.datasets[0].label.push(value.NomeGPA);
                    //vm.DadosChartsGpa.datasets[0].backgroundColor.push(ColorsChart[key]);


                    //vm.DadosChartsGpa.datasets[0].TotalMatricula = value.TotalMatricula;
                    //vm.DadosChartsGpa.datasets[0].TotalRematricula = value.TotalRematricula;


                    vm.TotalGeralGpa += value.Total;
                    vm.TotalMatriculaGpa += value.TotalMatricula;
                    vm.TotalRematriculaGpa += value.TotalRematricula;


                    vm.LstYName.push({
                        name: value.NomeGPA,
                        y: value.Total,
                        total: value.Total,
                        key: value.IdGPA,
                        TotalMatricula: value.TotalMatricula,
                        TotalRematricula: value.TotalRematricula
                    });

                });

                LoadChartGpa();


            }).error(function (data) {
                vm.loadChartGpa = false;
                growl.error("Falha ao carregar o gráfico do Gpa", { title: 'Atenção' });
            });
        }

        //Init all Charts and Configs
        LoadChartGpa();
        //InitChartCurso();

        //Function init chart GPA
        function LoadChartGpa() {

            vm.chartGpa = Highcharts.chart('chartGpa', {
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    type: 'pie',
                    //events: {
                    //    click: function (event) {
                    //        console.log(event);
                    //    }
                    //}
                },
                credits: {
                    enabled: false
                },
                title: {
                    text: 'Quantitativo por Área de Conhecimento '
                },
                lang: {
                    contextButtonTitle: "Menu de Formatos de arquivo",
                    downloadPDF: "<span class='fa fa-file-pdf'></span>&nbsp;Baixar arquivo PDF",
                    downloadJPEG: "<span class='fa fa-file-image'></span>&nbsp;Baixar arquivo JPEG",
                    downloadPNG: "<span class='fa fa-file-image'></span>&nbsp;Baixar arquivo PNG",
                    downloadSVG: "<span class='fa fa-file-image'></span>&nbsp;Baixar arquivo SVG",
                    printChart: "<span class='fa fa-print'></span>&nbsp;Imprimir Chart"
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.y} ({point.percentage:.1f}%)</b>'
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                            style: {
                                color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                            }
                        },
                    },
                    series: {
                        point: {
                            events: {
                                click: vm.ClickChartGpa
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


            ////Datasets
            //vm.DadosChartsGpa = {
            //    labels: [],
            //    datasets: [{
            //        data: [],
            //        dataId: [],
            //        label: [],
            //        backgroundColor: [],
            //        hoverBackgroundColor: []
            //    },
            //    ]
            //};

            ////Config
            //vm.OptionsChartGpa = {
            //    legend: {
            //        display: true,
            //        position: "left",
            //        labels: {
            //            fontColor: 'rgb(255, 99, 132)',
            //            padding: 20
            //        }
            //    },
            //    title: {
            //        display: true,
            //        text: 'Quantitativo de Alunos'
            //    },
            //    tooltips: {
            //        callbacks: {
            //            label: function (tooltipItem, data) {

            //                var index = tooltipItem.index;
            //                var datasetIndex = tooltipItem.datasetIndex;
            //                var dataset = data.datasets[datasetIndex];

            //                return (dataset.label[index] + ': ' + dataset.data[index]);
            //            }
            //        }
            //    }
            //};
        }


        vm.ClickChartGpa = function () {


            var obj = this;

            //if (event.element[0] == undefined)
            //    return false;
            //var index = event.element[0]._index;
            //var element = event.element[0];
            //var dataset = element._chart.config.data.datasets[0];
            //var dataChart = dataset.data[index];


            var IdGpa = obj.options.key;
            var totalMatricula = obj.options.TotalMatricula;
            var totalRematricula = obj.options.TotalRematricula;

            var label = obj.options.name;//dataset.label[index];
            var color = '';//dataset.backgroundColor[index];


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
            delete vm.TipoAcessoIes;
            delete vm.IdTipoAcessoIESUnificado;


        };


        /*Chart Área de Conhecimento*/

        /*Chart Curso*/
        vm.GetDadosCursos = GetDadosCursos;
        vm.IdGpaSelecionado = 0;
        vm.TotalGeralCursos = 0;
        vm.TotalMatriculaCursos = 0;
        vm.TotalRematriculaCursos = 0;
        function GetDadosCursos(idCampus, idGpa, idPeriodoLetivo) {
            vm.IdGpaSelecionado = idGpa;
            vm.LstChartDadosCursos = [];
            vm.LstChartDescricaoDadosCursos = [];
            if (idGpa < 0)
                return false;
            vm.loadChartCurso = true;
            matriculaRematriculaFactory.GetDadosCursos(idCampus, idGpa, idPeriodoLetivo, true, vm.IdTipoAcessoIESUnificado).success(function (data) {


                vm.LstChartDadosCursos[0] = { name: 'Matrícula', data: [] };
                vm.LstChartDadosCursos[1] = { name: 'Rematrícula', data: [] };
                //vm.DadosChartsCurso.datasets[0].label.push("Matrícula");
                //vm.DadosChartsCurso.datasets[1].label.push("Rematrícula");

                angular.forEach(data, function (value, key) {


                    vm.loadChartCurso = false;
                    //vm.DadosChartsCurso.labels.push(value.CursoNome);


                    //vm.DadosChartsCurso.datasets[0].data.push(value.TotalMatricula);
                    //vm.DadosChartsCurso.datasets[0].dataId.push(value.IdCurso);
                    //vm.DadosChartsCurso.datasets[0].backgroundColor.push(ColorsChart2[0]);


                    //vm.DadosChartsCurso.datasets[1].data.push(value.TotalRematricula);
                    //vm.DadosChartsCurso.datasets[1].dataId.push(value.IdCurso);
                    //vm.DadosChartsCurso.datasets[1].backgroundColor.push(ColorsChart2[1]);



                    vm.TotalGeralCursos += value.Total;
                    vm.TotalMatriculaCursos += value.TotalMatricula;
                    vm.TotalRematriculaCursos += value.TotalRematricula;

                    //vm.LstChartDadosCursos[0].name = value.CursoNome;
                    vm.LstChartDadosCursos[0].data.push({
                        y: value.TotalMatricula,
                        key: value.IdCurso
                    });

                    //vm.LstChartDadosCursos[1].name = value.CursoNome;
                    vm.LstChartDadosCursos[1].data.push({
                        y: value.TotalRematricula,
                        key: value.IdCurso
                    });

                    vm.LstChartDescricaoDadosCursos.push(value.CursoNome);

                    //[{
                    //    name: 'Year 1800',
                    //    data: [107, 31, 635, 203, 2]
                    //}, {
                    //    name: 'Year 1900',
                    //    data: [133, 156, 947, 408, 6]
                    //}, {
                    //    name: 'Year 2012',
                    //    data: [1052, 954, 4250, 740, 38]
                    //}]

                });

                LoadChartCurso();

            }).error(function (data) {
                vm.loadChartCurso = false;
                growl.error("Falha ao carregar o gráfico de curso", { title: 'Atenção' });
            });

        }

        //Function init chart Cursos
        function LoadChartCurso() {
            //vm.TotalGeralCursos = 0;
            //vm.TotalMatriculaCursos = 0;
            //vm.TotalRematriculaCursos = 0;


            Highcharts.chart('chartCursos', {
                chart: {
                    type: 'bar'
                },
                title: {
                    text: 'Quantidade de Alunos por Cursos'
                },
                lang: {
                    contextButtonTitle: "Menu de Formatos de arquivo",
                    downloadPDF: "<span class='fa fa-file-pdf'></span>&nbsp;Baixar arquivo PDF",
                    downloadJPEG: "<span class='fa fa-file-image'></span>&nbsp;Baixar arquivo JPEG",
                    downloadPNG: "<span class='fa fa-file-image'></span>&nbsp;Baixar arquivo PNG",
                    downloadSVG: "<span class='fa fa-file-image'></span>&nbsp;Baixar arquivo SVG",
                    printChart: "<span class='fa fa-print'></span>&nbsp;Imprimir Chart"
                },
                subtitle: {
                    text: (vm.Gpa.Nome) + (vm.TipoAcessoIes ? ' - ' + vm.TipoAcessoIes.Nome : '')
                },
                xAxis: {
                    categories: vm.LstChartDescricaoDadosCursos,
                    title: {
                        text: null
                    }
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: '',
                        align: 'high'
                    },
                    labels: {
                        overflow: 'justify'
                    }
                },
                //tooltip: {
                //    valueSuffix: ' millions'
                //},
                plotOptions: {
                    bar: {
                        dataLabels: {
                            enabled: true
                        }
                    },
                    series: {
                        point: {
                            events: {
                                click: vm.ClickChartCurso
                            }
                        }
                    }
                },
                legend: {
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'top',
                    x: -40,
                    y: 80,
                    floating: true,
                    borderWidth: 1,
                    backgroundColor: ((Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'),
                    shadow: true
                },
                credits: {
                    enabled: false
                },
                series: vm.LstChartDadosCursos
            });


            ////Datasets
            //vm.DadosChartsCurso = {
            //    labels: [],
            //    datasets: [
            //      {
            //          data: [],
            //          dataId: [],
            //          label: [],
            //          backgroundColor: [],
            //          hoverBackgroundColor: []
            //      }, {
            //          data: [],
            //          dataId: [],
            //          label: [],
            //          backgroundColor: [],
            //          hoverBackgroundColor: []
            //      }
            //    ]
            //};

            ////Config
            //vm.OptionsChartCurso = {
            //    legend: {
            //        display: true,
            //        position: "top",
            //        labels: {
            //            //fontColor: 'rgb(255, 99, 132)',
            //            padding: 30
            //        }
            //    },
            //    title: {
            //        display: true,
            //        text: 'Quantitativo de Alunos por Cursos'
            //    },
            //    scales: {
            //        xAxes: [{
            //            stacked: true
            //        }],
            //        yAxes: [{
            //            stacked: true
            //        }]
            //    }
            //};
        }

        vm.RefreshChartCurso = RefreshChartCurso;
        function RefreshChartCurso() {

            if (vm.IdGpaSelecionado > 0 && vm.IdPeriodoLetivo > 0 && vm.IdCampus > 0) {
                GetDadosCursos(vm.IdCampus, vm.IdGpaSelecionado, vm.IdPeriodoLetivo)
            } else {
                delete vm.IdGpaSelecionado;
            }
        }


        vm.ClickChartCurso = function () {

            console.log(this);

            var obj = this;

            //if (event.element[0] == undefined)
            //    return false;

            //var index = event.element[0]._index;
            //var element = event.element[0];


            //var dataset = element._chart.config.data.datasets[0];

            //var dataChart = dataset.data[index];
            var IdCurso = obj.options.key;
            var totalMatricula = obj.options.TotalMatricula;
            var totalRematricula = obj.options.TotalRematricula;

            var label = obj.options.name; //dataset.label[index];
            var color = '';//dataset.backgroundColor[index];


            vm.Curso = {
                Nome: label,
                Color: color,
                TotalMatricula: totalMatricula,
                TotalRematricula: totalRematricula
            };

            vm.IdCursoSelecionado = IdCurso;

            var idPeriodoletivo = (vm.IdPeriodoLetivo || 0),
                idCampus = (vm.IdCampus || 0);

            GetDadosCursoTurma(IdCurso, idPeriodoletivo);


        };
        /*Chart Curso*/

        /*Chart Tipo Acesso IES*/
        vm.RefreshChartTipoAcessoIES = RefreshChartTipoAcessoIES;
        function RefreshChartTipoAcessoIES() {
            if (vm.IdGpaSelecionado > 0 && vm.IdPeriodoLetivo > 0 && vm.IdCampus) {
                GetDadosTipoAcessoIES(vm.IdCampus, vm.IdGpaSelecionado, vm.IdPeriodoLetivo)
            } else {
                delete vm.IdGpaSelecionado;
            }
        }
        function LoadChartTipoAcessoIES() {


            vm.chartGpa = Highcharts.chart('chartTipoAcessoIes', {
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    type: 'pie',
                    events: {
                        click: function (event) {
                            console.log(event);
                        }
                    }
                },
                credits: {
                    enabled: false
                },
                title: {
                    text: 'Quantitativo de Alunos por Tipo de Acesso IES '
                },
                lang: {
                    contextButtonTitle: "Menu de Formatos de arquivo",
                    downloadPDF: "<span class='fa fa-file-pdf'></span>&nbsp;Baixar arquivo PDF",
                    downloadJPEG: "<span class='fa fa-file-image'></span>&nbsp;Baixar arquivo JPEG",
                    downloadPNG: "<span class='fa fa-file-image'></span>&nbsp;Baixar arquivo PNG",
                    downloadSVG: "<span class='fa fa-file-image'></span>&nbsp;Baixar arquivo SVG",
                    printChart: "<span class='fa fa-print'></span>&nbsp;Imprimir Chart"
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.y} ({point.percentage:.1f}%)</b>'
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                            style: {
                                color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                            }
                        },
                    },
                    series: {
                        point: {
                            events: {
                                click: vm.ClickChartTipoAcessoIES
                            }
                        }
                    }
                },
                series: [{
                    name: 'Total',
                    colorByPoint: true,
                    data: vm.LstChartTipoAcessoIES
                }]
            });

            ////Datasets
            //vm.DadosChartsTipoAcessoIES = {
            //    labels: [],
            //    datasets: [
            //      {
            //          data: [],
            //          dataId: [],
            //          label: [],
            //          backgroundColor: [],
            //          hoverBackgroundColor: []
            //      }
            //    ]
            //};

            ////Config
            //vm.OptionsChartTipoAcessoIES = {

            //    legend: {
            //        display: true,
            //        position: "left",
            //        labels: {
            //            fontColor: 'rgb(255, 99, 132)',
            //            padding: 5,
            //            fontSize: 12
            //        }
            //    },
            //    title: {
            //        display: true,
            //        text: 'Quantitativo de Alunos por Tipo de Acesso IES'
            //    },
            //};
        }
        function GetDadosTipoAcessoIES(idCampus, idGpa, idPeriodoLetivo) {
            vm.IdGpaSelecionado = idGpa;

            vm.LstChartTipoAcessoIES = [];

            if (idGpa < 0)
                return false;
            vm.loadChartTipoAcessoIES = true;

            vm.TotalGeralTipoAcesso = 0;
            vm.TotalMatriculaTipoAcesso = 0;
            vm.TotalRematriculaTipoAcesso = 0;



            matriculaRematriculaFactory.GetDadosTipoAcessoIES(idCampus, idGpa, idPeriodoLetivo).success(function (data) {

                vm.LstTipoAcessoIES = data;
                ListarOutrosTiposAcessoIES();
                angular.forEach(data, function (value, key) {


                    vm.loadChartTipoAcessoIES = false;
                    //vm.DadosChartsTipoAcessoIES.labels.push(value.NomeTipoAcessoIESUnificado);

                    //vm.DadosChartsTipoAcessoIES.datasets[0].data.push(value.Total);
                    //vm.DadosChartsTipoAcessoIES.datasets[0].dataId.push(value.IdTipoAcessoIESUnificado);


                    //vm.DadosChartsTipoAcessoIES.datasets[0].backgroundColor.push(ColorsChart[key]);

                    vm.TotalGeralTipoAcesso += value.Total;
                    vm.TotalMatriculaTipoAcesso += value.TotalMatricula;
                    vm.TotalRematriculaTipoAcesso += value.TotalRematricula;


                    vm.LstChartTipoAcessoIES.push({
                        name: value.NomeTipoAcessoIESUnificado,
                        y: value.Total,
                        key: value.IdTipoAcessoIESUnificado,
                        TotalMatricula: value.TotalMatricula,
                        TotalRematricula: value.TotalRematricula
                    });


                });

                LoadChartTipoAcessoIES();

            }).error(function (data) {
                vm.loadChartTipoAcessoIES = false;
                growl.error("Falha ao carregar o gráfico de Tipo de Acesso IES", { title: 'Atenção' });
            });

        }

        vm.ListarOutrosTiposAcessoIES = ListarOutrosTiposAcessoIES;
        function ListarOutrosTiposAcessoIES() {
            matriculaRematriculaFactory.GetAllTipoAcessoIES().success(function (data) {


                var lst = [];

                angular.forEach(data, function (value, key) {
                    var encontrou = false;
                    angular.forEach(vm.LstTipoAcessoIES, function (v, k) {
                        if (parseInt(v.IdTipoAcessoIes, 10) == value.Id) {
                            encontrou = true;
                        }
                    });

                    if (!encontrou) {
                        lst.push(value);
                    }
                });


                angular.forEach(lst, function (value, key) {
                    $scope.popover.content += value.Nome;
                });

                vm.LstOutrosTipoAcessoIES = lst;

            }).error(function (data) {
            })
        }

        vm.ClickChartTipoAcessoIES = function () {

            var obj = this;

            //if (event.element[0] == undefined)
            //    return false;

            //var index = event.element[0]._index;
            //var element = event.element[0];
            //var dataset = element._chart.config.data.datasets[0];

            //var dataChart = dataset.data[index];
            var IdTipoAcessoIESUnificado = obj.options.key; //dataset.dataId[index];

            var label = obj.options.name;  //vm.DadosChartsTipoAcessoIES.labels[index]; //dataset.label[index];
            var color = '';//dataset.backgroundColor[index];


            vm.TipoAcessoIes = {
                Nome: label,
                Color: color
            };

            console.log(vm.TipoAcessoIes);

            var idPeriodoletivo = (vm.IdPeriodoLetivo || 0),
                idCampus = (vm.IdCampus || 0);


            vm.IdTipoAcessoIESUnificado = IdTipoAcessoIESUnificado;

            GetDadosCursos(idCampus, vm.IdGpaSelecionado, idPeriodoletivo);

            //GetDadosTipoAcessoIES(idCampus, IdGpa, idPeriodoletivo);

            CloseListaTurmas();
            delete vm.IdCursoSelecionado;


        };
        /*Chart Tipo Acesso IES*/

        /*Chart turmas*/
        vm.RefreshChartTurma = RefreshChartTurma;
        function RefreshChartTurma() {
            if (vm.IdCursoSelecionado > 0 && vm.IdPeriodoLetivo > 0) {
                GetDadosCursoTurma(vm.IdCursoSelecionado, vm.IdPeriodoLetivo);
            } else {
                delete vm.IdCursoSelecionado;
            }

        }
        function LoadChartCursoTurma() {

            //vm.TotalGeralCursoTurma = 0;
            //vm.TotalMatriculaCursoTurma = 0;
            //vm.TotalRematriculaCursoTurma = 0;


            Highcharts.chart('chartCursoTurma', {
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Quantitativo de Alunos por Turma'
                },
                credits: {
                    enabled: false
                },
                lang: {
                    contextButtonTitle: "Menu de Formatos de arquivo",
                    downloadPDF: "<span class='fa fa-file-pdf'></span>&nbsp;Baixar arquivo PDF",
                    downloadJPEG: "<span class='fa fa-file-image'></span>&nbsp;Baixar arquivo JPEG",
                    downloadPNG: "<span class='fa fa-file-image'></span>&nbsp;Baixar arquivo PNG",
                    downloadSVG: "<span class='fa fa-file-image'></span>&nbsp;Baixar arquivo SVG",
                    printChart: "<span class='fa fa-print'></span>&nbsp;Imprimir Chart"
                },
                xAxis: {
                    categories: vm.LstChartDadosDescricaoTurma
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Número matrículas e rematrículas'
                    },
                    stackLabels: {
                        enabled: true,
                        style: {
                            fontWeight: 'bold',
                            color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                        }
                    }
                },
                legend: {
                    align: 'right',
                    x: -30,
                    verticalAlign: 'top',
                    y: 25,
                    floating: true,
                    backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white',
                    borderColor: '#CCC',
                    borderWidth: 1,
                    shadow: false
                },
                tooltip: {
                    headerFormat: '<b>{point.x}</b><br/>',
                    pointFormat: '{series.name}: {point.y}<br/>Total: {point.stackTotal}'
                },
                plotOptions: {
                    column: {
                        stacking: 'normal',
                        dataLabels: {
                            enabled: true,
                            color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white'
                        }
                    },
                    series: {
                        point: {
                            events: {
                                click: vm.ClickChartCursoTurmas
                            }
                        }
                    }
                    //series: {
                    //    events:{
                    //        click: vm.ClickChartCursoTurmas
                    //    }
                    //}
                },
                series: vm.LstChartDadosTurma
            });

        }
        vm.GetDadosCursoTurma = GetDadosCursoTurma;
        vm.TotalGeralCursoTurma = 0;
        vm.TotalMatriculaCursoTurma = 0;
        vm.TotalRematriculaCursoTurma = 0;
        function GetDadosCursoTurma(idCurso, idPeriodoLetivo) {
            vm.IdCursoSelecionado = idCurso;
            vm.CloseListaTurmas();

            vm.LstChartDadosDescricaoTurma = [];
            vm.LstChartDadosTurma = [];

            if (idCurso < 0)
                return false;
            vm.loadChartCursoTurma = true;
            matriculaRematriculaFactory.GetDadosCursoTurma(idCurso, idPeriodoLetivo, true, vm.IdTipoAcessoIESUnificado).success(function (data) {


                //vm.DadosChartsCursoTurma.datasets[0].label.push("Matrículas");
                //vm.DadosChartsCursoTurma.datasets[1].label.push("Rematrículas");

                //if (data.length <= 10) {
                //    vm.OptionsChartCursoTurma.scales.xAxes[0].ticks.fontSize = 10;
                //}


                vm.LstChartDadosTurma[0] = { name: 'Matrícula', data: [], dataId: [] };
                vm.LstChartDadosTurma[1] = { name: 'Rematrículas', data: [], dataId: [] };
                angular.forEach(data, function (value, key) {


                    vm.loadChartCursoTurma = false;

                    vm.LstChartDadosDescricaoTurma.push(value.TurmaSigla);

                    vm.LstChartDadosTurma[0].data.push({
                        y: value.TotalMatricula,
                        TotalMatricula: value.TotalMatricula,
                        TotalRematricula: value.TotalRematricula,
                        key: value.IdGradeLetivaTurma
                    })
                    vm.LstChartDadosTurma[1].data.push({
                        y: value.TotalRematricula,
                        TotalMatricula: value.TotalMatricula,
                        TotalRematricula: value.TotalRematricula,
                        key: value.IdGradeLetivaTurma
                    })


                    vm.TotalGeralCursoTurma += value.Total;
                    vm.TotalMatriculaCursoTurma += value.TotalMatricula;
                    vm.TotalRematriculaCursoTurma += value.TotalRematricula;

                });


                LoadChartCursoTurma();

            }).error(function (data) {
                vm.loadChartCursoTurma = false;
                growl.error("Falha ao carregar o gráfico de Turmas", { title: 'Atenção' });
            });

        };

        vm.IdGradeLetivaTurma = 0;

        vm.ClickChartCursoTurmas = function () {

            var obj = this;
            console.log(obj);



            var label = obj.category;
            var color = '';


            //Dados Rematricula
            //var datasetRematricula = element._chart.config.data.datasets[1];
            //var totalRematricula = datasetRematricula.data[index];
            var id = obj.key;

            //var label = element._chart.config.data.labels[index];
            //var color = datasetRematricula.backgroundColor[index];

            CloseListaTurmas();



            var idPeriodoletivo = (vm.IdPeriodoLetivo || 0),
                idCampus = (vm.IdCampus || 0);

            vm.IdGradeLetivaTurma = id;
            vm.Turma = {
                Nome: label,
                TotalMatricula: obj.TotalMatricula,
                TotalRematricula: obj.TotalRematricula
            };

            //vm.$apply()


            GetAlunosTurma(idCampus, idPeriodoletivo, id);

        };
        /*Chart turmas*/


        /*INICIO - Charts Resumo de Matricula e rematricula por periodo*/
        LoadChartResumoPorPeriodo();
        function LoadChartResumoPorPeriodo() {

            Highcharts.chart('chartResumoPeriodo', {

                title: {
                    text: 'Resumo de Matrículas e Rematrículas nos períodos '
                },
                credits: {
                    enabled: false
                },
                //subtitle: {
                //    text: 'Source: thesolarfoundation.com'
                //},
                lang: {
                    contextButtonTitle: "Menu de Formatos de arquivo",
                    downloadPDF: "<span class='fa fa-file-pdf'></span>&nbsp;Baixar arquivo PDF",
                    downloadJPEG: "<span class='fa fa-file-image'></span>&nbsp;Baixar arquivo JPEG",
                    downloadPNG: "<span class='fa fa-file-image'></span>&nbsp;Baixar arquivo PNG",
                    downloadSVG: "<span class='fa fa-file-image'></span>&nbsp;Baixar arquivo SVG",
                    printChart: "<span class='fa fa-print'></span>&nbsp;Imprimir Chart"
                },
                yAxis: {
                    title: {
                        text: 'Número de Matrículas e Rematrículas'
                    }
                },
                xAxis: {
                    categories: vm.LstMesResumoPeriodo
                },
                legend: {
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'middle'
                },

                plotOptions: {
                    series: {
                        label: {
                            connectorAllowed: false
                        }
                    }
                },
                series: vm.LstSeriesResumo,


                responsive: {
                    rules: [{
                        condition: {
                            maxWidth: 500
                        },
                        chartOptions: {
                            legend: {
                                layout: 'horizontal',
                                align: 'center',
                                verticalAlign: 'bottom'
                            }
                        }
                    }]
                }

            });


            ////Datasets
            //vm.DadosCharResumoPeriodo = {
            //    labels: [],
            //    datasets: [
            //      {
            //          data: [],
            //          dataId: [],
            //          label: [],

            //          hoverBackgroundColor: [],
            //          backgroundColor: colors.purple.fill,
            //          pointBackgroundColor: colors.purple.stroke,
            //          borderColor: colors.purple.stroke,
            //          pointHighlightStroke: colors.purple.stroke,

            //          borderCapStyle: 'butt',
            //          fill: false,
            //      }, {
            //          data: [],
            //          dataId: [],
            //          label: [],

            //          backgroundColor: colors.darkBlue.fill,
            //          pointBackgroundColor: colors.darkBlue.stroke,
            //          borderColor: colors.darkBlue.stroke,
            //          pointHighlightStroke: colors.darkBlue.stroke,
            //          borderCapStyle: 'butt',
            //          fill: false,
            //      }
            //    ]
            //};

            ////Config
            //vm.OptionsResumoPeriodo = {

            //    responsive: true,
            //    maintainAspectRatio: false,

            //    legend: {
            //        display: true,
            //        position: "top",
            //        labels: {
            //            fontColor: colors.black.stroke,
            //            padding: 5,
            //            fontSize: 12
            //        }
            //    },
            //    title: {
            //        display: true,
            //        text: 'Resumo de Matrícula e Rematrícula por Período'
            //    },


            //    scales: {
            //        xAxes: [{
            //            scaleLabel: {
            //                display: true,
            //                labelString: 'Mês'
            //            },
            //            display: true,
            //        }],
            //        yAxes: [{
            //            display: true,
            //            //stacked: true,
            //            scaleLabel: {
            //                display: true,
            //                labelString: 'Valores'
            //            },
            //            ticks: {
            //                min: 0,

            //            }
            //        }]
            //    },
            //    tooltips: {
            //        enabled: true,
            //        mode: 'index',
            //        position: 'average',
            //        intersect: false,
            //        displayColors: true,

            //    },
            //    hover: {
            //        mode: 'nearest',
            //        intersect: true
            //    },
            //};
        }
        var MONTHS = ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"];

        function SetMonth(mes) {
            return MONTHS[mes - 1];
        }

        function GetDadosResumoPorPeriodo() {


            vm.LstSeriesResumo = [];
            vm.LstMesResumoPeriodo = [];

            var IdCampus = (vm.IdCampus || 0),
                IdPeriodoLetivo = (vm.IdPeriodoLetivo || 0);

            if (IdCampus < 1 || IdPeriodoLetivo < 1) {
                return false;
            }
            vm.loadChartResumoPorPeriodo = true;
            matriculaRematriculaFactory.GetDadosResumoPorPeriodo(IdCampus, IdPeriodoLetivo).success(function (data) {
                vm.loadChartResumoPorPeriodo = false;

                //vm.DadosCharResumoPeriodo.datasets[0].label.push("Matrículas");
                //vm.DadosCharResumoPeriodo.datasets[1].label.push("Rematrículas");

                vm.LstSeriesResumo[0] = {
                    name: 'Matrículas',
                    data: []
                }

                vm.LstSeriesResumo[1] = {
                    name: 'Rematrículas',
                    data: []
                }

                angular.forEach(data, function (value, key) {
                    vm.LstMesResumoPeriodo.push(SetMonth(value.Mes) + '/' + value.Ano);
                    //vm.DadosCharResumoPeriodo.labels.push(SetMonth(value.Mes) + '/' + value.Ano);
                    //vm.DadosCharResumoPeriodo.datasets[0].data.push(value.TotalMatricula);
                    //vm.DadosCharResumoPeriodo.datasets[1].data.push(value.TotalRematricula);


                    vm.LstSeriesResumo[0].data.push(value.TotalMatricula);
                    vm.LstSeriesResumo[1].data.push(value.TotalRematricula);

                });

                LoadChartResumoPorPeriodo();


            }).error(function () {
                vm.loadChartResumoPorPeriodo = false;
                growl.error("Falha ao carregar o gráfico de Resumo por Período", { title: 'Atenção' });
            })



        }

        vm.RefreshChartResumoPorPeriodo = RefreshChartResumoPorPeriodo;
        function RefreshChartResumoPorPeriodo() {
            GetDadosResumoPorPeriodo();

        }
        /*FIM - Charts Resumo de Matricula e rematricula por periodo*/


        /*Grid Alunos por Turma*/
        vm.GetAlunosTurma = GetAlunosTurma;
        function GetAlunosTurma(idCampus, idPeriodoletivo, idGradeLetivaTurma) {

            vm.IdGradeLetivaTurma = idGradeLetivaTurma;
            vm.LstAlunosTurma = [];
            vm.loadListaAlunosTurma = true;

            matriculaRematriculaFactory.GetDadosAlunoCursoTurma(idCampus, idPeriodoletivo, idGradeLetivaTurma, true, vm.IdTipoAcessoIESUnificado).success(function (data) {
                vm.loadListaAlunosTurma = false;
                vm.LstAlunosTurma = data;

            }).error(function (data) {
                vm.loadListaAlunosTurma = false;
                growl.error("Falha ao carregar a lista de alunos da turma selecionada", { title: 'Atenção' });
            })

        }

        vm.SetFilterAlunosTurma = SetFilterAlunosTurma;
        function SetFilterAlunosTurma(filtro) {
            if (filtro == 1) {
                if (vm.RemoveAlunosMatricula)
                    vm.RemoveAlunosMatricula = false;
                else
                    vm.RemoveAlunosMatricula = true;
            }

            if (filtro == 2) {
                if (vm.RemoveAlunoRematricula)
                    vm.RemoveAlunoRematricula = false;
                else
                    vm.RemoveAlunoRematricula = true;
            }
        }

        vm.FilterListaAluno = FilterListaAluno;
        function FilterListaAluno() {
            return function (item) {

                if (item.Calouro) {
                    if (vm.RemoveAlunosMatricula)
                        return false;
                } else {
                    if (vm.RemoveAlunoRematricula)
                        return false;
                }


                console.log(item);
                return true;
            }

        }

        function CloseListaTurmas() {
            vm.IdGradeLetivaTurma = 0;
            vm.RemoveAlunoRematricula = false;
            vm.RemoveAlunosMatricula = false;
        }

        /*Grid Alunos por Turma*/



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

    }



})();

