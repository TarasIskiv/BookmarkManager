namespace Bookmark.Manager.Logic.Abstraction
{
    public interface ICacheService
    {
        Task WriteToCache<T>(string key, T data, TimeSpan time);
        Task<T> GetDataFromCahce<T>(string key);
        Task RemoveFromCahce(string key);
    }
}