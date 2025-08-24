using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Api.Core;
using Service.Api.Service;
using Service.Api.Service.SystemManager.Models.DTO;

namespace Service.Api.Controllers
{
    [Route("api/system-manager/company/{companyId}/module/{moduleId}/location")]
    [Authorize]
    public class LocationController : MainController
    {
        private readonly ILocationService _systemService;

        public LocationController(ILocationService systemService)
        {
            _systemService = systemService;
        }

        [HttpGet]
        public IActionResult GetAll(Guid companyId, Guid moduleId)
        {
            var response = _systemService.GetAllLocations(companyId, moduleId);
            return Ok(response);
        }

        [HttpGet("{sensorId}")]
        public IActionResult GetById(Guid companyId, Guid moduleId, Guid sensorId)
        {
            var response = _systemService.GetLocationById(companyId, moduleId, sensorId);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Register(Guid companyId, Guid moduleId, [FromBody] NewLocationRequest request)
        {
            request.CompanyId = companyId;
            request.ModuleId = moduleId;
            var response = _systemService.RegisterNewLocation(request);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Update(Guid companyId, Guid moduleId, [FromBody] LocationDto request)
        {
            request.ModuleId = moduleId;
            var response = _systemService.UpdateLocation(request);
            return Ok(response);
        }

        [HttpDelete("{sensorId}")]
        public IActionResult Delete(Guid companyId, Guid moduleId, Guid sensorId)
        {
            _systemService.DeleteLocation(sensorId);
            return Ok();
        }
    }
}
