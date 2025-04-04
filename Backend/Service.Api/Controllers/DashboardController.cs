using Microsoft.AspNetCore.Mvc;
using Service.Api.Core;
using Service.Api.Service.SystemManager.Application;

namespace Service.Api.Controllers
{
    public class DashboardController : MainController
    {
        public IDashboardService dashboardService { get; set; } = null!;
        public DashboardController(IDashboardService service)
        {
            dashboardService = service;
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> AddSensorData([FromQuery] Guid moduleId, [FromQuery] DateTime? dateTime)
        {
            var response = await dashboardService.GetDashboardDataAsync(moduleId, dateTime);

            return Ok(response);
        }
    }
}