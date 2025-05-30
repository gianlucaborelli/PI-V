using Service.Api.Service.SystemManager.Models.DTO;

namespace Service.Api.Service
{
    public interface ICompanyService
    {
        List<CompanyDto> GetAllCompanies();
        CompanyDto GetCompanyById(Guid id);
        NewCompanyResponse RegisterNewCompany(NewCompanyRequest request);
        UpdateCompanyResponse UpdateCompany(UpdateCompanyRequest request);
        void DeleteCompany(Guid id);
    }
}
