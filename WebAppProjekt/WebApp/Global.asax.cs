using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using WebApp.Controllers;
using WebAppRepo;
using WebAppRepo.Common;
using WebAppService;
using WebAppService.Common;

namespace WebApp
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<WebAppServices>().As<IWebAppService>();
            containerBuilder.RegisterType<WebAppRepository>().As<IRepo>();
            containerBuilder.RegisterType<AppController>();
            //containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            

            var container = containerBuilder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container);
           

        }
    }
}
