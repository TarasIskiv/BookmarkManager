using Bookmark.Manager.Core.Models;
using Bookmark.Manager.Core.Payloads;

namespace Bookmark.Manager.Logic.Abstraction
{
    public interface IBookmarkService
    {
        Task<List<UserBookmark>> GetFolderBookmarks(int userId, int folderId);
        Task UpdateBookmark(EditableBookmarkPayload bookmark);
        Task RemoveBookmark(int userId, int bookmarkId);
        Task<UserBookmark> GetBookmark(int userId, int bookmarkId);
        Task AddBookmark(int userId, EditableBookmarkPayload bookmarkPayload);
    }
}