using Autofac;
using Autofac.Integration.WebApi;

using NiceNet.Data.Entity.Context;
using NiceNet.DataAcessLayer;
using NiceNet.DataAcessLayer.Interface;

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

            var iRepository = Assembly.Load("NiceNet.DataAcessLayer.Interface");
            var repository = Assembly.Load("NiceNet.DataAcessLayer");
            var iManager = Assembly.Load("NiceNet.Manager.Interface");
            var manager = Assembly.Load("NiceNet.Manager");
            builder.RegisterAssemblyTypes(iRepository, repository)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .PropertiesAutowired();
            builder.RegisterAssemblyTypes(iManager, manager)
                .Where(t => t.Name.EndsWith("Manager"))
                .AsImplementedInterfaces()
                .PropertiesAutowired();

            // 注册Controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
