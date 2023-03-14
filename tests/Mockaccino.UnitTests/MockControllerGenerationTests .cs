using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

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
        }

        private static Func<HttpClient?, Task<HttpResponseMessage>> GetResourceById200()
        {
            return (client) => client.GetAsync("/resource/1");
        }
    }
}