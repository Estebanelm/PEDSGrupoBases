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
               routeTemplate: "api/{userid:int}/{controller}/{id}",
               defaults: new { userid = RouteParameter.Optional , id = RouteParameter.Optional }
           );

            //config.Routes.MapHttpRoute(
            //    name: "DigiTutorAPI",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);



        }
    }
}
