using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Api.Core;
using Service.Api.Service;
using Service.Api.Service.SystemManager.Models.DTO;

namespace Service.Api.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento de Localizações (Locations) de módulos de uma empresa.
    /// Permite operações de consulta, cadastro, atualização e remoção de localizações.
    /// </summary>
    [Route("api/system-manager/company/{companyId}/module/{moduleId}/location")]
    [Authorize]
    public class LocationController : MainController
    {
        private readonly ILocationService _systemService;

        /// <summary>
        /// Inicializa uma nova instância do <see cref="LocationController"/>.
        /// </summary>
        /// <param name="systemService">Serviço de localização.</param>
        public LocationController(ILocationService systemService)
        {
            _systemService = systemService;
        }

        /// <summary>
        /// Retorna todas as localizações de um módulo de uma empresa.
        /// </summary>
        /// <param name="companyId">Id da empresa.</param>
        /// <param name="moduleId">Id do módulo.</param>
        /// <returns>Lista de localizações.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<LocationDto>), StatusCodes.Status200OK)]
        public IActionResult GetAll(Guid companyId, Guid moduleId)
        {
            var response = _systemService.GetAllLocations(companyId, moduleId);
            return Ok(response);
        }

        /// <summary>
        /// Retorna uma localização específica pelo Id da localização.
        /// </summary>
        /// <param name="companyId">Id da empresa.</param>
        /// <param name="moduleId">Id do módulo.</param>
        /// <param name="locationId">Id da localização.</param>
        /// <returns>Dados da localização.</returns>
        [HttpGet("{locationId}")]
        [ProducesResponseType(typeof(LocationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(Guid companyId, Guid moduleId, Guid locationId)
        {
            var response = _systemService.GetLocationById(companyId, moduleId, locationId);
            if (response == null)
                return NotFound();
            return Ok(response);
        }

        /// <summary>
        /// Cadastra uma nova localização para o módulo da empresa.
        /// </summary>
        /// <param name="companyId">Id da empresa.</param>
        /// <param name="moduleId">Id do módulo.</param>
        /// <param name="request">Dados da nova localização.</param>
        /// <returns>Localização cadastrada.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(LocationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Register(Guid companyId, Guid moduleId, [FromBody] NewLocationRequest request)
        {
            request.CompanyId = companyId;
            request.ModuleId = moduleId;
            var response = _systemService.RegisterNewLocation(request);
            return Ok(response);
        }

        /// <summary>
        /// Atualiza uma localização existente.
        /// </summary>
        /// <param name="companyId">Id da empresa.</param>
        /// <param name="moduleId">Id do módulo.</param>
        /// <param name="request">Dados atualizados da localização.</param>
        /// <returns>Localização atualizada.</returns>
        [HttpPut]
        [ProducesResponseType(typeof(LocationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update(Guid companyId, Guid moduleId, [FromBody] LocationDto request)
        {
            request.ModuleId = moduleId;
            var response = _systemService.UpdateLocation(request);
            return Ok(response);
        }

        /// <summary>
        /// Remove uma localização pelo Id.
        /// </summary>
        /// <param name="companyId">Id da empresa.</param>
        /// <param name="moduleId">Id do módulo.</param>
        /// <param name="locationId">Id da localização.</param>
        /// <returns>Status da operação.</returns>
        [HttpDelete("{locationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(Guid companyId, Guid moduleId, Guid locationId)
        {
            _systemService.DeleteLocation(locationId);
            return Ok();
        }
    }
}
