namespace Service.Api.Service.SystemManager.Models.DTO
{
    public class UpdateModuleRequest
    {
        public Guid Id { get; set; }
        public required string Tag { get; set; }
        public string? EspId { get; set; }        
        public Guid CompanyId { get; set; }        
    }
}
