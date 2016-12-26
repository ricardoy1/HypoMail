'use strict';

describe("mailCtrl", function () {

    var $rootScope, $scope, $controller, $q, deferredGetMail, $modal, mailService, $routeParams, mailCtrl, deferredSendMail,
    $window, $location;

    beforeEach(module('ngRoute'));
    beforeEach(module('ui.bootstrap'));
    beforeEach(module('mailsManagerApp'));

    beforeEach(inject(function (_$rootScope_, _$controller_, _$q_, _$modal_, _$routeParams_) {
        $rootScope = _$rootScope_;
        $scope = $rootScope.$new();
        $controller = _$controller_;
        $q = _$q_;
        $modal = _$modal_;
        $routeParams = _$routeParams_;
    }));

    beforeEach(function () {
        mailService = {
            sendMail: function () {}
        };

        $window = {
            alert: function() {}
        };

        deferredGetMail = $q.defer();
        deferredSendMail = $q.defer();
        spyOn(mailService, 'sendMail').andReturn(deferredSendMail.promise);
        spyOn($window, 'alert');
        $routeParams.mailId = 1;
    });

    var createController = function () {
        return $controller('mailCtrl', {
            $scope: $scope,
            $modal: $modal,
            mailService: mailService,
            $routeParams: $routeParams,
            $window: $window,
            $location: $location
        });
    };

    describe('save orchestration', function() {
        it("should call sendMail service when sending Mail", function () {
            // Arrange
            var mail = {
                recipients: ['recipient1'],
                subject: 'super-test',
                message: 'Super-message'
            };
            createController();
            $scope.mail = mail;

            // Act
            $scope.sendMail();
            $scope.$root.$digest();

            // Assert
            expect(mailService.sendMail).toHaveBeenCalledWith(mail);
        });

        it("should NOT call sendMail service and show an ERROR message when sending Mail without any recipients.", function () {
            // Arrange
            var mail = {
                recipients: [],
                subject: 'without recipients',
                message: 'no message'
            };
            createController();
            $scope.mail = mail;

            // Act
            $scope.sendMail();
            $scope.$root.$digest();

            // Assert
            expect($scope.notificationAlert).toEqual(
            {
                message: 'The Mail is not valid: You need to include at least one recipient.',
                type: 'danger',
                status: 'show'
            });
        });


        it("should notify user when e-mail was successfully sent.", function () {
            // Arrange
            var mail = {
                recipients: ['recipient1'],
                subject: 'super-test',
                message: 'Super-message'
            };
            createController();
            $scope.mail = mail;

            // Act
            $scope.sendMail();
            deferredSendMail.resolve();
            $scope.$root.$digest();

            // Assert
            expect($window.alert).toHaveBeenCalled();
        });
    });
});