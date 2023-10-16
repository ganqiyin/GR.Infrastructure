using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GR.EfCore
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param> 
        /// <param name="connectionName"></param>
        /// <param name="modeType">注册方式<see cref="AddDbContextModeType"/></param> 
        /// <param name="factoryLifetime">
        /// 使用AddDbContextFactory 方法注入的生命周期类型：默认是单例，如果要实现多个请求并发则需要使用 ServiceLifetime.Scoped
        /// </param>
        /// <returns></returns>
        public static IServiceCollection AddMySql<TDbContext>(this IServiceCollection services, IConfiguration configuration, string connectionName= "Default", AddDbContextModeType modeType = AddDbContextModeType.AddDbContextPool, ServiceLifetime factoryLifetime = ServiceLifetime.Singleton)
         where TDbContext : DbContext
        {
            var connStr = configuration.GetConnectionString(connectionName);
            var dbVer = configuration.GetSection("MySql:Ver").Value ?? "8.0";
            services.AddMySql<TDbContext>(connStr, dbVer: dbVer, modeType: modeType, factoryLifetime: factoryLifetime);
            return services;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connStr">链接字符串</param>
        /// <param name="dbVer">数据库版本，默认8.0</param>
        /// <param name="modeType">注册方式<see cref="AddDbContextModeType"/></param> 
        /// <param name="factoryLifetime">
        /// 使用AddDbContextFactory 方法注入的生命周期类型：默认是单例，如果要实现多个请求并发则需要使用 ServiceLifetime.Scoped
        /// </param>
        /// <returns></returns>
        public static IServiceCollection AddMySql<TDbContext>(this IServiceCollection services, string connStr, string dbVer = "8.0", AddDbContextModeType modeType = AddDbContextModeType.AddDbContextPool, ServiceLifetime factoryLifetime = ServiceLifetime.Singleton)
         where TDbContext : DbContext
        {
            services.AddEfCore<TDbContext>();
            var serverVer = new MySqlServerVersion(dbVer);
            switch (modeType)
            {
                case AddDbContextModeType.AddDbContextPool:
                    services.AddDbContextPool<TDbContext>(options => options.UseMySql(connStr, serverVer));
                    break;
                case AddDbContextModeType.AddDbContextFactory:
                    services.AddDbContextFactory<TDbContext>(options => options.UseMySql(connStr, serverVer), lifetime: factoryLifetime);
                    break;
                case AddDbContextModeType.AddDbContext:
                default:
                    services.AddDbContext<TDbContext>(options => options.UseMySql(connStr, serverVer));
                    break;
            }
            return services;
        }
    }
}
