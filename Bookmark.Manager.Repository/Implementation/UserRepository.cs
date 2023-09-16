using Bookmark.Manager.Core.Models;
using Bookmark.Manager.Core.Payloads;
using Bookmark.Manager.Database;
using Bookmark.Manager.Repository.Abstraction;

namespace Bookmark.Manager.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly BookmarkManagerContext _context;

        public UserRepository(BookmarkManagerContext context)
        {
            _context = context;
        }
        public async Task<User> Login(UserLoginPayload userLogin)
        {
            var user = await Task.Run(() => _context.Users
                .SingleOrDefault(user => user.Email.Equals(userLogin.Email) && user.Password.Equals(userLogin.Password)));
            return user ?? default!;
        }

        public async Task<User> SignUp(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return _context.Users.SingleOrDefault(user => user.Email.Equals(user.Email) && user.Password.Equals(user.Password))!;
        }
    }
}