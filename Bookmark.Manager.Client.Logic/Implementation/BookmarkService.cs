using Bookmark.Manager.Client.Logic.Abstraction;
using Bookmark.Manager.Core.Models;

namespace Bookmark.Manager.Client.Logic.Implementation
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