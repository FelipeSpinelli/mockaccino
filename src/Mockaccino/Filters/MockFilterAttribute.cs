using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Mockaccino
{
    public class MockFilterAttribute : ActionFilterAttribute
    {
        private static MockaccinoSettings _settings = new();

        public string MockName { get; set; } = null!;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Request.EnableBuffering();

            var mock = _settings.Mocks.FirstOrDefault(x => x.Name == MockName);

            if (mock == null)
            {
                return;
            }

            var response = mock.Responses
                .OrderBy(x => x.Priority)
                .FirstOrDefault(x => x.Filter?.IsMatch(context.HttpContext) ?? true);

            if (response == null)
            {
                return;
            }

            context.Result = new ObjectResult(response.Content)
            {
                StatusCode = (int)response.StatusCode
            };
        }

        public static void Configure(MockaccinoSettings settings) => _settings = settings;
    }
}
