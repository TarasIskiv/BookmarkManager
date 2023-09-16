using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookmark.Manager.Core.Models
{
    public class UserBookmark
    {
        public int Id { get; set; }
        public int FolderId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = default!;
        public string URL { get; set; } = default!;
        public string? Color { get; set; } = null;
    }
}