using Microsoft.Extensions.Caching.Distributed;
using MsCoreOne.Application.Common.Interfaces;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace MsCoreOne.Infrastructure.Caching
{
    public class RedisCacheManager : IRedisCacheManager
    {
        private readonly IDistributedCache _distributedCache;

        public RedisCacheManager(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var valueByte = await _distributedCache.GetAsync(key);

            if (valueByte == null)
                return default(T);

            var valueStr = Encoding.UTF8.GetString(valueByte);

            return JsonConvert.DeserializeObject<T>(valueStr);
        }

        public async Task SetAsync<T>(string key, T value)
        {
            var valueStr = JsonConvert.SerializeObject(value);

            var valueByte = Encoding.UTF8.GetBytes(valueStr);

            var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));

            await _distributedCache.SetAsync(key, valueByte, options);
        }
    }
}
