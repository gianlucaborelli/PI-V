using Service.Api.Core;
using Service.Api.Service.SystemManager.Application;
using Service.Api.Service.SystemManager.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Service.Api.Controllers
{
    [Route("api/system-manager/company")]
    [Authorize]
    public class CompanyController : MainController
    {
        private readonly ISystemService _systemService;

        public CompanyController(ISystemService systemService)
        {
            _systemService = systemService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = _systemService.GetAllCompanies();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var response = _systemService.GetCompanyById(id);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Register([FromBody] NewCompanyRequest request)
        {
            var response = _systemService.RegisterNewCompany(request);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateCompanyRequest request)
        {
            var response = _systemService.UpdateCompany(request);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _systemService.DeleteCompany(id);
            return Ok();
        }
    }
}
