using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Autofac;
using WebApp.Controllers;
using WebAppRepo;
using WebAppRepo.Common;
using WebAppService;
using WebAppService.Common;

namespace WebApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

             
        }
    }
}
