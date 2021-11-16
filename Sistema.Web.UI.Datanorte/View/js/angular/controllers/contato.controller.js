//=========================================================================================
(function () {
    'use strict';

    angular.module($ngSession.ModuleName).controller("ContatoController", ContatoController);

    ContatoController.inject = ['$scope', '$http', '$filter', 'growl', 'contatoFactory', 'segurancaFactory', 'commonFactory', '$timeout', '$window', '$cookies'];

    //angular.module("appDatanorte").filter('cCMask', ['$filter', function ($filter) {
    //    return function (input) {
    //        return input.replace(/.(00)/mg, '');
    //    };
    //}]);

    function ContatoController($scope, $http, $filter, growl, contatoFactory, segurancaFactory, commonFactory, $timeout, $window, $cookies) {
        var vm = $scope;

        UserConfig();

        vm.Contato = {};
        vm.ContatoConsulta = {};
        vm.ConsultarContato = {};
        vm.TabOpen = 0;
        vm.status = 0;
        vm.modalInserir = false;

        vm.itemsByPage = 100;
        vm.listItemsByPage = [
            { Id: 10, Descricao: 10 },
            { Id: 25, Descricao: 25 },
            { Id: 50, Descricao: 50 },
            { Id: 100, Descricao: 100 },
            { Id: 150, Descricao: 150 },
            { Id: 200, Descricao: 200 },
            { Id: 300, Descricao: 300 },
            { Id: 400, Descricao: 400 },
            { Id: 500, Descricao: 500 },
            { Id: 600, Descricao: 600 },
            { Id: 700, Descricao: 700 }
        ];


        vm.listDDD = [
              { Id: 1, DDD: '65' }
            , { Id: 2, DDD: '66' }];

        vm.rowCollection = undefined;
        vm.rowCollectio2 = undefined;
        vm.rowCollection3 = undefined; //Grid Histórico
        vm.predicates = ['Id', 'Descricao'];
        vm.selectedPredicate = vm.predicates[0];

        vm.IsAuthorized = IsAuthorized;
        vm.Order = Order;
        vm.AtualizarGrid = AtualizarGrid;
        vm.ConsultarContato = ConsultarContato;
        vm.SetTabOpen = SetTabOpen;
        vm.IsTabOpen = IsTabOpen;
        vm.ResetConsulta = ResetConsulta;

        vm.ModalHistorico = ModalHistorico;

        vm.GetListCampusUsuario = GetListCampusUsuario;

        vm.ContatoInfo = {};
        
        //LoadGrid();

        GetEstado();
        //GetDDD();

        SetTabOpen(1);

        vm.ConsultarContato = ConsultarContato;


        function IsAuthorized(rf) {
            return segurancaFactory.IsAuthorized(rf);
        }

        function UserConfig() {
            segurancaFactory.GetCurrentUser().success(function (data) {
                vm.Usuario = data;
            });
        }

        function GetListCampusUsuario() {
            commonFactory.GetListCampusUsuario().success(function (data) {
                vm.LstUsuarioCampus = data;
            });
        }

        function GetEstado() {
            commonFactory.GetEstado().success(function (data) {
                vm.LstEstado = data;
                vm.rowlistEstado = vm.LstEstado;
                vm.ContatoConsulta.Estado = vm.rowlistEstado[10]; //mt

                $scope.apply;
                GetCidade();
                //$timeout(function () {
                //    
                //}, 500);
            });
        }

        vm.GetCidade = GetCidade;
        function GetCidade() {

            var uf = vm.ContatoConsulta.Estado.Sigla;

            if (uf != "" || uf != "") {
                $('#modal-cidade').modal();

                $timeout(function () {
                    contatoFactory.GetCidade(uf).success(function (data) {
                        vm.LstCidade = data;
                        vm.rowlistCidade = vm.LstCidade;
                        vm.ContatoConsulta.Cidade = vm.rowlistCidade[0]; //mt

                        $timeout(function () {
                            GetBairro();
                        }, 500);

                        HideModal();
                    });
                }, 500);
            }
            else {
                swal("Atenção!", "Por favor, preencha pelo menos um filtro para consulta!", "warning");
                vm.status = false;
            }
            //contatoFactory.GetCidade(uf).success(function (data) {
            //    vm.LstCidade = data;
            //    vm.rowlistCidade = vm.LstCidade;
            //    vm.ContatoConsulta.Cidade = vm.rowlistCidade[10]; //mt

            //    HideModal();
            //});
        }

        vm.GetBairro = GetBairro;
        function GetBairro() {


            var uf = vm.ContatoConsulta.Estado.Sigla;
            var cidade = vm.ContatoConsulta.Cidade.Nome;

            

            if (uf != "" || uf != "") {
                $('#modal-bairro').modal();

                $timeout(function () {
                    contatoFactory.GetBairro(uf,cidade).success(function (data) {
                        vm.LstBairro = data;
                        vm.rowlistBairro = vm.LstBairro;
                        vm.ContatoConsulta.Bairro = vm.rowlistBairro[10]; //mt

                        HideModal();
                    });
                }, 500);
            }
            else {
                swal("Atenção!", "Por favor, preencha pelo menos um filtro para consulta!", "warning");
                vm.status = false;
            }
            //contatoFactory.GetBairro(uf, cidade).success(function (data) {
            //    vm.LstBairro = data;
            //    vm.rowlistBairro = vm.LstBairro;
            //    vm.ContatoConsulta.Bairro = vm.rowlistBairro[10]; //mt

            //    HideModal();
            //});
        }

 


         //#region REGISTRO DE ATENDIMENTO
        function ModalHistorico(Contato) {
            var titModal = "Histórico de Atendimento do Contato: " + Contato.Nome;
            vm.tituloModalHistorico = titModal;
            ConfigureModal("#modal-historico", false, Contato, titModal);
            LoadHistorico(Contato.Id);
        }

        function LoadHistorico(id) {
            vm.status3 = 1;
            vm.rowCollection3 = [];

            var idContato = id;

            if (idContato != "" && idContato != undefined) {
                contatoFactory.GetHistorico(idContato).success(function (response) {
                    var _data = response;
                    vm.status3 = 2;
                    vm.rowCollection3 = _data;
                });
            }
        }

        vm.ModalInserir = ModalInserir;
        function ModalInserir(contato) {
            vm.ContatoHistorico = {};
            vm.ContatoHistorico.Contato = angular.copy(contato);
            vm.ContatoHistorico.DataOperacao = vm.Usuario.DataAtual;
            vm.ContatoHistorico.Contatos_id = vm.ContatoHistorico.Contato.Id;

            vm.tabs = 1;
            ConfigureModal("#modal-inserir", true, {}, "Inserir Atendimento", "Preencha o campo abaixo para registrar o atendimento.");
        }

        vm.Insert = Insert;
        function Insert() {
            contatoFactory.Insert(vm.ContatoHistorico).success(function (data, status) {
                CallbackSuccess('Atendimento gravado com sucesso!', vm.ContatoHistorico);
                vm.ContatoHistorico = {};
            });
        }

        //#endregion

        function ResetConsulta() {
            delete vm.ContatoConsulta;
            vm.ContatoConsulta = {};
        }

        function SetTabOpen(tabOpen) {
            vm.TabOpen = tabOpen;
        }

        function IsTabOpen(tabOpen) {
            return tabOpen === vm.TabOpen;
        }

        /*function LoadGrid() {
            vm.ContatoConsulta.LimitTo = 10;
        }*/

        // ATUALIZA A GRID DE CONSULTA DE ALUNOS DA TURMA
        function AtualizarGrid() {
            SetTabOpen(1);
            vm.rowCollection = undefined;
            vm.rowCollection2 = undefined;
            ConsultarDados();
            growl.success('Consulta atualizada com sucesso!', { title: 'Atualizada' })
        }

        // RESETAR CONSULTA DA GRID
        function ResetConsulta() {
            delete vm.ContatoConsulta;
            vm.ContatoConsulta = {};
        }

        function Order(order) {
            vm.OrderByType = !vm.OrderByType;
            vm.OrderBy = vm.OrderByType ? order : '-' + order;
            vm.rowCollection = $filter('orderBy')(vm.rowCollection, vm.OrderBy);
        }
          

        function ConfigureModal(idModal, pristine, contato, titulo, info) {
            vm.modalInserir = pristine;
            vm.btnInserir = pristine;
            vm.tituloModal = titulo;
            vm.infoModal = info;
            if (pristine) {
                vm.Contato = {};
                vm.inserirForm.$setPristine();
                vm.inserirForm.$resetForm();
            }
            vm.modalInserir = true;
            $(idModal).modal();
        }

        function CallbackSuccess(msg, contato, del) {
            $(".modal").modal("hide");


            if (del) {
                var page = angular.element('#pagerId').isolateScope().currentPage;
                vm.ContatoConsulta.Nome = contato.Nome;
                //AtualizarGrid();
            } else {
                vm.ContatoConsulta.Nome = contato.Nome;
                //AtualizarGrid();
            }

            delete vm.ContatoConsulta;
            vm.inserirForm.$setPristine();
            growl.success(msg, { title: 'Sucesso' });
        }

        //#region CONSULTAS DA TELA       

        function LoadContato() {
            var objContatoConsulta = $cookies.get('_lancContatoConsulta' + vm.Usuario.Id) != undefined ? JSON.parse($cookies.get('_lancContatoConsulta' + vm.Usuario.Id)) : undefined;

            if (objContatoConsulta !== undefined) {
                var nome = objContatoConsulta.Nome = undefined ? objContatoConsulta.Nome : "";
                //var idSituacao = objContatoConsulta.Situacao.Id;

                contatoFactory.ConsultarContato(nome).then(function (response) {
                    var _data = response.data;
                    vm.status = 2;
                    vm.rowCollection = _data;
                    vm.ContatoConsulta.LimitTo = 10;
                    vm.ContatoConsulta = {};
                    vm.ContatoConsulta = angular.copy(objContatoConsulta);
                });
            }
        }


        vm.ConsultarContato = ConsultarContato;
        function ConsultarContato(startPage, page, lastPage, descricao) {
            startPage = (startPage || true);
            page = (page || 0);
            lastPage = (lastPage || false);
            descricao = (descricao || '');

            growl.success('Carregando Informações!', { title: 'Carregando' });

            vm.rowCollection = undefined;

            if (vm.ContatoConsulta !== undefined) {
                vm.status = 1;
                vm.rowCollection = [];
                vm.rowCollection2 = [];

                ConsultarDados();

                if (startPage) {
                    $scope.$watch(function () { return angular.element('#pagerId').is(':visible') }, function () {
                        angular.element('#searchDescricao').val(descricao).trigger('input');
                    });
                }
                if (!startPage && page > 0 && !lastPage) {
                    $scope.$watch(function () { return angular.element('#pagerId').is(':visible') }, function () {
                        angular.element('#pagerId').isolateScope().selectPage(page);
                        if (descricao !== undefined)
                            angular.element('#searchDescricao').val(descricao).trigger('input');
                    });
                }
                if (!startPage && page === 0 && lastPage) {
                    //vm.rowCollection = $filter('orderBy')(vm.rowCollection, 'Id');
                    $scope.$watch(function () { return angular.element('#pagerId').is(':visible') }, function () {
                        var lastPage = angular.element('#pagerId').isolateScope().numPages;
                        angular.element('#pagerId').isolateScope().selectPage(lastPage);
                        angular.element('#searchDescricao').val('').trigger('input');
                    });
                }
            }
        }

        vm.HideModal = HideModal;
        function HideModal() {
            angular.element(".modal").modal('hide');
        }

        // CONSULTAR ALUNOS PARA INCLUSAO
        vm.ConsultarDados = ConsultarDados;
        function ConsultarDados() {
            var uf = vm.ContatoConsulta.Estado.Sigla !== undefined ? vm.ContatoConsulta.Estado.Sigla : "";
            var cidade = vm.ContatoConsulta.Cidade.Nome !== undefined ? vm.ContatoConsulta.Cidade.Nome : "";
            var dddInicial = vm.ContatoConsulta.DDDInicial !== undefined ? vm.ContatoConsulta.DDDInicial : "";
            var dddFinal = vm.ContatoConsulta.DDDFinal !== undefined ? vm.ContatoConsulta.DDDFinal : "";
            var telefone = vm.ContatoConsulta.Telefone !== undefined ? vm.ContatoConsulta.Telefone : "";
            var logradouro = vm.ContatoConsulta.Logradouro !== undefined ? vm.ContatoConsulta.Logradouro : "";
            var logradouroNr = vm.ContatoConsulta.LogradouroNumero !== undefined ? vm.ContatoConsulta.LogradouroNumero : "";
            var bairro = vm.ContatoConsulta.Bairro.Bairro !== undefined ? vm.ContatoConsulta.Bairro.Bairro : "";
            var cep = vm.ContatoConsulta.Cep !== undefined ? vm.ContatoConsulta.Cep : "";
            var cpf = vm.ContatoConsulta.Cpf !== undefined ? vm.ContatoConsulta.Cpf : "";
            var nome = vm.ContatoConsulta.Nome !== undefined ? vm.ContatoConsulta.Nome.trim() : "";

            if (cidade != "" || uf != "") {
                $('#modal-loading').modal();

                $timeout(function () {
                    contatoFactory.GetAll(nome, cpf, cep, bairro, logradouroNr, logradouro, telefone, dddFinal, dddInicial, cidade, uf).then(function (response) {
                        var _data = response.data;
                        vm.status = 2;
                        vm.rowCollection = _data;

                        HideModal();
                    });;
                }, 500);
            }
            else {
                swal("Atenção!", "Por favor, preencha pelo menos um filtro para consulta!", "warning");
                vm.status = false;
            }
        }


        //#endregion



        vm.AbortarConsulta = AbortarConsulta;
        function AbortarConsulta() {
            window.location = "./Contato.aspx";
        }

        vm.GetImprimir = GetImprimir;
        function GetImprimir(Tipo) {
            vm.ContatoRel = angular.copy(vm.ContatoConsulta);

            var uf = vm.ContatoRel.Estado.Sigla !== undefined ? vm.ContatoRel.Estado.Sigla : "";
            var cidade = vm.ContatoRel.Cidade.Nome !== undefined ? vm.ContatoRel.Cidade.Nome : "";
            var dddInicial = vm.ContatoRel.DDDInicial !== undefined ? vm.ContatoRel.DDDInicial : "";
            var dddFinal = vm.ContatoRel.DDDFinal !== undefined ? vm.ContatoRel.DDDFinal : "";
            var telefone = vm.ContatoRel.Telefone !== undefined ? vm.ContatoRel.Telefone : "";
            var logradouro = vm.ContatoRel.Logradouro !== undefined ? vm.ContatoRel.Logradouro : "";
            var logradouroNr = vm.ContatoRel.LogradouroNumero !== undefined ? vm.ContatoRel.LogradouroNumero : "";
            var bairro = vm.ContatoConsulta.Bairro.Bairro !== undefined ? vm.ContatoConsulta.Bairro.Bairro : "";
            var cep = vm.ContatoRel.Cep !== undefined ? vm.ContatoRel.Cep : "";
            var cpf = vm.ContatoRel.Cpf !== undefined ? vm.ContatoRel.Cpf : "";
            var nome = vm.ContatoRel.Nome !== undefined ? vm.ContatoRel.Nome.trim() : "";
            var formatoRelatorio = Tipo;

            var href = "../Report/Aspx/ContatoRel.aspx";
            window.open(href
              + "?vNome=" + nome +
                "&vCpf=" + cpf +
                "&vCep=" + cep +
                "&vBairro=" + bairro +
                "&vLogradouroNr=" + logradouroNr +
                "&vLogradouro=" + logradouro +
                "&vTelefone=" + telefone +
                "&vDDDFinal=" + dddFinal +
                "&vDDDInicial=" + dddInicial +
                "&vCidade=" + cidade +
                "&vUf=" + uf +
                "&vFormato=" + formatoRelatorio +
                "&vUsuario=" + vm.Usuario.Nome
            );
        };
        
    }
})();