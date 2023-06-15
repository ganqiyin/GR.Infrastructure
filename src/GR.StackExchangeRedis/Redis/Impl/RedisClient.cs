using StackExchange.Redis;
using System.Text.Json;

namespace GR.Redis.Impl
{
    public class RedisClient : IRedisClient
    {
        private readonly RedisConnection _connection;
        public RedisClient(RedisConnection connection)
        {
            _connection = connection;
        }

        public IDatabase GetDB(int db = -1)
        {
            return _connection.GetConnect().GetDatabase(db: db);
        }

        public string CombinePrefixForKey(string key)
        {
            var keyPrefix = _connection.GetKeyPrefix();
            return string.IsNullOrWhiteSpace(keyPrefix) ? key : string.Format("{0}:{1}", keyPrefix, key);
        }

        public bool KeyDelete(string key, int db = -1)
        {
            return GetDB(db: db).KeyDelete(CombinePrefixForKey(key));
        }

        public long KeyDelete(IList<string> keys, int db = -1)
        {
            var rkeys = keys.Select(x => (RedisKey)CombinePrefixForKey(x)).ToArray();
            return GetDB(db: db).KeyDelete(rkeys);
        }

        public bool KeyIsExists(string key, int db = -1)
        {
            return GetDB(db: db).KeyExists(CombinePrefixForKey(key));
        }

        public bool KeyRename(string orgKey, string newKey, int db = -1)
        {
            return GetDB(db: db).KeyRename(CombinePrefixForKey(orgKey), CombinePrefixForKey(newKey));
        }

        public bool KeyExpire(string key, TimeSpan? expire, int db = -1)
        {
            return GetDB(db: db).KeyExpire(CombinePrefixForKey(key), expire);
        }

        //================================以下是 string 相关=================================

        public bool Add(string key, string value, TimeSpan? expiry = null, int db = -1)
        {
            return GetDB(db: db).StringSet(CombinePrefixForKey(key), value, expiry: expiry);
        }

        public bool Add<T>(string key, T value, TimeSpan? expiry = null, int db = -1)
        {
            var data = JsonSerializer.Serialize(value);
            return GetDB(db: db).StringSet(CombinePrefixForKey(key), data, expiry: expiry);
        }

        public string Get(string key, int db = -1)
        {
            return GetDB(db: db).StringGet(CombinePrefixForKey(key));
        }

        public T Get<T>(string key, int db = -1)
        {
            var value = Get(key, db: db);
            var data = JsonSerializer.Deserialize<T>(value);
            return data;
        }

        public long Increment(string key, long value = 1, int db = -1)
        {
            return GetDB(db: db).StringIncrement(CombinePrefixForKey(key), value);
        }

        public long Decrement(string key, long value = 1, int db = -1)
        {
            return GetDB(db: db).StringDecrement(CombinePrefixForKey(key), value);
        }

        //================================以下是 list 队列相关=================================

        public void LRightPush<T>(string key, T value, int db = -1)
        {
            GetDB(db: db).ListRightPush(CombinePrefixForKey(key), JsonSerializer.Serialize(value));
        }

        public T LRightPop<T>(string key, int db = -1)
        {
            var data = GetDB(db: db).ListRightPop(CombinePrefixForKey(key));
            return JsonSerializer.Deserialize<T>(data);
        }

        public void LLeftPush<T>(string key, T value, int db = -1)
        {
            GetDB(db: db).ListLeftPush(CombinePrefixForKey(key), JsonSerializer.Serialize(value));
        }

        public T LLeftPop<T>(string key, int db = -1)
        {
            var data = GetDB(db: db).ListLeftPop(CombinePrefixForKey(key));
            return JsonSerializer.Deserialize<T>(data);
        }

        public void LRemove<T>(string key, T value, int db = -1)
        {
            GetDB(db: db).ListRemove(CombinePrefixForKey(key), JsonSerializer.Serialize(value));
        }

        public IList<T> LRange<T>(string key, int db = -1)
        {
            var datas = GetDB(db: db).ListRange(CombinePrefixForKey(key));
            return datas.ToModel<T>();
        }

        public long LLength(string key, int db = -1)
        {
            return GetDB(db: db).ListLength(CombinePrefixForKey(key));
        }

        //================================以下是 Hash 相关=================================

        public bool HIsExists(string key, string fieldName, int db = -1)
        {
            return GetDB(db: db).HashExists(CombinePrefixForKey(key), fieldName);
        }

