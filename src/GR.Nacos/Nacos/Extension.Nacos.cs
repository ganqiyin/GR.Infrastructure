using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nacos.AspNetCore.V2;

namespace GR.Nacos
{
    public static partial class Extension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// https://www.cnblogs.com/wucy/p/13230453.html
        /// https://www.cnblogs.com/buruainiaaaa/p/14121176.html
        /// </remarks>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="section"></param>
        /// <returns></returns>
        public static IServiceCollection AddNacos(this IServiceCollection services, IConfiguration configuration, string section = "nacos")
        {
            services.AddNacosAspNet(configuration, section: section);
            return services;
        }
    }
}
