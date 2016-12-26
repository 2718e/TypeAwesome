TypeAwesome
===========

TypeAwesome is a small C# program which uses the C# reflection feature to export .net classes and .net MVC controller action methods to a typescript file

The repository contains a sample solution based on slight modifications to one of the default MVC templates built into visual studio.

## Warnings and Notes

Note 1: This is intended much more as a demonstration than as a library. Or, to put it another way, if you want to use this you will probably want 
to customize it to your own needs, and the way to customize it is by modifying the source.

Note 2: This example uses but one of many ways of doing server calls and serialization. Specifically, the generated methods operate by: 
* serializing using JSON.stringify,
* making server calls using jquery.ajax
* returning a native javascript promise object that wraps the jquery.ajax call.

Warning 1: the build process in this example uses a command line post build event to run the exporter - this may cause problems if the project path on disk contains spaces. 

Warning 2: this project is not tested extensively.

## License

Licensed under [MIT License](https://opensource.org/licenses/MIT).

Note though that the third party software used by this project may have different licenses, and it is your responsibilty to find out what they are and make sure you comply with them.

## Description of projects

### TypeAwesome

This contains the TypescriptExportBuilder class which is the crux of the program. The rest of the projects in this solution are there to provide an example of it's use.

the "Contracts" file in this assembly contains interfaces that controllers and models should implement in order to be exported

### TypeAwesome.ExampleBackend

This contains controllers and models to be exported, The web project (TypeAwesome.ExampleFrontend) references this so as to use the controllers.

### TypeAwesome.Runner

This references TypeAwesome and TypeAwesome.ExampleBackend. It loads the TypeAwesome.ExampleBackend assembly, generates the exports, then writes those exports
to a location specified by a command argument. The project contains a post build event that writes the export to TypeAwesome.ExampleFrontend.

### TypeAwesome.ExampleFrontend

This is the modified MVC template project. It is configured to have a project dependency on TypeAwesomeRunner so that the post build event in TypeAwesomeRunner runs in order to
generate typescript from TypeAwesome.Backend before TypeAwesome.Frontend builds.

### TypeAwesome.Utils

Contains a very basic utility class for parsing command line arguments.