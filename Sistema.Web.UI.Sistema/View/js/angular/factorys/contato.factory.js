(function () {
    'use strict';

    angular.module($ngSession.ModuleName).factory("contatoFactory", contatoFactory);

    contatoFactory.inject = ['$http', 'errorRequest'];

    function contatoFactory($http, errorRequest) {
        var contatoFactory = {
            ConsultarContato: ConsultarContato,
            GetCurrentUser: GetCurrentUser,
            GetPermissions: GetPermissions
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


        function GetCurrentUser() {
            console.log('entrie aqui');
            return $http.get('/api/Contato/GetCurrentUser')
        };

        function GetPermissions() {
            return $http.get('/api/Contato/GetPermissions')
        };

        /*function GetSubmodulosAutenticados() {
            //return $http.get('/api/' + _controllerName + '/GetSubmodulosAutenticados');
            return $http.get('/api/Common/GetSubmodulosAutenticados');
        }
        */
    }

})();