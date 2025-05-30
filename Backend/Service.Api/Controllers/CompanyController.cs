using Service.Api.Core;
using Service.Api.Service.SystemManager.Application;
using Service.Api.Service.SystemManager.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Api.Service;

namespace Service.Api.Controllers
{
    [Route("api/system-manager/company")]
    [Authorize]
    public class CompanyController : MainController
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService systemService)
        {
            _companyService = systemService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = _companyService.GetAllCompanies();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var response = _companyService.GetCompanyById(id);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Register([FromBody] NewCompanyRequest request)
        {
            var response = _companyService.RegisterNewCompany(request);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateCompanyRequest request)
        {
            var response = _companyService.UpdateCompany(request);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _companyService.DeleteCompany(id);
            return Ok();
        }
    }
}
