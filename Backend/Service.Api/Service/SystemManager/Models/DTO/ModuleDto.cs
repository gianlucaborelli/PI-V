namespace Service.Api.Service.SystemManager.Models.DTO
{
    public class ModuleDto
    {
        public Guid Id { get; set; }
        public string Tag { get; set; }
        public string? EspId { get; set; }
        public Guid CompanyId { get; set; }

        public List<SensorDto> Sensors { get; set; } = [];
    }
}
