namespace Service.Api.Service.SystemManager.Models.Risks
{
    public class TermoRisk : Risk
    {
        public string Category { get; set; } = string.Empty;
        public string SubCategory { get; set; } = string.Empty;
        public string Activity { get; set; } = string.Empty;
        public int MetabolicRate { get; set; }
    }
}
