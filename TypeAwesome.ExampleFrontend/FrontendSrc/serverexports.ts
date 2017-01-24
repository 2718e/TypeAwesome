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
}

export interface ISimpleModel {
  Name : string;
  Amount : number;
}

export interface INestedArrayModel {
  Values : number[][][];
}

export interface ITestModel {
  NestedArray : INestedArrayModel;
  Model : ISimpleModel;
  Pets : string[];
  Food : string;
  LargeNumber : number;
  HowMany : number;
  Buoyancy : number;
  Measurement : number;
  SmallNumber : number;
  Letter : string;
  ALotOfStuff : number;
}

var TestParameterlessMethodMethodInfo: IMethodInfo<void, ISimpleModel> = {
    url: "/Test/ParameterlessMethod"
}

var TestOneParameterMethodMethodInfo: IMethodInfo<ISimpleModel, ITestModel> = {
    url: "/Test/OneParameterMethod"
}

export function TestOneParameterMethod(inputModel: ISimpleModel) {
    return CallMethod(TestOneParameterMethodMethodInfo, inputModel);
}

export function TestParameterlessMethod() {
    return CallParameterlessMethod(TestParameterlessMethodMethodInfo);
}

}