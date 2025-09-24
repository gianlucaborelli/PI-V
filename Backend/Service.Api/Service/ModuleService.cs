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
                .Include(m => m.Locations)
                    .ThenInclude(l => l.RiskLimits)
                .Include(m => m.AccessToken)
                .ToList();

            return modules.ToModuleDto();
        }

        public ModuleDto GetModuleById(Guid companyId, Guid id)
        {
            var userId = _loggedInUser.GetUserId();

            var module = _context.Modules
                .Where(m => m.Id == id && m.CompanyId == companyId && m.Company.UserCompanies.Any(uc => uc.UserId == userId))
                .Include(m => m.Locations)
                .Include(m => m.AccessToken)
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

           foreach (var locationRequest in request.Locations)
            {
                var newLocation = locationRequest.MapToLocation();
                foreach (var riskLimitRequest in locationRequest.RiskLimits)
                {
                    var risk = _context.Risks.Find(riskLimitRequest.RiskId) ?? throw new Exception($"Risk with ID {riskLimitRequest.RiskId} not found");
                    newLocation.RiskLimits.Add(risk);
                }
                newModule.Locations.Add(newLocation);
            }

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

            module.Name = request.Name;
            module.Description = request.Description;
            module.EspId = request.EspId;

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

        public bool GetNewModuleAccessToken(Guid companyId, Guid id)
        {
            var userId = _loggedInUser.GetUserId();
            var module = _context.Modules
                .Include(m => m.AccessToken)
                .Where(m => m.Id == id && m.CompanyId == companyId && m.Company.UserCompanies.Any(uc => uc.UserId == userId))
                .FirstOrDefault();
            if (module == null)
                throw new Exception("Module not found");
            
            module.AccessToken.RegenerateToken();
            
            return _context.SaveChanges() > 0;
        }

        public async Task<ModuleDto> ValidateModuleAccessToken(Guid companyId, Guid id, string moduleAccessToken)
        {
            var userId = _loggedInUser.GetUserId();
            var module = _context.Modules
                .Include(m => m.AccessToken)
                .Where(m => m.Id == id && m.CompanyId == companyId && m.Company.UserCompanies.Any(uc => uc.UserId == userId))
                .FirstOrDefault();
            if (module == null)
                throw new Exception("Module not found");

            var result = module.AccessToken?.Token == moduleAccessToken && module.AccessToken.IsActive && module.AccessToken.ExpiresAt > DateTime.UtcNow;

            if (!result)
                throw new Exception("Token invalid");

            module.AccessToken!.Revoke();
            await _context.SaveChangesAsync();

            var moduleResponse = _context.Modules
                .Include(m => m.Locations)
                .Where(m => m.Id == id && m.CompanyId == companyId && m.Company.UserCompanies.Any(uc => uc.UserId == userId))
                .FirstOrDefault();

            return moduleResponse.ToModuleDto();
        }

        public void RevokeModuleAccessToken(Guid companyId, Guid id)
        {
            var userId = _loggedInUser.GetUserId();
            var module = _context.Modules
                .Include(m => m.AccessToken)
                .Where(m => m.Id == id && m.CompanyId == companyId && m.Company.UserCompanies.Any(uc => uc.UserId == userId))
                .FirstOrDefault();

            if (module == null)
                throw new Exception("Module not found");
            
            module.AccessToken.Revoke();
            _context.SaveChanges();            
        }

        private void ValidateModuleOwnership(Guid companyId, Guid moduleId)
        {
            var userId = _loggedInUser.GetUserId();
            var module = _context.Modules
                .Where(m => m.Id == moduleId && m.CompanyId == companyId && m.Company.UserCompanies.Any(uc => uc.UserId == userId))
                .FirstOrDefault();
            if (module == null)
                throw new Exception("Module not found or you do not have permission to access it");
            if (module.CompanyId != companyId)
                throw new Exception("Module does not belong to the specified company");
        }
    }
}
