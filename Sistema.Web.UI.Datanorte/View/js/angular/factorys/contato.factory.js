(function () {
    'use strict';

    angular.module($ngSession.ModuleName).factory("contatoFactory", contatoFactory);

    contatoFactory.inject = ['$http', 'errorRequest'];

    function contatoFactory($http, errorRequest) {
        var contatoFactory = {
            ConsultarContato: ConsultarContato,
            GetHistorico: GetHistorico,
            Insert: Insert,
            GetAll: GetAll,
            GetPessoaCidade: GetPessoaCidade,
            GetCidade: GetCidade,
            GetBairro: GetBairro
        };
        return contatoFactory;

        
        function GetCidade(uf) {
            var config = {
                params: {
                    uf: uf
                }
            };
            return $http.get('/api/Contato/GetCidade', config);
        }

        function GetBairro(uf,cidade) {
            var config = {
                params: {
                    uf: uf,
                    cidade : cidade
                }
            };
            return $http.get('/api/Contato/GetBairro', config);
        }

        function ConsultarContato(nome) {
            var config = {
                params: {
                    nome: nome
                }
            };
            return $http.get('/api/Contato/ConsultarContato', config);
        }
        function GetAll(nome, cpf, cep, bairro, logradouroNr, logradouro, telefone, dddFinal, dddInicial, cidade, uf, errorCallback) {
            var errorFunction = (errorCallback || errorRequest.log);
            var config = {
                params: {
                    nome: nome,
                    cpf: cpf,
                    cep: cep,
                    bairro : bairro,
                    logradouroNumero : logradouroNr,
                    Logradouro : logradouro,
                    telefone : telefone,
                    dddFinal : dddFinal,
                    dddInicial : dddInicial,
                    cidade: cidade,
                    uf : uf
                }
            }
            return $http.get('/api/Contato/GetAll', config).error(errorFunction);
        }

        function GetHistorico(idContato, errorCallback) {
            var errorFunction = (errorCallback || errorRequest.log);
            return $http.get('/api/Contato/GetHistorico/' + idContato).error(errorFunction);
        }

        function Insert(contato) {
            return $http.post('/api/Contato/Insert', contato);
        }

        function GetPessoaCidade(uf) {
            return $http.post('/api/Contato/GetPessoaCidade', uf);
        }
    }

})();