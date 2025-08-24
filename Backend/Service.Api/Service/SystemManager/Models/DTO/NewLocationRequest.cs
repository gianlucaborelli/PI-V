namespace Service.Api.Service.SystemManager.Models.DTO
{
    public class NewLocationRequest
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid ModuleId{ get; set; }
        public List<NewRiskLimitDto> RiskLimits { get; set; } = [];
    }
}
