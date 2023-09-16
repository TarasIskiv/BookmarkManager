using Bookmark.Manager.Core.Models;
using Bookmark.Manager.Core.Payloads;
using Bookmark.Manager.Logic.Abstraction;

namespace Bookmark.Manager.Logic.Implementation
{
    public class UserService : IUserService
    {
        public Task Login(UserLoginPayload userLogin)
        {
            throw new NotImplementedException();
        }

        public Task SignUp(User user)
        {
            throw new NotImplementedException();
        }
    }
}