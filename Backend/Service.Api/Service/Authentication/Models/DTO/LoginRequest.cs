using System.ComponentModel.DataAnnotations;

namespace Service.Api.Service.Authentication.Models.DTO
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email is required"), EmailAddress(ErrorMessage = "Invalid Email format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required"), MinLength(6, ErrorMessage = "Password must have a minimum of 8 characters.")]
        public string Password { get; set; } = string.Empty;
    }
}
