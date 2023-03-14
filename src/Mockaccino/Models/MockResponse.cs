using System.Net;

namespace Mockaccino
{
    public class MockResponse
    {
        public int Priority { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Content { get; set; } = "{}";
        public MockRequestFilter? Filter { get; set; }
    }
}