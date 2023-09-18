using System.ComponentModel.DataAnnotations;
namespace Bookmark.Manager.Core.Payloads
{
    public class UserLoginPayload
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;
        [Required]
		[MaxLength(40)]
        [MinLength(5)]
        public string Password { get; set; } = default!;
    }
}