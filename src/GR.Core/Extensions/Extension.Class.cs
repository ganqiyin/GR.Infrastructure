using System.ComponentModel;
using System.Reflection;

namespace GR.Extensions
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 深拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T DeepClone<T>(this T obj)
        {
            if (obj == null)
            {
                return default(T);
            }

            //字符串或者值类型，直接返回
            if (obj is string || obj.GetType().IsValueType)
            {
                return obj;
            }
            //创建实例
            T retval = Activator.CreateInstance<T>();
            //获取字段
            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            foreach (var field in fields)
            {
                try
                {
                    var val = field.GetValue(obj).DeepClone();
                    field.SetValue(retval, val);
                }
                catch { }
            }
            return retval;
        }

        /// <summary>
        /// 获取display name
        /// </summary>
        /// <param name="pi"></param>
        /// <returns></returns>
        public static string GetDisplayName(this PropertyInfo pi)
        {
            var attrs = pi.GetCustomAttributes(typeof(DisplayNameAttribute), true);
            if (attrs.IsNotEmpty())
            {
                return ((DisplayNameAttribute)attrs[0]).DisplayName;
            }
            return pi.Name;
        }

        /// <summary>
        /// 获取对象属性的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pi"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetVal<T>(this PropertyInfo pi, T obj)
        {
            var resultPi = pi.GetValue(obj, null);
            return resultPi != null ? resultPi.ToString() : "";
        }
    }
}
