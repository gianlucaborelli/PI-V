using Service.Api.Service.Authentication.Extensions;
using System.Security.Claims;

namespace Service.Api.Service.Authentication
{
    public class LoggedInUser : ILoggedInUser
    {
        private readonly IHttpContextAccessor _accessor;

        public LoggedInUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => _accessor.HttpContext!.User.Identity!.Name!;

        public Guid GetUserId()
        {
            return IsAutenticated() ? Guid.Parse(_accessor.HttpContext!.User.GetUserId()) : Guid.Empty;
        }

        public string GetUserEmail()
        {
            return IsAutenticated() ? _accessor.HttpContext!.User.GetUserEmail() : "";
        }

        public bool IsAutenticated()
        {
            return _accessor.HttpContext!.User.Identity!.IsAuthenticated;
        }

        public bool IsInRole(string role)
        {
            return _accessor.HttpContext!.User.IsInRole(role);
        }

        public IEnumerable<Claim> GetUserClaims()
        {
            return _accessor.HttpContext!.User.Claims;
        }

        public HttpContext GetHttpContext()
        {
            return _accessor.HttpContext!;
        }
    }
}