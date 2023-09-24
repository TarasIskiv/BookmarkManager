using System.Net.Http.Json;
using Bookmark.Manager.Client.Logic.Abstraction;
using Bookmark.Manager.Core.Models;
using Bookmark.Manager.Core.Payloads;

namespace Bookmark.Manager.Client.Logic.Implementation
{
    public class FolderService : IFolderService
    {
        private readonly HttpClient _client;

        public FolderService(HttpClient client)
        {
            _client = client;
        }
        public async Task CreateFolder(EditableFolderPayload folder)
        {
            await _client.PostAsJsonAsync("api/Folder/AddFolder", folder);
        }

        public async Task<Folder> GetFolder(int folderId)
        {
            var folder = await _client.GetFromJsonAsync<Folder>($"api/Folder/GetFolder?folderId={folderId}");
            return folder ?? new();
        }

        public async Task<List<Folder>> GetNestedFolders(int? parentFolderId)
        {
            var folders = await _client.GetFromJsonAsync<List<Folder>>($"api/Folder/GetFolders?parentFolderId={parentFolderId}");
            return folders ?? new();
        }

        public async Task RemoveFolder(int folderId)
        {
            await _client.DeleteAsync($"api/Folder/RemoveFolder?folderId={folderId}");
        }

        public async Task UpdateFolder(int folderId, EditableFolderPayload folder)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"api/Folder/UpdateFolder?folderId={folderId}");
            request.Content = JsonContent.Create(folder);
            await _client.SendAsync(request);
        }
    }
}