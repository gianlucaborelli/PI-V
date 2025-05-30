using Microsoft.EntityFrameworkCore;
using Service.Api.Database;
using Service.Api.Service.Authentication;
using Service.Api.Service.Authentication.Models;
using Service.Api.Service.SystemManager.Models.DTO;
using Service.Api.Service.SystemManager.Models.Mapper;

namespace Service.Api.Service
{
    public class CompanyService : ICompanyService
    {
        private readonly ServiceDatabaseContext _context;
        private readonly ILoggedInUser _loggedInUser;

        public CompanyService(ServiceDatabaseContext context, ILoggedInUser loggedInUser)
        {
            _context = context;
            _loggedInUser = loggedInUser;
        }

        public List<CompanyDto> GetAllCompanies()
        {
            var userId = _loggedInUser.GetUserId();

            var companies = _context.Companies
                .Where(c => c.UserCompanies.Any(uc => uc.UserId == userId))
                .Include(c => c.UserCompanies)
                .ToList();

            return companies.ToCompanyDto();
        }

        public CompanyDto GetCompanyById(Guid id)
        {
            var userId = _loggedInUser.GetUserId();

            var company = _context.Companies
                .Where(c => c.Id == id && c.UserCompanies.Any(uc => uc.UserId == userId))
                .Include(c => c.UserCompanies)
                .FirstOrDefault();

            if (company == null)
                throw new Exception("Company not found");

            return company.ToCompanyDto();
        }

        public NewCompanyResponse RegisterNewCompany(NewCompanyRequest request)
        {
            var newCompany = request.MapToCompany();

            _context.Companies.Add(newCompany);
            _context.SaveChanges();

            _context.UserCompanies.Add(new UserCompany
            {
                CompanyId = newCompany.Id,
                UserId = _loggedInUser.GetUserId()
            });

            _context.SaveChanges();

            return newCompany.ToNewCompanyResponseDto();
        }

        public UpdateCompanyResponse UpdateCompany(UpdateCompanyRequest request)
        {
            var company = _context.Companies.FirstOrDefault(c => c.Id == request.Id);

            if (company == null)
                throw new Exception("Company not found");

            company.Name = request.Name;
            company.Tags = request.Tags;

            _context.SaveChanges();

            return company.ToCompanyUpdateResponseDto();
        }

        public void DeleteCompany(Guid id)
        {
            var userId = _loggedInUser.GetUserId();

            var company = _context.Companies
                .Where(c => c.Id == id && c.UserCompanies.Any(uc => uc.UserId == userId))
                .FirstOrDefault();

            if (company == null)
                throw new Exception("Company not found");

            _context.Companies.Remove(company);
            _context.SaveChanges();
        }
    }
}
