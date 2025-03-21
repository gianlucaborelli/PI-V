namespace Service.Api.Service.SystemManager.Models.DTO
{
    public class NewSensorRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CompanyId { get; set; }
        public Guid ModuleId{ get; set; }
        public string Type { get; set; }
    }
}
