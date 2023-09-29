using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookmark.Manager.Core.CustomModels;
using Bookmark.Manager.Core.Models;
using Bookmark.Manager.Core.Payloads;

namespace Bookmark.Manager.Client.Logic.Abstraction
{
    public interface IFolderService
    {
        Task CreateFolder(EditableFolderPayload folder);
        Task<Folder> GetFolder(int folderId);
        Task<List<Folder>> GetNestedFolders(int? parentFolderId);
        Task UpdateFolder(int folderId, EditableFolderPayload folder);
        Task RemoveFolder(int folderId);
        Task<List<FolderBreadcrumb>> GetFolderBreadcrumbs(int folderId);
    }
}