﻿using System;
using Bookmark.Manager.Client.Logic.Abstraction;
using Bookmark.Manager.Core.Models;
using Microsoft.AspNetCore.Components;

namespace Bookmark.Manager.Client.Pages
{
	partial class BookmarkPage
	{
		[Parameter, SupplyParameterFromQuery(Name = "ParentFolderId")] public int? ParentFolderId { get; set; } = null;
        [Inject] public IFolderService FolderService { get; set; } = default!;
        [Inject] public IBookmarkService BookmarkService { get; set; } = default!;
        [Inject] public NavigationManager NavManager { get; set; } = default!;
        private List<Folder> Folders { get; set; } = new();
        private Folder CurrentFolder { get; set; } = new();
        private List<UserBookmark> Bookmarks { get; set; } = new();
        public bool IsRemovingDisabled { get; set; } = true;
        protected override async Task OnParametersSetAsync()
        {
            await LoadPageData();
        }

        public async Task LoadPageData()
        {
            Folders = await FolderService.GetNestedFolders(ParentFolderId);
            if(ParentFolderId is not null) Bookmarks = await BookmarkService.GetFolderBookmarks(ParentFolderId.Value);
            IsRemovingDisabled = Folders.Any() || Bookmarks.Any();
            CurrentFolder = ParentFolderId is not null ? await FolderService.GetFolder(ParentFolderId.Value) : new();
            StateHasChanged();
        }

        public void MoveToSelectedFolder(int id)
        {
            NavManager.NavigateTo($"/home?ParentFolderId={id}");
        }

        public void NavigateToHome()
        {
            NavManager.NavigateTo($"/home");
        }

        public void NavigateToParent()
        {
            if (CurrentFolder.ParentFolderId is null)
            {
                NavManager.NavigateTo($"/home");
                return;
            }

            MoveToSelectedFolder(CurrentFolder.ParentFolderId.Value);

        }
    }
}

