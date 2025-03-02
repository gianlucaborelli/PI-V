namespace Service.Api.Service.Authentication.Models
{
    public class RefreshToken
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public required Guid UserId { get; set; }
        public required string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
