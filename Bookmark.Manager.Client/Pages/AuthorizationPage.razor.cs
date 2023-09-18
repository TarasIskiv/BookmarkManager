using Bookmark.Manager.Client.Logic.Abstraction;
using Bookmark.Manager.Core.Payloads;
using Microsoft.AspNetCore.Components;

namespace Bookmark.Manager.Client.Pages
{
    partial class AuthorizationPage
    {
        [Inject] private IUserService _userService {get; set;} = default!;
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
        [Inject] private NavigationManager NavManager { get; set; } = default!;
        public bool IsInLoginState {get; set;} = true;

        public async Task Login(UserLoginPayload userLogin)
        {
            await _userService.Login(userLogin);
            await AuthenticationStateProvider.GetAuthenticationStateAsync();
            NavManager.NavigateTo("/");
        }

        public async Task SignUp(UserSignUpPayload userSignUp)
        {
            await _userService.SignUp(userSignUp);
            await AuthenticationStateProvider.GetAuthenticationStateAsync();
            NavManager.NavigateTo("/");
        }

        public string GetHelperText() => !IsInLoginState ? "Already have an account?" : "New to Bookmark Manager?";
    }
}