using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nacos.AspNetCore.V2;
using Nacos.V2.DependencyInjection;

namespace GR.Nacos
{
    public static partial class Extension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// https://nacos-sdk-csharp.readthedocs.io/en/latest/introduction/gettingstarted.html
        /// https://www.cnblogs.com/wucy/p/13230453.html
        /// https://www.cnblogs.com/buruainiaaaa/p/14121176.html
        /// </remarks>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="section"></param>
        /// <returns></returns>
        public static IServiceCollection AddNacos(this IServiceCollection services, IConfigurationBuilder configBuilder, IConfiguration configuration, string section = "nacos")
        {
            // 注册服务到Nacos
            services.AddNacosAspNet(configuration, section: section);
            //添加配置中心:默认会使用JSON解析器来解析存在Nacos Server的配置
            configBuilder.AddNacosV2Configuration(configuration.GetSection(section));
            return services;
        }
    }
}
