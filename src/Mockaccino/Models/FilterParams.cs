using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Mockaccino.Models
{
    internal record FilterParams
    {
        public RouteData? RouteData { get; set; }
        public IQueryCollection? QueryParams { get; set; }
        public IHeaderDictionary? Headers { get; set; }
        public object? Body { get; set; }
    }
}
