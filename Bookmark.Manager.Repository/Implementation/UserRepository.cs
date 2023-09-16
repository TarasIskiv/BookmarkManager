using Bookmark.Manager.Core.Models;
using Bookmark.Manager.Core.Payloads;
using Bookmark.Manager.Repository.Abstraction;

namespace Bookmark.Manager.Repository.Implementation
{
    public class UserRepository : IUserRepository
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