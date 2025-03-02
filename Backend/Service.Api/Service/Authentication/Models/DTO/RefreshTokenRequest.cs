using System.ComponentModel.DataAnnotations;

namespace Service.Api.Service.Authentication.Models.DTO
{
    public class RefreshTokenRequest
    {
        [Required]
        public string AccessToken { get; set; } = string.Empty;

        [Required]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
