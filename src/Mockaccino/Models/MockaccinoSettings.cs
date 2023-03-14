using System;

namespace Mockaccino
{
    public record MockaccinoSettings
    {
        public const string SECTION = "MockaccinoSettings";

        public bool Enable { get; set; }
        public Mock[] Mocks { get; set; } = Array.Empty<Mock>();
    }
}