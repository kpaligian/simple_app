describe("Unit tests: ", function () {
    beforeEach(module("simple.app"));
    var scope, controller, httpBackend, createController, http;

    beforeEach(inject(function ($rootScope, $controller, _$httpBackend_, $http) {
        scope = $rootScope.$new();
        controller = $controller;
        httpBackend = _$httpBackend_;
        http = $http;
    }));

    afterEach(function () {
        httpBackend.verifyNoOutstandingExpectation();
        httpBackend.verifyNoOutstandingRequest();
    });

    describe("ApplicantsCtrl Tests:", function () {
        beforeEach(function () {
            httpBackend.when("GET", "resources/applicants").respond([
                {
                    Name: "costas",
                    Rate: 200
                }
            ]);

            createController = function () {
                return controller('simple.applications.ApplicantsCtrl', { '$scope': scope });
            };
        });

        it("should get an array of length equal to 1", function () {
            httpBackend.expectGET("resources/applicants");
            var ctrl = createController();
            httpBackend.flush();
            expect(scope.applicants.length).toBe(1);
        });

        it("should get the right name and rate", function () {
            httpBackend.expectGET("resources/applicants");
            var ctrl = createController();
            httpBackend.flush();
            expect(scope.applicants[0]['Name']).toBe('costas');
            expect(scope.applicants[0]['Rate']).toBe(200);
        });
    });

    describe("CreateApplicantCtrl testing", function () {
        var newApplicant = { "Name": "costa", "Rate": "200" };

        beforeEach(function () {
            newApplicant.Name = "costa";
            newApplicant.Rate = 200;

            createController = function () {
                return controller('simple.applications.CreateApplicantCtrl', { '$scope': scope });
            };
        });

        it("create applicant", function () {
            var ctrl = createController();
            httpBackend.whenPOST('/resources/create_applicant').respond(200, { 'Name': 'costa', 'Rate': '200' });
            httpBackend.expectPOST('/resources/create_applicant');

            scope.save(newApplicant);

            httpBackend.flush();
            expect(scope.name).toBe("costa");
        });
    });
});
