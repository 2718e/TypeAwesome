/// <reference path="../typings/index.d.ts" />

namespace ExampleBackend {

export interface IMethodInfo<TParam, TReturn> {
    url: string;
}

export function CallMethod<TParam, TReturn>(methodInfo: IMethodInfo<TParam, TReturn>, parameter: TParam) : Promise<TReturn> {
    var result = new Promise<TReturn>((resolve, reject) => {
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

export function CallParameterlessMethod<TReturn>(methodInfo: IMethodInfo<void, TReturn>) : Promise<TReturn> {
    var result = new Promise<TReturn>((resolve, reject) => {
        $.ajax({
            url: methodInfo.url
        }).then(resolve, reject);
    });
    return result
}export interface IExampleModel1 {
  Amount : number;
  Desciption : string;
}

export interface IExampleModel2 {
  Order : IExampleModel1[];
  CustomerName : string;
}

var ExampleParameterlessMethodMethodInfo: IMethodInfo<void, IExampleModel1> = {
    url: "/Example/ParameterlessMethod"
}

var ExampleOneParameterMethodMethodInfo: IMethodInfo<IExampleModel1, IExampleModel2> = {
    url: "/Example/OneParameterMethod"
}

export function ExampleOneParameterMethod(inputModel: IExampleModel1) {
    return CallMethod(ExampleOneParameterMethodMethodInfo, inputModel);
}

export function ExampleParameterlessMethod() {
    return CallParameterlessMethod(ExampleParameterlessMethodMethodInfo);
}

}