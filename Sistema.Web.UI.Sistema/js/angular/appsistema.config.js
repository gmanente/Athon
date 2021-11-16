angular.module($ngSession.ModuleName).config(['growlProvider', function (growlProvider) {
    growlProvider.onlyUniqueMessages(false);
    growlProvider.globalTimeToLive(4000);
}]);
