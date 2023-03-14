using System;

namespace Mockaccino
{
    public record Mock
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public HttpMethod Method { get; set; }
        public MockResponse[] Responses { get; set; } = Array.Empty<MockResponse>();
    }
}