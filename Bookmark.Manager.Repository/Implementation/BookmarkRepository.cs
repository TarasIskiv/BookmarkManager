using Bookmark.Manager.Core.Models;
using Bookmark.Manager.Database;
using Bookmark.Manager.Repository.Abstraction;

namespace Bookmark.Manager.Repository.Implementation
{
    public class BookmarkRepository : IBookmarkRepository
    {
        private readonly BookmarkManagerContext _context;

        public BookmarkRepository(BookmarkManagerContext context)
        {
            _context = context;
        }
        public async Task AddBookmark(UserBookmark bookmark)
        {
            await _context.Bookmarks.AddAsync(bookmark);
            await _context.SaveChangesAsync();
        }

        public async Task<UserBookmark> GetBookmark(int userId, int bookmarkId)
        {
            var bookmark = await Task.Run(() => _context.Bookmarks.SingleOrDefault(bookmark => bookmark.UserId == userId && bookmark.Id == bookmarkId));
            return bookmark ?? default!;
        }

        public async Task<List<UserBookmark>> GetFolderBookmarks(int userId, int folderId)
        {
            var bookmarks = await Task.Run(() => _context.Bookmarks.Where(bookmark => bookmark.UserId == userId && bookmark.FolderId == folderId).ToList());
            return bookmarks ?? new();
        }

        public async Task RemoveBookmark(UserBookmark bookmark)
        {
            _context.Bookmarks.Remove(bookmark);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookmark(UserBookmark bookmark)
        {
            _context.Bookmarks.Update(bookmark);
            await _context.SaveChangesAsync();
        }
    }
}