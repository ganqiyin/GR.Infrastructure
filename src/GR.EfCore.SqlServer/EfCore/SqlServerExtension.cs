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
      /// <param name="config"></param> 
      /// <param name="modeType">注册方式<see cref="AddDbContextModeType"/></param> 
      /// <returns></returns>
        public static IServiceCollection AddSqlServer<TDbContext>(this IServiceCollection services, IConfiguration config, AddDbContextModeType modeType = AddDbContextModeType.AddDbContextPool)
         where TDbContext : DbContext
        {
            var connStr = config.GetConnectionString("Default");
            services.AddSqlServer<TDbContext>(connStr, modeType: modeType);
            return services;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connStr">链接字符串</param>
        /// <param name="modeType">注册方式<see cref="AddDbContextModeType"/></param> 
        /// <returns></returns>
        public static IServiceCollection AddSqlServer<TDbContext>(this IServiceCollection services, string connStr, AddDbContextModeType modeType = AddDbContextModeType.AddDbContextPool)
         where TDbContext : DbContext
        {
            services.AddEfCore<TDbContext>();
            switch (modeType)
            {
                case AddDbContextModeType.AddDbContextPool:
                    services.AddDbContextPool<TDbContext>(options => options.UseSqlServer(connStr));
                    break;
                case AddDbContextModeType.AddDbContextFactory:
                    services.AddDbContextFactory<TDbContext>(options => options.UseSqlServer(connStr));
                    break;
                case AddDbContextModeType.AddDbContext:
                default:
                    services.AddDbContext<TDbContext>(options => options.UseSqlServer(connStr));
                    break;
            }
            return services;
        }
    }
}
