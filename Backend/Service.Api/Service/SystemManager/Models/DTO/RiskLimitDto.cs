namespace Service.Api.Service.SystemManager.Models.DTO
{
    public class RiskLimitDto
    {
        public Guid Id { get; set; }
        public double LimitValue { get; set; }
        public string Type { get; set; } = string.Empty; // discriminador (IBUTG, Noise, etc.)
    }
}
