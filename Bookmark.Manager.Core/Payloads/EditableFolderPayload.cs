using System;
using System.ComponentModel.DataAnnotations;

namespace Bookmark.Manager.Core.Payloads
{
	public class EditableFolderPayload
	{
        public int UserId { get; set; }
        public int? ParentFolderId { get; set; } = null;
        [Required]
        public string Name { get; set; } = default!;
    }
}

