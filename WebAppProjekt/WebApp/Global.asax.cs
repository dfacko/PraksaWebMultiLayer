using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AutoMapper;
using Models;
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
            containerBuilder.RegisterModule<WebAppServiceModule>();
            containerBuilder.RegisterModule<WebAppRepoModule>();
            containerBuilder.RegisterType<AppController>();


            containerBuilder.RegisterType<Mapper>().As<IMapper>();


            containerBuilder.Register(ctx => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            }));

            containerBuilder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().SingleInstance();
            


            var container = containerBuilder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container);
           

        }
    }
}
