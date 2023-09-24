using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Bookmark.Manager.API.Controllers
{
    public class BookmarkManagerControllerBase : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BookmarkManagerControllerBase(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public int UserId => int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? "0");

    }
}