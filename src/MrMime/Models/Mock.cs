using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrMime.Models
{
    internal record Mock
    {
        private readonly HttpMethod[] _withBodyHttpMethods = new HttpMethod[]
        { 
            HttpMethod.Post,
            HttpMethod.Put,
            HttpMethod.Patch,
            HttpMethod.Delete
        }; 

        public string Route { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public HttpMethod Method { get; set; }
        public MockResponse[] Responses { get; set; } = null!;

        public override string ToString()
        {
            const string FROMBODY_PARAMETERS = "[FromBody]object? body";
            var contentStringBuilder = new StringBuilder();

            contentStringBuilder.AppendLine($@"
                [Http{Method}(""{Route}"")]
                [Produces(MediaTypeNames.Application.Json)]
                public IActionResult {Name}({(_withBodyHttpMethods.Contains(Method) ? FROMBODY_PARAMETERS : string.Empty)})
                {{
                    /*
                        {Description}
                    */");

            foreach (var response in Responses.OrderBy(x => x.Priority))
            {
                contentStringBuilder.AppendLine(response.ToString());
            }

            contentStringBuilder.AppendLine($@"
                }}");

            return contentStringBuilder.ToString();
        }
    }
}