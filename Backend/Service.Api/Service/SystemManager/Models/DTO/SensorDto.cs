namespace Service.Api.Service.SystemManager.Models.DTO
{
    public class SensorDto
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid ModuleId { get; set; }
        public string Type { get; set; }
    }
}
