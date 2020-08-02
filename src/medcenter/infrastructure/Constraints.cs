using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing; 
using System;

namespace MedCenter.V1
{
    public class DonationTypeConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, 
                          IRouter route, 
                          string key, 
                          RouteValueDictionary values, 
                          RouteDirection direction)
        {
            var candidate = values[key]?.ToString();
            return Enum.TryParse(candidate, out DonationType result);      
        }
    }
}