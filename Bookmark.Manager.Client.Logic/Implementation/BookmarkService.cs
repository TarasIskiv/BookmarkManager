using System.Net.Http.Json;
using Bookmark.Manager.Client.Logic.Abstraction;
using Bookmark.Manager.Core.Models;
using Bookmark.Manager.Core.Payloads;

namespace Bookmark.Manager.Client.Logic.Implementation
{
    public class BookmarkService : IBookmarkService
    {
        private readonly HttpClient _client;

        public BookmarkService(HttpClient client)
        {
            _client = client;
        }
        public async Task AddBookmark(EditableBookmarkPayload editableBookmark)
        {
            await _client.PostAsJsonAsync("api/Bookmark/AddBookmark", editableBookmark);
        }

        public async Task<UserBookmark> GetBookmark(int id)
        {
            var bookmark = await _client.GetFromJsonAsync<UserBookmark>($"api/Bookmark/GetBookmark?bookmarkId={id}");
            return bookmark ?? new ();
        }

        public async Task<List<UserBookmark>> GetFolderBookmarks(int folderId)
        {
            var bookmarks = await _client.GetFromJsonAsync<List<UserBookmark>>($"api/Bookmark/GetBookmarks?folderId={folderId}");
            return bookmarks ?? new();
        }

        public async Task RemoveBookmark(int id)
        {
            await _client.DeleteAsync($"api/Bookmark/RemoveBookmark?bookmarkId={id}");
        }

        public async Task UpdateBookmark(int bookmarkId, EditableBookmarkPayload bookmark)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"api/Bookmark/UpdateBookmark?bookmarkId={bookmarkId}");
            request.Content = JsonContent.Create(bookmark);
            await _client.SendAsync(request);
        }
    }
}