(function () {
    'use strict';

    angular.module($ngSession.ModuleName).filter("primeiraLetraMaiuscula", primeiraLetraMaiuscula)

    primeiraLetraMaiuscula.inject = [];

    function primeiraLetraMaiuscula() {

        return function (input) {
            var listaInputs = input.split(" ");

            var listaInputsOk = listaInputs.map(function (descricao) {
                if (/(da|de)/.test(descricao)) return descricao;
                return descricao.charAt(0).toUpperCase() + descricao.substring(1).toLowerCase();
            });

            return listaInputsOk.join(" ");
        }
    }

})();