using GR.Json;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace GR.Extensions
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 反序列化【忽略大小写】：System.Text.Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Deserialize<T>(this string json)
            where T : class, new()
        {
            if (json == null)
                return null;
            return json.Deserialize<T>(true);
        }

        /// <summary>
        /// 反序列化：System.Text.Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="isPropertyNameCaseInsensitive">是否忽略大小写：true-是，false -否</param>
        /// <returns></returns>
        public static T Deserialize<T>(this string json, bool isPropertyNameCaseInsensitive)
            where T : class, new()
        {
            if (json == null)
                return null;
            var options = new JsonSerializerOptions
            {
                //属性名不区分大小写：true-不区分，false -区分
                PropertyNameCaseInsensitive = isPropertyNameCaseInsensitive
            };
            return JsonSerializer.Deserialize<T>(json, options);
        }

        /// <summary>
        /// 序列化：System.Text.Json
        /// </summary>
        /// <remarks>
        /// 参考：
        /// https://q.cnblogs.com/q/115234/
        /// https://www.cnblogs.com/xwgli/p/13331702.html
        /// </remarks>
        /// <param name="result"></param>
        /// <param name="isToLower">是否小写，默认true</param>
        /// <returns></returns>
        public static string Serialize(this Dto.IResult result, bool isToLower = true)
        {
            //var options = new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
            var options = new JsonSerializerOptions { Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) };
            if (isToLower)
            { 
                //配置 小写 格式，而不是默认的 camelCase 格式
                options.PropertyNamingPolicy = new TextJsonLowercasePolicy();
            }
            return JsonSerializer.Serialize(result, options: options);
        }

        /// <summary>
        /// 序列化：System.Text.Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="isPropertyNameCaseInsensitive">是否忽略大小写，默认true</param>
        /// <returns></returns>
        public static string Serialize<T>(this T data, bool isPropertyNameCaseInsensitive = true)
            where T : class, new()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = isPropertyNameCaseInsensitive,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            };
            return JsonSerializer.Serialize(data, options);
        }
    }
}
