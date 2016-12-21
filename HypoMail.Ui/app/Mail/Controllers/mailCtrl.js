'use strict';

angular.module('mailsManagerApp')
    .controller('mailCtrl',
    ['$scope', '$modal', 'mailService', '$routeParams', '$q', '$window', '$location',
        function ($scope, $modal, mailService, $routeParams, $q, $window, $location) {

            var init = function () {

                $scope.mail = {
                    from: 'developer@hypodomain.com',
                    to: [],
                    cc: [],
                    bcc: [],
                    subject: '',
                    message: ''
                };
            }

            var validateMail = function () {
                var deferred = $q.defer();
                if ($scope.mail.message) {
                    deferred.resolve();
                } else {
                    deferred.reject('The Mail is not valid.');
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
                    .then(redirectToMain)
                    .catch(showAlert);
            };

            init();
        }]);