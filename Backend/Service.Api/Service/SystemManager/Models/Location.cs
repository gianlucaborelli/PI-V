using Service.Api.Core.Entity;
using Service.Api.Service.SystemManager.Models.Risks;

namespace Service.Api.Service.SystemManager.Models
{
    public class Location : EntityBase
    {
        public required string Name { get; set; }
        public string? Description { get; set; }

        public Guid ModuleId { get; set; }
        public Module Module { get; set; } = null!;

        
        public List<Risk> RiskLimits { get; set; } = [];
        public List<SensorData> SensorDatas { get; set; } = [];
    }
}
