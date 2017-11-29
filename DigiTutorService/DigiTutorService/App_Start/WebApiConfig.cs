using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DigiTutorService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
            
            config.Routes.MapHttpRoute(
                name: "DigiTutorAPI",
                routeTemplate: "api/{userid}/{controller}/{id}",
                defaults: new { userid= RouterParameter.Optional, id = RouteParameter.Optional }
            );
           
        }
    }
}
