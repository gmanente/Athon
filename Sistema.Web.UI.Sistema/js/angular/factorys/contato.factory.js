(function () {
    'use strict';

    angular.module($ngSession.ModuleName).factory("contatoFactory", contatoFactory);

    contatoFactory.inject = ['$http', 'errorRequest'];

    function contatoFactory($http, errorRequest) {
        var contatoFactory = {
            ConsultarContato: ConsultarContato,
            GetAll: GetAll,
        };
        return contatoFactory;

        function ConsultarContato(nome) {
            var config = {
                params: {
                    nome: nome
                }
            };
            return $http.get('/api/Contato/ConsultarContato', config);
        }
        function GetAll(nome, nomeSocial, cpf, rg, nomeMae, nomePai, cidade, uf, errorCallback) {
            var errorFunction = (errorCallback || errorRequest.log);
            var config = {
                params: {
                    nome: nome,
                    nomeSocial: nomeSocial,
                    cpf: cpf,
                    rg: rg,
                    nomeMae: nomeMae,
                    nomePai: nomePai,
                    cidade: cidade,
                    uf: uf
                }
            }
            return $http.get('/api/Contato/GetAll', config).error(errorFunction);
        }

    }

})();