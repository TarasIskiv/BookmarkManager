using Bookmark.Manager.Core.Models;

namespace Bookmark.Manager.Logic.Abstraction
{
    public interface ITokenService
    {
        Task<string> GenerateToken(User user);
    }
}