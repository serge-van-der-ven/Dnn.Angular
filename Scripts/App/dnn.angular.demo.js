var dnnAngularDemo = angular.module('DnnAngularDemo', ['ngResource']);

dnnAngularDemo.constant('DemoConfig', {
    apiUrl: "/DesktopModules/Dnn.Angular.Demo/API/"
});

dnnAngularDemo.controller("DemoController", function () {
    var self = this;

    // Should not use hardcoded paths here...
    self.templateUrl = "/desktopmodules/dnn.angular.demo/scripts/views/demoview.html";
});

dnnAngularDemo.factory('DemoResource', ['$resource', 'DemoConfig', function ($resource, config) {
    return $resource(config.apiUrl + 'demo/:id', { id: '@id' }, {});
}]);


dnnAngularDemo.service("DemoService", ['DemoResource', function(demoResource) {
    var self = this;

    self.getItems = function getItems() {
        var deferred = demoResource.query();
        return deferred.$promise;
    }
}]);

dnnAngularDemo.controller("DemoFormController", ["DemoService", function (demoService) {
    var self = this;

    self.message = "Just a demo application";

    self.items = {};

    //self.updateItems = function updateItems(dataFromServer) {
    //    self.items = dataFromServer;
    //}

    //demoService.getItems()
    //    .then(self.updateItems);
}]);
