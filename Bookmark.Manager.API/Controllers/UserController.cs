
using Bookmark.Manager.Core.Payloads;
using Bookmark.Manager.Logic.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Bookmark.Manager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BookmarkManagerControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IHttpContextAccessor httpContextAccessor, IUserService userService) : base(httpContextAccessor)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginPayload userLogin)
        {
            try
            {
                var token = await _userService.Login(userLogin);
                return Ok(token);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] UserSignUpPayload userSignUpPayload)
        {
            try
            {
                var token = await _userService.SignUp(userSignUpPayload);
                return Ok(token);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
    }
}