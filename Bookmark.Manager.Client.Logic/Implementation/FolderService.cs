using Bookmark.Manager.Client.Logic.Abstraction;
using Bookmark.Manager.Core.Models;

namespace Bookmark.Manager.Client.Logic.Implementation
{
    public class FolderService : IFolderService
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