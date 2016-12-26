/// <reference path="serverexports.ts" />
function TestExports() {
    ExampleBackend.ExampleParameterlessMethod().then(modelv1 => {
        console.log(modelv1);
        return ExampleBackend.ExampleOneParameterMethod(modelv1);
    }, console.log).then(modelv2 => {
        console.log(modelv2);
    }, console.log);
}
//# sourceMappingURL=testexports.js.map