using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Api.Core;
using Service.Api.Service;

namespace Service.Api.Controllers
{
    [Route("api/system-manager/risk")]
    [Authorize]
    public class RiskController : MainController
    {
        private readonly IRiskService _riskService;
        public RiskController(IRiskService riskService)
        {
            _riskService = riskService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]string riskType)
        {
            var response = await _riskService.GetAllRIsk(riskType);
            return Ok(response);
        }
    }
}
