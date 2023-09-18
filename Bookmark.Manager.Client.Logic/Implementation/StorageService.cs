using Blazored.LocalStorage;
using Bookmark.Manager.Client.Logic.Abstraction;

namespace Bookmark.Manager.Client.Logic.Implementation
{
    public class StorageService : IStorageService
    {
        private readonly ILocalStorageService _localStorageService;

        public StorageService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }
        public async Task<T> GetDataFromStorage<T>(string key)
        {
            return await _localStorageService.GetItemAsync<T>(key);
        }

        public async Task RemoveDataFromStorage(string key)
        {
            await _localStorageService.RemoveItemAsync(key);
        }

        public async Task WriteDataToStorage<T>(string key, T data)
        {
            await _localStorageService.SetItemAsync(key, data);
        }
    }
}