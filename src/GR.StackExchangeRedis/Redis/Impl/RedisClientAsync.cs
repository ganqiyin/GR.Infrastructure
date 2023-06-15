using StackExchange.Redis;
using System.Text.Json;

namespace GR.Redis.Impl
{
    public class RedisClientAsync : RedisClient, IRedisClientAsync
    {
        public RedisClientAsync(RedisConnection connection)
            : base(connection)
        {
        }

        public async Task<bool> KeyDeleteAsync(string key, int db = -1)
        {
            return await GetDB(db: db).KeyDeleteAsync(CombinePrefixForKey(key));
        }

        public async Task<long> KeyDeleteAsync(IList<string> keys, int db = -1)
        {
            var rkeys = keys.Select(x => (RedisKey)CombinePrefixForKey(x)).ToArray();
            return await GetDB(db: db).KeyDeleteAsync(rkeys);
        }

        public async Task<bool> KeyIsExistsAsync(string key, int db = -1)
        {
            return await GetDB(db: db).KeyExistsAsync(CombinePrefixForKey(key));
        }

        public async Task<bool> KeyRenameAsync(string orgKey, string newKey, int db = -1)
        {
            return await GetDB(db: db).KeyRenameAsync(CombinePrefixForKey(orgKey), CombinePrefixForKey(newKey));
        }

        public async Task<bool> KeyExpireAsync(string key, TimeSpan? expire, int db = -1)
        {
            return await GetDB(db: db).KeyExpireAsync(CombinePrefixForKey(key), expire);
        }

        //================================以下是 string 相关=================================

        public async Task<bool> AddAsync(string key, string value, TimeSpan? expiry = null, int db = -1)
        {
            return await GetDB(db: db).StringSetAsync(CombinePrefixForKey(key), value, expiry: expiry);
        }

        public async Task<bool> AddAsync<T>(string key, T value, TimeSpan? expiry = null, int db = -1)
        {
            var data = JsonSerializer.Serialize(value);
            return await GetDB(db: db).StringSetAsync(CombinePrefixForKey(key), data, expiry: expiry);
        }

        public async Task<string> GetAsync(string key, int db = -1)
        {
            return await GetDB(db: db).StringGetAsync(CombinePrefixForKey(key));
        }

        public async Task<T> GetAsync<T>(string key, int db = -1)
        {
            var value = await GetAsync(key, db: db);
            var data = JsonSerializer.Deserialize<T>(value);
            return data;
        }

        public async Task<long> IncrementAsync(string key, long value = 1, int db = -1)
        {
            return await GetDB(db: db).StringIncrementAsync(CombinePrefixForKey(key), value);
        }

        public async Task<long> DecrementAsync(string key, long value = 1, int db = -1)
        {
            return await GetDB(db: db).StringDecrementAsync(CombinePrefixForKey(key), value);
        }

        //================================以下是 list 队列相关=================================

        public async Task LRightPushAsync<T>(string key, T value, int db = -1)
        {
            await GetDB(db: db).ListRightPushAsync(CombinePrefixForKey(key), JsonSerializer.Serialize(value));
        }

        public async Task<T> LRightPopAsync<T>(string key, int db = -1)
        {
            var data = await GetDB(db: db).ListRightPopAsync(CombinePrefixForKey(key));
            return JsonSerializer.Deserialize<T>(data);
        }

        public async Task LLeftPushAsync<T>(string key, T value, int db = -1)
        {
            await GetDB(db: db).ListLeftPushAsync(CombinePrefixForKey(key), JsonSerializer.Serialize(value));
        }

        public async Task<T> LLeftPopAsync<T>(string key, int db = -1)
        {
            var data = await GetDB(db: db).ListLeftPopAsync(CombinePrefixForKey(key));
            return JsonSerializer.Deserialize<T>(data);
        }

        public async Task LRemoveAsync<T>(string key, T value, int db = -1)
        {
            await GetDB(db: db).ListRemoveAsync(CombinePrefixForKey(key), JsonSerializer.Serialize(value));
        }

        public async Task<IList<T>> LRangeAsync<T>(string key, int db = -1)
        {
            var datas = await GetDB(db: db).ListRangeAsync(CombinePrefixForKey(key));
            var list = new List<T>();
            foreach (var data in datas)
            {
                var m = JsonSerializer.Deserialize<T>(data);
                list.Add(m);
            }
            return list;
        }

