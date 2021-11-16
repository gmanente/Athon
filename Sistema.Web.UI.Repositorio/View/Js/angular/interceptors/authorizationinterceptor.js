angular.module($ngSession.ModuleName).factory("authorizationInterceptor", function () {
	return {
	    request: function (config) {
	        var url = config.url;
	        if (url.includes('api')) {
	            config.headers['AuthControl'] = $ngSession.ControllerName;
	        }
			return config;
		}
	};
});