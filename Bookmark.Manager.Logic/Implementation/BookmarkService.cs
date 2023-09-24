using Bookmark.Manager.Core.Helpers;
using Bookmark.Manager.Core.Models;
using Bookmark.Manager.Core.Payloads;
using Bookmark.Manager.Logic.Abstraction;
using Bookmark.Manager.Logic.Mapping;
using Bookmark.Manager.Repository.Abstraction;

namespace Bookmark.Manager.Logic.Implementation
{
    public class BookmarkService : IBookmarkService
    {
        private readonly IBookmarkRepository _bookmarkRepository;
        private readonly ICacheService _cacheService;

        public BookmarkService(IBookmarkRepository bookmarkRepository, ICacheService cacheService)
        {
            _bookmarkRepository = bookmarkRepository;
            _cacheService = cacheService;
        }

        public async Task AddBookmark(EditableBookmarkPayload bookmarkPayload)
        {
            var bookmark = bookmarkPayload.ToBookmark();
            await _bookmarkRepository.AddBookmark(bookmark);
            await UpdateBookmarksCache(bookmarkPayload.UserId, bookmarkPayload.FolderId);
        }

        public async Task<UserBookmark> GetBookmark(int userId, int bookmarkId)
        {
            return await _bookmarkRepository.GetBookmark(userId, bookmarkId);
        }

        public async Task<List<UserBookmark>> GetFolderBookmarks(int userId, int folderId)
        {
            var key = _cacheService.GenerateKey(userId, folderId, RedisCacheKey.Bookmarks);
            var bookmarks = await _cacheService.GetDataFromCahce<List<UserBookmark>>(key);
            if (bookmarks is default(List<UserBookmark>) || !bookmarks.Any())
            {
                bookmarks = await _bookmarkRepository.GetFolderBookmarks(userId, folderId);
                await _cacheService.UpdateCache(key, bookmarks);
            }

            return bookmarks;
        }

        public async Task RemoveBookmark(int userId, int bookmarkId)
        {
            var bookmark = await _bookmarkRepository.GetBookmark(userId, bookmarkId);
            await _bookmarkRepository.RemoveBookmark(bookmark);
            await UpdateBookmarksCache(userId, bookmark.FolderId);
        }

        public async Task UpdateBookmark(EditableBookmarkPayload bookmark)
        {
            var dbBookmark = bookmark.ToBookmark();
            await _bookmarkRepository.UpdateBookmark(dbBookmark);
            await UpdateBookmarksCache(bookmark.UserId, bookmark.FolderId);
        }

        private async Task UpdateBookmarksCache(int userId, int folderId)
        {
            var key = _cacheService.GenerateKey(userId, folderId, RedisCacheKey.Bookmarks);
            var bookmarks = await _bookmarkRepository.GetFolderBookmarks(userId, folderId);
            await _cacheService.UpdateCache(key, bookmarks);
            
        }
    }
}