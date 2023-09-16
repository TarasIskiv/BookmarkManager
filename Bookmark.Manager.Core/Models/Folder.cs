using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookmark.Manager.Core.Models
{
    public class Folder
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? ParentFolderId { get; set; } = null;
        public string Name { get; set; } = default!;
    }
}