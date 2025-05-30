using Microsoft.EntityFrameworkCore;
using Service.Api.Database;
using Service.Api.Service.Authentication;
using Service.Api.Service.SystemManager.Models.DTO;
using Service.Api.Service.SystemManager.Models.Mapper;

namespace Service.Api.Service
{
    public class ModuleService : IModuleService
    {
        private readonly ServiceDatabaseContext _context;
        private readonly ILoggedInUser _loggedInUser;

        public ModuleService(ServiceDatabaseContext context, ILoggedInUser loggedInUser)
        {
            _context = context;
            _loggedInUser = loggedInUser;
        }

        public List<ModuleDto> GetAllModules(Guid companyId)
        {
            var userId = _loggedInUser.GetUserId();

            var modules = _context.Modules
                .Where(m => m.CompanyId == companyId && m.Company.UserCompanies.Any(uc => uc.UserId == userId))
                .Include(m => m.Sensors)
                .ToList();

            return modules.ToModuleDto();
        }

        public ModuleDto GetModuleById(Guid companyId, Guid id)
        {
            var userId = _loggedInUser.GetUserId();

            var module = _context.Modules
                .Where(m => m.Id == id && m.CompanyId == companyId && m.Company.UserCompanies.Any(uc => uc.UserId == userId))
                .Include(m => m.Sensors)
                .FirstOrDefault();

            if (module == null)
                throw new Exception("Module not found");

            return module.ToModuleDto();
        }

        public ModuleDto RegisterNewModule(NewModuleRequest request)
        {
            var userId = _loggedInUser.GetUserId();

            var company = _context.Companies
                .Where(c => c.Id == request.CompanyId && c.UserCompanies.Any(uc => uc.UserId == userId))
                .FirstOrDefault();

            if (company == null)
                throw new Exception("Company not found");

            var newModule = request.MapToModule();

            _context.Modules.Add(newModule);
            _context.SaveChanges();

            return newModule.ToModuleDto();
        }

        public ModuleDto UpdateModule(UpdateModuleRequest request)
        {
            var userId = _loggedInUser.GetUserId();

            var module = _context.Modules
                .Where(m => m.Id == request.Id && m.CompanyId == request.CompanyId && m.Company.UserCompanies.Any(uc => uc.UserId == userId))
                .FirstOrDefault();

            if (module == null)
                throw new Exception("Module not found");

            module.EspId = request.EspId;
            module.Tag = request.Tag;

            _context.SaveChanges();

            return module.ToModuleDto();
        }

        public void DeleteModule(Guid companyId, Guid id)
        {
            var userId = _loggedInUser.GetUserId();

            var module = _context.Modules
                .Where(m => m.Id == id && m.CompanyId == companyId && m.Company.UserCompanies.Any(uc => uc.UserId == userId))
                .FirstOrDefault();

            if (module == null)
                throw new Exception("Module not found");

            _context.Modules.Remove(module);
            _context.SaveChanges();
        }
    }

}
