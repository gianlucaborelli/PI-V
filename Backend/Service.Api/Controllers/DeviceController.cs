using Microsoft.AspNetCore.Mvc;
using Service.Api.Core;
using Service.Api.Service.DeviceManager;
using Service.Api.Service.DeviceManager.Models;
using Service.Api.Service.SystemManager.Models.DTO;

namespace Service.Api.Controllers
{
    public class DeviceController : MainController
    {
        private readonly IDeviceService sensorDataService ;
        public DeviceController(IDeviceService service) 
        {
            sensorDataService = service;
        }

        /// <summary>
        /// Registra novos dados de sensores.    
        /// <response code="200">Retorna o módulo validado</response>
        /// <response code="400">Formato invalido</response>
        [HttpPost("sensor-data")]
        public async Task<IActionResult> AddSensorData([FromBody] SensorDataRequest request)
        {
            var response = await sensorDataService.AddSensorDataAsync(request);
            if (response)
                return Ok();
            else
                return BadRequest("Failed to add sensor data");
        }


        /// <summary>
        /// Valida a sincronização do módulo através de um token.
        /// </summary>
        /// <param name="request">Token do módulo que será validado.</param>        
        /// <response code="200">Retorna o módulo validado</response>
        /// <response code="401">Token inválido</response>
        /// <returns>Objeto <see cref="ModuleDto"/> representando o módulo validado.</returns>
        [ProducesResponseType(typeof(ModuleDto), StatusCodes.Status200OK)]
        [HttpPost("validate-module-synchronization")]
        public async Task<IActionResult> ValidateModuleSynchronization([FromBody] ValidateModuleRequest request)
        {
            try
            {
                var locations = await sensorDataService.ValidateModuleSincronizationAsync(request.ModuleToken);
                return Ok(locations);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
