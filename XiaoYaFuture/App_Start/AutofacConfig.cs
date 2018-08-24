using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using XiaoYaFuture.Common;
using XiaoYaFuture.Data.Entity.Context;
using XiaoYaFuture.DataAcessLayer;
using XiaoYaFuture.DataAcessLayer.Interface;

namespace XiaoYaFuture
{
    public static class AutofacConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ContainerBuilder builder = new ContainerBuilder();

            // 注册Controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // 注册数据库Context
            builder.Register<XYFContext>(x => new XYFContext())
                .InstancePerLifetimeScope();

            // 注册BaseDal<>
            builder.RegisterGeneric(typeof(BaseDal<,>))
                .As(typeof(IBaseDal<,>))
                .InstancePerLifetimeScope();

            // 注册BaseRepository<>
            builder.RegisterGeneric(typeof(BaseRepository<,>))
                .As(typeof(IBaseRepository<,>))
                .InstancePerLifetimeScope();

            var iocType = typeof(IDependency);

            #region 注册所有实现IDependency的接口类
            // 需要引入对应的项目
            var IoCProjectNames = new List<string>()
            {
                "XiaoYaFuture.Manager",
            };

            foreach (var IoCProjectName in IoCProjectNames)
            {
                var assembly = Assembly.Load(IoCProjectName);

                builder.RegisterAssemblyTypes(assembly)
                       .Where(t => iocType.IsAssignableFrom(t) && t != iocType && !t.IsAbstract)
                       .AsImplementedInterfaces()
                       .InstancePerLifetimeScope();
            }
            #endregion

            #region 注册所有Repository
            // 需要引入对应的项目
            IoCProjectNames = new List<string>()
            {
                "XiaoYaFuture.DataAcessLayer",
            };

            foreach (var IoCProjectName in IoCProjectNames)
            {
                var assembly = Assembly.Load(IoCProjectName);

                builder.RegisterAssemblyTypes(assembly)
                       .Where(t => iocType.IsAssignableFrom(t) && !t.IsAbstract && t != iocType && t.Name.EndsWith("Repository"))
                       .InstancePerLifetimeScope();
            }
            #endregion

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
