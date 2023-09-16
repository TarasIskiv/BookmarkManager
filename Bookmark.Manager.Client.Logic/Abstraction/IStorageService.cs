namespace Bookmark.Manager.Client.Logic.Abstraction
{
    public interface IStorageService
    {
        Task<T> GetDataFromStorage<T>(string key);
        Task WriteDataToStorage(string key, Task data);
        Task RemoveDataFromStorage(string key);
    }
}