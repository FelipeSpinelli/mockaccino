using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mockaccino
{
    public static class MockaccinoServiceCollectionExtensions
    {
        public static IServiceCollection AddMockaccino(this IServiceCollection services, IConfiguration configuration)
        {
            var mockaccinoSettings = configuration.GetSection(MockaccinoSettings.SECTION).Get<MockaccinoSettings>();

            MockFilterAttribute.Configure(mockaccinoSettings);
            return services;
        }
    }
}
