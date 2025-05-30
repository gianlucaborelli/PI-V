using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Api.Core;
using Service.Api.Service.SystemManager.Application;
using Service.Api.Service.SystemManager.Models.DTO;

namespace Service.Api.Controllers
{
    [Route("api/system-manager/company/{companyId}/module")]
    [Authorize]
    public class ModuleController : MainController
    {
        private readonly ISystemService _systemService;

        public ModuleController(ISystemService systemService)
        {
            _systemService = systemService;
        }

        [HttpGet]
        public IActionResult GetAll(Guid companyId)
        {
            var response = _systemService.GetAllModules(companyId);
            return Ok(response);
        }

        [HttpGet("{moduleId}")]
        public IActionResult GetById(Guid companyId, Guid moduleId)
        {
            var response = _systemService.GetModuleById(companyId, moduleId);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Register(Guid companyId, [FromBody] NewModuleRequest request)
        {
            request.CompanyId = companyId;
            var response = _systemService.RegisterNewModule(request);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Update(Guid companyId, [FromBody] UpdateModuleRequest request)
        {
            request.CompanyId = companyId;
            var response = _systemService.UpdateModule(request);
            return Ok(response);
        }

        [HttpDelete("{moduleId}")]
        public IActionResult Delete(Guid companyId, Guid moduleId)
        {
            _systemService.DeleteModule(companyId, moduleId);
            return Ok();
        }
    }
}
