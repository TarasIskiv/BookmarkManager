using Bookmark.Manager.Core.Models;
using Bookmark.Manager.Core.Payloads;

namespace Bookmark.Manager.Repository.Abstraction
{
    public interface IUserRepository
    {
        Task<User> Login(UserLoginPayload userLogin);
        Task<User> SignUp(User user);
        Task<bool> VerifyEmailAvailability(string email);
    }
}