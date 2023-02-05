using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using MrMime.Models;
using Newtonsoft.Json;
using Scriban;
using System;
//#if DEBUG
//using System.Diagnostics;
//#endif
using System.Linq;
using System.Text;

namespace MrMime
{
    [Generator]
    public class MrMimeSourceGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
//#if DEBUG
//            Debugger.Launch();
//#endif
        }

        public void Execute(GeneratorExecutionContext context)
        {
            const string MRMIME_SETTINGS_FILENAME = "mr-mime.settings.json";

            var @namespace = context.Compilation?.AssemblyName ?? "MrMime";

            var mrMimeSettingsJsonFile = context.AdditionalFiles
                .Where(f => f.Path.EndsWith(MRMIME_SETTINGS_FILENAME, StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault();

            if (mrMimeSettingsJsonFile == null)
            {
                return;
            }

            var jsonContent = mrMimeSettingsJsonFile.GetText(context.CancellationToken)?.ToString();

            if (jsonContent == null)
            {
                return;
            }

            var mocks = JsonConvert.DeserializeObject<Mock[]>(jsonContent) ?? Array.Empty<Mock>();

            var content = GetContent(@namespace, mocks);

            context.AddSource("MockController.g.cs", SourceText.From(content, Encoding.UTF8));
        }

        static string GetContent(string @namespace, Mock[] mocks)
        {
            var template = Template.Parse(CodeBoilerplates.MockController);
            return template.Render(new { Namespace = @namespace, Mocks = mocks });
        }
    }
}