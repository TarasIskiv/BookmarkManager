using System;
using System.ComponentModel.DataAnnotations;

namespace Bookmark.Manager.Core.Payloads
{
	public class EditableBookmarkPayload
	{
        public int FolderId { get; set; }
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; } = default!;
        [Required]
        [Url]
        public string URL { get; set; } = default!;
        public string? Color { get; set; } = null;
    }
}

