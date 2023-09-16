using Bookmark.Manager.Core.Models;

namespace Bookmark.Manager.Repository.Abstraction
{
    public interface IBookmarkRepository
    {
        Task<List<UserBookmark>> GetFolderBookmarks(int folderId);
        Task UpdateBookmark(int id, UserBookmark bookmark);
        Task RemoveBookmark(int id, UserBookmark bookmark); 
    }
}