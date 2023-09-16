using MudBlazor;

namespace Bookmark.Manager.Client.Shared
{
    partial class MainLayout
    {
        public MudThemeProvider MudThemeProvider { get; set; }
        MudTheme MudTheme { get; set; }
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
        public void ConfigureCustomPalette()
        {
            MudTheme = new MudTheme()
            {
                Palette = new PaletteLight()
                {
                    Primary = Colors.Teal.Darken4,
                    Secondary = Colors.Teal.Darken1
                },
                PaletteDark = new PaletteDark()
                {
                    Primary = Colors.Teal.Lighten3,
                    Secondary = Colors.Teal.Lighten1
                }
            };
        }

    }
}