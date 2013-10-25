describe("Midway: Testing Modules", function () {
    describe("App Module:", function () {
        var mod;
        beforeEach(function () {
            mod = module('simple.app');
        });

        it("should be registered", function () {
            expect(mod).not.toBe(null);
        });
    });
});
