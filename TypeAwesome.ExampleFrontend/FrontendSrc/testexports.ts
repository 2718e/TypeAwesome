/// <reference path="serverexports.ts" />

function TestExports() {
    ExampleBackend.ExampleExampleParameterlessMethod(modelv1 => {
        console.log(modelv1);
        ExampleBackend.ExampleExampleOneParameterMethod(modelv1, modelv2 => {
            console.log(modelv2);
        }, console.log);
    }, console.log );
}