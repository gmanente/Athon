//=========================================================================================
(function () {
    'use strict';

    angular.module($ngSession.ModuleName).controller("ContatoController", ContatoController);

    ContatoController.inject = ['$scope', '$http', '$filter', 'growl', 'contatoFactory', 'commonFactory', '$timeout', '$window', '$cookies'];

    function ContatoController($scope, $http, $filter, growl, contatoFactory,  commonFactory, $timeout, $window, $cookies) {
        var vm = $scope;

        //UserConfig();

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

        vm.listNatureza = [
            { Id: 'D', Nome: 'Despesa' }
            , { Id: 'R', Nome: 'Receita' }];

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

        vm.GetListCampusUsuario = GetListCampusUsuario;
        
        //LoadGrid();
        //SetTabOpen(1);

        vm.ConsultarContato = ConsultarContato;

        function IsAuthorized(rf) {
            return contatoFactory.IsAuthorized(rf);
        }

        function UserConfig() {
            contatoFactory.GetCurrentUser().success(function (data) {
                vm.Usuario = data;
            });
        }

        function GetListCampusUsuario() {
            contatoFactory.GetListCampusUsuario().success(function (data) {
                vm.LstUsuarioCampus = data;
            });
        }

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

        function LoadGrid() {
            vm.ContatoConsulta.LimitTo = 10;
        }

        function Order(order) {
            vm.OrderByType = !vm.OrderByType;
            vm.OrderBy = vm.OrderByType ? order : '-' + order;
            vm.rowCollection = $filter('orderBy')(vm.rowCollection, vm.OrderBy);
        }

        function AtualizarGrid() {
            vm.rowCollection = undefined;
            vm.status = 0;
            //Consultar();
            growl.success('Consulta atualizada com sucesso!', { title: 'Atualizada' })
        }

        function ConfigureModal(idModal, pristine, contato, titulo, info) {
            vm.modalInserir = pristine;
            vm.btnInserir = pristine;
            vm.tituloModal = titulo;
            vm.infoModal = info;
            if (pristine) {
                vm.CentroCusto = {};
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
                vm.ContatoConsulta.Id = contato.Id;
                AtualizarGrid();
            } else {
                vm.ContatoConsulta.Id = contato.Id;
                AtualizarGrid();
            }

            delete vm.ContatoConsulta;
            vm.inserirForm.$setPristine();
            growl.success(msg, { title: 'Sucesso' });
        }

        //#region CONSULTAS DA TELA       

        function LoadContato() {
            var objContatoConsulta = $cookies.get('_lancContatoConsulta' + vm.Usuario.Id) != undefined ? JSON.parse($cookies.get('_lancContatoConsulta' + vm.Usuario.Id)) : undefined;

            if (objContatoConsulta !== undefined) {
                var dataInicio = objContatoConsulta.Nome = undefined ? objContatoConsulta.Nome : "";
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
        function ConsultarContato() {
            var nome = vm.ContatoConsulta.Nome !== undefined ? vm.ContatoConsulta.Nome : "";
            //var idSituacao = vm.AtividadeConsulta.Situacao.Id;

            if (nome !== "" ) {
                contatoFactory.ConsultarContato(nome).then(function (response) {
                    var _data = response.data;
                    vm.status = 2;
                    vm.rowCollection = _data;
                    vm.ContatoConsulta.LimitTo = 10;

                    $cookies.put('_lancContatoConsulta' + vm.Usuario.Id, JSON.stringify(vm.ContatoConsulta));
                });
            }
            else {
                swal("Atenção!", "Por favor, preencha pelo menos um filtro para consulta!", "warning");
                vm.status = false;
            }
        }


        //#endregion

        
    }
})();