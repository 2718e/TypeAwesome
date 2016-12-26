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
    var ExampleParameterlessMethodMethodInfo = {
        url: "/Example/ParameterlessMethod"
    };
    var ExampleOneParameterMethodMethodInfo = {
        url: "/Example/OneParameterMethod"
    };
    function ExampleOneParameterMethod(inputModel) {
        return CallMethod(ExampleOneParameterMethodMethodInfo, inputModel);
    }
    ExampleBackend.ExampleOneParameterMethod = ExampleOneParameterMethod;
    function ExampleParameterlessMethod() {
        return CallParameterlessMethod(ExampleParameterlessMethodMethodInfo);
    }
    ExampleBackend.ExampleParameterlessMethod = ExampleParameterlessMethod;
})(ExampleBackend || (ExampleBackend = {}));
//# sourceMappingURL=serverexports.js.map