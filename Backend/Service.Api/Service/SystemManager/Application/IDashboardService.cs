using Service.Api.Service.SystemManager.Models;

namespace Service.Api.Service.SystemManager.Application
{
    public interface IDashboardService
    {
        Task<DashboardResponse> GetDashboardDataAsync(Guid moduleId, DateTime? queryDate);
        
    }
}
