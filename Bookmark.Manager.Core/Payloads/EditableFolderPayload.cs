using System;
namespace Bookmark.Manager.Core.Payloads
{
	public class EditableFolderPayload
	{
        public int UserId { get; set; }
        public int? ParentFolderId { get; set; } = null;
        public string Name { get; set; } = default!;
    }
}

