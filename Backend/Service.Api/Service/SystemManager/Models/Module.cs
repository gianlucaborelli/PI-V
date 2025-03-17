using Service.Api.Core.Entity;

namespace Service.Api.Service.SystemManager.Models
{
    public class Module : EntityBase
    {
        public required string Tag { get; set; }
        public string? EspId { get; set; }

        // Chave estrangeira para Company
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }

        public List<Sensor> Sensors { get; set; } = [];
    }
}
