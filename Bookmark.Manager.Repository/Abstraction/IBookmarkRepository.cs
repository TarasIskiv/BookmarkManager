using Bookmark.Manager.Core.Models;
using Bookmark.Manager.Core.Payloads;

namespace Bookmark.Manager.Repository.Abstraction
{
    public interface IBookmarkRepository
    {
        Task<List<UserBookmark>> GetFolderBookmarks(int userId, int folderId);
        Task UpdateBookmark(UserBookmark bookmark);
        Task RemoveBookmark(UserBookmark bookmark);
        Task<UserBookmark> GetBookmark(int userId, int bookmarkId);
        Task AddBookmark(UserBookmark bookmark);
    }
}