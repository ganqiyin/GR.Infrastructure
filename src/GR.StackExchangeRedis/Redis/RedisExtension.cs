using GR.Redis.Impl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace GR.Redis
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class RedisExtension
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceCollection AddStackExchangeRedis(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<RedisConnection>();
            services.AddScoped<IRedisClientAsync, RedisClientAsync>();
            services.AddScoped<IRedisClient, RedisClient>();
            services.AddOptions();
            services.Configure<RedisConfigOption>(config.GetSection("Redis"));
            return services;
        }

        /// <summary>
        /// 转换数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        public static IList<T> ToModel<T>(this RedisValue[] values)
        {
            var list = new List<T>();
            if (values != null && values.Length > 0)
            {
                foreach (var data in values)
                {
                    var m = JsonSerializer.Deserialize<T>(data);
                    list.Add(m);
                }
            }
            return list;
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Deserialize<T>(this string json)
            where T : class, new()
        {
            if (json == null)
                return null;
            var options = new JsonSerializerOptions
            {
                //属性名不区分大小写：true-不区分，false -区分
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<T>(json, options);
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="isPropertyNameCaseInsensitive">
        /// 属性名不区分大小写：true-不区分，false -区分
        /// </param>
        /// <returns></returns>
        public static string Serialize<T>(this T data, bool isPropertyNameCaseInsensitive = true)
            where T : class, new()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = isPropertyNameCaseInsensitive,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };
            return JsonSerializer.Serialize(data, options);
        }
    }
}
