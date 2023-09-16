using Bookmark.Manager.Core.Models;
using Bookmark.Manager.Logic.Abstraction;

namespace Bookmark.Manager.Logic.Implementation
{
    public class BookmarkService : IBookmarkService
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