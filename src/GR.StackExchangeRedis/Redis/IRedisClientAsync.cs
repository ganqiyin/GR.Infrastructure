using StackExchange.Redis;

namespace GR.Redis
{
    /// <summary>
    /// 异步客户端接口
    /// </summary>
    public interface IRedisClientAsync : IRedisClient
    {
        /// <summary>
        /// 删除缓存KEY
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        Task<bool> KeyDeleteAsync(string key, int db = -1);

        /// <summary>
        /// 删除缓存KEY
        /// </summary>
        /// <param name="keys">缓存KEY集合</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        Task<long> KeyDeleteAsync(IList<string> keys, int db = -1);

        /// <summary>
        /// 缓存KEY是否存在
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        Task<bool> KeyIsExistsAsync(string key, int db = -1);

        /// <summary>
        /// 重命名缓存KEY
        /// </summary>
        /// <param name="orgKey">原缓存KEY名称</param>
        /// <param name="newKey">新的缓存KEY名称</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        Task<bool> KeyRenameAsync(string orgKey, string newKey, int db = -1);

        /// <summary>
        /// 设置缓存KEY过期时间
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="expire">过期时间</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        Task<bool> KeyExpireAsync(string key, TimeSpan? expire, int db = -1);

        /// <summary>
        /// 【string】添加数据
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="value">缓存数据</param>
        /// <param name="expiry">过期时间</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        Task<bool> AddAsync(string key, string value, TimeSpan? expiry = null, int db = -1);

        /// <summary>
        /// 【string】添加数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">缓存KEY</param>
        /// <param name="value">缓存数据</param>
        /// <param name="expiry">过期时间</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        Task<bool> AddAsync<T>(string key, T value, TimeSpan? expiry = null, int db = -1);

        /// <summary>
        /// 【string】获取数据
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        Task<string> GetAsync(string key, int db = -1);

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">缓存KEY</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key, int db = -1);

        /// <summary>
        /// 计算器 加 N
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="value">要加的值</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        Task<long> IncrementAsync(string key, long value = 1, int db = -1);

        /// <summary>
        /// 计算器 减 N
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="value">要减去的值</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        Task<long> DecrementAsync(string key, long value = 1, int db = -1);

        /// <summary>
        /// 【list】从右边【入】 队列
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">缓存KEY</param>
        /// <param name="value">要入队列的数据</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        Task LRightPushAsync<T>(string key, T value, int db = -1);

        /// <summary>
        /// 【list】从右边【出】队列
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">缓存KEY</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        Task<T> LRightPopAsync<T>(string key, int db = -1);

        /// <summary>
        /// 【list】从左边【入】队列列
        /// </summary> 
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">缓存KEY</param>
        /// <param name="value">要入队列的数据</param>
        /// <param name="db">数据库，默认-1</param>
        Task LLeftPushAsync<T>(string key, T value, int db = -1);

        /// <summary>
        /// 【list】从左边【出】队列列
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">缓存KEY</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        Task<T> LLeftPopAsync<T>(string key, int db = -1);

        /// <summary>
        /// 【list】移除队列中得某项数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        Task LRemoveAsync<T>(string key, T value, int db = -1);

        /// <summary>
        /// 【list】获取所有数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">缓存KEY</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        Task<IList<T>> LRangeAsync<T>(string key, int db = -1);

        /// <summary>
        /// 【list】队列长度
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        Task<long> LLengthAsync(string key, int db = -1);

        /// <summary>
        /// 【Hash】判断某个数据是否已经被缓存了
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        Task<bool> HIsExistsAsync(string key, string fieldName, int db = -1);

        /// <summary>
        /// 【Hash】添加数据到hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<bool> HSetAsync<T>(string key, string fieldName, T value, int db = -1);

        /// <summary>
        /// 【Hash】删除hash表中某个字段
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        Task<bool> HDeleteAsync(string key, string fieldName, int db = -1);

        /// <summary>
        /// 【Hash】删除hash表中某些字段
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="fieldNames">字段名称集合</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        Task<long> HDeleteAsync(string key, IList<RedisValue> fieldNames, int db = -1);

        /// <summary>
        /// 【Hash】获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="fieldName"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<T> HGetAsync<T>(string key, string fieldName, int db = -1);

        /// <summary>
        /// 【Hash】计算器 加 N
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="value">要加的值</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        Task<long> HIncrementAsync(string key, string fieldName, long value = 1, int db = -1);

        /// <summary>
        /// 【Hash】计算器 减 N
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="value">要减去的值</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        Task<long> HDecrementAsync(string key, string fieldName, long value = 1, int db = -1);

        /// <summary>
        /// 【Hash】获取所有数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<IList<T>> HKeysAsync<T>(string key, int db = -1);


        /// <summary>
        /// 【Set】添加数据
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="value">需要添加的数据</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        Task<bool> SAddAsync(string key, string value, int db = -1);

        /// <summary>
        /// 【Set】添加数据
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="values">需要添加的数据集合</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        Task<long> SAddAsync(string key, IList<RedisValue> values, int db = -1);

        /// <summary>
        /// 【Set】获取所有数据
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        Task<RedisValue[]> SMembersAsync(string key, int db = -1);

        /// <summary>
        /// 【Set】集合是否有该数据
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="value">数据</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        Task<bool> SContainsAsync(string key, string value, int db = -1);

        /// <summary>
        /// 【Set】集合长度
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        Task<long> SLengthAsync(string key, int db = -1);

        /// <summary>
        /// 【Set】获取并移除数据：随机返回一个数据，并移除该数据
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        Task<string> SPopAsync(string key, int db = -1);

        /// <summary>
        /// 【Sorted Set】添加数据
        /// </summary>
        /// <typeparam name="T">添加数据的类型</typeparam>
        /// <param name="key">缓存KEY</param>
        /// <param name="value">数据</param>
        /// <param name="score">排序序号</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        Task<bool> SSortedAddAsync<T>(string key, T value, double score, int db = -1);

        /// <summary>
        /// 【Sorted Set】删除数据
        /// </summary>
        /// <typeparam name="T">删除数据的类型</typeparam>
        /// <param name="key">缓存KEY</param>
        /// <param name="value">数据</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        Task<bool> SSortedRemoveAsync<T>(string key, T value, int db = -1);

        /// <summary>
        /// 【Sorted Set】获取全部数据
        /// </summary>
        /// <typeparam name="T">返回数据的类型</typeparam>
        /// <param name="key">缓存KEY</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        Task<IList<T>> SSortedRangeByRankAsync<T>(string key, int db = -1);

        /// <summary>
        /// 【Sorted Set】获取有序集合长度
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        Task<long> SSortedLengthAsync(string key, int db = -1);
    }
}
