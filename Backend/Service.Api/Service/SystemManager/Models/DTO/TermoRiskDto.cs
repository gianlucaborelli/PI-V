using System.Text.Json.Serialization;

namespace Service.Api.Service.SystemManager.Models.DTO
{

    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(TermoRiskDto), typeDiscriminator: "TermoRisk")]
    public abstract class RiskDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class TermoRiskDto : RiskDto
    {
        public string Category { get; set; } = string.Empty;
        public string SubCategory { get; set; } = string.Empty;
        public string Activity { get; set; } = string.Empty;
        public int MetabolicRate { get; set; }
    }
}
