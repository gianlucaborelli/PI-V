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
    /// <summary>
    /// Controller responsável pelas operações de autenticação de usuários.
    /// </summary>
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

        /// <summary>
        /// Construtor do AuthenticationController.
        /// </summary>
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

        /// <summary>
        /// Realiza o registro de um novo usuário.
        /// </summary>
        /// <param name="createUser">Dados do usuário para registro.</param>
        /// <returns>Resultado da operação de registro.</returns>
        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
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

        /// <summary>
        /// Realiza o login do usuário.
        /// </summary>
        /// <param name="requestDto">Dados de login do usuário.</param>
        /// <returns>Token de acesso e refresh token.</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
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

        /// <summary>
        /// Realiza a atualização do token de acesso utilizando o refresh token.
        /// </summary>
        /// <param name="request">Dados do token de acesso e refresh token.</param>
        /// <returns>Novos tokens de acesso e refresh.</returns>
        [HttpPost("refresh-token")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ICollection<string>), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
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

        /// <summary>
        /// Realiza o logout do usuário autenticado.
        /// </summary>
        /// <returns>Resultado da operação de logout.</returns>
        [HttpPost("logout")]
        [Authorize]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return NoContent();
        }
    }
}
