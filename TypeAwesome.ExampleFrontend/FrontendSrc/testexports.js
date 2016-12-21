/// <reference path="serverexports.ts" />
function TestExports() {
    ExampleBackend.ExampleExampleParameterlessMethod(function (modelv1) {
        console.log(modelv1);
        ExampleBackend.ExampleExampleOneParameterMethod(modelv1, function (modelv2) {
            console.log(modelv2);
        }, console.log);
    }, console.log);
}
//# sourceMappingURL=testexports.js.map