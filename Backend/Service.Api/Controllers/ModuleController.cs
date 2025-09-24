using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Api.Core;
using Service.Api.Service;
using Service.Api.Service.SystemManager.Models.DTO;

namespace Service.Api.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento de módulos de uma empresa.
    /// Permite operações de CRUD, geração, validação e revogação de tokens de acesso de módulos.
    /// </summary>
    [Route("api/system-manager/company/{companyId}/module")]
    [Authorize]
    public class ModuleController : MainController
    {
        private readonly IModuleService _systemService;

        /// <summary>
        /// Inicializa uma nova instância do <see cref="ModuleController"/>.
        /// </summary>
        /// <param name="systemService">Serviço de módulos.</param>
        public ModuleController(IModuleService systemService)
        {
            _systemService = systemService;
        }

        /// <summary>
        /// Retorna todos os módulos de uma empresa.
        /// </summary>
        /// <param name="companyId">ID da empresa.</param>
        /// <returns>Lista de módulos.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<ModuleDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAll(Guid companyId)
        {
            var response = _systemService.GetAllModules(companyId);
            return Ok(response);
        }

        /// <summary>
        /// Retorna um módulo específico pelo ID.
        /// </summary>
        /// <param name="companyId">ID da empresa.</param>
        /// <param name="moduleId">ID do módulo.</param>
        /// <returns>Dados do módulo.</returns>
        [HttpGet("{moduleId}")]
        [ProducesResponseType(typeof(ModuleDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(Guid companyId, Guid moduleId)
        {
            var response = _systemService.GetModuleById(companyId, moduleId);
            return Ok(response);
        }

        /// <summary>
        /// Registra um novo módulo para a empresa.
        /// </summary>
        /// <param name="companyId">ID da empresa.</param>
        /// <param name="request">Dados do novo módulo.</param>
        /// <returns>Módulo criado.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ModuleDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Register(Guid companyId, [FromBody] NewModuleRequest request)
        {
            request.CompanyId = companyId;
            var response = _systemService.RegisterNewModule(request);
            return Ok(response);
        }

        /// <summary>
        /// Atualiza os dados de um módulo existente.
        /// </summary>
        /// <param name="companyId">ID da empresa.</param>
        /// <param name="request">Dados atualizados do módulo.</param>
        /// <returns>Módulo atualizado.</returns>
        [HttpPut]
        [ProducesResponseType(typeof(ModuleDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(Guid companyId, [FromBody] UpdateModuleRequest request)
        {
            request.CompanyId = companyId;
            var response = _systemService.UpdateModule(request);
            return Ok(response);
        }

        /// <summary>
        /// Exclui um módulo da empresa.
        /// </summary>
        /// <param name="companyId">ID da empresa.</param>
        /// <param name="moduleId">ID do módulo.</param>
        /// <returns>Status da operação.</returns>
        [HttpDelete("{moduleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(Guid companyId, Guid moduleId)
        {
            _systemService.DeleteModule(companyId, moduleId);
            return Ok();
        }

        /// <summary>
        /// Gera um novo token de acesso para o módulo.
        /// </summary>
        /// <param name="companyId">ID da empresa.</param>
        /// <param name="moduleId">ID do módulo.</param>
        /// <returns>Token de acesso gerado.</returns>
        [HttpPost("{moduleId}/generate-access-token")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetNewModuleAccessToken(Guid companyId, Guid moduleId)
        {
            var response = _systemService.GetNewModuleAccessToken(companyId, moduleId);
            return Ok(response);
        }

        ///// <summary>
        ///// Valida o token de acesso do módulo.
        ///// </summary>
        ///// <param name="companyId">ID da empresa.</param>
        ///// <param name="moduleId">ID do módulo.</param>
        ///// <param name="moduleAccessToken">Token de acesso do módulo.</param>
        ///// <returns>Status da validação.</returns>
        //[HttpPost("{moduleId}/validate-access-token")]
        //[ProducesResponseType(typeof(ModuleDto), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public IActionResult ValidateModuleAccessToken(Guid companyId, Guid moduleId, [FromBody] string moduleAccessToken)
        //{
        //    var isValid = _systemService.ValidateModuleAccessToken(companyId, moduleId, moduleAccessToken);
        //    return Ok(isValid);
        //}

        /// <summary>
        /// Revoga o token de acesso do módulo.
        /// </summary>
        /// <param name="companyId">ID da empresa.</param>
        /// <param name="moduleId">ID do módulo.</param>
        /// <returns>Status da operação.</returns>
        [HttpPost("{moduleId}/revoke-access-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult RevokeModuleAccessToken(Guid companyId, Guid moduleId)
        {
            _systemService.RevokeModuleAccessToken(companyId, moduleId);
            return Ok();
        }
    }
}
