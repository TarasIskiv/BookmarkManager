using Bookmark.Manager.Core.Models;
using Bookmark.Manager.Core.Payloads;

namespace Bookmark.Manager.Logic.Abstraction
{
    public interface IFolderService
    {
        Task CreateFolder(EditableFolderPayload folder);
        Task<Folder> GetFolder(int userId, int folderId);
        Task<List<Folder>> GetNestedFolders(int userId, int? parentFolderId);
        Task UpdateFolder(int folderId, EditableFolderPayload folder);
        Task RemoveFolder(int userId, int folderId);
    }
}