namespace Service.Api.Service.SystemManager.Models.DTO
{
    public class LocationDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }        
        public string? Description { get; set; }
        public Guid ModuleId { get; set; }

        public List<RiskDto> RiskLimits { get; set; } = [];
        public List<SensorDataDto> SensorDatas { get; set; } = [];
    }
}
