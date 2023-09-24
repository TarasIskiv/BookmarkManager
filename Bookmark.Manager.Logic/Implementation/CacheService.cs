using System.Text.Json;
using Bookmark.Manager.Logic.Abstraction;
using Microsoft.Extensions.Caching.Distributed;

namespace Bookmark.Manager.Logic.Implementation
{
    public class CacheService : ICacheService
    {
        private IDistributedCache _cache;
        private DistributedCacheEntryOptions _options { get; set; } = new();

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
            _options.AbsoluteExpirationRelativeToNow = new TimeSpan(0, 30, 0);
        }

        public async Task<T> GetDataFromCahce<T>(string key)
        {
            var data = await _cache.GetStringAsync(key);
            if (data is null) return default!;

            var parsedData = JsonSerializer.Deserialize<T>(data);
            return parsedData ?? default!;
        }

        public async Task WriteToCache<T>(string key, T data, TimeSpan time)
        {
            var serializedData = JsonSerializer.Serialize(data);
            await _cache.SetStringAsync(key, serializedData, _options);

        }

        public async Task RemoveFromCahce(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}