using System.Net.Http;
using System.Net.Http.Json;
using Bookmark.Manager.Client.Logic.Abstraction;
using Bookmark.Manager.Core.Helpers;
using Bookmark.Manager.Core.Models;
using Bookmark.Manager.Core.Payloads;

namespace Bookmark.Manager.Client.Logic.Implementation
{
    public class UserService : IUserService
    {
        private HttpClient _httpClient;
        private readonly IStorageService _storageService;

        public UserService(HttpClient httpClient, IStorageService storageService)
        {
            _httpClient = httpClient;
            _storageService = storageService;
        }
        public async Task Login(UserLoginPayload userLogin)
        {
            var response = await _httpClient.PostAsJsonAsync("api/User/Login", userLogin);
            if(response.IsSuccessStatusCode) await WriteToken(response.Content);
           
        }

        public async Task SignUp(UserSignUpPayload userSignUp)
        {
            var response = await _httpClient.PostAsJsonAsync("api/User/SignUp", userSignUp);  
            if(response.IsSuccessStatusCode) await WriteToken(response.Content);
     
        }

        private async Task WriteToken(HttpContent content)
        {
            string parsedResponse = await content.ReadAsStringAsync();
            await _storageService.WriteDataToStorage<string>(CacheKey.AuthToken.ToString(), parsedResponse);
        }

        public async Task Logout()
        {
            await _storageService.RemoveDataFromStorage(CacheKey.AuthToken.ToString());
        }

        public async Task<bool> VerifyEmailAvailability(string email)
        {
            return await _httpClient.GetFromJsonAsync<bool>($"api/User/DoesEmailAvailable?email={email}");
        }
    }
}