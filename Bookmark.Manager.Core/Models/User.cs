using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookmark.Manager.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        [EmailAddressAttribute]
        public string Email { get; set; } = default!;
        [Required]
		[MaxLength(40)]
        [MinLength(5)]
        public string Password { get; set; } = default!;
    }
}