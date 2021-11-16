
(function () {
    'use strict';

    // Defining angularjs Module
    var app = angular.module($ngSession.ModuleName, $ngComponents.inject());

    angular.module($ngSession.ModuleName).config(['growlProvider', function (growlProvider) {
        growlProvider.onlyUniqueMessages(false);
        growlProvider.globalTimeToLive(4000);
    }]);

    // Smart-table directive
    app.directive('pageSelect', function () {
        return {
            restrict: 'E',
            template: '<input type="text" class="select-page" ng-model="inputPage" ng-change="selectPage(inputPage)">',
            link: function (scope, element, attrs) {
                scope.$watch('currentPage', function (c) {
                    scope.inputPage = c;
                });
            }
        }
    });

    // Ajax directive
    app.factory('httpInterceptor', ['$q', '$rootScope',
        function ($q, $rootScope) {
            var loadingCount = 0;

            return {
                request: function (config) {
                    if (++loadingCount === 1) $rootScope.$broadcast('loading:progress');
                    return config || $q.when(config);
                },

                response: function (response) {
                    if (--loadingCount === 0) $rootScope.$broadcast('loading:finish');
                    return response || $q.when(response);
                },

                responseError: function (response) {
                    if (--loadingCount === 0) $rootScope.$broadcast('loading:finish');
                    return $q.reject(response);
                }
            };
        }
    ]).config(['$httpProvider', function ($httpProvider) {
        $httpProvider.interceptors.push('httpInterceptor');
    }]);


    // Util provider
    app.provider('$util', [function $utilProvider() {
        this.$get = function () {
            return {
                // Retorna o valor de um objeto se ele existir, evitando um TYPEERROR
                fetchObj: function (obj, prop, defaultValue) {
                    if (obj === null || typeof obj === 'undefined')
                        return (typeof defaultValue === 'undefined') ? '' : defaultValue;

                    var _index = prop.indexOf('.')
                    if (_index > -1)
                        return this.fetchObj(obj[prop.substring(0, _index)], prop.substr(_index + 1), defaultValue);

                    return obj[prop];
                }
            };
        };
    }]);


    app.filter('numberFixedLen', function () {
        return function (a, b) {
            return (1e4 + "" + a).slice(-b);
        };
    });

    // RootScope
    app.run(function ($rootScope, $templateCache, segurancaFactory) {

        $rootScope.$on('loading:progress', function () {
            //console.log('loading:progress');
        });

        $rootScope.$on('loading:finish', function () {
            //console.log('loading:finish');
        });

        $rootScope.$on('scope.stored', function (event, data) {
            console.log("scope.stored", data);
        });

        /** ******************************************************************
         *             TEMPLATES
         *  ******************************************************************/

        // Smart-table custom template
        $templateCache.put('template/smart-table/pagination.html',
            '<div class="pagination" ng-if="pages.length >= 2"><ul class="pagination">' +
            '<li ng-class="(currentPage == 1) ? \'disabled\':\'\'"><a ng-click="selectPage(1)">&laquo;</a></li>' +
            '<li ng-class="(currentPage == 1) ? \'disabled\':\'\'"><a ng-click="selectPage(currentPage - 1)">&lsaquo;</a></li>' +
            '<li class="page-select"><a><page-select></page-select> de {{numPages}}</a></li>' +
            '<li ng-class="(currentPage == numPages) ? \'disabled\':\'\'"><a ng-click="selectPage(currentPage + 1)">&rsaquo;</a></li>' +
            '<li ng-class="(currentPage == numPages) ? \'disabled\':\'\'"><a ng-click="selectPage(numPages)">&raquo;</a></li>' +
            '</ul></div>');


        /** ******************************************************************
         *             PARÂMETROS
         *  ******************************************************************/

        // vm.rowCollection
        $rootScope.rowCollection = [];

        // vm.itemsByPage
        $rootScope.itemsByPage = 10;

        // vm.listItemsByPage
        $rootScope.listItemsByPage = [
            { Id: 10, Descricao: 10 },
            { Id: 25, Descricao: 25 },
            { Id: 50, Descricao: 50 },
            { Id: 100, Descricao: 100 }
        ];

        // vm.predicates
        $rootScope.predicates = ['Id', 'Descricao'];

        // vm.selectedPredicate
        $rootScope.selectedPredicate = $rootScope.predicates[0];

        // vm.parseInt
        $rootScope.parseInt = window.parseInt;


        /** ******************************************************************
         *             FUNÇÕES ÚTEIS
         *  ******************************************************************/

        // vm.IsAuthorized();
        $rootScope.IsAuthorized = function (rf) {
            return segurancaFactory.IsAuthorized(rf);
        }

        // vm.UserConfig();
        $rootScope.UserConfig = function () {
            segurancaFactory.GetCurrentUser().success(function (data) {
                $rootScope.Usuario = data;
            });
        }
        $rootScope.UserConfig();

        // vm.ConfigModal();
        $rootScope.ConfigModal = function (titulo, subtitulo, id) {
            this.modal = {
                Titulo: titulo,
                SubTitulo: subtitulo
            }
            angular.element(id).modal();
        }

        // vm.ModalConfig();
        $rootScope.ModalConfig = function (id, titulo, subtitulo, info) {
            this.modal = {
                Titulo: titulo,
                SubTitulo: subtitulo,
                Info: info
            }
            angular.element(id).modal();
        }

        // vm.Order();
        $rootScope.Order = function (order) {
            if (order === '0') {
                this.rowCollection = $filter('orderBy')(this.rowCollection, 'Id');
                this.rowCollection = $filter('orderBy')(this.rowCollection, 'Id');
            } else {
                this.rowCollection = $filter('orderBy')(this.rowCollection, 'Descricao');
            }
        }

        // vm.CompareDate();
        $rootScope.CompareDate = function (dt) {
            var datatermino = moment(dt);
            var today = moment(new Date());
            return datatermino.diff(today, 'days') >= 0;
        }

        // vm.inArray()
        $rootScope.inArray = function (item, array) {
            return (-1 !== array.indexOf(item));
        };

        // vm.inList()
        $rootScope.inList = function (item, string) {
            var _item = String(item);
            var _array = string.split(',');
            return (-1 !== _array.indexOf(_item));
        };

        // vm.getType()
        $rootScope.getType = function (elem) {
            return Object.prototype.toString.call(elem).slice(8, -1);
        };

        // vm.isArray()
        $rootScope.isArray = function (elem) {
            return this.getType(elem) === 'Array';
        };

        // vm.isObject()
        $rootScope.isObject = function (elem) {
            return this.getType(elem) === 'Object';
        };

        // vm.isString()
        $rootScope.isString = function (elem) {
            return this.getType(elem) === 'String';
        };

        // vm.isBoolean()
        $rootScope.isBoolean = function (elem) {
            return this.getType(elem) === 'Boolean';
        };

        // vm.isNumber()
        $rootScope.isNumber = function (elem) {
            return this.getType(elem) === 'Number';
        };

        // vm.isFunction()
        $rootScope.isFunction = function (elem) {
            return this.getType(elem) === 'Function';
        };

        // vm.isUndefined()
        $rootScope.isUndefined = function (elem) {
            return this.getType(elem) === 'Undefined';
        };

    });

}());