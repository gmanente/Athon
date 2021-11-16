angular.module($ngSession.ModuleName).config(function ($httpProvider) {
	$httpProvider.interceptors.push("authorizationInterceptor");
	$httpProvider.interceptors.push("errorInterceptor");
	// $httpProvider.interceptors.push("loadingInterceptor");
});