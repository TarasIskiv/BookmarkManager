using Bookmark.Manager.Core.Models;
using Bookmark.Manager.Core.Payloads;

namespace Bookmark.Manager.Logic.Abstraction
{
    public interface IUserService
    {
        Task<string> Login(UserLoginPayload userLogin);
        Task<string> SignUp(UserSignUpPayload userSignUp);
    }
}