using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Amazon.Runtime.Internal.Util;
using Booking.Application.Caching;
using StackExchange.Redis;

namespace Booking.Persistence.Caching
{
    public class RedisCacheService : ICacheService
    {
        private readonly IConnectionMultiplexer redisCon;
        private readonly IDatabase cache;
        private TimeSpan ExpireTime => TimeSpan.FromDays(1);

        public RedisCacheService(IConnectionMultiplexer redisCon)
        {
            this.redisCon = redisCon;
            this.cache = redisCon.GetDatabase();
        }

        public async Task Clear(string key)
        {
            await cache.KeyDeleteAsync(key);
        }

        public void ClearAll()
        {
            var endpoints = redisCon.GetEndPoints(true);

            foreach (var endpoint in endpoints)
            {
                var server = redisCon.GetServer(endpoint);
                server.FlushAllDatabases();
            }
        }

        public T GetOrAdd<T>(string key, Func<T> action) where T : class
        {
            var result = cache.StringGet(key);
            if (result.IsNull)
            {
                result = JsonSerializer.SerializeToUtf8Bytes(action());
                cache.StringSet(key, result, ExpireTime);
            }
            return JsonSerializer.Deserialize<T>(result);
        }

        public async Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> action) where T : class
        {
            var result = await cache.StringGetAsync(key);
            if (result.IsNull)
            {
                result = JsonSerializer.SerializeToUtf8Bytes(await action());
                cache.StringSet(key, result);
            }
            return JsonSerializer.Deserialize<T>(result);
        }

        public async Task<string> GetValueAsync(string key)
        {
            return await cache.StringGetAsync(key);
        }

        public async Task<bool> SetValueAsync(string key, string value)
        {
            return await cache.StringSetAsync(key, value, ExpireTime);
        }
    }
}
