using Bookmark.Manager.Logic.Abstraction;

namespace Bookmark.Manager.Logic.Implementation
{
    public class CacheService : ICacheService
    {
        public Task<T> GetDataFromCahce<T>(string key)
        {
            throw new NotImplementedException();
        }

        public Task WriteToCache<T>(string key, T data)
        {
            throw new NotImplementedException();
        }
    }
}