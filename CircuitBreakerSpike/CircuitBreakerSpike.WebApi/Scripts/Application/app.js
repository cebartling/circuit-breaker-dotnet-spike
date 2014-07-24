

var app = angular.module('CircuitBreaker', []);

var controller = app.controller('OrderManagementController', function($scope, $log, $http) {

    this.hasErrors = false;
    this.exceptionThrowingEnabled = false;
    this.numberOfInvocations = 0;
    this.retrieveOrdersButtonText = 'Start retrieving orders';
    this.isRetrievingOrders = false;
    this.exceptionThrowingButtonText = 'Start throwing exceptions';

    this.doToggleRetrieveOrders = function() {
        if (!this.isRetrievingOrders) {
            $log.info('Starting the retrieval of orders.');
            this.numberOfInvocations = 0;
            this.isRetrievingOrders = true;
            this.retrieveOrdersButtonText = 'Stop retrieving orders';
            this.executeOrderRetrieval();
        } else {
            $log.info('Stopping the retrieval of orders.');
            this.isRetrievingOrders = false;
            this.retrieveOrdersButtonText = 'Start retrieving orders';
        }
    };

    this.executeOrderRetrieval = function() {
        if (this.isRetrievingOrders) {
            $log.info('Retrieving orders...');
            var promise = $http.get('api/Orders');
            promise.success($.proxy(function(data) {
                this.numberOfInvocations += 1;
                this.hasErrors = false;
                $log.info('Received orders from server-side: ' + data);
            }, this));
            promise.error($.proxy(function() {
                this.numberOfInvocations += 1;
                this.hasErrors = true;
            }, this));

            var func = $.proxy(function() { this.executeOrderRetrieval(); }, this);
            setTimeout(func, 2000);
        }
    };

    this.toggleExceptionThrowingInRepositories = function() {
        if (this.exceptionThrowingEnabled) {
            $log.info('Disabling exception throwing.');
            this.exceptionThrowingEnabled = false;
            this.exceptionThrowingButtonText = 'Start throwing exceptions';
            $http.post('/api/Exceptions', { Enabled: false });
        } else {
            $log.info('Enabling exception throwing.');
            this.exceptionThrowingEnabled = true;
            this.exceptionThrowingButtonText = 'Stop throwing exceptions';
            $http.post('/api/Exceptions', { Enabled: true });
        }
    };

});