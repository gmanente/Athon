(function () {
    'use strict';

    angular.module($ngSession.ModuleName).filter("trim", trim)

    trim.inject = [];

    function trim() {

        return function (value) {
            if (!angular.isString(value)) {
                return value;
            }
            return value.replace(/^\s+|\s+$/g, ''); // you could use .trim, but it's not going to work in IE<9
        };
    }

})();