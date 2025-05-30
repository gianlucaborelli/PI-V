using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Api.Core;
using Service.Api.Service;
using Service.Api.Service.SystemManager.Application;
using Service.Api.Service.SystemManager.Models.DTO;

namespace Service.Api.Controllers
{
    [Route("api/system-manager/company/{companyId}/module/{moduleId}/sensor")]
    [Authorize]
    public class SensorController : MainController
    {
        private readonly ISensorService _systemService;

        public SensorController(ISensorService systemService)
        {
            _systemService = systemService;
        }

        [HttpGet]
        public IActionResult GetAll(Guid companyId, Guid moduleId)
        {
            var response = _systemService.GetAllSensors(companyId, moduleId);
            return Ok(response);
        }

        [HttpGet("{sensorId}")]
        public IActionResult GetById(Guid companyId, Guid moduleId, Guid sensorId)
        {
            var response = _systemService.GetSensorById(companyId, moduleId, sensorId);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Register(Guid companyId, Guid moduleId, [FromBody] NewSensorRequest request)
        {
            request.CompanyId = companyId;
            request.ModuleId = moduleId;
            var response = _systemService.RegisterNewSensor(request);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Update(Guid companyId, Guid moduleId, [FromBody] SensorDto request)
        {
            request.ModuleId = moduleId;
            var response = _systemService.UpdateSensor(request);
            return Ok(response);
        }

        [HttpDelete("{sensorId}")]
        public IActionResult Delete(Guid companyId, Guid moduleId, Guid sensorId)
        {
            _systemService.DeleteSensor(sensorId);
            return Ok();
        }
    }
}
