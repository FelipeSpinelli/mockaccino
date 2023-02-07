using System.Text;

namespace Mockaccino.Models
{
    internal record MockRequestFilter
    {
        public MockRequestFilterSource From { get; set; }
        public string ApplyOn { get; set; } = null!;
        public object WhenEqualsTo { get; set; } = new();

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

        private string FilterByRoute()
        {
            return $@"
            if (RouteFilterMatches(""{ApplyOn}"", ""{WhenEqualsTo}""))
            {{
    {{returnSentence}}
            }}";
        }

        private string FilterByQuery()
        {
            return $@"
            if (QueryFilterMatches(""{ApplyOn}"", ""{WhenEqualsTo}""))
            {{
    {{returnSentence}}
            }}";
        }

        private string FilterByHeader()
        {
            return $@"
            if (HeaderFilterMatches(""{ApplyOn}"", ""{WhenEqualsTo}""))
            {{
    {{returnSentence}}
            }}";
        }

        private string FilterByBody()
        {
            return $@"
            if (BodyFilterMatches(body, ""{ApplyOn}"", ""{WhenEqualsTo}""))
            {{
    {{returnSentence}}
            }}";
        }
    }
}