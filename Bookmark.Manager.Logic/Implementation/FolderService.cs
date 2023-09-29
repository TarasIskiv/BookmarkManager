using Bookmark.Manager.Core.CustomModels;
using Bookmark.Manager.Core.Helpers;
using Bookmark.Manager.Core.Models;
using Bookmark.Manager.Core.Payloads;
using Bookmark.Manager.Logic.Abstraction;
using Bookmark.Manager.Logic.Mapping;
using Bookmark.Manager.Repository.Abstraction;

namespace Bookmark.Manager.Logic.Implementation
{
    public class FolderService : IFolderService
    {
        private readonly ICacheService _cacheService;
        private readonly IFolderRepository _folderRepository;

        public FolderService(ICacheService cacheService, IFolderRepository folderRepository)
        {
            _cacheService = cacheService;
            _folderRepository = folderRepository;
        }

        public async Task CreateFolder(EditableFolderPayload folder)
        {
            var mappedFolder = folder.ToFolder();
            await _folderRepository.CreateFolder(mappedFolder);
            await UpdateFoldersCache(folder.UserId, folder.ParentFolderId);
        }

        public async Task<Folder> GetFolder(int userId, int folderId)
        {
            return await _folderRepository.GetFolder(userId, folderId);
        }

        public async Task<List<FolderBreadcrumb>> GetFolderBreadcrumbs(int userId, int folderId)
        {
            var folder = await _folderRepository.GetFolder(userId, folderId);
            if (folder is null) return new();
            var folderStack = new Stack<Folder>();
            folderStack.Push(folder);
            while (folder.ParentFolderId is not null)
            {
                folder = await _folderRepository.GetFolder(userId, folder.ParentFolderId.Value);
                folderStack.Push(folder);
            }

            return folderStack.Select(f => new FolderBreadcrumb() { Id = f.Id, Name = f.Name }).ToList();
        }

        public async Task<List<Folder>> GetNestedFolders(int userId, int? parentFolderId)
        {
            var key = _cacheService.GenerateKey(userId, parentFolderId, RedisCacheKey.Folders);
            var folders = await _cacheService.GetDataFromCahce<List<Folder>>(key);
            if(folders is default(List<Folder>) || !folders.Any())
            {
                folders = await _folderRepository.GetNestedFolders(userId, parentFolderId);
                await _cacheService.UpdateCache(key, folders);
            }
            return folders;
        }

        public async Task RemoveFolder(int userId, int folderId)
        {
            var folder = await _folderRepository.GetFolder(userId, folderId);
            await _folderRepository.RemoveFolder(folder);
            await UpdateFoldersCache(userId, folder.ParentFolderId);
        }

        public async Task UpdateFolder(int folderId, EditableFolderPayload folder)
        {
            var mappedFolder = folder.ToFolder();
            mappedFolder.Id = folderId;
            await _folderRepository.UpdateFolder(mappedFolder);
            await UpdateFoldersCache(folder.UserId, folder.ParentFolderId);
        }

        private async Task UpdateFoldersCache(int userId, int? parentFolderId = null)
        {
            var key = _cacheService.GenerateKey(userId, parentFolderId, RedisCacheKey.Folders);
            var folders = await _folderRepository.GetNestedFolders(userId, parentFolderId);
            await _cacheService.UpdateCache(key, folders);
        }
    }
}