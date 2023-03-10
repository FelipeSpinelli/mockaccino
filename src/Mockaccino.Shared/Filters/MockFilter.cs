using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace Mockaccino
{
    internal class MockFilter : ActionFilterAttribute
    {
        private static readonly Mock[]? _mocks;

        static MockFilter()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "mockaccina.settings.json");
            if (!File.Exists(filePath))
            {
                return;
            }

            _mocks = JsonConvert.DeserializeObject<Mock[]>(File.ReadAllText(filePath));
        }

        public string MockName { get; set; } = null!;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Request.EnableBuffering();

            var mock = GetMocks().FirstOrDefault(x => x.Name == MockName);

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

        private static Mock[] GetMocks() => _mocks ?? Array.Empty<Mock>();
    }
}
