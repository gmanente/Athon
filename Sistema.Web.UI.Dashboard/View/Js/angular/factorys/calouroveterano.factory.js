(function () {
    'use strict';

    // Defining angularjs Module
    angular.module($ngSession.ModuleName).factory('calouroVeteranoFactory', calouroVeteranoFactory);

    calouroVeteranoFactory.$inject = ['$http', 'errorRequest', 'limitToFilter'];

    function calouroVeteranoFactory($http, errorRequest, limitToFilter) {
        var _self = {
            GetCampusUsuario: GetCampusUsuario,
            GetPeriodoLetivo: GetPeriodoLetivo
        };
        return _self;



        function GetCampusUsuario() {
            return $http.get('/api/CalouroVeterano/GetCampusUsuario');
        }

        function GetPeriodoLetivo(idPeriodoLetivo, descricao, idPeriodoLetivoAtual) {
            idPeriodoLetivo = (idPeriodoLetivo || 0);
            descricao = (descricao || '');
            idPeriodoLetivoAtual = (idPeriodoLetivoAtual || 0)
            return $http.get('/api/CalouroVeterano/GetPeriodoLetivo', {
                params:
                    {
                        idPeriodoLetivo: idPeriodoLetivo,
                        descricao: descricao,
                        idPeriodoLetivoAtual: idPeriodoLetivoAtual
                    }
            });
        }

     
        //Calouros e veteranos
        function GetDadosCalouroVeterano(idCampus, idPeriodoLetivo) {
            return $http.get('/api/CalouroVeterano/GetDadosCalouroVeterano', {
                params: {
                    idCampus: idCampus,
                    idPeriodoLetivo: idPeriodoLetivo
                }
            });
        }
    }
}());