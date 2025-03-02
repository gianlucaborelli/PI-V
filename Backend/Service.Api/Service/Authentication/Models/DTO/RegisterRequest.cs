using System.ComponentModel.DataAnnotations;

namespace Service.Api.Service.Authentication.Models.DTO
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Name is required")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Email is required"), EmailAddress(ErrorMessage = "Invalid Email format")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required"), MinLength(6, ErrorMessage = "Password must have a minimum of 8 characters.")]
        public required string Password { get; set; }

        [Compare("Password")]
        public required string ConfirmPassword { get; set; }
    }
}
