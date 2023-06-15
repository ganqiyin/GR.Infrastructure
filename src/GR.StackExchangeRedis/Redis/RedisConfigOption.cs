using System.Text;

namespace GR.Redis
{
    /// <summary>
    /// 配置
    /// </summary>
    public class RedisConfigOption
    {
        /// <summary>
        /// 服务器地址列表
        /// </summary>
        public List<RedisHostConfigOption> Hosts { get; set; }

        /// <summary>
        /// 实例名字
        /// </summary>
        public string InstanceName { get; set; } = "Default";

        /// <summary>
        /// 系统自定义KEY前缀
        /// </summary>
        public string KeyPrefix { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 发送消息以帮助保持套接字活动的时间（秒）（默认时间60s）
        /// </summary>
        public int KeepAlive { get; set; } = 60;

        /// <summary>
        /// 数据库索引
        /// </summary>
        public int DatabaseId { get; set; } = 0;

        /// <summary>
        /// 链接超时时间
        /// </summary>
        public int ConnectTimeout { get; set; } = 5000;

        /// <summary>
        /// 异步超时时间
        /// </summary>
        public int SyncTimeout { get; set; } = 1000;

        /// <summary>
        /// 连接池
        /// </summary>
        public int Poolsize { get; set; } = 5;

        /// <summary>
        /// 组装连接串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            //127.0.0.1:6379,password=,defaultDatabase=0,connectTimeout=5000,syncTimeout=1000,poolsize=5,prefix=
            var connStr = new StringBuilder(Hosts.ToStr());
            if (!string.IsNullOrWhiteSpace(Password))
            {
                connStr.AppendFormat(",password={0}", Password);
            }
            if (DatabaseId >= 0 && DatabaseId <= 15)
            {
                connStr.AppendFormat(",defaultDatabase={0}", DatabaseId);
            }
            if (ConnectTimeout > 0)
            {
                connStr.AppendFormat(",connectTimeout={0}", ConnectTimeout);
            }
            if (SyncTimeout > 0)
            {
                connStr.AppendFormat(",syncTimeout={0}", ConnectTimeout);
            }
            //if (Poolsize > 0)
            //{
            //    connStr.AppendFormat(",poolsize={0}", Poolsize);
            //}
            if (KeepAlive > 0)
            {
                connStr.AppendFormat(",keepAlive={0}", KeepAlive);
            }
            //if (!string.IsNullOrWhiteSpace(KeyPrefix))
            //{
            //    connStr.AppendFormat(",prefix={0}", KeyPrefix);
            //}
            return connStr.ToString();
        }
    }

    public class RedisHostConfigOption
    {

        /// <summary>
        /// 服务器地址
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; } = 6379;

        public override string ToString()
        {
            return string.Format("{0}:{1}", Host, Port);
        }
    }

    /// <summary>
    /// 扩展
    /// </summary>
    public static class RedisConfigExtension
    {
        /// <summary>
        /// 组装
        /// </summary>
        /// <param name="hosts"></param>
        /// <returns></returns>
        public static string ToStr(this List<RedisHostConfigOption> hosts)
        {
            if (hosts == null || hosts.Count <= 0)
            {
                return "";
            }
            var txt = new StringBuilder();
            for (var i = 0; i < hosts.Count; i++)
            {
                if (i > 0)
                {
                    txt.Append(',');
                }
                txt.Append(hosts[i].ToString());
            }
            return txt.ToString();
        }

    }
}
