using Bookmark.Manager.Core.Models;
using Bookmark.Manager.Repository.Abstraction;

namespace Bookmark.Manager.Repository.Implementation
{
    public class FolderRepository : IFolderRepository
    {
        public Task CreateFolder(Folder folder)
        {
            throw new NotImplementedException();
        }

        public Task<Folder> GetFolder(int folderId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Folder>> GetNestedFolders(int parentFolderId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFolder(int folderId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateFolder(int folderId, Folder folder)
        {
            throw new NotImplementedException();
        }
    }
}