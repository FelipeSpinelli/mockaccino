using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Text.Json;

namespace Mockaccino.UnitTests
{
    public class MockControllerGenerationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public MockControllerGenerationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [MemberData(nameof(GetMockedRequests))]
        public async Task Mock_GivenAMockedRoute_ShouldResponseAsExpected(Func<HttpClient?, Task<HttpResponseMessage>> request, int expectedStatusCode, string expectedResponseContent)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await request(client);

            // Assert
            Assert.Equal((int)response.StatusCode, expectedStatusCode);
            Assert.Equal(await response.Content.ReadAsStringAsync(), expectedResponseContent);
        }

        public static IEnumerable<object[]> GetMockedRequests()
        {
            yield return new object[] { GetResourceById200(), 200, JsonConvert.SerializeObject(new { Name = "Successfull scenario", Age = 32 }) };
            yield return new object[] { GetResourceById404(), 404, JsonConvert.SerializeObject(new[] { new { Message = "Resource not found" } }) };
            yield return new object[] { CreateResource200(), 200, JsonConvert.SerializeObject(new { Id = "3213s2ads324d8a8wdas2d31" }) };
            yield return new object[] { CreateResource400(), 400, JsonConvert.SerializeObject(new[] { new { Message = "Name cannot be empty" } }) };
        }

        private static Func<HttpClient?, Task<HttpResponseMessage>> GetResourceById200()
        {
            return (client) => client.GetAsync("/resource/1");
        }

        private static Func<HttpClient?, Task<HttpResponseMessage>> GetResourceById404()
        {
            return (client) => client.GetAsync("/resource/2");
        }

        private static Func<HttpClient?, Task<HttpResponseMessage>> CreateResource200()
        {
            return (client) => client.PostAsJsonAsync("/resource", new { Name = "Teste" });
        }

        private static Func<HttpClient?, Task<HttpResponseMessage>> CreateResource400()
        {
            return (client) => client.PostAsJsonAsync("/resource", new { Name = string.Empty });
        }
    }
}