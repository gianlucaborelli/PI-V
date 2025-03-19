using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Api.Core;
using Service.Api.Service.SystemManager.Application;
using Service.Api.Service.SystemManager.Models.DTO;

namespace Service.Api.Controllers
{
    [Route("api/system-manager")]
    [Authorize]
    public class SystemManagerController : MainController
    {
        private readonly ISystemService _systemService;
        public SystemManagerController(ISystemService systemService)
        {
            _systemService = systemService;
        }

        [HttpGet("company")]
        public IActionResult GetAllCompany()
        {
            var response = _systemService.GetAllCompanies();
            return Ok(response);
        }

        [HttpGet("company/{id}")]
        public IActionResult GetCompanyById(Guid id)
        {
            var response = _systemService.GetCompanyById(id);
            return Ok(response);
        }

        [HttpPost("company")]
        public IActionResult RegisterCompany([FromBody] NewCompanyRequest request)
        {
            var response = _systemService.RegisterNewCompany(request);
            return Ok(response);
        }

        [HttpPut("company")]
        public IActionResult UpdateCompany([FromBody] UpdateCompanyRequest request)
        {
            var response = _systemService.UpdateCompany(request);
            return Ok(response);
        }

        [HttpDelete("company/{id}")]
        public IActionResult DeleteCompany(Guid id)
        {
            _systemService.DeleteCompany(id);
            return Ok();
        }

        [HttpGet("company/{companyId}/module")]
        public IActionResult GetAllModules(Guid companyId)
        {
            var response = _systemService.GetAllModules(companyId);
            return Ok(response);
        }

        [HttpGet("company/{companyId}/module/{moduleId}")]
        public IActionResult GetModuleById(Guid companyId, Guid moduleId)
        {
            var response = _systemService.GetModuleById(companyId, moduleId);
            return Ok(response);
        }

        [HttpPost("company/{companyId}/module")]
        public IActionResult RegisterModule([FromRoute]Guid companyId, [FromBody] NewModuleRequest request)
        {
            request.CompanyId = companyId;
            var response = _systemService.RegisterNewModule(request);
            return Ok(response);
        }

        [HttpPut("company/{companyId}/module")]
        public IActionResult UpdateModule([FromRoute] Guid companyId, [FromBody] UpdateModuleRequest request)
        {
            request.CompanyId = companyId;
            var response = _systemService.UpdateModule(request);
            return Ok(response);
        }

        [HttpDelete("company/{companyId}/module/{moduleId}")]
        public IActionResult DeleteModule(Guid companyId, Guid moduleId)
        {
            _systemService.DeleteModule(companyId, moduleId);
            return Ok();
        }
    }
}
