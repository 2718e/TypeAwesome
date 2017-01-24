/// <reference path="../typings/index.d.ts" />
var ExampleBackend;
(function (ExampleBackend) {
    function CallMethod(methodInfo, parameter) {
        var result = new Promise((resolve, reject) => {
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: methodInfo.url,
                data: JSON.stringify(parameter)
            }).then(resolve, reject);
        });
        return result;
        //.then(onSuccess,onError);
    }
    ExampleBackend.CallMethod = CallMethod;
    function CallParameterlessMethod(methodInfo) {
        var result = new Promise((resolve, reject) => {
            $.ajax({
                url: methodInfo.url
            }).then(resolve, reject);
        });
        return result;
    }
    ExampleBackend.CallParameterlessMethod = CallParameterlessMethod;
    var TestParameterlessMethodMethodInfo = {
        url: "/Test/ParameterlessMethod"
    };
    var TestOneParameterMethodMethodInfo = {
        url: "/Test/OneParameterMethod"
    };
    function TestOneParameterMethod(inputModel) {
        return CallMethod(TestOneParameterMethodMethodInfo, inputModel);
    }
    ExampleBackend.TestOneParameterMethod = TestOneParameterMethod;
    function TestParameterlessMethod() {
        return CallParameterlessMethod(TestParameterlessMethodMethodInfo);
    }
    ExampleBackend.TestParameterlessMethod = TestParameterlessMethod;
})(ExampleBackend || (ExampleBackend = {}));
//# sourceMappingURL=serverexports.js.map