(function() {
    'use strict';

    angular.module($ngSession.ModuleName).directive('breadcrumb', function () {
        var template = ' <div>'
                      + '    <ol class="breadcrumb">'
                      + '        <li>'
                      + '            <a ng-href="{{options.firstUrl}}" title="{{options.titleFirst}}" target="_self">{{options.titleFirst}}</a>'
                      + '        </li>'
                      + '        <li ng-hide="!options.secondUrl">'
                      + '            <a ng-href="{{options.secondUrl}}" title="{{options.titleSecondUrl}}" target="_self">{{options.titleSecondUrl}}</a>'
                      + '        </li>'
                      + '        <li class="active current">Manutenção'
                      + '        </li>'
                      + '        <li ng-hide="!options.nomeUsuario" class="active current pull-right" title="Nome do usuário"><i class="fa fa-user"></i> {{ options.nomeUsuario }}'
                      + '        </li>'
                      + '    </ol>'
                      + '</div>'

        return {
            restrict: 'AE',
            template: template,
            scope: {
                options: '='
            }
        };
    });

})();