using System;
using Bookmark.Manager.Core.Models;
using Bookmark.Manager.Database;
using Bookmark.Manager.Logic.Abstraction;
using Bookmark.Manager.Logic.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Bookmark.Manager.Logic.Seeder
{
	public static class SeederService
	{
		public static async Task Seed(IApplicationBuilder builder)
		{
			using var serviceScope = builder.ApplicationServices.CreateScope();
			var context = serviceScope.ServiceProvider.GetService<BookmarkManagerContext>();
			var passwordEncryptionService = serviceScope.ServiceProvider.GetService<IEncryptionService>();

			if (context is null || context.Users.Any() || passwordEncryptionService is null) return;

			var defaultUser = new User() { Email = "test@abc.com", Password = passwordEncryptionService.DecryptPassword("qwert123") };
			await context.Users.AddAsync(defaultUser);
			await context.SaveChangesAsync();
            defaultUser = context.Users.First(user => user.Email.Equals(defaultUser.Email));


			#region Folders

			var homeFolders = new List<Folder>()
			{

                 new Folder()
                 {
                     Name = "Coding",
                     UserId = defaultUser.Id,
                     ParentFolderId = null,
                     Id = 0
                 }
            };

            await context.Folders.AddRangeAsync(homeFolders);
            await context.SaveChangesAsync();

            #region Coding Folder

            var codingFolder = context.Folders.First(folder => folder.Name.Equals("Coding"));

            var codingFolders = new List<Folder>()
            {

                 new Folder()
                 {
                     Name = "DotNet",
                     UserId = defaultUser.Id,
                     ParentFolderId = codingFolder.Id
                 },
                new Folder()
                {
                    Name = "Angular",
                    UserId = defaultUser.Id,
                    ParentFolderId = codingFolder.Id
                }
            };

            await context.Folders.AddRangeAsync(codingFolders);
            await context.SaveChangesAsync();

            #endregion
            #endregion

            #region Bookmarks
            #region Coding Folder Bookmarks
            var codingBookmarks = new List<UserBookmark>()
            {
                new UserBookmark()
                {
                    Name = "Stack Overflow",
                    Color = "#ed861f",
                    URL = "https://stackoverflow.com/",
                    FolderId = codingFolder.Id,
                    UserId = defaultUser.Id
                },
                new UserBookmark()
                {
                    Name = "GitHub",
                    Color = "#0b066b",
                    URL = "https://github.com/",
                    FolderId = codingFolder.Id,
                    UserId = defaultUser.Id
                }
            };

            await context.Bookmarks.AddRangeAsync(codingBookmarks);
            await context.SaveChangesAsync();

            #endregion
            #region DotNet Folder Bookmarks

            var dotnetFolder = context.Folders.First(folder => folder.Name.Equals("DotNet"));

            var dotnetBookmarks = new List<UserBookmark>()
            {
                new UserBookmark()
                {
                    Name = "DotNet",
                    Color = "#46066b",
                    URL = "https://dotnet.microsoft.com/en-us/",
                    FolderId = dotnetFolder.Id,
                    UserId = defaultUser.Id
                },
                new UserBookmark()
                {
                    Name = "MudBlazor",
                    Color = "#8c26c7",
                    URL = "https://mudblazor.com/",
                    FolderId = dotnetFolder.Id,
                    UserId = defaultUser.Id
                }
            };

            await context.Bookmarks.AddRangeAsync(dotnetBookmarks);
            await context.SaveChangesAsync();

            #endregion
            #region Angular Folder Bookmarks

            var angularFolder = context.Folders.First(folder => folder.Name.Equals("Angular"));

            var angularBookmarks = new List<UserBookmark>()
            {
                new UserBookmark()
                {
                    Name = "Angular Guide",
                    Color = "#f21f18",
                    URL = "https://angular.io/guide/inputs-outputs",
                    FolderId = angularFolder.Id,
                    UserId = defaultUser.Id
                },
                new UserBookmark()
                {
                    Name = "RxJS",
                    Color = "#f21865",
                    URL = "https://rxjs.dev/",
                    FolderId = angularFolder.Id,
                    UserId = defaultUser.Id
                }
            };

            await context.Bookmarks.AddRangeAsync(angularBookmarks);
            await context.SaveChangesAsync();

            #endregion
            #endregion
        }

    }
}

