namespace Bookmark.Manager.Logic.Abstraction
{
    public interface ICacheService
    {
        Task WriteToCache<T>(string key, T data);
        Task<T> GetDataFromCahce<T>(string key);
    }
}