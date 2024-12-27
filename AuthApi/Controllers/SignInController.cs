using AuthApi.Entities.Identity;
using AuthApi.Models.Authentication;
using AuthApi.Repositories.Identity;
using AuthApi.Services.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AuthApi.Controllers
{

    [ApiController]
    [Route("sign-in")]
    public class SignInController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepository;

        public SignInController(
            SignInManager<User> signInManager,
            IJwtService jwtService,
            IUserRepository userRepository)
        {
            _signInManager = signInManager;
            _jwtService = jwtService;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Sign in and receive all necessary authentication tokens.
        /// </summary>
        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        [SwaggerOperation(
            "Is valid",
            "<hr/><p><strong>&#x1F510; Authentication: None</p><p><hr/></hr>" +
            "Sign in and receive all necessary authentication tokens."
        )]
        public async Task<ActionResult<AuthenticationTokens>> SignIn([FromBody] SignInUser signInUser)
        {
            if (!ModelState.IsValid)
                return Unauthorized();

            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(signInUser.UserName, signInUser.Password, false, false);

            if (!result.Succeeded)
                return Unauthorized();

            User? user = await _userRepository.GetOneOrDefaultByName(signInUser.UserName);

            if (user == null)
                return Unauthorized();

            if (user.LockoutEnabled)
                return Unauthorized();

            user.LastLogin = DateTimeOffset.UtcNow;
            await _userRepository.Update(user);

            string ip = HttpContext.Connection.RemoteIpAddress.ToString();

            var roles = await _userRepository.GetUserRoles(user);

            var tokens = _jwtService.GenerateTokenPair(user, roles, ip);

            if (tokens == null) return Unauthorized();

            return Ok(tokens);

        }

        /// <summary>
        /// Allow the user to refresh his authentication tokens
        /// </summary>
        [HttpPost]
        [Route("refresh")]
        [AllowAnonymous]
        [SwaggerOperation(
            "Is valid",
            "<hr/><p><strong>&#x1F510; Authentication: None</p><p><hr/></hr>" +
            "Allow the user to refresh his authentication tokens."
        )]
        public async Task<ActionResult<AuthenticationTokens>> Refresh([FromBody] AuthenticationTokens tokens)
        {
            if (!ModelState.IsValid)
                return Unauthorized();

            string ip = HttpContext.Connection.RemoteIpAddress.ToString();

            if (!_jwtService.ValidateTokenPairForRefresh(tokens, ip)) return Unauthorized();

            var userId = _jwtService.GetUserIdFromToken(tokens.AccessToken);

            var user = await _userRepository.GetOneOrDefault(userId);

            if (user == null)
                return Unauthorized();

            if (user.LockoutEnabled)
                return Unauthorized();

            user.LastLogin = DateTimeOffset.UtcNow;
            await _userRepository.Update(user);

            var roles = await _userRepository.GetUserRoles(user);

            return Ok(_jwtService.GenerateTokenPair(user, roles, ip));
        }
    }
}
