using System;
namespace Bookmark.Manager.Core.Payloads
{
	public class EditableBookmarkPayload
	{
        public int FolderId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = default!;
        public string URL { get; set; } = default!;
        public string? Color { get; set; } = null;
    }
}

