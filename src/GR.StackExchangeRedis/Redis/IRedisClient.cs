using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GR.Redis
{
    /// <summary>
    /// 同步客户端接口
    /// </summary>
    public partial interface IRedisClient
    {
        /// <summary>
        /// 获取数据库
        /// </summary>
        /// <param name="db">数据库</param>
        /// <returns></returns>
        IDatabase GetDB(int db = -1);

        /// <summary>
        /// 缓存KEY补充前缀
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <returns></returns>
        string CombinePrefixForKey(string key);

        /// <summary>
        /// 【string】删除缓存KEY
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        bool KeyDelete(string key, int db = -1);

        /// <summary>
        /// 删除缓存KEY
        /// </summary>
        /// <param name="keys">缓存KEY集合</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        long KeyDelete(IList<string> keys, int db = -1);

        /// <summary>
        /// 缓存KEY是否存在
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        bool KeyIsExists(string key, int db = -1);

        /// <summary>
        /// 重命名缓存KEY
        /// </summary>
        /// <param name="orgKey">原缓存KEY名称</param>
        /// <param name="newKey">新的缓存KEY名称</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        bool KeyRename(string orgKey, string newKey, int db = -1);

        /// <summary>
        /// 设置缓存KEY过期时间
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="expire">过期时间</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        bool KeyExpire(string key, TimeSpan? expire, int db = -1);

        /// <summary>
        /// 【string】添加数据
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="value"></param>
        /// <param name="expiry">过期时间</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        bool Add(string key, string value, TimeSpan? expiry = null, int db = -1);

        /// <summary>
        /// 【string】添加数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry">过期时间</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        bool Add<T>(string key, T value, TimeSpan? expiry = null, int db = -1);

        /// <summary>
        /// 【string】获取数据
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        string Get(string key, int db = -1);

        /// <summary>
        /// 【string】获取数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">缓存KEY</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        T Get<T>(string key, int db = -1);

        /// <summary>
        /// 计算器 加 N
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="value">要加的值</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        long Increment(string key, long value = 1, int db = -1);

        /// <summary>
        /// 计算器 减 N
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="value">要减去的值</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        long Decrement(string key, long value = 1, int db = -1);

        /// <summary>
        /// 【list】从右边【入】队列
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">缓存KEY</param>
        /// <param name="value">要入队列的数据</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        void LRightPush<T>(string key, T value, int db = -1);

        /// <summary>
        /// 【list】从右边【出】队列
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">缓存KEY</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        T LRightPop<T>(string key, int db = -1);

        /// <summary>
        /// 【list】从左边【入】队列
        /// </summary> 
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">缓存KEY</param>
        /// <param name="value">要入队列的数据</param>
        /// <param name="db">数据库，默认-1</param>
        void LLeftPush<T>(string key, T value, int db = -1);

        /// <summary>
        /// 【list】从左边【出】队列
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">缓存KEY</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        T LLeftPop<T>(string key, int db = -1);

        /// <summary>
        /// 【list】移除队列中得某项数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        void LRemove<T>(string key, T value, int db = -1);

        /// <summary>
        /// 【list】获取所有数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">缓存KEY</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        IList<T> LRange<T>(string key, int db = -1);

        /// <summary>
        /// 【list】 队列长度
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        long LLength(string key, int db = -1);

        /// <summary>
        /// 【Hash】判断某个数据是否已经被缓存了
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        bool HIsExists(string key, string fieldName, int db = -1);

        /// <summary>
        /// 【Hash】添加数据到hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        bool HSet<T>(string key, string fieldName, T value, int db = -1);

        /// <summary>
        /// 【Hash】删除hash表中某个字段
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        bool HDelete(string key, string fieldName, int db = -1);

        /// <summary>
        /// 【Hash】删除hash表中某些字段
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="fieldNames">字段名称集合</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        long HDelete(string key, IList<RedisValue> fieldNames, int db = -1);

        /// <summary>
        /// 【Hash】获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="fieldName"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        T HGet<T>(string key, string fieldName, int db = -1);

        /// <summary>
        /// 【Hash】计算器 加 N
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="value">要加的值</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        long HIncrement(string key, string fieldName, long value = 1, int db = -1);

        /// <summary>
        /// 【Hash】计算器 减 N
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="value">要减去的值</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        long HDecrement(string key, string fieldName, long value = 1, int db = -1);

        /// <summary>
        /// 【Hash】获取所有数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        IList<T> HKeys<T>(string key, int db = -1);

        /// <summary>
        /// 【Set】添加数据
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="value">需要添加的数据</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        bool SAdd(string key, string value, int db = -1);

        /// <summary>
        /// 【Set】添加数据
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="values">需要添加的数据集合</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        long SAdd(string key, IList<RedisValue> values, int db = -1);

        /// <summary>
        /// 【Set】获取所有数据
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        RedisValue[] SMembers(string key, int db = -1);

        /// <summary>
        /// 【Set】集合是否有该数据
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="value">数据</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        bool SContains(string key, string value, int db = -1);

        /// <summary>
        /// 【Set】集合长度
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        long SLength(string key, int db = -1);

        /// <summary>
        /// 【Set】获取并移除数据：随机返回一个数据，并移除该数据
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        string SPop(string key, int db = -1);

        /// <summary>
        /// 【Sorted Set】添加数据
        /// </summary>
        /// <typeparam name="T">添加数据的类型</typeparam>
        /// <param name="key">缓存KEY</param>
        /// <param name="value">数据</param>
        /// <param name="score">排序序号</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        bool SSortedAdd<T>(string key, T value, double score, int db = -1);

        /// <summary>
        /// 【Sorted Set】删除数据
        /// </summary>
        /// <typeparam name="T">删除数据的类型</typeparam>
        /// <param name="key">缓存KEY</param>
        /// <param name="value">数据</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        bool SSortedRemove<T>(string key, T value, int db = -1);

        /// <summary>
        /// 【Sorted Set】获取全部数据
        /// </summary>
        /// <typeparam name="T">返回数据的类型</typeparam>
        /// <param name="key">缓存KEY</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        IList<T> SSortedRangeByRank<T>(string key, int db = -1);

        /// <summary>
        /// 【Sorted Set】获取有序集合长度
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="db">数据库，默认-1</param>
        /// <returns></returns>
        long SSortedLength(string key, int db = -1);

        /// <summary>
        /// Redis发布订阅:订阅
        /// </summary>
        /// <param name="subChannel">主题</param>
        /// <param name="handler"></param>
        /// <param name="connectionString">数据库连接字符串</param>
        void Subscribe(string subChannel, Action<RedisChannel, RedisValue> handler = null, string connectionString = null);

        /// <summary>
        /// Redis发布订阅:发布
        /// </summary>
        /// <typeparam name="T">消息数据的类型</typeparam>
        /// <param name="channel">消息管道</param>
        /// <param name="msg">消息数据</param>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <returns></returns>
        long Publish<T>(string channel, T msg, string connectionString = null);

        /// <summary>
        /// Redis发布订阅:取消订阅
        /// </summary>
        /// <param name="channel">消息管道</param>
        /// <param name="connectionString">数据库连接字符串</param>
        void Unsubscribe(string channel, string connectionString = null);


        /// <summary>
        /// Redis发布订阅  取消全部订阅
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        void UnsubscribeAll(string connectionString = null);
    }
}
