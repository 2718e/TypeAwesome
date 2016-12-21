using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TypeAwesome
{


    [Serializable]
    public class NotExportableException : Exception
    {
        public NotExportableException() { }
        public NotExportableException(string message) : base(message) { }
        public NotExportableException(string message, Exception inner) : base(message, inner) { }
        protected NotExportableException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public class TypescriptExportBuilder
    {

        private List<Type> modelClasses;
        private List<MethodInfo> actionMethods;
        private StringBuilder exportBuilder;


        public TypescriptExportBuilder()
        {
            this.modelClasses = new List<Type>();
            this.actionMethods = new List<MethodInfo>();
            this.exportBuilder = new StringBuilder();
        }

        public void AddReference(string path)
        {
            AddTypescript($"/// <reference path=\"{path}\" />\r\n\r\n");
        }

        public void AddTypingsReference(string relativepathFromOutputDirToWebProjectRoot)
        {
            AddReference($"{relativepathFromOutputDirToWebProjectRoot}/typings/index.d.ts");
        }

        public void IncludeAssembly(Assembly inServerAssembly) {
            var resultBuilder = new StringBuilder();
            var types = inServerAssembly.ExportedTypes.ToList();
            var myModelClasses = types.Where(typeInfo => typeof(ITypedJModel).IsAssignableFrom(typeInfo)).ToList();
            if (myModelClasses.Any(inClass=> inClass.IsGenericType))
            {
                throw new NotExportableException("Models with Generic Parameters are not supported"); 
            }
            var myControllerClasses = types.Where(typeInfo => typeInfo.IsSubclassOf(typeof(Controller))).ToList();
            var myMethodsToExport = myControllerClasses.SelectMany(inController => inController.GetMethods().Where(methodInfo => typeof(ITypedJsonResult).IsAssignableFrom(methodInfo.ReturnType))).ToList();
            this.modelClasses.AddRange(myModelClasses);
            this.actionMethods.AddRange(myMethodsToExport);
        }

        public void OpenNamespace(string inNamespaceName)
        {
            this.AddTypescript(String.Format("namespace {0} {1}\r\n\r\n", inNamespaceName, "{"));
        }

        public void CloseNamespace()
        {
            this.AddTypescript("}");
        }

        /// <summary>
        /// Converts A C# type, as found in the property of a model class to a typescript type
        /// </summary>
        /// <param name="cSharpType">the type of</param>
        /// <param name="inModelNameTemplate"></param>
        /// <returns></returns>
        private string resolvePropertyType(Type cSharpType)
        {
            var result = "";
            // possible improvement, a lookup table may be better than a chain of if elses.
            if (cSharpType.IsPrimitive)
            {
                if (cSharpType == typeof(bool))
                {
                    result = "boolean";
                } else if (cSharpType == typeof(char))
                {
                    result = "string";
                } else
                {
                    result = "number";
                }
            } else if (cSharpType == typeof(string))
            {
                result = "string";
            }
            else if (cSharpType == typeof(decimal))
            {
                result = "number";
            } else if (typeof(ITypedJModel).IsAssignableFrom(cSharpType))
            {
                result = $"I{cSharpType.Name}";
            } else if (cSharpType.IsArray) { //should possibly allow ICollection as well, could also have a warning about .net serializing empty arrays to null.
                var typeofArray = resolvePropertyType(cSharpType.GetElementType());
                result = $"{typeofArray}[]";
            } else
            {
                result = "any";
                // delete or comment the line below to allow non exportable types to just be considered typescripts any when exported
                throw new NotExportableException($"encountered invalid propert type {cSharpType.Name}. All exported models must implement ITypedJModel, " +
                    "and must only have public properties that are primitives or themselves classes that implement ITypedJModel, or be arrays " +
                    "of a type that would be a valid public property");
            }
            return result;
        }

        /// <summary>
        /// Appends script as-is to the export, useful for setting variables.
        /// </summary>
        /// <param name="inScript">The script to add Verbatim to the export</param>
        public void AddTypescript(string inScript)
        {
            this.exportBuilder.Append(inScript);
        }
        
        /// <summary>
        /// Generates an interface for each model in the assemblies.
        /// </summary>
        public void GenerateModelInterfaces()
        {
            foreach (var model in this.modelClasses)
            {
                var typeName = this.resolvePropertyType(model);
                exportBuilder.AppendFormat("export interface {0} {1}", typeName, "{\r\n");
                var properties = model.GetProperties().ToList();
                foreach (var property in properties)
                {
                    var propertyTypeName = resolvePropertyType(property.PropertyType);
                    var propertyName = property.Name;
                    exportBuilder.Append($"  {propertyName} : {propertyTypeName};\r\n");
                }
                exportBuilder.Append("}\r\n\r\n");
            }
        }

        /// <summary>
        /// when putting values into a template, all instances of the substring {CONTROLLERKEY} will be replaced with the controller name declaring the action method.
        /// The word "Controller" will be removed from the Controller name before replacing, e.g. "ExampleController" would mean {CONTROLLERKEY} would be replaced by
        /// "Example"
        /// </summary>
        public static readonly string CONTROLLERKEY = "CONTROLLER";

        /// <summary>
        /// when putting values into a template, all instances of the substring {ACTIONKEY} will be replaced with the name of the action method, e.g for a method
        /// TypedJsonNetResult<ExampleModel2> ExampleAction(ExampleModel model) , {ACTIONKEY} will be replaced by "ExampleAction"
        /// </summary>
        public static readonly string ACTIONKEY = "ACTION";

        /// <summary>
        /// When putting values into a template, all instances of the substring {PARAMETERNAMEKEY} will be replaced by the name of the parameter to the action method, e.g.
        /// for a method TypedJsonNetResult<ExampleModel2> ExampleAction(ExampleModel model), {PARAMETERNAMEKEY} will be replaced by "model"
        /// </summary>
        public static readonly string PARAMETERNAMEKEY = "PARAMETERNAME";

        /// <summary>
        /// When putting values into a template, all instances of the substring {PARAMETERTYPEKEY} will be replaced by the type of the parameter to the action method, e.g.
        /// for a method TypedJsonNetResult<ExampleModel2> ExampleAction(ExampleModel model), {PARAMETERTYPEKEY} will be replaced by "ExampleModel"
        /// </summary>
        public static readonly string PARAMETERTYPEKEY = "PARAMETERTYPE";

        /// <summary>
        /// When putting values into a template, all instances of the substring {RETURNTYPEKEY} will be replaced by the type of the return value of the action method, e.g.
        /// for a method TypedJsonNetResult<ExampleModel2> ExampleAction(ExampleModel model), {RETURNTYPEKEY} will be replaced by "ExampleModel2"
        /// </summary>
        public static readonly string RETURNTYPEKEY = "RETURNTYPE";

        /// <summary>
        /// In the template, replaces the keys defined above with values determined by reflection over actionMethod
        /// </summary>
        /// <param name="actionMethod">The Action Method to generate typescript code based on</param>
        /// <param name="template">The template to replace keys in in order to generate the typescript code</param>
        /// <returns>A string that is the result of replacing the keys in template</returns>
        private string ReplaceTemplate(MethodInfo actionMethod, string template)
        {
            string controller = actionMethod.DeclaringType.Name.Replace("Controller", "");
            string action = actionMethod.Name;
            string parameterType = "void";
            string parameterName = "parameter";
            string returnType = resolvePropertyType(actionMethod.ReturnType.GetGenericArguments()[0]);
            var parameters = actionMethod.GetParameters().ToList();
            if (parameters.Count() > 1)
            {
                throw new NotExportableException("Only action methods with no parameters or exactly one parameter are supported, if you have a method with multiple parameters that you want to export, consider refactoring it to take an ITypedJModel containing fields for each parameter, then passing that model type.");
            }
            if (parameters.Count() == 1)
            {
                parameterType = resolvePropertyType(parameters[0].ParameterType);
                parameterName = parameters[0].Name;
            }
            string result = template.Replace(CONTROLLERKEY, controller)
                .Replace(ACTIONKEY, action)
                .Replace(PARAMETERNAMEKEY, parameterName)
                .Replace(PARAMETERTYPEKEY, parameterType)
                .Replace(RETURNTYPEKEY, returnType);
            return result;
        }

        public void GenerateForOneParameterActionMethods(string inTemplate)
        {
            this.actionMethods.Where(methodInfo => methodInfo.GetParameters().Count() == 1)
                .Select(methodInfo => ReplaceTemplate(methodInfo, inTemplate)).ToList()
                .ForEach(generatedCode => exportBuilder.Append(generatedCode));
        }

        public void GenerateForParameterlessActionMethods(string inTemplate)
        {
            this.actionMethods.Where(methodInfo => methodInfo.GetParameters().Count() == 0)
                .Select(methodInfo => ReplaceTemplate(methodInfo, inTemplate)).ToList()
                .ForEach(generatedCode => exportBuilder.Append(generatedCode));
        }

        /// <summary>
        /// Generates code from a given template for all action methods - if a template contains {PARAMETERTYPEKEY} or {PARAMETERNAMEKEY},
        /// these will be taken to be "void" and "parameter" respectively for those methods with no parameter
        /// </summary>
        /// <param name="inTemplate">the template for </param>
        public void GenerateForAllActionMethods(string inTemplate)
        {
            this.actionMethods
                .Select(methodInfo => ReplaceTemplate(methodInfo, inTemplate)).ToList()
                .ForEach(generatedCode => exportBuilder.Append(generatedCode));
        }

        public string GetExport()
        {
            return this.exportBuilder.ToString();
        }

        public void BuildDefaultExport(Assembly assemblyToExport, string namespaceName)
        {
            OpenNamespace(namespaceName);
            IncludeAssembly(assemblyToExport);
            AddTypescript(File.ReadAllText(Path.Combine(".", "TypeAwesomeFiles", "defaultincludes.tstemplate")));
            GenerateModelInterfaces();
            GenerateForAllActionMethods(File.ReadAllText(Path.Combine(".", "TypeAwesomeFiles", "defaultallactions.tstemplate")));
            GenerateForOneParameterActionMethods(File.ReadAllText(Path.Combine(".", "TypeAwesomeFiles", "defaultoneparameteractions.tstemplate")));
            GenerateForParameterlessActionMethods(File.ReadAllText(Path.Combine(".", "TypeAwesomeFiles", "defaultparameterlessactions.tstemplate")));
            CloseNamespace();
        }


    }
}
