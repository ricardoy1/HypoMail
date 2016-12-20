'use strict';

describe("mailCtrl", function () {

    var $rootScope, $scope, $controller, $q, deferredGetMail, $modal, mailService, $routeParams, mailCtrl, deferredSaveMail,
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
            getMail: function() {},
            saveMail: function () {},
            deleteMail: function() {}
        };

        $window = {
            alert: function() {}
        };

        $location = {
            path: function () { }
        };

        deferredGetMail = $q.defer();
        deferredSaveMail = $q.defer();
        spyOn(mailService, 'getMail').andReturn(deferredGetMail.promise);
        spyOn(mailService, 'saveMail').andReturn(deferredSaveMail.promise);
        spyOn($window, 'alert');
        spyOn($location, 'path');
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

    describe('retrieval', function() {
        it("should retrieve Mail and set its data in the scope", function () {
            // Act
            var Mail = {
                name: 'Mail 1',
                modules: ['module 1']
            };

            var response = {
                data: Mail
            };

            createController();
            deferredGetMail.resolve(response);

            // Assert
            expect(mailService.getMail).toHaveBeenCalled();
            $scope.$root.$digest();
            expect($scope.Mail).toBe(Mail);
        });
    });

    describe('modules', function() {
        it("should add new module to the scope", function () {
            // Arrange
            var modalDeferred = $q.defer();
            spyOn($modal, 'open').andReturn({ result: modalDeferred.promise });
            deferredGetMail.resolve();

            // Act
            createController();
            $scope.addModule();
            modalDeferred.resolve('new-module');

            // Assert
            expect($modal.open).toHaveBeenCalled();
            $scope.$root.$digest();
            expect($scope.Mail).toBeDefined();
            expect($scope.Mail.modules).toEqual(['new-module']);
        });

        it("should delete module from the scope", function () {
            // Arrange
            deferredGetMail.resolve({
                data: {
                    modules: ['module1']
                }
            });

            // Act
            createController();
            $scope.$root.$digest();
            $scope.deleteModule(0);

            // Assert
            expect($scope.Mail.modules).toEqual([]);
        });

        it("should edit module and set new changes back to the scope", function () {
            // Arrange
            var modalDeferred = $q.defer();
            spyOn($modal, 'open').andReturn({ result: modalDeferred.promise });
            var module = {
                name: 'module-name',
                duration: 'module-duration',
                description: 'module-description'
            };

            var response = {
                data: {
                    modules: [module]
                }
            };

            var modalModule = {
                name: 'module-name-1',
                duration: 'module-duration-1',
                description: 'module-description-1'
            };
            deferredGetMail.resolve(response);

            // Act
            createController();
            $scope.$root.$digest();
            $scope.editModule(module);

            modalDeferred.resolve(modalModule);

            // Assert
            $scope.$root.$digest();
            expect($scope.Mail.modules).toEqual([modalModule]);
        });
    });

    describe('duration', function() {
        it("should calculate total duration", function() {
            // Arrange
            var Mail = {
                name: 'Mail 1',
                modules: [
                    { duration: 2 },
                    { duration: 4 },
                    { duration: 8 }
                ]
            };

            var response = {
                data: Mail
            };

            // Act
            createController();
            deferredGetMail.resolve(response);
            $scope.$root.$digest();

            // Assert
            expect($scope.getTotalMailDuration()).toBe(14);
        });

        it("should calculate total Mail duration to Zero when there are no modules.", function() {
            // Arrange
            var Mail = {
                name: 'Mail 1',
                modules: []
            };
            createController();
            $scope.Mail = Mail;

            // Act
            var result = $scope.getTotalMailDuration();

            // Assert
            expect(result).toBe(0);
        });
    });

    describe('save orchestration', function() {
        it("should call save Mail service when saving Mail", function () {
            // Arrange
            var Mail = {
                name: 'Mail 1',
                modules: ['module 1']
            };
            createController();
            $scope.Mail = Mail;

            // Act
            $scope.saveMail();
            $scope.$root.$digest();

            // Assert
            expect(mailService.saveMail).toHaveBeenCalledWith(Mail);
        });

        it("should notify user when the Mail was successfully saved", function () {
            // Arrange
            var Mail = {
                name: 'Mail 1',
                modules: ['module 1']
            };
            createController();
            $scope.Mail = Mail;

            // Act
            $scope.saveMail();
            deferredSaveMail.resolve();
            $scope.$root.$digest();

            // Assert
            expect($window.alert).toHaveBeenCalledWith("The Mail has been successfully saved.");
        });

        it("should redirect to list page when the Mail was successfully saved", function () {
            // Arrange
            var Mail = {
                name: 'Mail 1',
                modules: ['module 1']
            };
            createController();
            $scope.Mail = Mail;

            // Act
            $scope.saveMail();
            deferredSaveMail.resolve();
            $scope.$root.$digest();

            // Assert
            expect($location.path).toHaveBeenCalledWith("/");
        });

        it("should display an alert when the validation is not successful", function () {
            // Arrange
            var Mail = {};
            createController();
            $scope.Mail = Mail;

            // Act
            $scope.saveMail();
            $scope.$root.$digest();

            // Assert
            expect($window.alert).toHaveBeenCalled();
        });

        it("should display an alert when the save Mail service call is not successful", function () {
            // Arrange
            var Mail = {
                name: 'Mail 1',
                modules: ['module 1']
            };
            createController();
            $scope.Mail = Mail;

            // Act
            $scope.saveMail();
            deferredSaveMail.reject();
            $scope.$root.$digest();

            // Assert
            expect($window.alert).toHaveBeenCalled();
        });
    });
});