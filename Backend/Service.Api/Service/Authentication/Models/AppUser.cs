using Microsoft.AspNetCore.Identity;

namespace Service.Api.Service.Authentication.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public required string Name { get; set; }

        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}
