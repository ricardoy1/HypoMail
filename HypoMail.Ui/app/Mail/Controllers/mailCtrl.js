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
                $window.alert(message);
            }

            var notifyUser = function () {
                $window.alert("The Mail has been successfully sent.");
            }

            var redirectToMain = function () {
                $location.path('/');
            }

            $scope.sendMail = function () {
                validateMail()
                    .then(sendMail)
                    .then(notifyUser)
                    .then(init)
                    .catch(showAlert);
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

            $scope.deleteRecipient = function(index) {
                $scope.mail.recipients.splice(index, 1);
            };

            init();
        }]);