using Microsoft.AspNetCore.Components;

namespace Bookmark.Manager.Client.Shared.Components
{
    partial class HeaderComponent
    {
        [Parameter] public EventCallback ThemeChanged {get; set;}

        public async Task ChangeTheme()
        {
            await ThemeChanged.InvokeAsync();
        }
        
        public Task LogOut()
        {
            throw new Exception();
        }
    }
}