namespace Service.Api.Service.SystemManager.Models.DTO
{
    public class NewModuleRequest
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? EspId { get; set; }
        public Guid CompanyId { get; set; } 
        
        public List<NewLocationRequest> Locations { get; set; } = [];

    }
}
