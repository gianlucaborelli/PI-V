using Service.Api.Service.SystemManager.Models.DTO;

namespace Service.Api.Service
{
    public interface IRiskService
    {
        Task<List<RiskDto>> GetAllRIsk(string riskType);
    }
}
