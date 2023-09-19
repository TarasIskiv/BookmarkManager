using Bookmark.Manager.Core.Models;
using Bookmark.Manager.Core.Payloads;
using Bookmark.Manager.Logic.Abstraction;
using Bookmark.Manager.Logic.Mapping;
using Bookmark.Manager.Repository.Abstraction;

namespace Bookmark.Manager.Logic.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly ITokenService _tokenService;

        public UserService(IUserRepository userRepository, IEncryptionService encryptionService, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _encryptionService = encryptionService;
            _tokenService = tokenService;
        }
        public async Task<string> Login(UserLoginPayload userLogin)
        {
            userLogin.Password = _encryptionService.DecryptPassword(userLogin.Password);
            var user = await _userRepository.Login(userLogin);
            if(user is default(User)) return string.Empty;
            var token = await _tokenService.GenerateToken(user);
            return token;
        }
        public async Task<string> SignUp(UserSignUpPayload userSignUp)
        {
            var newUser = userSignUp.ToUser();
            newUser.Password = _encryptionService.DecryptPassword(newUser.Password);
            var user = await _userRepository.SignUp(newUser);
            return await _tokenService.GenerateToken(user);
        }

        public async Task<bool> VerifyEmailAvailability(string email)
        {
            return await _userRepository.VerifyEmailAvailability(email);
        }
    }
}