using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Mockaccino.Models;
using Newtonsoft.Json;
using System.Linq;

namespace Mockaccino
{
    public class MockFilterAttribute : ActionFilterAttribute
    {
        private static MockaccinoSettings _settings = new();

        public string MockName { get; set; } = null!;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var filterParams = new FilterParams
            {
                QueryParams = context.HttpContext.Request.Query,
                RouteData = context.HttpContext.GetRouteData(),
                Headers = context.HttpContext.Request.Headers,
                Body = GetBodyContent(context)
            };

            var mock = _settings.Mocks.FirstOrDefault(x => x.Name == MockName);

            if (mock == null)
            {
                return;
            }

            var response = mock.Responses
                .OrderBy(x => x.Priority)
                .FirstOrDefault(x => x.Filter?.IsMatch(filterParams) ?? true);

            if (response == null)
            {
                return;
            }

            context.Result = new ObjectResult(JsonConvert.DeserializeObject(response.Content))
            {
                StatusCode = (int)response.StatusCode
            };
        }

        public static void Configure(MockaccinoSettings settings) => _settings = settings;

        private object? GetBodyContent(ActionExecutingContext context)
        {
            if (!(context.ActionArguments?.Any() ?? false) || !(context.ActionDescriptor.Parameters?.Any() ?? false))
            {
                return null;
            }

            foreach(var actionParameter in context.ActionDescriptor.Parameters)
            {
                if (!actionParameter.BindingInfo.BindingSource.IsFromRequest)
                {
                    continue;
                }

                if (!actionParameter.BindingInfo.BindingSource.CanAcceptDataFrom(BindingSource.Body))
                {
                    continue;
                }

                return context.ActionArguments[actionParameter.Name];
            }

            return null;
        }
    }
}
