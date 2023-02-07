using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Mockaccino.Models;
using Scriban;
using System;
#if DEBUG
using System.Diagnostics;
#endif
using System.Linq;
using System.Text;

namespace Mockaccino
{
    [Generator]
    public class ApiClientSourceGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
//#if DEBUG
//            Debugger.Launch();
//#endif
        }

        public void Execute(GeneratorExecutionContext context)
        {
            const string JSON_FILE = ".json";

            var @namespace = context.Compilation?.AssemblyName ?? "AutoApiClient";

            var jsonFile = context.AdditionalFiles
                .Where(f => f.Path.EndsWith(JSON_FILE, StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault();

            if (jsonFile == null)
            {
                return;
            }

            var jsonContent = jsonFile.GetText(context.CancellationToken)?.ToString();

            if (jsonContent == null)
            {
                return;
            }

            context.AddSource($"{@namespace}ApiClient.g.cs", SourceText.From($"namespace {@namespace} {{ public partial class X {{ }} }}", Encoding.UTF8));
        }

        static string GetContent(string @namespace, Mock[] mocks)
        {
            var template = Template.Parse(CodeBoilerplates.MockController);
            return template.Render(new { Namespace = @namespace, Mocks = mocks });
        }
    }
}