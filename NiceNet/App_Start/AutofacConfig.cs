﻿using Autofac;
using Autofac.Integration.WebApi;
using NiceNet.Controllers;
using NiceNet.Data.Entity.Context;
using NiceNet.DataAcessLayer;
using NiceNet.DataAcessLayer.Interface;
using NiceNet.Filters;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace NiceNet
{
    public static class AutofacConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ContainerBuilder builder = new ContainerBuilder();

            // 注册数据库Context
            builder.Register<XYFContext>(x => new XYFContext())
                .InstancePerLifetimeScope()
                .PropertiesAutowired();

            // 注册BaseDal<>
            builder.RegisterGeneric(typeof(BaseDal<>))
                .As(typeof(IBaseDal<>))
                .InstancePerLifetimeScope()
                .PropertiesAutowired();

            // 注册BaseRepository<>
            builder.RegisterGeneric(typeof(BaseRepository<,>))
                .As(typeof(IBaseRepository<,>))
                .InstancePerLifetimeScope()
                .PropertiesAutowired();

            // 注册Repository
            var repository = Assembly.Load("NiceNet.DataAcessLayer");
            builder.RegisterAssemblyTypes(repository)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .PropertiesAutowired();

            // 注册Manager
            var manager = Assembly.Load("NiceNet.Manager");
            builder.RegisterAssemblyTypes(manager)
                .Where(t => t.Name.EndsWith("Manager"))
                .AsImplementedInterfaces()
                .PropertiesAutowired();

            // 注册过滤器Provider
            builder.RegisterWebApiFilterProvider(config);

            // 注册Custom过滤器
            builder.Register(c =>  new CustomAuthorizationFilter())
                .AsWebApiAuthorizationFilterFor<BaseController>()
                .InstancePerLifetimeScope()
                .PropertiesAutowired();
            builder.Register(c => new CustomActionFilter())
                .AsWebApiActionFilterFor<BaseController>()
                .InstancePerLifetimeScope()
                .PropertiesAutowired();
            builder.Register(c => new CustomExceptionFilter())
                .AsWebApiExceptionFilterOverrideFor<BaseController>()
                .InstancePerLifetimeScope()
                .PropertiesAutowired();
            builder.Register(c => new CustomAuthenticationFilter())
                .AsWebApiAuthenticationFilterFor<BaseController>()
                .InstancePerLifetimeScope()
                .PropertiesAutowired();

            // 注册Controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly())
                .PropertiesAutowired();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
