using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text;

namespace Mockaccino
{
    public record MockRequestFilter
    {
        public MockRequestFilterSource From { get; set; }
        public string ApplyOn { get; set; } = null!;
        public string Value { get; set; } = null!;
        public bool IsPattern { get; set; }

        public override string ToString()
        {
            var contentStringBuilder = new StringBuilder();

            contentStringBuilder.AppendLine(From switch
            {
                MockRequestFilterSource.QueryParams => FilterByQuery(),
                MockRequestFilterSource.Route => FilterByRoute(),
                MockRequestFilterSource.Body => FilterByBody(),
                MockRequestFilterSource.Headers => FilterByHeader(),
                _ => string.Empty
            });

            return contentStringBuilder.ToString();
        }

        internal bool IsMatch(HttpContext httpContext)
        {
            return From switch
            {
                MockRequestFilterSource.QueryParams => httpContext.Request.Query.QueryFilterMatches(ApplyOn, Value, IsPattern),
                MockRequestFilterSource.Route => httpContext.GetRouteData().RouteFilterMatches(ApplyOn, Value, IsPattern),
                MockRequestFilterSource.Headers => httpContext.Request.Headers.HeaderFilterMatches(ApplyOn, Value, IsPattern),
                MockRequestFilterSource.Body => httpContext.Request.Body.BodyFilterMatches(ApplyOn, Value, IsPattern),
                _ => false
            };
        }

        private string FilterByRoute()
        {
            return $@"
            if (Request.HttpContext.GetRouteData().RouteFilterMatches(""{ApplyOn}"", ""{Value}"", isPattern: {(IsPattern ? "true" : "false")}))
            {{
                {{returnSentence}}
            }}";
        }

        private string FilterByQuery()
        {
            return $@"
            if (Request.Query.QueryFilterMatches(""{ApplyOn}"", ""{Value}"", isPattern: {(IsPattern ? "true" : "false")}))
            {{
                {{returnSentence}}
            }}";
        }

        private string FilterByHeader()
        {
            return $@"
            if (Request.Headers.HeaderFilterMatches(""{ApplyOn}"", ""{Value}"", isPattern: {(IsPattern ? "true" : "false")}))
            {{
                {{returnSentence}}
            }}";
        }

        private string FilterByBody()
        {
            return $@"
            if (Request.Body.BodyFilterMatches(""{ApplyOn}"", ""{Value}"", isPattern: {(IsPattern ? "true" : "false")}))
            {{
                {{returnSentence}}
            }}";
        }
    }
}