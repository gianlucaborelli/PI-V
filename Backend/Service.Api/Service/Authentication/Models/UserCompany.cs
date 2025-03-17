using Service.Api.Service.SystemManager.Models;

namespace Service.Api.Service.Authentication.Models
{
    public class UserCompany
    {
        public Guid UserId { get; set; }        

        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
