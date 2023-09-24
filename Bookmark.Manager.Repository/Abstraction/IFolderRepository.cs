using Bookmark.Manager.Core.Models;

namespace Bookmark.Manager.Repository.Abstraction
{
    public interface IFolderRepository
    {
        Task CreateFolder(Folder folder);
        Task<Folder> GetFolder(int userId, int folderId);
        Task<List<Folder>> GetNestedFolders(int userId, int? parentFolderId);
        Task UpdateFolder(Folder folder);
        Task RemoveFolder(Folder folder);
    }
}