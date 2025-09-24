using Service.Api.Service.SystemManager.Models.DTO;

namespace Service.Api.Service.SystemManager.Models
{
    public class DashboardResponse
    {
       public List<LocationDataSummary> LocationsSummary { get; set; } = [];
    }

    public class LocationDataSummary
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public double IbutgEstimation { get; set; }
        public double IbutgReferenceLimit { get; set; }
        public double HumidityAverage { get; set; }
        public double TemperatureAverage { get; set; }
        public double MaxTemperature { get; set; }
        public double MinTemperature { get; set; }
        public List<SensorDataDto> Series { get; set; } = [];
    }
}
