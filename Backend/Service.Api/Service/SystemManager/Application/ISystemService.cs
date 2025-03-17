using Service.Api.Service.SystemManager.Models.DTO;

namespace Service.Api.Service.SystemManager.Application;

public interface ISystemService
{
    List<CompanyDto> GetAllCompanies();
    CompanyDto GetCompanyById(Guid id);    
    NewCompanyResponse RegisterNewCompany (NewCompanyRequest request);
    UpdateCompanyResponse UpdateCompany(UpdateCompanyRequest request);

}
