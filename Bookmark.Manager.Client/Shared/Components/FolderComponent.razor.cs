using System;
using Bookmark.Manager.Client.Logic.Abstraction;
using Bookmark.Manager.Client.Shared.Dialogs;
using Bookmark.Manager.Core.Models;
using Bookmark.Manager.Core.Payloads;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Bookmark.Manager.Client.Shared.Components
{
	partial class FolderComponent
	{
		[Parameter] public List<Folder> Folders { get; set; } = new();
		[Parameter] public EventCallback FoldersRefreshed { get; set; } = default!;
        [Parameter] public EventCallback<int> FolderNavigated { get; set; } = default!;
        [Inject] public IDialogService DialogService { get; set; } = default!;
		[Inject] public IFolderService FolderService { get; set; } = default!;

		private DialogOptions _dialogOptions = new DialogOptions()
        {
            DisableBackdropClick = true,
            Position = DialogPosition.Center
        };

        protected override async Task OnParametersSetAsync()
        {
			await InvokeAsync(StateHasChanged);
        }

		public async Task EditFolder(Folder folder)
		{
            var parameters = new DialogParameters<EditableFolderDialog>();
            parameters.Add(param => param.EditableFolder, new EditableFolderPayload() { Name = folder.Name, UserId = folder.UserId, ParentFolderId = folder.ParentFolderId});
            var dialog = DialogService.Show<EditableFolderDialog>("Update Folder", parameters, _dialogOptions);
            var result = await dialog.Result;

            if (result.Canceled) return;

            var folderResult = result.Data as EditableFolderPayload;

            await FolderService.UpdateFolder(folder.Id, folderResult!);
            await FoldersRefreshed.InvokeAsync();
		}

		public async void AddNewFolder()
		{
			var parameters = new DialogParameters<EditableFolderDialog>();
            parameters.Add(param => param.EditableFolder, new EditableFolderPayload());
			var dialog = DialogService.Show<EditableFolderDialog>("Create Folder", parameters, _dialogOptions);
			var result = await dialog.Result;

			if (result.Canceled) return;

			var folder = result.Data as EditableFolderPayload;

			await FolderService.CreateFolder(folder!);
            await FoldersRefreshed.InvokeAsync();
        }

		public async Task Navigate(int folderId)
		{
			await FolderNavigated.InvokeAsync(folderId);
        }
	}
}