        public async Task<long> LLengthAsync(string key, int db = -1)
        {
            return await GetDB(db: db).ListLengthAsync(CombinePrefixForKey(key));
        }

        //================================以下是 Hash 相关=================================

        public async Task<bool> HIsExistsAsync(string key, string fieldName, int db = -1)
        {
            return await GetDB(db: db).HashExistsAsync(CombinePrefixForKey(key), fieldName);
        }

        public async Task<bool> HSetAsync<T>(string key, string fieldName, T value, int db = -1)
        {
            return await GetDB(db: db).HashSetAsync(CombinePrefixForKey(key), fieldName, JsonSerializer.Serialize(value));
        }

        public async Task<T> HGetAsync<T>(string key, string fieldName, int db = -1)
        {
            var data = await GetDB(db: db).HashGetAsync(CombinePrefixForKey(key), fieldName);
            return JsonSerializer.Deserialize<T>(data);
        }

        public async Task<bool> HDeleteAsync(string key, string fieldName, int db = -1)
        {
            return await GetDB(db: db).HashDeleteAsync(CombinePrefixForKey(key), fieldName);
        }

        public async Task<long> HDeleteAsync(string key, IList<RedisValue> fieldNames, int db = -1)
        {
            return await GetDB(db: db).HashDeleteAsync(CombinePrefixForKey(key), fieldNames.ToArray());
        }

        public async Task<long> HIncrementAsync(string key, string fieldName, long value = 1, int db = -1)
        {
            return await GetDB(db: db).HashIncrementAsync(CombinePrefixForKey(key), fieldName, value: value);
        }

        public async Task<long> HDecrementAsync(string key, string fieldName, long value = 1, int db = -1)
        {
            return await GetDB(db: db).HashDecrementAsync(CombinePrefixForKey(key), fieldName, value: value);
        }

        public async Task<IList<T>> HKeysAsync<T>(string key, int db = -1)
        {
            var datas = await GetDB(db: db).HashKeysAsync(CombinePrefixForKey(key));
            var list = new List<T>();
            if (datas != null && datas.Length > 0)
            {
                foreach (var data in datas)
                {
                    var m = JsonSerializer.Deserialize<T>(data);
                    list.Add(m);
                }
            }
            return list;
        }

        //================================以下是 Set 相关=================================

        public async Task<bool> SAddAsync(string key, string value, int db = -1)
        {
            return await GetDB(db: db).SetAddAsync(CombinePrefixForKey(key), value);
        }

        public async Task<long> SAddAsync(string key, IList<RedisValue> values, int db = -1)
        {
            return await GetDB(db: db).SetAddAsync(CombinePrefixForKey(key), values.ToArray());
        }

        public async Task<RedisValue[]> SMembersAsync(string key, int db = -1)
        {
            return await GetDB(db: db).SetMembersAsync(CombinePrefixForKey(key));
        }

        public async Task<bool> SContainsAsync(string key, string value, int db = -1)
        {
            return await GetDB(db: db).SetContainsAsync(CombinePrefixForKey(key), value);
        }

        public async Task<long> SLengthAsync(string key, int db = -1)
        {
            return await GetDB(db: db).SetLengthAsync(CombinePrefixForKey(key));
        }

        public async Task<string> SPopAsync(string key, int db = -1)
        {
            return await GetDB(db: db).SetPopAsync(CombinePrefixForKey(key));
        }

        //================================以下是  Sorted Set （有序集合） 相关=================================

        public async Task<bool> SSortedAddAsync<T>(string key, T value, double score, int db = -1)
        {
            return await GetDB(db: db).SortedSetAddAsync(CombinePrefixForKey(key), JsonSerializer.Serialize(value), score);
        }

        public async Task<bool> SSortedRemoveAsync<T>(string key, T value, int db = -1)
        {
            return await GetDB(db: db).SortedSetRemoveAsync(CombinePrefixForKey(key), JsonSerializer.Serialize(value));
        }

        public async Task<IList<T>> SSortedRangeByRankAsync<T>(string key, int db = -1)
        {
            var datas = await GetDB(db: db).SortedSetRangeByRankAsync(CombinePrefixForKey(key));
            return datas.ToModel<T>();
        }

        public async Task<long> SSortedLengthAsync(string key, int db = -1)
        {
            return await GetDB(db: db).SortedSetLengthAsync(CombinePrefixForKey(key));
        }
    }
}
