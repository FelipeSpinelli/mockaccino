using Mockaccino.Models;

namespace Mockaccino
{
    public class MockRequestFilter
    {
        public MockRequestFilterSource From { get; set; }
        public string ApplyOn { get; set; } = null!;
        public string Value { get; set; } = null!;
        public bool IsPattern { get; set; }

        internal bool IsMatch(FilterParams filterParams)
        {
            return From switch
            {
                MockRequestFilterSource.QueryParams => filterParams.QueryParams?.QueryFilterMatches(ApplyOn, Value, IsPattern) ?? false,
                MockRequestFilterSource.Route => filterParams.RouteData?.RouteFilterMatches(ApplyOn, Value, IsPattern) ?? false,
                MockRequestFilterSource.Headers => filterParams.Headers?.HeaderFilterMatches(ApplyOn, Value, IsPattern) ?? false,
                MockRequestFilterSource.Body => filterParams.Body?.BodyFilterMatches(ApplyOn, Value, IsPattern) ?? false,
                _ => false
            };
        }
    }
}