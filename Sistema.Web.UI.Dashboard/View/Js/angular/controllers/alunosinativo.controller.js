(function () {
    'use strict';

    // Defining angularjs Module
    var app = angular.module($ngSession.ModuleName)


    app.controller('AlunosInativoController', AlunosInativoController);

    AlunosInativoController.$inject = ['$scope', 'growl', '$filter', '$cookies', 'alunosInativoFactory', '$popover'];

    function AlunosInativoController($scope, growl, $filter, $cookies, alunosInativoFactory, $popover) {
        var vm = $scope;

        $scope.popover = {
            title: "Outros tipo acesso IES",
            content: ""
        };

        //Veriaveis relatime
        vm.realTimeGPA = false;
        vm.realTimeCurso = false;
        vm.realTimeSituacaoAcademica = false;
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
            alunosInativoFactory.GetCampusUsuario().success(function (data) {
                vm.LstCampusUsuario = data;
            }).error(function (data) {
                growl.error("Falha ao carregar o campo Campus", { title: 'Atenção' });
            });
        }
        GetCampusUsuario();

        //Consulta Periodo Letivo e Carrega o combo
        function GetPeriodoLetivo() {
            alunosInativoFactory.GetPeriodoLetivo().success(function (data) {
                vm.LstPeriodoLetivo = data;
            }).error(function (data) {
                growl.error("Falha ao carregar o campo Periodo Letivo", { title: 'Atenção' });
            });
        }

        //Controle de Cookies de Consultas
        function GetCookieConsulta() {
            var idPeriodoLetivo = parseInt($cookies.get('AlunosInativoIdPeriodoLetivo'), 10),
                idCampus = parseInt($cookies.get('AlunosInativoIdCampus'), 10);

            vm.IdPeriodoLetivo = idPeriodoLetivo;
            vm.IdCampus = idCampus;

            console.log(idPeriodoLetivo);

            GetDadosAreaConhecimento();
          




        }

        //Salva no cookie a consulta
        function SetCookiesConsulta() {
            var idPeriodoLetivo = vm.IdPeriodoLetivo,
                idCampus = vm.IdCampus;

            $cookies.put('AlunosInativoIdPeriodoLetivo', idPeriodoLetivo),
            $cookies.put('AlunosInativoIdCampus', idCampus);
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
            alunosInativoFactory.GetDadosAreaConhecimento(idPeriodoletivo, idCampus, false).success(function (data) {
                vm.loadChartGpa = false;

                angular.forEach(data, function (value, key) {

                    vm.DadosChartsGpa.labels.push(value.Sigla + ' (' + value.Total + ')');

                    vm.DadosChartsGpa.datasets[0].data.push(value.Total);

                    vm.DadosChartsGpa.datasets[0].dataId.push(value.IdGPA);

                    vm.DadosChartsGpa.datasets[0].label.push(value.NomeGPA);
                    vm.DadosChartsGpa.datasets[0].backgroundColor.push(ColorsChart[key]);


                    //vm.DadosChartsGpa.datasets[0].TotalMatricula = value.TotalMatricula;
                    //vm.DadosChartsGpa.datasets[0].TotalRematricula = value.TotalRematricula;


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
            GetDadosSituacaoAcademica(idCampus, IdGpa, idPeriodoletivo);
            CloseListaTurmas();

            delete vm.IdCursoSelecionado;


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
            InitChartCurso();

            if (idGpa < 0)
                return false;
            vm.loadChartCurso = true;
            alunosInativoFactory.GetDadosCursos(idCampus, idGpa, idPeriodoLetivo, false).success(function (data) {


                vm.DadosChartsCurso.datasets[0].label.push("Total de Aluno Inativos");
                //vm.DadosChartsCurso.datasets[1].label.push("Rematrícula");

                angular.forEach(data, function (value, key) {


                    vm.loadChartCurso = false;
                    vm.DadosChartsCurso.labels.push(value.CursoNome);


                    vm.DadosChartsCurso.datasets[0].data.push(value.Total);
                    vm.DadosChartsCurso.datasets[0].dataId.push(value.IdCurso);
                    vm.DadosChartsCurso.datasets[0].backgroundColor.push(colors.darkBlue.fill);
                 

                 
                    vm.TotalGeralCursos += value.Total;
                    //vm.TotalMatriculaCursos += value.TotalMatricula;
                    //vm.TotalRematriculaCursos += value.TotalRematricula;

                });

            }).error(function (data) {
                vm.loadChartCurso = false;
                growl.error("Falha ao carregar o gráfico de curso", { title: 'Atenção' });
            });

        }

        //Function init chart Cursos
        function InitChartCurso() {
            vm.TotalGeralCursos = 0;
            vm.TotalMatriculaCursos = 0;
            vm.TotalRematriculaCursos = 0;
            //Datasets
            vm.DadosChartsCurso = {
                labels: [],
                datasets: [
                  {
                      data: [],
                      dataId: [],
                      label: [],
                      backgroundColor: [],
                      hoverBackgroundColor: [],
                      borderColor : []
                  },
                  //{
                  //    data: [],
                  //    dataId: [],
                  //    label: [],
                  //    backgroundColor: [],
                  //    hoverBackgroundColor: []
                  //}
                ]
            };

            //Config
            vm.OptionsChartCurso = {
                legend: {
                    display: true,
                    position: "top",
                    labels: {
                        //fontColor: 'rgb(255, 99, 132)',
                        padding: 30
                    }
                },
                title: {
                    display: true,
                    text: 'Quantitativo de Alunos por Cursos'
                },
                scales: {
                    xAxes: [{
                        stacked: true
                    }],
                    yAxes: [{
                        stacked: true
                    }]
                }
            };
        }

        vm.RefreshChartCurso = RefreshChartCurso;
        function RefreshChartCurso() {

            if (vm.IdGpaSelecionado > 0 && vm.IdPeriodoLetivo > 0 && vm.IdCampus > 0) {
                GetDadosCursos(vm.IdCampus, vm.IdGpaSelecionado, vm.IdPeriodoLetivo)
            } else {
                delete vm.IdGpaSelecionado;
            }
        }


        vm.ClickChartCurso = function (event) {


            if (event.element[0] == undefined)
                return false;

            var index = event.element[0]._index;
            var element = event.element[0];


            var dataset = element._chart.config.data.datasets[0];

            var dataChart = dataset.data[index];
            var IdCurso = dataset.dataId[index];
            var totalMatricula = dataset.TotalMatricula;
            var totalRematricula = dataset.TotalRematricula;

            var label = dataset.label[index];
            var color = dataset.backgroundColor[index];


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
        vm.RefreshChartSituacaoAcademica = RefreshChartSituacaoAcademica;
        function RefreshChartSituacaoAcademica() {
            if (vm.IdGpaSelecionado > 0 && vm.IdPeriodoLetivo > 0 && vm.IdCampus) {
                GetDadosSituacaoAcademica(vm.IdCampus, vm.IdGpaSelecionado, vm.IdPeriodoLetivo)
            } else {
                delete vm.IdGpaSelecionado;
            }
        }
        function InitChartSituacaoAcademica() {
            //Datasets
            vm.DadosChartsSituacaoAcademica = {
                labels: [],
                datasets: [
                  {
                      data: [],
                      dataId: [],
                      label: [],
                      backgroundColor: [],
                      hoverBackgroundColor: []
                  }
                ]
            };

            //Config
            vm.OptionsChartSituacaoAcademica = {

                legend: {
                    display: true,
                    position: "left",
                    labels: {
                        fontColor: 'rgb(255, 99, 132)',
                        padding: 5,
                        fontSize: 12
                    }
                },
                title: {
                    display: true,
                    text: 'Quantitativo de Alunos por Tipo de Acesso IES'
                },
            };
        }
        function GetDadosSituacaoAcademica(idCampus, idGpa, idPeriodoLetivo) {
            vm.IdGpaSelecionado = idGpa;
            InitChartSituacaoAcademica();

            if (idGpa < 0)
                return false;
            vm.loadChartSituacaoAcademica = true;

            vm.TotalGeralTipoAcesso = 0;
            vm.TotalMatriculaTipoAcesso = 0;
            vm.TotalRematriculaTipoAcesso = 0;

            alunosInativoFactory.GetDadosSituacaoAcademica(idCampus, idGpa, idPeriodoLetivo, false).success(function (data) {

                vm.LstSituacaoAcademica = data;
                
                angular.forEach(data, function (value, key) {


                    vm.loadChartSituacaoAcademica = false;
                    vm.DadosChartsSituacaoAcademica.labels.push(value.SituacaoAcademica + " (" + value.Total + ")");

                    vm.DadosChartsSituacaoAcademica.datasets[0].data.push(value.Total);
                    vm.DadosChartsSituacaoAcademica.datasets[0].dataId.push(value.IdCurso);


                    vm.DadosChartsSituacaoAcademica.datasets[0].backgroundColor.push(ColorsChart[key]);

                    vm.TotalGeralTipoAcesso += value.Total;
                    vm.TotalMatriculaTipoAcesso += value.TotalMatricula;
                    vm.TotalRematriculaTipoAcesso += value.TotalRematricula;


                });

            }).error(function (data) {
                vm.loadChartSituacaoAcademica = false;
                growl.error("Falha ao carregar o gráfico de Tipo de Acesso IES", { title: 'Atenção' });
            });

        }
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
        function InitChartCursoTurma() {

            vm.TotalGeralCursoTurma = 0;
            vm.TotalMatriculaCursoTurma = 0;
            vm.TotalRematriculaCursoTurma = 0;

            //Datasets
            vm.DadosChartsCursoTurma = {
                labels: [],
                datasets: [
                  {
                      data: [],
                      dataId: [],
                      label: [],
                      backgroundColor: [],
                      hoverBackgroundColor: []
                  }
                ]
            };

            //Config
            vm.OptionsChartCursoTurma = {

                legend: {
                    display: true,
                    position: "top",
                    labels: {
                        //fontColor: 'rgb(255, 99, 132)',
                        padding: 10,
                        fontSize: 12
                    }
                },
                title: {
                    display: true,
                    text: 'Quantitativo de Alunos por Turma'
                },

                scales: {
                    xAxes: [{
                        stacked: true,
                        ticks: {
                            fontSize: 8,
                            padding: 0,
                            min: 0,
                            offset: false,
                            autoSkip: true
                        }
                    }],
                    yAxes: [{
                        stacked: true
                    }]
                }
            };
        }
        vm.GetDadosCursoTurma = GetDadosCursoTurma;
        vm.TotalGeralCursoTurma = 0;
        vm.TotalMatriculaCursoTurma = 0;
        vm.TotalRematriculaCursoTurma = 0;
        function GetDadosCursoTurma(idCurso, idPeriodoLetivo) {
            vm.IdCursoSelecionado = idCurso;
            InitChartCursoTurma();
            vm.CloseListaTurmas();

            if (idCurso < 0)
                return false;
            vm.loadChartCursoTurma = true;
            alunosInativoFactory.GetDadosCursoTurma(idCurso, idPeriodoLetivo, false).success(function (data) {


                vm.DadosChartsCursoTurma.datasets[0].label.push("Total de Aluno Inativo");

                if (data.length <= 10) {
                    vm.OptionsChartCursoTurma.scales.xAxes[0].ticks.fontSize = 10;
                }



                angular.forEach(data, function (value, key) {


                    vm.loadChartCursoTurma = false;
                    vm.DadosChartsCursoTurma.labels.push(value.TurmaSigla + " (" + value.Total + ")");

                    vm.DadosChartsCursoTurma.datasets[0].data.push(value.Total);
                    vm.DadosChartsCursoTurma.datasets[0].dataId.push(value.IdGradeLetivaTurma);
                    vm.DadosChartsCursoTurma.datasets[0].backgroundColor.push(colors.darkBlue.fill);

                    //vm.DadosChartsCursoTurma.datasets[1].data.push(value.TotalRematricula);
                    //vm.DadosChartsCursoTurma.datasets[1].dataId.push(value.IdGradeLetivaTurma);
                    //vm.DadosChartsCursoTurma.datasets[1].backgroundColor.push(ColorsChart[1]);



                    vm.TotalGeralCursoTurma += value.Total;
                

                });

            }).error(function (data) {
                vm.loadChartCursoTurma = false;
                growl.error("Falha ao carregar o gráfico de Turmas", { title: 'Atenção' });
            });

        };

        vm.IdGradeLetivaTurma = 0;

        vm.ClickChartCursoTurmas = function (event) {

            if (event.element[0] == undefined)
                return false;

            var index = event.element[0]._index;
            var element = event.element[0];

            //Dados Matricula
            var datasetMatricula = element._chart.config.data.datasets[0];
            var totalMatricula = datasetMatricula.data[index];
            var id = datasetMatricula.dataId[index];

            var label = element._chart.config.data.labels[index];
            var color = datasetMatricula.backgroundColor[index];


            //Dados Rematricula
            CloseListaTurmas();


            var idPeriodoletivo = (vm.IdPeriodoLetivo || 0),
              idCampus = (vm.IdCampus || 0);

            vm.IdGradeLetivaTurma = id;
            vm.Turma = {
                Nome: label
            };

            //vm.$apply()


            GetAlunosTurma(idCampus, idPeriodoletivo, id);

        };
        /*Chart turmas*/


        /*INICIO - Charts Resumo de Matricula e rematricula por periodo*/
        InitChartResumoPorPeriodo();
        function InitChartResumoPorPeriodo() {

            //Datasets
            vm.DadosCharResumoPeriodo = {
                labels: [],
                datasets: [
                  {
                      data: [],
                      dataId: [],
                      label: [],

                      hoverBackgroundColor: [],
                      backgroundColor: colors.purple.fill,
                      pointBackgroundColor: colors.purple.stroke,
                      borderColor: colors.purple.stroke,
                      pointHighlightStroke: colors.purple.stroke,

                      borderCapStyle: 'butt',
                      fill: false,
                  }, {
                      data: [],
                      dataId: [],
                      label: [],

                      backgroundColor: colors.darkBlue.fill,
                      pointBackgroundColor: colors.darkBlue.stroke,
                      borderColor: colors.darkBlue.stroke,
                      pointHighlightStroke: colors.darkBlue.stroke,
                      borderCapStyle: 'butt',
                      fill: false,
                  }
                ]
            };

            //Config
            vm.OptionsResumoPeriodo = {

                responsive: true,
                maintainAspectRatio: false,

                legend: {
                    display: true,
                    position: "top",
                    labels: {
                        fontColor: colors.black.stroke,
                        padding: 5,
                        fontSize: 12
                    }
                },
                title: {
                    display: true,
                    text: 'Resumo de Matrícula e Rematrícula por Período'
                },


                scales: {
                    xAxes: [{
                        scaleLabel: {
                            display: true,
                            labelString: 'Mês'
                        },
                        display: true,
                    }],
                    yAxes: [{
                        display: true,
                        //stacked: true,
                        scaleLabel: {
                            display: true,
                            labelString: 'Valores'
                        },
                        ticks: {
                            min: 0,

                        }
                    }]
                },
                tooltips: {
                    enabled: true,
                    mode: 'index',
                    position: 'average',
                    intersect: false,
                    displayColors: true,

                },
                hover: {
                    mode: 'nearest',
                    intersect: true
                },
            };
        }
        var MONTHS = ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"];

        function SetMonth(mes) {
            return MONTHS[mes - 1];
        }


        /*FIM - Charts Resumo de Matricula e rematricula por periodo*/


        /*Grid Alunos por Turma*/
        vm.GetAlunosTurma = GetAlunosTurma;
        function GetAlunosTurma(idCampus, idPeriodoletivo, idGradeLetivaTurma) {

            vm.IdGradeLetivaTurma = idGradeLetivaTurma;
            vm.LstAlunosTurma = [];
            vm.loadListaAlunosTurma = true;

            alunosInativoFactory.GetDadosAlunoCursoTurma(idCampus, idPeriodoletivo, idGradeLetivaTurma, false).success(function (data) {
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
            if (vm.realTimeSituacaoAcademica) {
                RefreshChartSituacaoAcademica();
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

