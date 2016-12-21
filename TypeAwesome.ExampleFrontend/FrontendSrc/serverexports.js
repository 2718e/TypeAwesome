/// <reference path="../typings/index.d.ts" />
var ExampleBackend;
(function (ExampleBackend) {
    function CallMethod(methodInfo, parameter, onSuccess, onError) {
        $.ajax({
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            url: methodInfo.url,
            data: JSON.stringify(parameter)
        }).then(onSuccess, onError);
    }
    ExampleBackend.CallMethod = CallMethod;
    function CallParameterlessMethod(methodInfo, onSuccess, onError) {
        $.ajax({
            url: methodInfo.url
        }).then(onSuccess, onError);
    }
    ExampleBackend.CallParameterlessMethod = CallParameterlessMethod;
    var ExampleExampleParameterlessMethodMethodInfo = {
        url: "/Example/ExampleParameterlessMethod"
    };
    var ExampleExampleOneParameterMethodMethodInfo = {
        url: "/Example/ExampleOneParameterMethod"
    };
    function ExampleExampleOneParameterMethod(inputModel, onSuccess, onError) {
        CallMethod(ExampleExampleOneParameterMethodMethodInfo, inputModel, onSuccess, onError);
    }
    ExampleBackend.ExampleExampleOneParameterMethod = ExampleExampleOneParameterMethod;
    function ExampleExampleParameterlessMethod(onSuccess, onError) {
        CallParameterlessMethod(ExampleExampleParameterlessMethodMethodInfo, onSuccess, onError);
    }
    ExampleBackend.ExampleExampleParameterlessMethod = ExampleExampleParameterlessMethod;
})(ExampleBackend || (ExampleBackend = {}));
//# sourceMappingURL=serverexports.js.map