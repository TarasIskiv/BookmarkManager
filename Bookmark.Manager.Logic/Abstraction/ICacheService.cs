using Bookmark.Manager.Core.Helpers;

namespace Bookmark.Manager.Logic.Abstraction
{
    public interface ICacheService
    {
        Task UpdateCache<T>(string key, T data);
        Task<T> GetDataFromCahce<T>(string key);
        string GenerateKey(int userId, int? parentFolderId, RedisCacheKey key);
    }
}