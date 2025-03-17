using Microsoft.EntityFrameworkCore;
using Service.Api.Database;
using Service.Api.Service.Authentication;
using Service.Api.Service.Authentication.Models;
using Service.Api.Service.SystemManager.Models.DTO;
using Service.Api.Service.SystemManager.Models.Mapper;

namespace Service.Api.Service.SystemManager.Application;

public class SystemService : ISystemService
{
    private readonly ServiceDatabaseContext _context;
    private readonly ILoggedInUser _loggedInUser;


    public SystemService(ServiceDatabaseContext context, IJwtAuthManager jwtAuthManager, ILoggedInUser loggedInUser )
    {
        _context = context;        
        _loggedInUser = loggedInUser;
    }

    public List<CompanyDto> GetAllCompanies()
    {
        var userId = _loggedInUser.GetUserId();

        var companies = _context.Companies
            .Where(c => c.UserCompanies.Any(uc => uc.UserId == userId)) // Filtra apenas as empresas do usuário
            .Include(c => c.UserCompanies) // Opcional, caso precise dos detalhes da relação
            .ToList();

        return companies.ToCompanyDto();
    }

    public CompanyDto GetCompanyById(Guid id)
    {
        var userId = _loggedInUser.GetUserId();

        var company = _context.Companies
            .Where(c => c.Id == id && c.UserCompanies.Any(uc => uc.UserId == userId)) // Filtro antes
            .Include(c => c.UserCompanies) // Include após o filtro
            .FirstOrDefault();

        if (company == null)
        {
            throw new Exception("Company not found");
        }
        return company.ToCompanyDto();
    }

    public NewCompanyResponse RegisterNewCompany(NewCompanyRequest request)
    {
        var newConpany = request.MapToCompany();

        _context.Companies.Add(newConpany);

        _context.SaveChanges();        

        var newUserCompany = new UserCompany
        {
            CompanyId = newConpany.Id,
            UserId = _loggedInUser.GetUserId()
        };

        _context.UserCompanies.Add(newUserCompany);
        _context.SaveChanges();

        return newConpany.ToNewCompanyResponseDto();
    }

    public UpdateCompanyResponse UpdateCompany(UpdateCompanyRequest request)
    {
        var company = _context.Companies.FirstOrDefault(x => x.Id == request.Id);
        if (company == null)
        {
            throw new Exception("Company not found");
        }
        company.Name = request.Name;
        company.Tags = request.Tags;
        
        _context.Companies.Update(company);
        _context.SaveChanges();
        return company.ToCompanyUpdateResponseDto();
    }
}
