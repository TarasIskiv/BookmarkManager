using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookmark.Manager.Core.Models;

namespace Bookmark.Manager.Client.Logic.Abstraction
{
    public interface IBookmarkService
    {
        Task<List<UserBookmark>> GetFolderBookmarks(int folderId);
        Task UpdateBookmark(int id, UserBookmark bookmark);
        Task RemoveBookmark(int id, UserBookmark bookmark); 
    }
}