using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookmark.Manager.Core.Models;
using Bookmark.Manager.Core.Payloads;

namespace Bookmark.Manager.Client.Logic.Abstraction
{
    public interface IBookmarkService
    {
        Task<List<UserBookmark>> GetFolderBookmarks(int folderId);
        Task UpdateBookmark(int bookmarkId, EditableBookmarkPayload bookmark);
        Task AddBookmark(EditableBookmarkPayload editableBookmark);
        Task<UserBookmark> GetBookmark(int id);
        Task RemoveBookmark(int id); 
    }
}