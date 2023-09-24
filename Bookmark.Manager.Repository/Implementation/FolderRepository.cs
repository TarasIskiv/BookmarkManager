using Bookmark.Manager.Core.Models;
using Bookmark.Manager.Database;
using Bookmark.Manager.Repository.Abstraction;

namespace Bookmark.Manager.Repository.Implementation
{
    public class FolderRepository : IFolderRepository
    {
        private readonly BookmarkManagerContext _context;

        public FolderRepository(BookmarkManagerContext context)
        {
            _context = context;
        }

        public async Task CreateFolder(Folder folder)
        {
            await _context.Folders.AddAsync(folder);
            await _context.SaveChangesAsync();
        }

        public async Task<Folder> GetFolder(int userId, int folderId)
        {
            var folder = await Task.Run(() => _context.Folders.SingleOrDefault(folder => folder.UserId == userId && folder.Id == folderId));
            return folder ?? new Folder();
        }

        public async Task<List<Folder>> GetNestedFolders(int userId, int? parentFolderId)
        {
            var folders = await Task.Run(() => _context.Folders.Where(folder => folder.UserId == userId && folder.ParentFolderId == parentFolderId).ToList());
            return folders ?? new ();
        }

        public async Task RemoveFolder(Folder folder)
        {
            _context.Folders.Remove(folder);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFolder(Folder folder)
        {
            var dbFolder = await GetFolder(folder.UserId, folder.Id);
            if (dbFolder is null || dbFolder.Id == 0) return;
            _context.Folders.Update(folder);
            await _context.SaveChangesAsync();
        }
    }
}