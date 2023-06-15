namespace GR.Http
{
    /// <summary>
    ///  http 客户端 配置
    /// </summary>
    public class HttpClientOption
    {
        /// <summary>
        /// 域名
        /// </summary>
        /// <remarks>
        /// 例如：http://open.kinwong.com/
        /// </remarks>
        public string BaseAddress { get; set; }

        /// <summary>
        /// Http 客户端名称
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// 超时时间，默认60秒
        /// </summary>
        public int TimeOut { get; set; } = 60;

        /// <summary>
        /// 请求头
        /// </summary>
        public Dictionary<string, string> Headers { get; set; }
    }
}
