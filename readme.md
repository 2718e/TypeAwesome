TypeAwesome
===========

TypeAwesome is a small C# program which uses the C# reflection feature to export .net classes and .net MVC controller action methods to a typescript file

The repository contains a sample solution based on slight modifications to one of the default MVC templates built into visual studio.

## License

Licensed under [MIT License](https://opensource.org/licenses/MIT).

Note though that the third party software used by this project may have different licenses, and it is your responsibilty to find out what they are and make sure you comply with them.

## Project structure

The main project is the "TypeAwesome" assembly. The other assemblies are there in order to allow basic testing of it, and to show one method for integrating it into a build process.

# Installation

to use this in a .net MVC solution, do the following steps:

1. add the TypeAwesome project to your solution.
2. Move the controllers and models which you want TypeAwesome to operate on to an assembly separate from the Web Project. (here, the model/controller assembly is TypeAwesome.TestAssembly and the web project is TypeAwesome.ExampleFrontend)
3. make the model/controller assembly reference TypeAwesome - then make the models implement ITypedJModel and the controller methods return TypedJsonNetResult<{Model type}>
4. make the Web Project reference the model/controller assembly.
5. Come up with some way to make TypeAwesome run on the model/controller assembly and output into the Web Project during the build process. Here, this is done by a post build event on the TypeAwesome.Runner project.

## Notes

Note: This project 
* does not intend to support all methods of serialization and server calls. 
* does not support any method of mapping Controllers to routes besides the MVC default. 
* limits controller methods to be one parameter or no parameters.
* does not offer any method of customization besides altering the code directly.

Ran without modification, the generated methods operate by: 
* serializing using JSON.stringify,
* making server calls using jquery.ajax
* returning a native javascript promise object that wraps the jquery.ajax call.

# Possible Issues

1. The build process in this example uses a command line post build event to run the exporter - this may cause problems if the project path on disk contains spaces. 
2. While converting the C# decimal type to the typescript number type does technically work, there is precision loss. Support for decimal may be removed in a later version
as precision loss seems to violate the intended purpose of the decimal type.
