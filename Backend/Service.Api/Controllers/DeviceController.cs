using Microsoft.AspNetCore.Mvc;
using Service.Api.Core;
using Service.Api.Service.DeviceManager;
using Service.Api.Service.DeviceManager.Models;

namespace Service.Api.Controllers
{
    public class DeviceController : MainController
    {
        public ISensorDataService sensorDataService { get; set; } = null!;
        public DeviceController(ISensorDataService service) 
        {
            sensorDataService = service;
        }

        [HttpPost("sensor-data")]
        public async Task<IActionResult> AddSensorData([FromBody] SensorDataRequest request)
        {
            var response = await sensorDataService.AddSensorDataAsync(request.SensorId, request.Value);
            if (response)
                return Ok();
            else
                return BadRequest("Failed to add sensor data");
        }
    }
}
