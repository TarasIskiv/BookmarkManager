using System;
using Bookmark.Manager.Client.Logic.Abstraction;
using Bookmark.Manager.Client.Shared.Dialogs;
using Bookmark.Manager.Core.Models;
using Bookmark.Manager.Core.Payloads;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace Bookmark.Manager.Client.Shared.Components
{
	partial class BookmarkComponent
	{
		[Parameter] public List<UserBookmark> Bookmarks { get; set; } = new();
		[Parameter] public EventCallback BookmarksChanged { get; set; }
        [Parameter] public int CurrentFolderId { get; set; }
		[Inject] public IBookmarkService BookmarkService { get; set; } = default!;
        [Inject] public IDialogService DialogService { get; set; } = default!;
        [Inject] public IJSRuntime JSRuntime { get; set; } = default!;
        private DialogOptions _dialogOptions = new DialogOptions()
        {
            DisableBackdropClick = true,
            Position = DialogPosition.Center
        };

        protected override async Task OnParametersSetAsync()
        {
            await InvokeAsync(StateHasChanged);
        }

        public async Task UpdateBookmark(UserBookmark bookmark)
		{
            var parameters = new DialogParameters<EditableBookmarkDialog>();
            parameters.Add(param => param.EditableBookmark, new EditableBookmarkPayload() { Name = bookmark.Name, UserId = bookmark.UserId, FolderId = bookmark.FolderId, Color = bookmark.Color, URL = bookmark.URL });
            var dialog = DialogService.Show<EditableBookmarkDialog>("Update Bookmark", parameters, _dialogOptions);
            var result = await dialog.Result;

            if (result.Canceled) return;

            var bookmarkResult = result.Data as EditableBookmarkPayload;

            await BookmarkService.UpdateBookmark(bookmark.Id, bookmarkResult!);
            await UpdateBookmarks();
        }

		public async Task AddBookmark()
		{
            var parameters = new DialogParameters<EditableBookmarkDialog>();
            parameters.Add(param => param.EditableBookmark, new EditableBookmarkPayload() { FolderId = CurrentFolderId });
            var dialog = DialogService.Show<EditableBookmarkDialog>("Create Bookmark", parameters, _dialogOptions);
            var result = await dialog.Result;

            if (result.Canceled) return;

            var bookmarkResult = result.Data as EditableBookmarkPayload;

            await BookmarkService.AddBookmark(bookmarkResult!);
            await UpdateBookmarks();
        }

		public async Task UpdateBookmarks()
		{
			await BookmarksChanged.InvokeAsync();
		}

        public async Task DeleteBookmark(int Id)
        {
            await BookmarkService.RemoveBookmark(Id);
            await UpdateBookmarks();
        }

		public async Task Navigate(string url)
		{
            await JSRuntime.InvokeAsync<object>("open", url, "_blank");
        }

        public string GetBookmarkStyle(UserBookmark bookmark) => string.Concat(GetPadding(), GetBookmarkBackgroundColor(bookmark));

		private string GetBookmarkBackgroundColor(UserBookmark bookmark) => $"background-color:{bookmark.Color ?? Colors.Grey.Default} !important;";
        private string GetPadding() => "padding: 0 1em;";
	}
}

