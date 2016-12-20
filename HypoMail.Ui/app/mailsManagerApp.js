'use strict';

var mailsManagerApp = angular.module('mailsManagerApp', ['ngRoute', 'ui.bootstrap'])
    .config([
        '$routeProvider', function ($routeProvider) {
            $routeProvider.when('/', { templateUrl: '/app/Mail/Templates/Mail.html', controller: 'mailCtrl' });
        }
    ])
;