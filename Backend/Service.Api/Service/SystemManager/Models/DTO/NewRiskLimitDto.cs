namespace Service.Api.Service.SystemManager.Models.DTO
{
    public class NewRiskLimitDto
    {
        public Guid RiskId { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}
