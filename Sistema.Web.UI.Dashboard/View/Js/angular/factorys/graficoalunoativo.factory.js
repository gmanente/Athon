(function () {
    'use restrict';

    // Defining angularjs Module
    angular.module($ngSession.ModuleName).factory('graficoAlunoAtivoFactory', graficoAlunoAtivoFactory);

    graficoAlunoAtivoFactory.$inject = ['$http', 'errorRequest', 'limitToFilter'];

    function graficoAlunoAtivoFactory($http, errorRequest) {
        var _self = {
            GetCampusUsuario: GetCampusUsuario,
            GetPeriodoLetivo: GetPeriodoLetivo,
            GetDadosPorPeriodoLetivo: GetDadosPorPeriodoLetivo
        };
        return _self;

        function GetCampusUsuario() {
            return $http.get('/api/GraficoAlunoAtivo/GetCampusUsuario');
        }

        function GetPeriodoLetivo(idPeriodoLetivo) {
            idPeriodoLetivo = (idPeriodoLetivo || 0);
            return $http.get('/api/GraficoAlunoAtivo/GetPeriodoLetivo', { params: { idPeriodoLetivo: idPeriodoLetivo } });
        }


        function GetDadosPorPeriodoLetivo(idCampus, idPeriodoLetivo) {
            return $http.get('/api/GraficoAlunoAtivo/GetDadosPorPeriodoLetivo', {                
                params: {                    
                    idCampus: idCampus,
                    idPeriodoLetivo: idPeriodoLetivo
                }
            });
        }

    }


}());