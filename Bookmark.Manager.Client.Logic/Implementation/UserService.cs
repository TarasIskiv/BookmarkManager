using System.Net.Http;
using System.Net.Http.Json;
using Bookmark.Manager.Client.Logic.Abstraction;
using Bookmark.Manager.Core.Models;
using Bookmark.Manager.Core.Payloads;

namespace Bookmark.Manager.Client.Logic.Implementation
{
    public class UserService : IUserService
    {
        private HttpClient _httpClient;
        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task Login(UserLoginPayload userLogin)
        {
            await _httpClient.PostAsJsonAsync("", userLogin);
        }

        public async Task SignUp(UserSignUpPayload userSignUp)
        {
            await _httpClient.PostAsJsonAsync("", userSignUp);        
        }
    }
}