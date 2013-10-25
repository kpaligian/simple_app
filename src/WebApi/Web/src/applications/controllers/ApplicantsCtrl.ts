module Simple.Applications {

    export class ApplicantsCtrl {   
        $inject = ['$scope','$http'];
        constructor($scope,$http) {
            $scope.applicants = [];

            $http.get("resources/applicants")
                .then(function (result) {
                    angular.copy(result.data, $scope.applicants);
                },
                () => {
                    console.log("api error");
                }
                );
        }

        create() {


        }
    }

    angular.module("simple.applications").controller("simple.applications.ApplicantsCtrl", ApplicantsCtrl);

}