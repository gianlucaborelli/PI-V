using Service.Api.Core.Entity;

namespace Service.Api.Service.SystemManager.Models
{
    public class Sensor : EntityBase
    {
        public required string Name { get; set; }
        public string? Description { get; set; }

        public Guid ModuleId { get; set; }
        public Module Module { get; set; } = null!;

        public List<SensorData> SensorDatas { get; set; } = [];

        public SensorType SensorType { get; set; }
    }
}
