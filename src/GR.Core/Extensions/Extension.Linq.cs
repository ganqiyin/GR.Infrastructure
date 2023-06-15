using GR.Dto;
using System.Linq.Expressions;

namespace GR.Extensions
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="request"><see cref="GetListWithPagingRequest"/></param>
        /// <returns></returns>
        public static IQueryable<T> PageBy<T>(this IQueryable<T> query, GetListWithPagingRequest request)
        {
            return query.PageBy(request.PageIndex, request.PageSize);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IQueryable<T> PageBy<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            if (pageIndex >= 1 && pageSize >= 1)
            {
                var rowIndex = (pageIndex - 1) * pageSize;
                return query.Skip(rowIndex).Take(pageSize);
            }
            else
            {
                return query;
            }
        }

        /// <summary>
        /// 查询条件筛选
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, bool condition, Expression<Func<T, bool>> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// 查询条件筛选
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, bool> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }
    }
}
