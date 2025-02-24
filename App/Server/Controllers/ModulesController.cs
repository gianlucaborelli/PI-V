using Microsoft.AspNetCore.Mvc;
using Shared.Models.DTOs;
using Server.Services;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController(IModuleService moduleService) : ControllerBase
    {
        private readonly IModuleService _moduleService = moduleService;

        [HttpGet]
        public async Task<ActionResult> GetAllModules()
        {
            var result = await _moduleService.GetModules();
            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetModuleById(int id)
        {
            var result = await _moduleService.GetModule(id);
            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateModule([FromBody] CreateModuleRequest request)
        {
            var result = await _moduleService.Create(request);
            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);
        }

        [HttpPost("sensor")]
        public async Task<ActionResult> CreateSensor([FromBody] AddSensorOnModuleRequest request)
        {
            var result = await _moduleService.AddSensor(request);
            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);
        }

        [HttpPut()]
        public async Task<ActionResult> UpdateModule([FromBody] UpdateModuleRequest request)
        {
            var result = await _moduleService.Update(request);
            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);
        }

        [HttpPut("sensor")]
        public async Task<ActionResult> UpdateSensor([FromBody] UpdateSensorOnModuleRequest request)
        {
            var result = await _moduleService.UpdateSensor(request);
            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);
        }

        [HttpDelete("{request}")]
        public async Task<ActionResult> DeleteModule(int request)
        {
            var result = await _moduleService.Delete(request);
            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);
        }

        [HttpDelete("sensor/{request}")]
        public async Task<ActionResult> DeleteSensor(int request)
        {
            var result = await _moduleService.DeleteSensor(request);
            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);
        }
    }
}
