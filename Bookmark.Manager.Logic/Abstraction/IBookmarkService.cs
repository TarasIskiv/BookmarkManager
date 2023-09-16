using Bookmark.Manager.Core.Models;

namespace Bookmark.Manager.Logic.Abstraction
{
    public interface IBookmarkService
    {
        Task<List<UserBookmark>> GetFolderBookmarks(int folderId);
        Task UpdateBookmark(int id, UserBookmark bookmark);
        Task RemoveBookmark(int id, UserBookmark bookmark); 
    }
}