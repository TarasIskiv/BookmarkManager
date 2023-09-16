using Bookmark.Manager.Core.Models;
using Bookmark.Manager.Repository.Abstraction;

namespace Bookmark.Manager.Repository.Implementation
{
    public class BookmarkRepository : IBookmarkRepository
    {
        public Task<List<UserBookmark>> GetFolderBookmarks(int folderId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveBookmark(int id, UserBookmark bookmark)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBookmark(int id, UserBookmark bookmark)
        {
            throw new NotImplementedException();
        }
    }
}