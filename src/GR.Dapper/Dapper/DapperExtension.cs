using GR.Dapper.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace GR.Dapper
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class DapperExtension
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connStr"></param>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static IServiceCollection AddDapper(this IServiceCollection services, string connStr, DapperDBType dbType)
        {
            services.AddSingleton<IDapperRepository, DapperRepository>(service =>
            {
                return new DapperRepository(connStr, dbType);
            });
            return services;
        }
    }
}
