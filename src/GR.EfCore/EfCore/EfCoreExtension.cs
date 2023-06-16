using GR.EfCore.Repository;
using GR.EfCore.Repository.Impl;
using GR.EfCore.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GR.EfCore
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static partial class Extension
    {
        public static IServiceCollection AddEfCore<TDbContext>(this IServiceCollection services)
            where TDbContext : DbContext
        {
            //
            services.AddScoped(typeof(IEfCoreRepository<,>), typeof(EfCoreRepositoryBase<,>));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork<TDbContext>));
            services.AddScoped(typeof(IUnitOfWork<TDbContext>), typeof(UnitOfWork<TDbContext>));
            //
            return services;
        }

        /// <summary>
        /// 添加实体与表映射
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void AddEntityTypeConfigurations<T>(this ModelBuilder modelBuilder)
            where T : class
        {
            var types = typeof(T).Assembly.GetTypes();
            var filterTypes = types.Where(x => !string.IsNullOrEmpty(x.Namespace));
            var result = filterTypes.Where(x => !x.IsAbstract && x.GetInterfaces().Any(y => y.GetTypeInfo().IsGenericType && y.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));
            foreach (var configurationInstance in result.Select(Activator.CreateInstance))
            {
                modelBuilder.ApplyConfiguration((dynamic)configurationInstance);
            }
        }
    }
}
