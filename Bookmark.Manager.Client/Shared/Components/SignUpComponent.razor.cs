using Bookmark.Manager.Core.Payloads;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Bookmark.Manager.Client.Shared.Components
{
    partial class SignUpComponent
    {
        [Parameter] public EventCallback<UserSignUpPayload> UserChanged {get; set;}
        public UserSignUpPayload UserSignUp {get; set;} = new();

        public bool ArePasswordsEqual()
        {
            if(string.IsNullOrEmpty(UserSignUp.Password) || string.IsNullOrEmpty(UserSignUp.Password)) return true;
            return Equals(UserSignUp.Password, UserSignUp.RepeatedPassword);
        }

        public string GetErrorTextForNotEqualPasswords() => "Passwords must be equal";

        public async Task SignUp(EditContext context)
        {
            if(!(context.Validate() && ArePasswordsEqual())) return;
            await UserChanged.InvokeAsync(UserSignUp);
        }
    }
}