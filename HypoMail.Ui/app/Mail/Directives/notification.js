'use strict';

angular.module('mailsManagerApp')
    .directive('mailNotification', [function () {
    return {
        restrict: 'A',
        template:
            "<div class='alert alert-{{mailAlert.type}}' ng-show='mailAlert.message' data-notification='{{mailAlert.status}}' role='alert'>" +
                "{{mailAlert.message}}</div>",
        scope: {
            mailAlert: "="
        },
        replace: true
};
}]);