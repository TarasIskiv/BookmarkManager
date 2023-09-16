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
        public async Task Login(UserLoginPayload userLogin)
        {
            userLogin.Password = _encryptionService.DecryptPassword(userLogin.Password);
            var user = await _userRepository.Login(userLogin);
            var token = await _tokenService.GenerateToken(user);
            //return user;
        }
        public async Task SignUp(UserSignUpPayload userSignUp)
        {
            var newUser = userSignUp.ToUser();
            newUser.Password = _encryptionService.DecryptPassword(newUser.Password);
            var user = await _userRepository.SignUp(newUser);
            var token = await _tokenService.GenerateToken(user);
            //return user;
        }
    }
}