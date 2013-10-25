/// <reference path="../js_reference.ts" />


describe("Midway: Testing Modules",  () => {
    describe("App Module:", function() {

        var mod;
        beforeEach(() => {
            mod = module('simple.app');
        });

        it("should be registered", () => {
            expect(mod).not.toBe(null);
        });

    });
});