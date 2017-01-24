/// <reference path="serverexports.ts" />

// test whether or not the generated methods actually work.
function TestExports() {
    ExampleBackend.TestParameterlessMethod().then(simpleModel => {
        console.log(simpleModel);
        return ExampleBackend.TestOneParameterMethod(simpleModel)
    }, console.log).then(testModel => {
        console.log(testModel);
        console.assert(testModel.ALotOfStuff === 12345678910, "Test for long failed");
        console.assert(testModel.Food === "Bananas", "Test for string failed");
        console.assert(testModel.Pets[1] === "dog", "Test for array failed");
        console.assert(testModel.SmallNumber === 10, "Test for byte failed");
        console.assert(testModel.HowMany === 12345, "Test for int failed");
        console.assert(testModel.LargeNumber === 123456789.101112131415161718, "Test for decimal failed");
        console.assert(testModel.Letter === 'A', "Test for char failed");
        console.assert(testModel.Buoyancy === 12.345, "Test for float failed" );
        console.assert(testModel.Measurement === 1.38064852e-23, "Test for double failed");
        console.assert(testModel.Model.Amount === 9001, "Test of model within model failed");
        console.assert(testModel.NestedArray.Values[1][1][1] === 8.8, "Test of nested arrays failed");
        }, console.log);
}