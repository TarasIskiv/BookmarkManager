using System.Text.Json;
using Bookmark.Manager.Core.Helpers;
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

        private async Task RemoveFromCahce(string key)
        {
            await _cache.RemoveAsync(key);
        }

        public string GenerateKey(int userId, int? parentFolderId, RedisCacheKey key)
        {
            var builderParams = new List<string>() { userId.ToString(), key.ToString() };
            if (parentFolderId.HasValue) builderParams.Add(parentFolderId.Value.ToString());
            return String.Join("/", builderParams);
        }

        public async Task UpdateCache<T>(string key, T data)
        {
            await RemoveFromCahce(key);
            var serializedData = JsonSerializer.Serialize(data);
            await _cache.SetStringAsync(key, serializedData, _options);
        }
    }
}