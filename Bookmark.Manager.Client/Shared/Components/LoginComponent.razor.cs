using System.Reflection.Metadata;
using Bookmark.Manager.Core.Payloads;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace Bookmark.Manager.Client.Shared.Components
{
    partial class LoginComponent
    {
        [Parameter] public EventCallback<UserLoginPayload> UserChanged {get; set;}
        public UserLoginPayload UserLogin {get; set;} = new();

        public InputType PasswordType { get; set; } = InputType.Password;
        public string PasswordVisibilityIcon = Icons.Material.Filled.VisibilityOff;

        public async Task Login(EditContext context)
        {
            if(!context.Validate()) return;
            await UserChanged.InvokeAsync(UserLogin);
        }

        private bool IsSubmitButtonDisabled(EditContext context)
        {
            if (string.IsNullOrWhiteSpace(UserLogin.Email) || string.IsNullOrWhiteSpace(UserLogin.Password)) return true;

            return !context.Validate();
        }

        public void ChangePasswordVisibility()
        {
            PasswordVisibilityIcon = InputType.Password == PasswordType ? Icons.Material.Filled.Visibility : Icons.Material.Filled.VisibilityOff;
            PasswordType = InputType.Password == PasswordType ? InputType.Text : InputType.Password;
            StateHasChanged();
        }
    }
}