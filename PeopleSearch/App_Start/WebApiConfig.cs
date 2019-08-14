﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PeopleSearch
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Add(new System.Net.Http.Formatting.RequestHeaderMapping("Accept",
                              "text/html",
                              StringComparison.InvariantCultureIgnoreCase,
                              true,
                              "application/json"));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ActionApi",
                routeTemplate: "api/{controller}/{action}/{name}",
                defaults: new { name = RouteParameter.Optional }
            );

        }
    }
}
