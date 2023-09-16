using Bookmark.Manager.Core.Models;
using Bookmark.Manager.Core.Payloads;

namespace Bookmark.Manager.Repository.Abstraction
{
    public interface IUserRepository
    {
        Task Login(UserLoginPayload userLogin);
        Task SignUp(User user);
    }
}