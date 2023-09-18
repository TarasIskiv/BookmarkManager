namespace Bookmark.Manager.Client.Logic.Abstraction
{
    public interface IStorageService
    {
        Task<T> GetDataFromStorage<T>(string key);
        Task WriteDataToStorage<T>(string key, T data);
        Task RemoveDataFromStorage(string key);
    }
}