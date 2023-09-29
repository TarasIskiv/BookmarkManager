using System;
using System.Text.Json;
using Bookmark.Manager.Client.Logic.Abstraction;
using Bookmark.Manager.Core.Helpers;
using Microsoft.JSInterop;

namespace Bookmark.Manager.Client.Logic.Implementation
{
    public class SessionStorageService : ISessionStorageService
    {
        private readonly IJSRuntime _jsRuntime;

        public SessionStorageService(IJSRuntime jSRuntime)
        {
            _jsRuntime = jSRuntime;
        }
        public async Task<T> Get<T>(CacheKey key)
        {
             var data = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", key.ToString());
             return JsonSerializer.Deserialize<T>(data) ?? default(T)!;
        }

        public async Task SetData<T>(CacheKey key, T data)
        {
            var serializedData = JsonSerializer.Serialize(data);
            await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", key.ToString(), serializedData);
        }
    }
}

