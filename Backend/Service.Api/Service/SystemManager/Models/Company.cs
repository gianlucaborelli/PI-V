using Service.Api.Core.Entity;

namespace Service.Api.Service.SystemManager.Models
{
    public class Company: EntityBase
    {
        public required string Name { get; set; }
        
        public List<string> Tags { get; set; } = [];

        public List<Module> Modules { get; set; } = [];
    }
}
