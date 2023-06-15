using System.Text.Encodings.Web;
using System.Text.Json;

namespace GR.Extensions
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 反序列化：System.Text.Json
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
        /// 序列化：System.Text.Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="isPropertyNameCaseInsensitive"></param>
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
