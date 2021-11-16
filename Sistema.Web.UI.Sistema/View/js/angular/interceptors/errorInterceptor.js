angular.module($ngSession.ModuleName).run(['$rootScope', function ($rootScope) {

    $rootScope.debug = false;

    $rootScope.initDebug = function () {
        $rootScope.debug = true;
    };

}]);

angular.module($ngSession.ModuleName).factory("errorInterceptor", function ($q, $location, $timeout, $rootScope) {
	var _statusCode;
	var _statusCodeRedirect = [401, 403];
	var _statusCodeConflit = 409;
	var _urlRedirect = 'Erro.aspx?s=';
    var _message;

	// private functions
	var _isErrorRedirect = function () {
		return _statusCodeRedirect.some(function (elem) {
			return elem === _statusCode;
		});
	};
	var _isConflited = function () {
		return _statusCode === _statusCodeConflit;
	};

	var _isUnauthorized = function () {
		return _statusCode === 401 || _statusCode === 403;
	};

    var _redirectError = function () {
        window.open(_urlRedirect + _statusCode, '_self');
	};

	var _conflitAlert = function () {
		swal("Ops... Conflito", "Não foi possível realizar o cadastro.<br> <span class='text-info'><i class='fa fa-info-circle'></i> Já existe um item semelhante a este</span>", "warning");
	};

	var _unauthorizedAlert = function () {
		swal("Acesso Negado", "Você não possui a permissão necessária para este acesso.<br> <span class='text-info'><i class='fa fa-info-circle'></i> Verifique seu Perfil</span>", "error");
	};

    var _customErrorAlert = function () {
        $timeout(function () {
            swal("Ops...", "Atenção!<br>" + _message + "<br> <span class='text-info'><i class='fa fa-info-circle'></i> Verifique e Tente Novamente</span>", "error");
        });
    };

	return {
        responseError: function (rejection) {
			_statusCode = rejection.status;
            if (rejection.data != null) {
                _message = rejection.data.Message;

                if (_isErrorRedirect() && !$rootScope.debug)
                    _redirectError();
                else if (_isConflited())
                    _conflitAlert();
                else
                    _customErrorAlert()
            }

			return $q.reject(rejection);
		}
	};
});