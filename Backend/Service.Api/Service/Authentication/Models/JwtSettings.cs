namespace Service.Api.Service.Authentication.Models
{
    public class JwtSettings
    {
        public required string Secret { get; init; }
        public int AccessTokenExpireInMinutes { get; init; }
        public int RefreshTokenExpireInMinutes { get; init; }
        public required string Issuer { get; init; }
        public required string Audience { get; init; }
    }
}
