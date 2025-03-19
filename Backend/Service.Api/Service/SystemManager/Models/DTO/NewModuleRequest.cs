namespace Service.Api.Service.SystemManager.Models.DTO
{
    public class NewModuleRequest
    {
        public string Tag { get; set; }
        public string? EspId { get; set; }
        public Guid CompanyId { get; set; }

        public List<NewSensorRequest> Sensors { get; set; } = [];
    }
}
