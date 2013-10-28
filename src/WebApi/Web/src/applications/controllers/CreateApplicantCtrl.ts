module Simple.Applications {

    export class CreateApplicantCtrl {
        $inject = ['$scope', '$http', '$log'];
        constructor($scope, $http) {
            $scope.name = "";
            $scope.save = function (user) {
                $http.post("/simple-app/resources/applicants", user, { 'Content-Type': 'application/json' })
                    .then((data) => {
                        $scope.name = data.data.Name;
                    },
                    function () {
                        console.log("post fail");
                    }
                );
            };
        }
    }

    angular.module("simple.applications").controller("simple.applications.CreateApplicantCtrl", CreateApplicantCtrl);
}