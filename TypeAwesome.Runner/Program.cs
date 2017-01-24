using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TypeAwesome;
using Utils;

namespace TypeAwesomeRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var commandParser = new CommandLineParser(args);
     
            var outPath = commandParser.GetOptionArgument("--out-path",Path.Combine(".","typeawesomeresults.ts"));
            var thingToExport = Assembly.Load("TypeAwesome.ExampleMethods");
            var exporter = new TypescriptExportBuilder();
            if (commandParser.HasOptionArgument("--out-dir-to-project-root-path"))
            {
                exporter.AddTypingsReference(commandParser.GetOptionArgument("--out-dir-to-project-root-path", "."));
            }
            exporter.BuildDefaultExport(thingToExport, "ExampleBackend");
            File.WriteAllText(outPath, exporter.GetExport());
        }
    }
}
