using Bookmark.Manager.Core.Models;

namespace Bookmark.Manager.Logic.Abstraction
{
    public interface IFolderService
    {
        Task CreateFolder(Folder folder);
        Task<Folder> GetFolder(int folderId);
        Task<List<Folder>> GetNestedFolders(int parentFolderId);
        Task UpdateFolder(int folderId, Folder folder);
        Task RemoveFolder(int folderId);
    }
}