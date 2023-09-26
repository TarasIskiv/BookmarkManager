using System;
using Bookmark.Manager.Core.Payloads;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace Bookmark.Manager.Client.Shared.Dialogs
{
	partial class EditableBookmarkDialog
	{
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter] public EditableBookmarkPayload EditableBookmark { get; set; } = new();
        private EditContext _bookmarkContext;
        protected override void OnInitialized()
        {
            _bookmarkContext = new EditContext(EditableBookmark);
        }

        public bool IsSaveButtonDisabled() => !_bookmarkContext.Validate();

        public void Save()
        {
            if (!_bookmarkContext.Validate()) return;
            MudDialog.Close(DialogResult.Ok(EditableBookmark));
        }
        void Cancel() => MudDialog.Cancel();
    }
}

