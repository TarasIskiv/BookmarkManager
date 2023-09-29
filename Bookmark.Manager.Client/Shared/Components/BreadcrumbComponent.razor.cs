using System;
using Bookmark.Manager.Client.Logic.Abstraction;
using Bookmark.Manager.Core.CustomModels;
using Bookmark.Manager.Core.Helpers;
using Bookmark.Manager.Core.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using static MudBlazor.CategoryTypes;

namespace Bookmark.Manager.Client.Shared.Components
{
	partial class BreadcrumbComponent
	{
		[Inject] public ISessionStorageService SessionStorage { get; set; } = default!;
		[Inject] public IFolderService FolderService { get; set; } = default!;

		[Parameter] public Folder CurrentFolder { get; set; } = default!;
		[Parameter] public EventCallback UnavailableFolderAppeared {get; set;}

		private FolderBreadcrumb HomeFolder { get; set; } = new FolderBreadcrumb() { Name = "Home", Id = null };
		public List<BreadcrumbItem> Breadcrumbs { get; set; } = new();
        protected override async Task OnParametersSetAsync()
        {
			await SetBreadcrumbs(CurrentFolder);
			await GetBreadcrumbs();
			StateHasChanged();
        }

        public async Task SetBreadcrumbs(Folder folder)
		{
			if (folder is null || folder.Id == 0)
			{
				await SetDefaultBreadcrumb();
				return;
			}

            var breadcrumbs = await GetBreadcrumbsFromStorage();
			if (!breadcrumbs.Any()) breadcrumbs.Add(HomeFolder);
			var checkIfExists = breadcrumbs.Any(breadcrumb => breadcrumb.Id == folder.Id);
			if(checkIfExists)
			{
				var foundedBreadcrumb = breadcrumbs.First(breadcrumb => breadcrumb.Id == folder.Id);
				breadcrumbs = breadcrumbs.Take(breadcrumbs.IndexOf(foundedBreadcrumb) + 1).ToList();
				await SessionStorage.SetData(CacheKey.Breadcrumb, breadcrumbs);
				return;
			}

			checkIfExists = breadcrumbs.Any(breadcrumb => breadcrumb.Id == folder.ParentFolderId);
			if(checkIfExists)
			{
				breadcrumbs.Add(new FolderBreadcrumb() { Id = folder.Id, Name = folder.Name });
                await SessionStorage.SetData(CacheKey.Breadcrumb, breadcrumbs);
				return;
            }

            await LoadBreadcrumbsFromServer(folder);
        }

		public async Task<List<FolderBreadcrumb>> GetBreadcrumbsFromStorage() => await SessionStorage.Get<List<FolderBreadcrumb>>(CacheKey.Breadcrumb);

		private async Task GetBreadcrumbs()
		{
			var breadcrumbs = await GetBreadcrumbsFromStorage();
            Breadcrumbs = breadcrumbs.Select(breadcrumb => new BreadcrumbItem(breadcrumb.Name, href: (breadcrumb.Id is null ? "/home" : $"/home?ParentFolderId={breadcrumb.Id}"))).ToList();
        }

		private async Task LoadBreadcrumbsFromServer(Folder folder)
		{
			var breadcrumbs = new List<FolderBreadcrumb>() { HomeFolder };
            var receivedBreadcrumbs = await FolderService.GetFolderBreadcrumbs(folder.Id);
			if (receivedBreadcrumbs.Count() == 0)
			{
				await UnavailableFolderAppeared.InvokeAsync();
			}
			breadcrumbs.AddRange(receivedBreadcrumbs);
            await SessionStorage.SetData(CacheKey.Breadcrumb, breadcrumbs);
        }

        private async Task SetDefaultBreadcrumb()
		{
            await SessionStorage.SetData(CacheKey.Breadcrumb, new List<FolderBreadcrumb>() { HomeFolder});
        }
    }
}

