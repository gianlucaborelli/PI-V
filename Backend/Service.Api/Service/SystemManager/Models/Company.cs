using Service.Api.Core.Entity;
using Service.Api.Service.Authentication.Models;

namespace Service.Api.Service.SystemManager.Models
{
    public class Company: EntityBase
    {
        public List<UserCompany> UserCompanies { get; set; } = [];

        public required string Name { get; set; }
        
        public List<Module> Modules { get; set; } = [];
    }
}
