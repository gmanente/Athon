(function () {
    'use strict';

    angular.module($ngSession.ModuleName).factory("commonFactory", commonFactory)

    commonFactory.inject = ['$http'];

    function commonFactory($http) {

        var _commonFactory = {
            GetCidades: GetCidades,
            GetCidadeById: GetCidadeById,
            GetPessoaByCpfCnpj: GetPessoaByCpfCnpj,
            GetListCampusUsuario: GetListCampusUsuario,
            GetDepartamentoUsuario: GetDepartamentoUsuario,
            GetEnderecoCorreios: GetEnderecoCorreios,
            GetEventosCotacao: GetEventosCotacao,
            GetEventosOrdemCompra: GetEventosOrdemCompra,
            GetListCombo: GetListCombo
        };

        return _commonFactory;

        function GetCidades(str) {
            return $http.get('/api/Common/GetCidades/' + str);
        }

        function GetPessoaByCpfCnpj(cpfCnpj) {
            return $http.get('/api/Common/GetPessoaByCpfCnpj/' + cpfCnpj);
        }

        function GetCidadeById(id) {
            return $http.get('/api/Common/GetCidadeById/' + id);
        }

        function GetDepartamentoUsuario() {
            return $http.get('/api/Common/GetDepartamentoUsuario');
        }

        function GetListCampusUsuario() {
            return $http.get('/api/Common/GetListCampusUsuario');
        }

        function GetEventosCotacao() {
            return $http.get('/api/Common/GetEventosCotacao');
        }

        function GetEventosOrdemCompra() {
            return $http.get('/api/Common/GetEventosOrdemCompra');
        }

        function GetListCombo(nameAction) {
            return $http.get('/api/Common/Get' + nameAction);
        }


        function GetEnderecoCorreios(cep) {
            if (cep !== "") {
                cep = cep.replace(/[^\d]+/g, '');
                if (/^[0-9]{8}$/.test(cep)) {
                    return $http.get("//viacep.com.br/ws/" + cep + "/json");
                }
            }
        }



    }

})();