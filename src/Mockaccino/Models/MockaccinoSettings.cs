using System;

namespace Mockaccino
{
    public class MockaccinoSettings
    {
        public const string SECTION = "MockaccinoSettings";

        public bool Enabled { get; set; }
        public Mock[] Mocks { get; set; } = Array.Empty<Mock>();
    }
}