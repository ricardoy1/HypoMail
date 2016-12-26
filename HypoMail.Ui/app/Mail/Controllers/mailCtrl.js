'use strict';

angular.module('mailsManagerApp')
    .controller('mailCtrl',
    ['$scope', '$modal', 'mailService', '$routeParams', '$q', '$window', '$location',
        function ($scope, $modal, mailService, $routeParams, $q, $window, $location) {

            var init = function () {

                $scope.mail = {
                    from: 'developer@hypodomain.com',
                    recipients: [],
                    subject: '',
                    message: ''
                };

                $scope.newMail = {
                    type: 'TO',
                    address: ''
                };

                $scope.notificationAlert = {};
            }

            var validateMail = function () {
                var deferred = $q.defer();
                if ($scope.mail.recipients.length > 0) {
                    deferred.resolve();
                } else {
                    deferred.reject('The Mail is not valid: You need to include at least one recipient.');
                }

                return deferred.promise;
            }

            var sendMail = function () {
                return mailService.sendMail($scope.mail);
            };

            var showAlert = function (message) {
                if (message && message.data && message.data.message) {
                    showError(message.data.message);
                } else if (message) {
                    showError(message);
                }else {
                    showError("There has been an unexpected error while trying to send the e-mail.");
                }
            }

            var notifyUser = function () {
                $window.alert("The Mail has been successfully sent.");
            }

            $scope.deleteRecipient = function (index) {
                $scope.mail.recipients.splice(index, 1);
            };

            var turnOnSpinner = function() {
                $scope.loading = true;
            };

            var turnOffSpinner = function () {
                $scope.loading = false;
            };

            $scope.sendMail = function () {
                validateMail()
                    .then(turnOnSpinner)
                    .then(sendMail)
                    .then(notifyUser)
                    .then(init)
                    .catch(showAlert)
                    .finally(turnOffSpinner);
            };

            $scope.addNewRecipient = function () {

                $scope.mail.recipients.push($scope.newMail);

                var lastSelectedType = $scope.newMail.type;

                $scope.newMail = {
                    address: '',
                    type: lastSelectedType
                }

                $scope.mailForm.$setPristine();
            };


            var showError = function(message) {
                showNotification(message, 'danger');
            };

            var showInfo = function (message) {
                showNotification(message, 'info');
            };

            var showNotification = function(message, type) {
                $scope.notificationAlert = {
                    message: message,
                    type: type,
                    status: 'show'
                };
            };

            init();
        }]);