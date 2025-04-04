using Service.Api.Service.SystemManager.Models.DTO;

namespace Service.Api.Service.SystemManager.Models
{
    public class DashboardResponse
    {
        public double IbtgEstimation { get; set; }
        public List<SensorDataDto> Series { get; set; } = [];
    }
}
