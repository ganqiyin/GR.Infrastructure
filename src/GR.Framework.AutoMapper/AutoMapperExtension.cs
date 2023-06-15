using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GR.AutoMapper
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class AutoMapperExtension
    {
        private static IServiceProvider _serviceProvider;

        /// <summary>
        /// 主要是为了获取 IServiceProvider
        /// </summary>
        /// <param name="app"></param>
        public static void UseAutoMapper(this IApplicationBuilder app)
        {
            _serviceProvider = app.ApplicationServices;
        }


        /// <summary>
        /// 对现有对象赋值
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static TDestination Map<TSource, TDestination>(this TSource source, TDestination destination)
        {
            var mapper = _serviceProvider.GetRequiredService<IMapper>();
            return mapper.Map(source, destination);
        }

        /// <summary>
        /// 创建新对象并赋值
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TDestination Map<TSource, TDestination>(this TSource source)
        {
            var mapper = _serviceProvider.GetRequiredService<IMapper>();
            return mapper.Map<TSource, TDestination>(source);
        }

        /// <summary>
        /// 创建新对象并赋值
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<TDestination> Map<TSource, TDestination>(this IEnumerable<TSource> source)
        {
            var mapper = _serviceProvider.GetRequiredService<IMapper>();
            return mapper.Map<List<TDestination>>(source);
        }
    }
}
