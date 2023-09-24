using System;
using Bookmark.Manager.Core.Payloads;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace Bookmark.Manager.Client.Shared.Dialogs
{
	partial class EditableFolderDialog
	{
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter] public EditableFolderPayload EditableFolder { get; set; } = new();
        private EditContext _folderContext; 
        protected override void OnInitialized()
        {
            _folderContext = new EditContext(EditableFolder);
        }

        public void Save()
        {
            if (!_folderContext.Validate()) return;
            MudDialog.Close(DialogResult.Ok(EditableFolder));
        }
        void Cancel() => MudDialog.Cancel();
    }
}