        public bool HSet<T>(string key, string fieldName, T value, int db = -1)
        {
            return GetDB(db: db).HashSet(CombinePrefixForKey(key), fieldName, JsonSerializer.Serialize(value));
        }

        public T HGet<T>(string key, string fieldName, int db = -1)
        {
            var data = GetDB(db: db).HashGet(CombinePrefixForKey(key), fieldName);
            return JsonSerializer.Deserialize<T>(data);
        }

        public bool HDelete(string key, string fieldName, int db = -1)
        {
            return GetDB(db: db).HashDelete(CombinePrefixForKey(key), fieldName);
        }

        public long HDelete(string key, IList<RedisValue> fieldNames, int db = -1)
        {
            return GetDB(db: db).HashDelete(CombinePrefixForKey(key), fieldNames.ToArray());
        }

        public long HIncrement(string key, string fieldName, long value = 1, int db = -1)
        {
            return GetDB(db: db).HashIncrement(CombinePrefixForKey(key), fieldName, value: value);
        }

        public long HDecrement(string key, string fieldName, long value = 1, int db = -1)
        {
            return GetDB(db: db).HashDecrement(CombinePrefixForKey(key), fieldName, value: value);
        }

        public IList<T> HKeys<T>(string key, int db = -1)
        {
            var datas = GetDB(db: db).HashKeys(CombinePrefixForKey(key));
            return datas.ToModel<T>();
        }

        //================================以下是 Set 相关=================================

        public bool SAdd(string key, string value, int db = -1)
        {
            return GetDB(db: db).SetAdd(CombinePrefixForKey(key), value);
        }

        public long SAdd(string key, IList<RedisValue> values, int db = -1)
        {
            return GetDB(db: db).SetAdd(CombinePrefixForKey(key), values.ToArray());
        }

        public RedisValue[] SMembers(string key, int db = -1)
        {
            return GetDB(db: db).SetMembers(CombinePrefixForKey(key));
        }

        public bool SContains(string key, string value, int db = -1)
        {
            return GetDB(db: db).SetContains(CombinePrefixForKey(key), value);
        }

        public long SLength(string key, int db = -1)
        {
            return GetDB(db: db).SetLength(CombinePrefixForKey(key));
        }

        public string SPop(string key, int db = -1)
        {
            return GetDB(db: db).SetPop(CombinePrefixForKey(key));
        }


        //================================以下是 Sorted Set （有序集合）相关=================================

        public bool SSortedAdd<T>(string key, T value, double score, int db = -1)
        {
            return GetDB(db: db).SortedSetAdd(CombinePrefixForKey(key), JsonSerializer.Serialize(value), score);
        }

        public bool SSortedRemove<T>(string key, T value, int db = -1)
        {
            return GetDB(db: db).SortedSetRemove(CombinePrefixForKey(key), JsonSerializer.Serialize(value));
        }

        public IList<T> SSortedRangeByRank<T>(string key, int db = -1)
        {
            var datas = GetDB(db: db).SortedSetRangeByRank(CombinePrefixForKey(key));
            return datas.ToModel<T>();
        }

        public long SSortedLength(string key, int db = -1)
        {
            return GetDB(db: db).SortedSetLength(CombinePrefixForKey(key));
        }

        //================================以下是 发布订阅 相关=================================

        public void Subscribe(string subChannel, Action<RedisChannel, RedisValue> handler = null, string connectionString = null)
        {
            ISubscriber sub = _connection.GetConnect(connectionString: connectionString).GetSubscriber();
            sub.Subscribe(subChannel, (channel, message) =>
            {
                if (handler == null)
                {
                    Console.WriteLine(subChannel + " 订阅收到消息：" + message);
                }
                else
                {
                    handler(channel, message);
                }
            });
        }

        public long Publish<T>(string channel, T msg, string connectionString = null)
        {
            ISubscriber sub = _connection.GetConnect(connectionString: connectionString).GetSubscriber();
            return sub.Publish(channel, JsonSerializer.Serialize(msg));
        }

        public void Unsubscribe(string channel, string connectionString = null)
        {
            ISubscriber sub = _connection.GetConnect(connectionString: connectionString).GetSubscriber();
            sub.Unsubscribe(channel);
        }

        public void UnsubscribeAll(string connectionString = null)
        {
            ISubscriber sub = _connection.GetConnect(connectionString: connectionString).GetSubscriber();
            sub.UnsubscribeAll();
        }
    }
}
