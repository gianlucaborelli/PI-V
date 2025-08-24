using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Api.Core;
using Service.Api.Service;
using Service.Api.Service.SystemManager.Models.DTO;

namespace Service.Api.Controllers
{
    [Route("api/system-manager/company/{companyId}/module")]
    [Authorize]
    public class ModuleController : MainController
    {
        private readonly IModuleService _systemService;

        public ModuleController(IModuleService systemService)
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

        // Generate Access Token for Module
        [HttpPost("{moduleId}/generate-access-token")]
        public IActionResult GetNewModuleAccessToken(Guid companyId, Guid moduleId)
        {
            var response = _systemService.GetNewModuleAccessToken(companyId, moduleId);
            return Ok(response);
        }

        // Validate Module Access Token transfer to DeviceControllers
        [HttpPost("{moduleId}/validate-access-token")]
        public IActionResult ValidateModuleAccessToken(Guid companyId, Guid moduleId, [FromBody] string moduleAccessToken)
        {
            var isValid = _systemService.ValidateModuleAccessToken(companyId, moduleId, moduleAccessToken);
            return Ok(isValid);
        }

        // Revoke Module Access Token
        [HttpPost("{moduleId}/revoke-access-token")]
        public IActionResult RevokeModuleAccessToken(Guid companyId, Guid moduleId)
        {
            _systemService.RevokeModuleAccessToken(companyId, moduleId);
            return Ok();
        }
    }
}
