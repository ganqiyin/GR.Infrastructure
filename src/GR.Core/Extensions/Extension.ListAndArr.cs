namespace GR.Extensions
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsEmpty<T>(this IEnumerable<T> list)
        {
            if (list == null || list.Count() <= 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 是否不为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsNotEmpty<T>(this IEnumerable<T> list)
        {
            return !list.IsEmpty();
        }
    }
}
