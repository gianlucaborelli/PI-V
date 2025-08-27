using Microsoft.AspNetCore.Mvc;
using Service.Api.Core;
using Service.Api.Service.DeviceManager;
using Service.Api.Service.DeviceManager.Models;

namespace Service.Api.Controllers
{
    public class DeviceController : MainController
    {
        private readonly IDeviceService sensorDataService ;
        public DeviceController(IDeviceService service) 
        {
            sensorDataService = service;
        }

        [HttpPost("sensor-data")]
        public async Task<IActionResult> AddSensorData([FromBody] SensorDataRequest request)
        {
            var response = await sensorDataService.AddSensorDataAsync(request);
            if (response)
                return Ok();
            else
                return BadRequest("Failed to add sensor data");
        }

        [HttpPost("validate-module-synchronization")]
        public async Task<IActionResult> ValidateModuleSynchronization([FromBody] ValidateModuleRequest request)
        {
            try
            {
                var locations = await sensorDataService.ValidateModuleSincronizationAsync(request.ModuleToken);
                return Ok(locations);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
