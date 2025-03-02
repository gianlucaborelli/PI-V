using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Authentication;
using Service.Api.Core;
using Service.Api.Service.Authentication.Models;
using Service.Api.Service.Authentication;
using Service.Api.Database;
using Service.Api.Service.Authentication.Extensions;
using Service.Api.Service.Authentication.Models.DTO;

namespace Service.Api.Controllers
{
    [Route("api/users/account")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiController]
    public class AuthenticationController : MainController
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAuthenticationService _authService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtAuthManager _jwtManager;
        private readonly IdentityContext _identityDbContext;

        public AuthenticationController(
            ILogger<AuthenticationController> logger,
            SignInManager<AppUser> signInManager,
            IAuthenticationService authService,
            UserManager<AppUser> userManager,
            IJwtAuthManager jwtManager,
            IdentityContext identityDbContext)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _authService = authService;
            _jwtManager = jwtManager;
            _identityDbContext = identityDbContext;
            _logger.LogInformation("Authentication controller called");
        }

        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> Register(RegisterRequest createUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = await _userManager.FindByEmailAsync(createUser.Email);

            if (user is not null)
            {
                return Conflict("User already exists.");
            }

            var identityUser = new AppUser
            {
                Name = createUser.Name,
                UserName = createUser.Email,
                Email = createUser.Email,
                EmailConfirmed = false
            };

            var identityResult = await _userManager.CreateAsync(identityUser, createUser.Password);

            if (identityResult.Succeeded)
            {
                //var validationToken = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);

                return Created();
            }

            foreach (var error in identityResult.Errors)
                AddError(error.Description);

            return CustomResponse();
        }
        
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> Login(LoginRequest requestDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = await _userManager.FindByEmailAsync(requestDto.Email);

            if (user is not null)
            {
                var result = await _signInManager.PasswordSignInAsync(user!.UserName!, requestDto.Password, false, true);

                if (result.Succeeded)
                {
                    var role = await _userManager.GetRolesAsync(user!) ?? throw new AuthenticationException();
                    var accessToken = _jwtManager.GenerateAccessToken(user, role);
                    var refreshToken = await _jwtManager.GenerateRefreshToken(user.Id);

                    return CustomResponse(new { AccessToken = accessToken, RefreshToken = refreshToken });
                }

                if (result.IsLockedOut)
                {
                    AddError("This user is temporarily blocked");
                    return CustomResponse();
                }

                if (result.IsNotAllowed)
                {
                    AddError("You need to confirm your email!");
                    return CustomResponse();
                }
            }            

            AddError("Incorrect user or password");
            return CustomResponse();
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var principal = _jwtManager.GetPrincipalFromExpiredToken(request.AccessToken);
            var result = await _jwtManager.ValidateRefresToken(request.RefreshToken, principal.GetUserId());


            if (result.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(principal.GetUserEmail());

                var role = await _userManager.GetRolesAsync(user!) ?? throw new AuthenticationException();

                var accessToken = _jwtManager.GenerateAccessToken(user!, role);
                var refreshToken = await _jwtManager.GenerateRefreshToken(user!.Id);

                return CustomResponse(new { AccessToken = accessToken, RefreshToken = refreshToken });
            }

            ICollection<string> errors = [];

            foreach (var error in result.Errors)
            {
                errors.Add(error.ErrorMessage);
            }

            return Unauthorized(errors);
        }
                
        [HttpPost("logout")]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return NoContent();
        }       
    }
}
