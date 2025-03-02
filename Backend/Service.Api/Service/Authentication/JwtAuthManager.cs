using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Service.Api.Service.Authentication.Models;
using FluentValidation.Results;
using Service.Api.Database;
using Microsoft.EntityFrameworkCore;



namespace Service.Api.Service.Authentication
{
    public class JwtAuthManager(
    IOptions<JwtSettings> jwtSettings,
        UserManager<AppUser> userManager,
        IdentityContext context) : IJwtAuthManager
    {
        private readonly ValidationResult ValidationResult = new();
        private readonly UserManager<AppUser> _userManager = userManager;
        private JwtSettings _jwtSettings = jwtSettings.Value;
        private readonly IdentityContext _context = context;

        public string GenerateAccessToken(AppUser user, IList<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtSettings.Audience,
                Issuer = _jwtSettings.Issuer,
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email!),
                ]),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpireInMinutes),
                NotBefore = DateTime.UtcNow,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature)
            };
            tokenDescriptor.Subject.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<string> GenerateRefreshToken(Guid userId)
        {
            var refreshToken = new RefreshToken
            {
                UserId = userId,
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                ExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.RefreshTokenExpireInMinutes),
                CreatedAt = DateTime.UtcNow
            };

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.RefreshTokens
                        .Where(u => u.UserId.Equals(userId))
                        .ExecuteDeleteAsync();

                    _context.RefreshTokens.Add(refreshToken);

                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }

            return refreshToken.Token;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false
            };

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        public async Task<ValidationResult> ValidateRefresToken(string token, string userId)
        {
            var result = await _context.RefreshTokens
                                    .FirstOrDefaultAsync(u => u.UserId.Equals(userId));

            if (result is null)
            {
                ValidationResult.Errors.Add(
                            new FluentValidation.Results.ValidationFailure(
                                string.Empty, "Invalid Refresh Token."));
                return ValidationResult;
            }

            if (result.ExpiresAt < DateTime.UtcNow)
            {
                ValidationResult.Errors.Add(
                            new FluentValidation.Results.ValidationFailure(
                                string.Empty, "Refresh Token expired."));
            }

            return ValidationResult;
        }
    }
}
