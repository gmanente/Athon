(function () {
    'use strict';
    
    angular
        .module($ngSession.ModuleName) // GetModuloApp()
        .factory('segurancaFactory', segurancaFactory);

    segurancaFactory.$inject = ['$http'];

    function segurancaFactory($http) {
        var _controllerName = $ngSession.ControllerName;
        var segurancaFactory = {
            GetCurrentUser: GetCurrentUser,
            GetPermissions: GetPermissions,
            IsAuthorized: IsAuthorized,
            GetSubmodulosAutenticados: GetSubmodulosAutenticados
        };

        return segurancaFactory;

        function GetCurrentUser() {
            return $http.get('/api/Common/GetCurrentUser')
        };

        function GetPermissions() {
            return $http.get('/api/Common/GetPermissions')
        };

        function GetSubmodulosAutenticados() {
            return $http.get('/api/Common/GetSubmodulosAutenticados');
        }

        function IsAuthorized(rf) {           
            var key = md5("AllPermissions_" + _controllerName);
            var cookieValue = getCookie(key);
			
			if(cookieValue == ''){
				key = md5("AllPermissions");
				cookieValue = getCookie(key);
			}
			
            var LstPermissions = JSON.parse(Base64.decode(cookieValue));
            var _isAuthorized = LstPermissions[rf.toLowerCase()]
            return _isAuthorized;
        }               
    }
})();

