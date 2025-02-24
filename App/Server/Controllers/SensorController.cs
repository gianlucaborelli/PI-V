using Microsoft.AspNetCore.Mvc;
using Server.Services;
using Shared.Models.DTOs;
using Shared.Requests;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorController(ISensorService sensorService) : ControllerBase
    {
        private readonly ISensorService _sensorService = sensorService;

        [HttpGet]
        public async Task<ActionResult> GetAllByDate([FromQuery] DateTime date, [FromQuery] int sensorId)
        {
            SensorDataByDayRequest request = new SensorDataByDayRequest { Date = date, SensorId = sensorId };
            var result = await _sensorService.GetSensorDataByDay(request);
            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);
        }

        [HttpGet("module")]
        public async Task<ActionResult> GetAllByDateAndMonitorId([FromQuery] DateTime date, [FromQuery] int moduleId)
        {
            SensorDataByDayAndModuleId request = new SensorDataByDayAndModuleId { Date = date, ModuleId = moduleId };
            var result = await _sensorService.GetSensorDataByModuleIdAndDate(request);
            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);
        }

        [HttpPost]
        public async Task<ActionResult> SetSensorData([FromBody] SensorDataDto date)
        {            
            var result = await _sensorService.SetSensorDataAsync(date);
            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);
        }
    }
}
