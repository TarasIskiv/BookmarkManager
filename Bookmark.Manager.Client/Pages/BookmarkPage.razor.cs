using System;
using Bookmark.Manager.Client.Logic.Abstraction;
using Bookmark.Manager.Core.Models;
using Microsoft.AspNetCore.Components;

namespace Bookmark.Manager.Client.Pages
{
	partial class BookmarkPage
	{
		[Parameter] public int? ParentFolderId { get; set; } = null;
        [Inject] public IFolderService FolderService { get; set; } = default!;
        [Inject] public IBookmarkService BookmarkService { get; set; } = default!;
        [Inject] public NavigationManager NavManager { get; set; } = default!;
        private List<Folder> Folders { get; set; } = new();
        private List<UserBookmark> Bookmarks { get; set; } = new();
        public bool IsRemovingDisabled { get; set; } = true;
        protected override async Task OnParametersSetAsync()
        {
            await LoadPageData();
        }

        public async Task LoadPageData()
        {
            Folders = await FolderService.GetNestedFolders(ParentFolderId);
            IsRemovingDisabled = Folders.Any();
            StateHasChanged();
        }

        public void MoveToSelectedFolder(int id)
        {
            NavManager.NavigateTo($"/home/{id}");
        }
    }
}

