'use strict';

angular.module('mailsManagerApp').service('mailService',
    ['$http', function ($http) {

        var sendMail = function(mail) {
            var url = '/api/Mails/send';
            var promise = $http.post(url, mail);
            return promise;
        }

        return {
            sendMail: sendMail
        }
    }]);