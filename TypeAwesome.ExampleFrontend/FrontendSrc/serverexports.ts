/// <reference path="../typings/index.d.ts" />

namespace ExampleBackend {

export interface IMethodInfo<TParam, TReturn> {
    url: string;
}

export function CallMethod<TParam, TReturn>(methodInfo: IMethodInfo<TParam, TReturn>, parameter: TParam, onSuccess : (TReturn) => void, onError : (xhr: JQueryXHR) => void) {
    $.ajax({
		type: 'POST',
		contentType: "application/json; charset=utf-8",
        dataType: 'json',
        url: methodInfo.url,
        data: JSON.stringify(parameter)
    }).then(onSuccess,onError);
}

export function CallParameterlessMethod<TReturn>(methodInfo: IMethodInfo<void, TReturn>, onSuccess: (TReturn) => void, onError: (xhr: JQueryXHR) => void) {
    $.ajax({
        url: methodInfo.url
    }).then(onSuccess, onError);
}

export interface IExampleModel1 {
  Amount : number;
  Desciption : string;
}

export interface IExampleModel2 {
  Order : IExampleModel1[];
  CustomerName : string;
}

var ExampleExampleParameterlessMethodMethodInfo: IMethodInfo<void, IExampleModel1> = {
    url: "/Example/ExampleParameterlessMethod"
}

var ExampleExampleOneParameterMethodMethodInfo: IMethodInfo<IExampleModel1, IExampleModel2> = {
    url: "/Example/ExampleOneParameterMethod"
}

export function ExampleExampleOneParameterMethod(inputModel: IExampleModel1, onSuccess: (model: IExampleModel2) => void, onError: (xhr: JQueryXHR) => void) {
    CallMethod(ExampleExampleOneParameterMethodMethodInfo, inputModel, onSuccess, onError);
}

export function ExampleExampleParameterlessMethod(onSuccess: (model: IExampleModel1) => void, onError: (xhr: JQueryXHR) => void) {
    CallParameterlessMethod(ExampleExampleParameterlessMethodMethodInfo, onSuccess, onError);
}

}