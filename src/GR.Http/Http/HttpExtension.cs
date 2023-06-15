using System.Net.Http.Headers;
using System.Net;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GR.Http
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static partial class HttpExtension
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options">
        ///  客户端配置列表
        ///  1-如果headers 为空，则默认会添加一个  Accept=application/json;
        /// </param>
        /// <returns></returns>
        public static IServiceCollection AddHttpClient(this IServiceCollection services, params HttpClientOption[] options)
        {
            if (options != null && options.Any())
            {
                foreach (var option in options)
                {
                    services.AddHttpClient(option);
                }
            }
            return services;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="option">
        ///  客户端配置列表
        ///  1-如果headers 为空，则默认会添加一个  Accept=application/json;
        /// </param>
        /// <returns></returns>
        public static IHttpClientBuilder AddHttpClient(this IServiceCollection services, HttpClientOption option)
        {
            return services.AddHttpClient(option.ClientName, c =>
            {
                c.BaseAddress = new Uri(option.BaseAddress);
                c.Timeout = TimeSpan.FromSeconds(option.TimeOut);
                if (option.Headers != null && option.Headers.Count > 0)
                {
                    foreach (var header in option.Headers)
                    {
                        c.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }
                else
                {
                    c.DefaultRequestHeaders.Add("Accept", "application/json");
                }
            });
        }

        /// <summary>
        /// POST 请求
        /// </summary>
        /// <param name="httpClientFactory"></param>
        /// <param name="logger"></param>
        /// <param name="requestUri">请求资源</param>
        /// <param name="content">提交body 参数</param>
        /// <param name="logTitle">日志标题</param>
        /// <param name="contentType">请求类型，默认application/json</param>
        /// <returns>
        /// item1:是否成功
        /// item2: 成功的时候，返回数据，不成功的时候返回错误消息
        /// </returns>
        public static async Task<Tuple<bool, string>> PostAsync(this IHttpClientFactory httpClientFactory, ILogger logger, string requestUri, string content, string logTitle = "", string contentType = "application/json")
        {
            return await httpClientFactory.PostAsync(logger, requestUri, content, "", logTitle: logTitle, contentType: contentType);
        }

        /// <summary>
        /// POST 请求
        /// </summary>
        /// <param name="httpClientFactory"></param>
        /// <param name="logger"></param>
        /// <param name="requestUri">请求资源</param>
        /// <param name="content">提交body 参数</param>
        /// <param name="clientName">http客户端名称</param>
        /// <param name="logTitle">日志标题</param>
        /// <param name="contentType">请求类型，默认application/json</param>
        /// <returns>
        /// item1:是否成功
        /// item2: 成功的时候，返回数据，不成功的时候返回错误消息
        /// </returns>
        public static async Task<Tuple<bool, string>> PostAsync(this IHttpClientFactory httpClientFactory, ILogger logger, string requestUri, string content, string clientName, string logTitle = "", string contentType = "application/json")
        {
            try
            {
                using (var httpClient = httpClientFactory.Create(clientName))
                {
                    var buffer = Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                    var response = await httpClient.PostAsync(requestUri, byteContent);
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        return new Tuple<bool, string>(false, string.Format("{1}，状态码-{0}", response.StatusCode, logTitle));
                    }
                    string result = await response.Content.ReadAsStringAsync();
                    return new Tuple<bool, string>(true, result);
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, logTitle);
                return new Tuple<bool, string>(true, string.Format("{1}：{0}", ex.Message, logTitle));
            }
        }

        /// <summary>
        /// GET 请求
        /// </summary>
        /// <param name="httpClientFactory"></param>
        /// <param name="logger"></param>
        /// <param name="requestUri">请求资源</param>
        /// <param name="logTitle">日志标题</param>
        /// <returns>
        /// item1:是否成功
        /// item2: 成功的时候，返回数据，不成功的时候返回错误消息
        /// </returns>
        public static async Task<Tuple<bool, string>> GetAsync(this IHttpClientFactory httpClientFactory, ILogger logger, string requestUri, string logTitle = "")
        {
            return await httpClientFactory.GetAsync(logger, requestUri, logTitle: logTitle);
        }

        /// <summary>
        /// GET 请求
        /// </summary>
        /// <param name="httpClientFactory"></param>
        /// <param name="logger"></param>
        /// <param name="requestUri">请求资源</param>
        /// <param name="clientName">http客户端名称</param>
        /// <param name="logTitle">日志标题</param>
        /// <returns>
        /// item1:是否成功
        /// item2: 成功的时候，返回数据，不成功的时候返回错误消息
        /// </returns>
        public static async Task<Tuple<bool, string>> GetStringAsync(this IHttpClientFactory httpClientFactory, ILogger logger, string requestUri, string clientName, string logTitle = "")
        {
            try
            {
                using (var httpClient = httpClientFactory.Create(clientName))
                {
                    string result = await httpClient.GetStringAsync(requestUri);
                    return new Tuple<bool, string>(true, result);
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, logTitle);
                return new Tuple<bool, string>(true, string.Format("{1}：{0}", ex.Message, logTitle));
            }
        }

        /// <summary>
        /// 初始化 httpClient
        /// </summary>
        /// <param name="httpClientFactory"></param>
        /// <param name="clientName"></param>
        /// <returns></returns>
        public static HttpClient Create(this IHttpClientFactory httpClientFactory, string clientName)
        {
            return string.IsNullOrWhiteSpace(clientName) ? httpClientFactory.CreateClient() : httpClientFactory.CreateClient(clientName);
        }
    }
}
