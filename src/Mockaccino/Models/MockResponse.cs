using System.Net;

namespace Mockaccino
{
    public record MockResponse
    {
        public int Priority { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public object Content { get; set; } = new();
        public MockRequestFilter? Filter { get; set; }
    }
}