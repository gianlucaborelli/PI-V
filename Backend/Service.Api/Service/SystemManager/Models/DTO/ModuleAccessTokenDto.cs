namespace Service.Api.Service.SystemManager.Models.DTO
{
    public class ModuleAccessTokenDto
    {
        public string Token { get; } = string.Empty;
        public bool IsActive { get; } 
        public DateTime ExpiresAt { get; }

        public ModuleAccessTokenDto( string token, bool isActive,  DateTime expiresAt )
        {
            Token = token;
            IsActive = isActive;
            ExpiresAt = expiresAt;
        }
    }
}
