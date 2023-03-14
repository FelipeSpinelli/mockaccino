using System;

namespace Mockaccino
{
    public class Mock
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public HttpMethod Method { get; set; }
        public MockResponse[] Responses { get; set; } = Array.Empty<MockResponse>();
    }
}