using Bookmark.Manager.Core.Models;
using Bookmark.Manager.Core.Payloads;

namespace Bookmark.Manager.Logic.Abstraction
{
    public interface IUserService
    {
        Task Login(UserLoginPayload userLogin);
        Task SignUp(User user);
    }
}