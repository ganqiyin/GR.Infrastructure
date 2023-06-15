using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Registry;

namespace GR.Http
{
    public static partial class HttpExtension
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="option">
        ///  客户端配置
        ///  1-如果headers 为空，则默认会添加一个  Accept=application/json;</param>
        /// <param name="retryTimes"></param>
        /// <returns></returns>
        public static IServiceCollection AddHttpClientWithPolicy(this IServiceCollection services, HttpClientOption option, params TimeSpan[] retryTimes)
        {
            services.AddHttpClient(option).AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(retryTimes));
            return services;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="option">
        ///  客户端配置
        ///  1-如果headers 为空，则默认会添加一个  Accept=application/json;</param>
        /// <param name="retryCout">重试次数</param>
        /// <param name="intervalTimeSecond">重试幂等时间初始值，默认2秒</param>
        /// <returns></returns>
        public static IServiceCollection AddHttpClientWithPolicy(this IServiceCollection services, HttpClientOption option, int retryCout, int intervalTimeSecond = 2)
        {
            services.AddHttpClient(option).AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(retryCout, i => TimeSpan.FromSeconds(i * intervalTimeSecond)));
            return services;
        }


        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="option">
        ///  客户端配置
        ///  1-如果headers 为空，则默认会添加一个  Accept=application/json;</param>
        /// <param name="policy"></param>
        /// <returns></returns>
        public static IServiceCollection AddHttpClientWithPolicy(this IServiceCollection services, HttpClientOption option, IAsyncPolicy<HttpResponseMessage> policy)
        {
            services.AddHttpClient(option).AddPolicyHandler(policy);
            return services;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="option">
        ///  客户端配置
        ///  1-如果headers 为空，则默认会添加一个  Accept=application/json;</param>
        /// <param name="policySelector"></param>
        /// <returns></returns>
        public static IServiceCollection AddHttpClientWithPolicy(this IServiceCollection services, HttpClientOption option, Func<HttpRequestMessage, IAsyncPolicy<HttpResponseMessage>> policySelector)
        {
            services.AddHttpClient(option).AddPolicyHandler(policySelector);
            return services;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="option">
        ///  客户端配置
        ///  1-如果headers 为空，则默认会添加一个  Accept=application/json;</param>
        /// <param name="policySelector"></param>
        /// <returns></returns>
        public static IServiceCollection AddHttpClientWithPolicy(this IServiceCollection services, HttpClientOption option, Func<IServiceProvider, HttpRequestMessage, IAsyncPolicy<HttpResponseMessage>> policySelector)
        {
            services.AddHttpClient(option).AddPolicyHandler(policySelector);
            return services;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="option">
        ///  客户端配置
        ///  1-如果headers 为空，则默认会添加一个  Accept=application/json;</param>
        /// <param name="policyFactory"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IServiceCollection AddHttpClientWithPolicy(this IServiceCollection services, HttpClientOption option, Func<IServiceProvider, HttpRequestMessage, string, IAsyncPolicy<HttpResponseMessage>> policyFactory, Func<HttpRequestMessage, string> keySelector)
        {
            services.AddHttpClient(option).AddPolicyHandler(policyFactory, keySelector);
            return services;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="option">
        ///  客户端配置
        ///  1-如果headers 为空，则默认会添加一个  Accept=application/json;</param>
        /// <param name="policyKey"></param>
        /// <returns></returns>
        public static IServiceCollection AddHttpClientWithPolicyFromRegistry(this IServiceCollection services, HttpClientOption option, string policyKey)
        {
            services.AddHttpClient(option).AddPolicyHandlerFromRegistry(policyKey);
            return services;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="option">
        ///  客户端配置
        ///  1-如果headers 为空，则默认会添加一个  Accept=application/json;</param>
        /// <param name="policySelector"></param>
        /// <returns></returns>
        public static IServiceCollection AddHttpClientWithPolicyFromRegistry(this IServiceCollection services, HttpClientOption option, Func<IReadOnlyPolicyRegistry<string>, HttpRequestMessage, IAsyncPolicy<HttpResponseMessage>> policySelector)
        {
            services.AddHttpClient(option).AddPolicyHandlerFromRegistry(policySelector);
            return services;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="option">
        ///  客户端配置
        ///  1-如果headers 为空，则默认会添加一个  Accept=application/json;</param>
        /// <param name="configurePolicy"></param>
        /// <returns></returns>
        public static IServiceCollection AddHttpClientWithTransientHttpErrorPolicy(this IServiceCollection services, HttpClientOption option, Func<PolicyBuilder<HttpResponseMessage>, IAsyncPolicy<HttpResponseMessage>> configurePolicy)
        {
            services.AddHttpClient(option).AddTransientHttpErrorPolicy(configurePolicy);
            return services;
        }
    }
}
