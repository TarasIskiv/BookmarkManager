using Bookmark.Manager.Client.Logic.Abstraction;
using Microsoft.AspNetCore.Components.Authorization;

namespace Bookmark.Manager.Client
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IStorageService _storageService;

        public CustomAuthStateProvider(IStorageService storageService)
        {
            _storageService = storageService;
        }
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            throw new NotImplementedException();
        }
    }
}