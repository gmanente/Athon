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

        
        LoadGrid();
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


        vm.ModalConsultar = ModalConsultar;
        function ModalConsultar() {
            vm.consulta = {};
            $("#modal-consultar").modal();
        }

        /*vm.ValidSearch = ValidSearch;
        function ValidSearch() {
            if ((vm.consultaForm.Nome.$pristine || vm.consultaForm.Nome.$viewValue == '' || vm.consultaForm.Nome.$invalid) &&
                (vm.consultaForm.NomeSocial.$pristine || vm.consultaForm.NomeSocial.$viewValue == '' || vm.consultaForm.NomeSocial.$invalid) &&
                (vm.consultaForm.Cpf.$pristine || vm.consultaForm.Cpf.$viewValue == '' || vm.consultaForm.Cpf.$invalid) &&
                (vm.consultaForm.Rg.$pristine || vm.consultaForm.Rg.$viewValue == '' || vm.consultaForm.Rg.$invalid) &&
                (vm.consultaForm.NomeMae.$pristine || vm.consultaForm.NomeMae.$viewValue == '' || vm.consultaForm.NomeMae.$invalid) &&
                (vm.consultaForm.NomePai.$pristine || vm.consultaForm.NomePai.$viewValue == '' || vm.consultaForm.NomePai.$invalid) &&
                (vm.consultaForm.Cidade.$pristine || vm.consultaForm.Cidade.$viewValue == '' || vm.consultaForm.Cidade.$invalid) &&
                (vm.consultaForm.Uf.$pristine || vm.consultaForm.Uf.$viewValue == '' || vm.consultaForm.Uf.$invalid)
            )
                return true;
        }*/

        vm.GetSearch = GetSearch;
        function GetSearch() {
            LoadGrid();
            $('#modal-consultar').modal('toggle');
        }

        function LoadGrid(startPage, page, lastPage, descricao) {
            startPage = (startPage || true);
            page = (page || 0);
            lastPage = (lastPage || false);
            descricao = (descricao || '');

            vm.status = 1;

            if (vm.consulta !== undefined) {
                vm.status = 1;
                vm.rowCollection = [];

                var nome = vm.ContatoConsulta.Nome !== undefined ? vm.ContatoConsulta.Nome.trim() : "";
                var nomeSocial = vm.ContatoConsulta.NomeSocial !== undefined ? vm.ContatoConsulta.NomeSocial.trim() : "";
                var cpf = vm.ContatoConsulta.Cpf !== undefined ? vm.ContatoConsulta.Cpf.trim() : "";
                var rg = vm.ContatoConsulta.Rg !== undefined ? vm.ContatoConsulta.Rg : "";
                var nomeMae = vm.ContatoConsulta.NomeMae !== undefined ? vm.ContatoConsulta.NomeMae : "";
                var nomePai = vm.ContatoConsulta.NomePai !== undefined ? vm.ContatoConsulta.NomePai : "";
                var cidade = vm.ContatoConsulta.Cidade !== undefined ? vm.ContatoConsulta.Cidade : "";
                var uf = vm.ContatoConsulta.Uf !== undefined ? vm.ContatoConsulta.Uf : "";

                if (nome != "" || nomeSocial != "" || cpf != "" || rg != "" || nomeMae != "" || nomePai != "" || cidade != "" || uf != "") {

                    contatoFactory.GetAll(nome, nomeSocial, cpf, rg, nomeMae, nomePai, cidade, uf).then(function (response) {
                        var _data = response.data;
                        vm.status = 2;
                        vm.rowCollection = _data;

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
                    });
                }
            }
        }
        
    }
})();