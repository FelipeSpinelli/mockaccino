using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Mockaccino
{
    public record MockRequestFilter
    {
        public MockRequestFilterSource From { get; set; }
        public string ApplyOn { get; set; } = null!;
        public string Value { get; set; } = null!;
        public bool IsPattern { get; set; }

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
    }
}