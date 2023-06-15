using Snowflake.Core;

namespace GR.Snowflake
{
    /// <summary>
    /// 雪花ID
    /// https://www.cnblogs.com/xiaohanxixi/p/13197362.html
    /// </summary>
    internal sealed class SnowflakeIdGenerator
    {
        /* 完全懒汉模式*/
        //private static readonly Lazy<SnowflakeHelper> _lazy = new Lazy<SnowflakeHelper>(() => new SnowflakeHelper());

        //public static SnowflakeHelper Instance { get { return _lazy.Value; } }

        //private SnowflakeHelper() { }

        /*不完全懒汉，但又不加锁的线程安全*/
        private static readonly SnowflakeIdGenerator _instance = new SnowflakeIdGenerator();
        static SnowflakeIdGenerator() { }

        private readonly IdWorker _idWorker;

        private SnowflakeIdGenerator()
        {
            _idWorker = new IdWorker(1, 1);
        }

        /// <summary>
        /// 实列
        /// </summary>
        public static SnowflakeIdGenerator Instance { get { return _instance; } }

        /// <summary>
        /// 生成一个ID
        /// </summary>
        /// <returns></returns>
        public long GenerateId()
        {
            return _idWorker.NextId();
        }

        /// <summary>
        /// 生成指定个数ID
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public IEnumerable<long> GenerateIds(int len)
        {
            var list = new List<long>();
            for (var i = 0; i < len; i++)
            {
                Thread.Sleep(1);
                list.Add(GenerateId());
            }
            return list;
        }
    }
}
