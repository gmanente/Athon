(function () {
    'use strict';

    angular.module('appBiblioteca')
    .directive('datetimepicker', function () {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attrs, ngModelCtrl) {
                element.datepicker({
                    format: 'dd/mm/yyyy',
                    language: 'pt-BR'
                }).on('changeDate', function (objDatepicker) {
                    $(this).datepicker('hide');
                    var dataFormat = moment(objDatepicker.date).format("DD/MM/YYYY");
                    // Triggers a digest to update your model
                    scope.$apply(function () {
                        ngModelCtrl.$setViewValue(dataFormat);
                    });
                });

            }
        }
    });
})();