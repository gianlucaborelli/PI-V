namespace Service.Api.Service.SystemManager.Models.DTO
{
    public class NewRiskLimitRequest
    {           
        public Guid RiskId { get; set; }

        /// <summary>
        /// Location ao qual o limite pertence
        /// </summary>
        public Guid LocationId { get; set; }
    }
}
