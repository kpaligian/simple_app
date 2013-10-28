/// <reference path="../reference.ts" />

angular.module("simple.applications", []).config(($stateProvider, $urlRouterProvider) => {

    //Default State
    $urlRouterProvider.otherwise('/applications');

    var views = '/simple-app/web/src/applications/views/';

    //States
    $stateProvider
        .state('applications', {
            url: "/applications",
            views: {
                "sidebarView@": {
                    templateUrl: views + "sidebar.tpl.html"
                },
                "mainContentView": {
                    templateUrl: views + "applicants.tpl.html"
                }
            }
        })
        .state('empty', {
            url: "/empty",
            views: {
                "sidebarView": {
                    templateUrl: views + "emptySidebar.html"
                },
                "mainContentView": {
                    templateUrl: views + "emptyMain.html"
                }
            }
        })
        .state("applications.createapplicant", {
            url: "/insert",
            views: {
                "mainContentView@": {
                    templateUrl: views + "createapplicant.tpl.html"
                }
            }
        });

});