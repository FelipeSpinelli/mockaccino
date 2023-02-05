using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MrMime.Models
{
    internal record MockResponse
    {
        public int Priority { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public object Content { get; set; } = new();
        public MockRequestFilter? Filter { get; set; }

        public override string ToString()
        {
            var contentStringBuilder = new StringBuilder();

            var serializedContent = JsonConvert.SerializeObject(Content, Formatting.None)
                .Replace(@"""", @"\""");
            var returnSentence = @$"
            return StatusCode({(int)StatusCode}, JsonConvert.DeserializeObject<object>(""{serializedContent}""));";

            contentStringBuilder.AppendLine(Filter.GetConditionWith(returnSentence));

            return contentStringBuilder.ToString();
        }
    }
}