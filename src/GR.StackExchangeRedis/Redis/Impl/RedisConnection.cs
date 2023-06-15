using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Collections.Concurrent;

namespace GR.Redis.Impl
{
    /// <summary>
    /// ConnectionMultiplexer对象管理帮助类
    /// https://cloud.tencent.com/developer/article/1670962
    /// https://www.cnblogs.com/xwc1996/p/11973611.html
    /// </summary>
    public class RedisConnection
    {
        private readonly RedisConfigOption _config;
        private readonly ILogger _logger;
        private readonly ConcurrentDictionary<string, ConnectionMultiplexer> _connectionDic;
        public RedisConnection(ILogger<RedisConnection> logger, IOptions<RedisConfigOption> options)
        {
            if (options == null) throw new ArgumentNullException("Redis 配置文件不存在");
            _config = options.Value;
            _connectionDic = new ConcurrentDictionary<string, ConnectionMultiplexer>();
            _logger = logger;
        }

        /// <summary>
        /// 获取连接
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public ConnectionMultiplexer GetConnect(string connectionString = null)
        {
            connectionString ??= _config.ToString();
            var connect = ConnectionMultiplexer.Connect(connectionString);

            //注册如下事件
            connect.ConnectionFailed += MuxerConnectionFailed;
            connect.ConnectionRestored += MuxerConnectionRestored;
            connect.ErrorMessage += MuxerErrorMessage;
            connect.ConfigurationChanged += MuxerConfigurationChanged;
            connect.HashSlotMoved += MuxerHashSlotMoved;
            connect.InternalError += MuxerInternalError;

            return _connectionDic.GetOrAdd(_config.InstanceName, connect);
        }

        /// <summary>
        /// 获取缓存KEY前缀
        /// </summary>
        /// <returns></returns>
        public string GetKeyPrefix()
        {
            return _config.KeyPrefix;
        }


        #region 事件

        /// <summary>
        /// 配置更改时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MuxerConfigurationChanged(object sender, EndPointEventArgs e)
        {
            _logger.LogInformation("Configuration changed: {point}", e.EndPoint);
        }

        /// <summary>
        /// 发生错误时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MuxerErrorMessage(object sender, RedisErrorEventArgs e)
        {
            _logger.LogError("ErrorMessage: {msg}", e.Message);
        }

        /// <summary>
        /// 重新建立连接之前的错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MuxerConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            _logger.LogError("ConnectionRestored: {point} ", e.EndPoint);
        }

        /// <summary>
        /// 连接失败 ， 如果重新连接成功你将不会收到这个通知
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MuxerConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            var msg = string.Format("{0},{1},{2}", e.EndPoint, e.FailureType, e.Exception == null ? "" : ", " + e.Exception.Message);
            _logger.LogError("重新连接：Endpoint failed: {msg}", msg);
        }

        /// <summary>
        /// 更改集群
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MuxerHashSlotMoved(object sender, HashSlotMovedEventArgs e)
        {
            _logger.LogInformation("HashSlotMoved:NewEndPoint-{NewEndPoint},OldEndPoint-{OldEndPoint}", e.NewEndPoint, e.OldEndPoint);
        }

        /// <summary>
        /// redis类库错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MuxerInternalError(object sender, InternalErrorEventArgs e)
        {
            _logger.LogError("InternalError:{message}", e.Exception.Message);
        }

        #endregion 事件
    }
}
