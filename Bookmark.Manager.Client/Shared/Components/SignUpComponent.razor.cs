using System;
using Bookmark.Manager.Client.Logic.Abstraction;
using Bookmark.Manager.Core.Payloads;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace Bookmark.Manager.Client.Shared.Components
{
    partial class SignUpComponent
    {
        [Parameter] public EventCallback<UserSignUpPayload> UserChanged {get; set;}
        [Inject] private IUserService _userService { get; set; } = default!;
        public UserSignUpPayload UserSignUp {get; set;} = new();
        public bool DoesEmailAvailable { get; set; } = true;

        public InputType PasswordType { get; set; } = InputType.Password;
        public string PasswordVisibilityIcon = Icons.Material.Filled.VisibilityOff;

        public InputType RepeatedPasswordType { get; set; } = InputType.Password;
        public string RepeatedPasswordVisibilityIcon = Icons.Material.Filled.VisibilityOff;

        public bool ArePasswordsEqual()
        {
            if(string.IsNullOrEmpty(UserSignUp.Password) || string.IsNullOrEmpty(UserSignUp.Password)) return true;
            return Equals(UserSignUp.Password, UserSignUp.RepeatedPassword);
        }

        private async Task VerifyEmailAvailability()
        {
            DoesEmailAvailable = await _userService.VerifyEmailAvailability(UserSignUp.Email);
            StateHasChanged();
        }

        public string GetErrorTextForNotEqualPasswords() => "Passwords must be equal";

        public string GetErrorTextForTakenEmail() => "This email has been taken";

        public async Task SignUp(EditContext context)
        {
            var isValid = await IsProvidedDataValid(context);
            if (!isValid) return;
            await UserChanged.InvokeAsync(UserSignUp);
        }

        private bool IsSubmitButtonDisabled(EditContext context)
        {
            if (string.IsNullOrWhiteSpace(UserSignUp.Email) || string.IsNullOrWhiteSpace(UserSignUp.Password) || string.IsNullOrWhiteSpace(UserSignUp.RepeatedPassword)) return true;
            if (!DoesEmailAvailable) return true;
            return !context.Validate();
        }

        private async Task<bool> IsProvidedDataValid(EditContext context)
        {
            DoesEmailAvailable = true;
            if (!(context.Validate() && ArePasswordsEqual())) return false;
            await VerifyEmailAvailability();
            if (!DoesEmailAvailable) return false;

            return true;
        }

        public void ChangePasswordVisibility(bool isMainPassword)
        {
            if(isMainPassword)
            {

                PasswordVisibilityIcon = InputType.Password == PasswordType ? Icons.Material.Filled.Visibility : Icons.Material.Filled.VisibilityOff;
                PasswordType = InputType.Password == PasswordType ? InputType.Text : InputType.Password;
            }
            else
            {
                RepeatedPasswordVisibilityIcon = InputType.Password == RepeatedPasswordType ? Icons.Material.Filled.Visibility : Icons.Material.Filled.VisibilityOff;
                RepeatedPasswordType = InputType.Password == RepeatedPasswordType ? InputType.Text : InputType.Password;
            }
            StateHasChanged();
        }

    }
}