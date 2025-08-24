namespace Service.Api.Service.SystemManager.Models.DTO
{
    public class UpdateModuleRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? EspId { get; set; }        
        public Guid CompanyId { get; set; }        
    }
}
