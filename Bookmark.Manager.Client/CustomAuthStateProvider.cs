using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Bookmark.Manager.Client.Logic.Abstraction;
using Bookmark.Manager.Core.Helpers;
using Microsoft.AspNetCore.Components.Authorization;

namespace Bookmark.Manager.Client
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IStorageService _storageService;
        private readonly HttpClient _client;

        public CustomAuthStateProvider(IStorageService storageService, HttpClient client)
        {
            _storageService = storageService;
            _client = client;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _storageService.GetDataFromStorage<string>(CacheKey.AuthToken.ToString());
            var identity = new ClaimsIdentity();
            _client.DefaultRequestHeaders.Authorization = null;

            if(!string.IsNullOrEmpty(token))
            {
                try
                {
                    identity = new ClaimsIdentity(ParseClaimsFromJWT(token), "jwt");
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
                }
                catch
                {
                    await _storageService.RemoveDataFromStorage(CacheKey.AuthToken.ToString());
                    identity = new ClaimsIdentity();
                }
            }

            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }

        private byte[] ParseBase64(string base64)
        {
            switch(base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        private IEnumerable<Claim> ParseClaimsFromJWT(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            var claims = keyValuePairs?.Select(x => new Claim(x.Key, x.Value.ToString()));

            return claims;
        }
    }
}