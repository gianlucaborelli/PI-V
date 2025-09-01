using Service.Api.Core;
using Service.Api.Service.SystemManager.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Api.Service;

namespace Service.Api.Controllers
{
    /// <summary>
    /// Controller responsável por gerenciar operações relacionadas à entidade Company.
    /// Fornece endpoints para listar, consultar, registrar, atualizar e excluir empresas.
    /// </summary>
    [Route("api/system-manager/company")]
    [Authorize]
    public class CompanyController : MainController
    {
        private readonly ICompanyService _companyService;

        /// <summary>
        /// Inicializa uma nova instância de <see cref="CompanyController"/>.
        /// </summary>
        /// <param name="systemService">Serviço de empresa.</param>        
        public CompanyController(ICompanyService systemService)
        {
            _companyService = systemService;
        }

        /// <summary>
        /// Retorna todas as empresas cadastradas.
        /// </summary>
        /// <returns>Lista de <see cref="System.Collections.Generic.List{CompanyDto}"/> representando as empresas cadastradas.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<CompanyDto>), 200)]
        public IActionResult GetAll()
        {
            var response = _companyService.GetAllCompanies();
            return Ok(response);
        }

        /// <summary>
        /// Retorna os dados de uma empresa pelo seu identificador.
        /// </summary>
        /// <param name="id">Identificador da empresa.</param>
        /// <returns>Objeto <see cref="CompanyDto"/> representando a empresa cadastrada com o ID passado.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CompanyDto), 200)]
        public IActionResult GetById(Guid id)
        {
            var response = _companyService.GetCompanyById(id);
            return Ok(response);
        }

        /// <summary>
        /// Cadastra uma nova empresa.
        /// </summary>
        /// <param name="request">Dados da nova empresa.</param>
        [HttpPost]
        [ProducesResponseType(typeof(NewCompanyResponse), 200)]
        public IActionResult Register([FromBody] NewCompanyRequest request)
        {
            var response = _companyService.RegisterNewCompany(request);
            return Ok(response);
        }

        /// <summary>
        /// Atualiza os dados de uma empresa existente.
        /// </summary>
        /// <param name="request">Dados atualizados da empresa.</param>
        [HttpPut]
        [ProducesResponseType(typeof(UpdateCompanyResponse), 200)]
        public IActionResult Update([FromBody] UpdateCompanyRequest request)
        {
            var response = _companyService.UpdateCompany(request);
            return Ok(response);
        }

        /// <summary>
        /// Exclui uma empresa pelo seu identificador.
        /// </summary>
        /// <param name="id">Identificador da empresa.</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public IActionResult Delete(Guid id)
        {
            _companyService.DeleteCompany(id);
            return Ok();
        }
    }
}
