using Bookmark.Manager.Core.Models;
using Bookmark.Manager.Core.Payloads;

namespace Bookmark.Manager.Logic.Mapping
{
    public static class CustomMapper
    {
        public static User ToUser(this UserSignUpPayload userSignUp)
        {
            return new User()
            {
                Email = userSignUp.Email,
                Password = userSignUp.Password
            };
        }
    }
}