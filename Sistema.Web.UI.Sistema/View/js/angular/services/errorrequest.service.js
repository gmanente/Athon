(function () {
    angular
		.module($ngSession.ModuleName)
		.service('errorRequest', errorRequest);

    function errorRequest() {
        var _statusCode;
        var _statusCodeRedirect = [401, 403, 404];
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

        var _customErrorAlert = function () {
            swal("Ops...", "Atenção! <br>" + _message + ". <br> <span class='text-info'><i class='fa fa-info-circle'></i> Verifique e Tente Novamente</span>", "error");
        };

        var _unauthorizedAlert = function () {
            swal("Acesso Negado", "Você não possui a permissão necessária para este acesso.<br> <span class='text-info'><i class='fa fa-info-circle'></i> Verifique seu Perfil</span>", "error");
        };

        // public functions
        this.log = function (data, statusCode) {
            _statusCode = statusCode;
            if (data != null) {
                _message = data.Message;

                if (_isErrorRedirect())
                    _redirectError();
                else if (_isConflited())
                    _conflitAlert();
                else
                    _customErrorAlert()
            }
        };

        this.logSeguranca = function (data, statusCode) {
            if (_isUnauthorized())
                _unauthorizedAlert();
            else {
                _customErrorAlert();
            }
        };
    }
})();