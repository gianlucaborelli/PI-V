using Service.Api.Core.Entity;

namespace Service.Api.Service.SystemManager.Models
{
    public class Module : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? EspId { get; set; }
        
        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = null!;

        public List<Location> Locations { get; set; } = [];
        public ModuleAccessToken AccessToken { get; set; }

        protected Module()
        {         
            AccessToken = new ModuleAccessToken(Guid.Empty);
        }

        public Module(string name, Guid companyId, string? description = null, string? espId = null)
        {
            Name = name;
            CompanyId = companyId;
            Description = description;
            EspId = espId;
            AccessToken = new ModuleAccessToken(this.Id);
        }
    }
}
