

var app = angular.module('CircuitBreaker', []);

var controller = app.controller('OrderManagementController', function($scope, $log, $http) {

    this.hasErrors = false;
    this.exceptionThrowingEnabled = false;
    this.numberOfInvocations = 0;

    this.doRetrieveOrders = function () {
        $log.info('Retrieving orders...');

        var promise = $http.get('api/Orders');
        promise.success($.proxy(function (data) {
            this.numberOfInvocations += 1;
            this.hasErrors = false;
            $log.info('Received orders from server-side: ' + data);
        }, this));
        promise.error($.proxy(function() {
            this.numberOfInvocations += 1;
            this.hasErrors = true;
        }, this));
    };

    this.disableExceptionThrowingInRepositories = function() {
        $log.info('Disabling exception throwing.');

        this.exceptionThrowingEnabled = false;
    };

    this.enableExceptionThrowingInRepositories = function() {
        $log.info('Enabling exception throwing.');

        this.exceptionThrowingEnabled = true;
    };

});