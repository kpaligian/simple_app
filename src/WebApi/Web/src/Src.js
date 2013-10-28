angular.module("simple.applications", []).config(function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise('/applications');

    var views = '/simple-app/web/src/applications/views/';

    $stateProvider.state('applications', {
        url: "/applications",
        views: {
            "sidebarView@": {
                templateUrl: views + "sidebar.tpl.html"
            },
            "mainContentView": {
                templateUrl: views + "applicants.tpl.html"
            }
        }
    }).state('empty', {
        url: "/empty",
        views: {
            "sidebarView": {
                templateUrl: views + "emptySidebar.html"
            },
            "mainContentView": {
                templateUrl: views + "emptyMain.html"
            }
        }
    }).state("applications.createapplicant", {
        url: "/insert",
        views: {
            "mainContentView@": {
                templateUrl: views + "createapplicant.tpl.html"
            }
        }
    });
});
var Simple;
(function (Simple) {
    (function (Applications) {
        var ApplicantsCtrl = (function () {
            function ApplicantsCtrl($scope, $http) {
                this.$inject = ['$scope', '$http'];
                $scope.applicants = [];

                $http.get("/simple-app/resources/applicants").then(function (result) {
                    angular.copy(result.data, $scope.applicants);
                }, function () {
                    console.log("api error");
                });
            }
            ApplicantsCtrl.prototype.create = function () {
            };
            return ApplicantsCtrl;
        })();
        Applications.ApplicantsCtrl = ApplicantsCtrl;

        angular.module("simple.applications").controller("simple.applications.ApplicantsCtrl", ApplicantsCtrl);
    })(Simple.Applications || (Simple.Applications = {}));
    var Applications = Simple.Applications;
})(Simple || (Simple = {}));
var Simple;
(function (Simple) {
    (function (Applications) {
        var CreateApplicantCtrl = (function () {
            function CreateApplicantCtrl($scope, $http) {
                this.$inject = ['$scope', '$http', '$log'];
                $scope.name = "";
                $scope.save = function (user) {
                    $http.post("/simple-app/resources/applicants", user, { 'Content-Type': 'application/json' }).then(function (data) {
                        $scope.name = data.data.Name;
                    }, function () {
                        console.log("post fail");
                    });
                };
            }
            return CreateApplicantCtrl;
        })();
        Applications.CreateApplicantCtrl = CreateApplicantCtrl;

        angular.module("simple.applications").controller("simple.applications.CreateApplicantCtrl", CreateApplicantCtrl);
    })(Simple.Applications || (Simple.Applications = {}));
    var Applications = Simple.Applications;
})(Simple || (Simple = {}));
var Simple;
(function (Simple) {
    var app = angular.module('simple.app', [
        'ui.router',
        'simple.applications'
    ]);
})(Simple || (Simple = {}));
