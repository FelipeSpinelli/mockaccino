using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Mockaccino
{
    internal static class FiltersExtensions
    {
        public static bool QueryFilterMatches(this IQueryCollection query, string queryFilterKey, string queryFilterValue, bool isPattern)
        {
            if (!query.ContainsKey(queryFilterKey))
            {
                return false;
            }

            return isPattern
                ? IsMatch(queryFilterValue, query[queryFilterKey].ToString())
                : query[queryFilterKey].ToString()
                    .Equals(queryFilterValue, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool RouteFilterMatches(this RouteData routeData, string routeFilterKey, string routeFilterValue, bool isPattern)
        {
            if (!routeData.Values.ContainsKey(routeFilterKey))
            {
                return false;
            }

            return isPattern
                ? IsMatch(routeFilterValue, routeData.Values[routeFilterKey]?.ToString() ?? string.Empty)
                : routeData.Values[routeFilterKey]?.Equals(routeFilterValue) ?? false;
        }

        public static bool HeaderFilterMatches(this IHeaderDictionary headers, string headerFilterKey, string headerFilterValue, bool isPattern)
        {
            if (!headers.ContainsKey(headerFilterKey))
            {
                return false;
            }

            return isPattern
                ? IsMatch(headerFilterValue, headers[headerFilterKey])
                : headers[headerFilterKey].Equals(headerFilterValue);
        }

        public static bool BodyFilterMatches(this Stream body, string bodyFilterProperty, string bodyFilterValue, bool isPattern)
        {
            if (body == null)
            {
                return false;
            }

            body.Position = 0;

            var jToken = JToken.Parse(new StreamReader(body).ReadToEnd());

            return isPattern
                ? IsMatch(bodyFilterValue, jToken[bodyFilterProperty]?.ToString() ?? string.Empty)
                : jToken[bodyFilterProperty]?.ToString().Equals(bodyFilterValue) ?? false;
        }

        private static bool IsMatch(string pattern, string value) => new Regex(pattern).IsMatch(value);
    }
}
