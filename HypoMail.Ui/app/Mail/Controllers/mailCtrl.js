'use strict';

angular.module('mailsManagerApp')
    .controller('mailCtrl',
    ['$scope', '$modal', 'mailService', '$routeParams', '$q', '$window', '$location',
        function ($scope, $modal, mailService, $routeParams, $q, $window, $location) {

            var init = function () {

                $scope.mail = {
                    from: 'lionel.messi@mailgun.org',
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

                var errorMessage = '';
                if ($scope.mail.recipients.length === 0) {
                    errorMessage += 'You need to include at least one recipient.\n';

                } else {
                    var countTo = 0;
                    for (var i = 0; i < $scope.mail.recipients.length; i++) {
                        if ($scope.mail.recipients[i].type === 'TO') {
                            countTo++;
                        }
                    }

                    if (countTo === 0) {
                        errorMessage += 'You need to include at least one recipient in the "To" list.\n';
                    }
                }
                
                if (!$scope.mail.subject){
                    errorMessage += 'Subject is mandatory.\n';
                }

                if (!$scope.mail.message) {
                    errorMessage += 'The email messsage is mandatory.\n';
                }

                if (errorMessage) {
                    deferred.reject({ errorMessage: errorMessage });
                }

                deferred.resolve();
                return deferred.promise;
            }

            var sendMail = function () {
                return mailService.sendMail($scope.mail);
            };

            var showAlert = function (message) {
                if (message && message.data && message.data.message) {

                    showError(message.data.message);

                } else if (message && message.errorMessage) {

                    showError(message.errorMessage);

                } else {

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

            var showNotification = function(message, type) {
                $scope.notificationAlert = {
                    message: message,
                    type: type,
                    status: 'show'
                };
            };

            init();
        }]);