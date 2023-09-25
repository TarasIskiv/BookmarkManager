using Bookmark.Manager.Client.Logic.Abstraction;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Bookmark.Manager.Client.Shared
{
    partial class MainLayout
    {
        [Inject] private NavigationManager NavManager { get; set; } = default!;
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
        [Inject] private IUserService UserService { get; set; } = default!;
        public MudThemeProvider MudThemeProvider { get; set; } = default!;
        public MudTheme MudTheme { get; set; } = default!;
        private bool _isDarkMode;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                ConfigureCustomPalette();
                _isDarkMode = await MudThemeProvider.GetSystemPreference();
                StateHasChanged();
            }
        }
        public void ChangeTheme()
        {
            _isDarkMode = !_isDarkMode;
        }
        
        public async Task LogOut()
        {
            await UserService.Logout();
            await AuthenticationStateProvider.GetAuthenticationStateAsync();
            NavManager.NavigateTo("/authorization");
        }
        public void ConfigureCustomPalette()
        {
            MudTheme = new MudTheme()
            {
                Palette = new PaletteLight()
                {
                    Primary = Colors.Teal.Darken4,
                    Secondary = Colors.Teal.Darken1,
                    Info = Colors.Teal.Lighten5
                },
                PaletteDark = new PaletteDark()
                {
                    Primary = Colors.Teal.Lighten3,
                    Secondary = Colors.Teal.Lighten1,
                    Info = Colors.Shades.Black
                }
            };
        }

    }
}