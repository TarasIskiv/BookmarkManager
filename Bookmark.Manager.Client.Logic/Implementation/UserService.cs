using Bookmark.Manager.Client.Logic.Abstraction;
using Bookmark.Manager.Core.Models;
using Bookmark.Manager.Core.Payloads;

namespace Bookmark.Manager.Client.Logic.Implementation
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