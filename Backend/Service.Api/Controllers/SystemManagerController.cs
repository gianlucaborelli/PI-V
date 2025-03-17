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
    }
}
