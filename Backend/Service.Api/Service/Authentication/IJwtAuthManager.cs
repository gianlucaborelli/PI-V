using FluentValidation.Results;
using Service.Api.Service.Authentication.Models;
using System.Security.Claims;

namespace Service.Api.Service.Authentication
{
    public interface IJwtAuthManager
    {
        string GenerateAccessToken(AppUser user, IList<string> roles);
        Task<string> GenerateRefreshToken(Guid userId);
        Task<ValidationResult> ValidateRefresToken(string token, string userId);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
