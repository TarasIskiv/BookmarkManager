
using Bookmark.Manager.Core.Payloads;
using Bookmark.Manager.Logic.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookmark.Manager.API.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
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

        [HttpGet("DoesEmailAvailable")]
        public async Task<IActionResult> VerifyEmailAvaibility([FromQuery] string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email)) return BadRequest();
                return Ok(await _userService.VerifyEmailAvailability(email));
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
    }
}