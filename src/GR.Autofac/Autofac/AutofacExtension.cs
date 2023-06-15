using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace GR.Autofac
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class AutofacExtension
    {
        /// <summary>
        /// 注册 fautofac
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IHostBuilder UseAutofac(this IHostBuilder builder)
        {
            builder.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            return builder;
        }

        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static ContainerBuilder AddServices(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            builder.RegisterAssemblyTypes(assemblies)
                 .Where(x => typeof(IScopedDenpency).IsAssignableFrom(x) && !x.IsAbstract)
                .AsSelf()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .PropertiesAutowired();

            builder.RegisterAssemblyTypes(assemblies)
            .Where(x => typeof(ISingletonDenpency).IsAssignableFrom(x) && !x.IsAbstract)
              .AsSelf()
              .AsImplementedInterfaces()
              .SingleInstance()
              .PropertiesAutowired();


            builder.RegisterAssemblyTypes(assemblies)
              .Where(x => typeof(ITraintDenpency).IsAssignableFrom(x) && !x.IsAbstract)
              .AsSelf()
              .AsImplementedInterfaces()
              .InstancePerDependency()
              .PropertiesAutowired();

            return builder;
        }
    }
}
