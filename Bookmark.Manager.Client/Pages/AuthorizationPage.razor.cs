using Bookmark.Manager.Client.Logic.Abstraction;
using Bookmark.Manager.Core.Payloads;
using Microsoft.AspNetCore.Components;

namespace Bookmark.Manager.Client.Pages
{
    partial class AuthorizationPage
    {
        [Inject] private IUserService _userService {get; set;} = default!;
        public bool IsInLoginState {get; set;} = true;

        public  Task Login(UserLoginPayload userLogin)
        {
            throw new Exception();
        }

        public  Task SignUp(UserSignUpPayload userSignUp)
        {
            throw new Exception();
        }

        public string GetHelperText() => IsInLoginState ? "Already have an account?" : "New to Bookmark Manager?";
    }
}