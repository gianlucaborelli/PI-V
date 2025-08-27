using Service.Api.Database;
using Service.Api.Service.Authentication;
using Service.Api.Service.SystemManager.Models.DTO;
using Service.Api.Service.SystemManager.Models.Mapper;

namespace Service.Api.Service
{
    public class LocationService : ILocationService
    {
        private readonly ServiceDatabaseContext _context;
        private readonly ILoggedInUser _loggedInUser;

        public LocationService(ServiceDatabaseContext context, ILoggedInUser loggedInUser)
        {
            _context = context;
            _loggedInUser = loggedInUser;
        }

        public List<LocationDto> GetAllLocations(Guid companyId, Guid moduleId)
        {
            var userId = _loggedInUser.GetUserId();

            var locations = _context.Locations
                .Where(s => s.ModuleId == moduleId && s.Module.CompanyId == companyId && s.Module.Company.UserCompanies.Any(uc => uc.UserId == userId))
                .ToList();

            return locations.ToDto();
        }

        public LocationDto GetLocationById(Guid companyId, Guid moduleId, Guid locationId)
        {
            var userId = _loggedInUser.GetUserId();

            var location = _context.Locations
                .Where(s => s.Id == locationId && s.ModuleId == moduleId && s.Module.CompanyId == companyId && s.Module.Company.UserCompanies.Any(uc => uc.UserId == userId))
                .FirstOrDefault();

            if (location == null)
                throw new Exception("Location not found");

            return location.ToDto();
        }

        public LocationDto RegisterNewLocation(NewLocationRequest request)
        {
            var userId = _loggedInUser.GetUserId();

            var module = _context.Modules
                .Where(m => m.Id == request.ModuleId && m.Company.UserCompanies.Any(uc => uc.UserId == userId))
                .FirstOrDefault();

            if (module == null)
                throw new Exception("Module not found");

            var newLocation = request.MapToLocation();

            foreach (var newRiskRequest in request.RiskLimits)
            {
                var risk = _context.Risks.Find(newRiskRequest.RiskId) ?? throw new Exception($"Risk with ID {newRiskRequest.RiskId} not found");
                newLocation.RiskLimits.Add(risk);
            }

            _context.Locations.Add(newLocation);
            _context.SaveChanges();

            return newLocation.ToDto();
        }

        public LocationDto UpdateLocation(LocationDto request)
        {
            var userId = _loggedInUser.GetUserId();

            var location = _context.Locations
                .Where(s => s.Id == request.Id && s.Module.Company.UserCompanies.Any(uc => uc.UserId == userId))
                .FirstOrDefault();

            if (location == null)
                throw new Exception("Location not found");

            location.Name = request.Name;
            location.Description = request.Description;
            
            _context.SaveChanges();

            return location.ToDto();
        }

        public void DeleteLocation(Guid sensorId)
        {
            var userId = _loggedInUser.GetUserId();

            var location = _context.Locations
                .Where(s => s.Id == sensorId && s.Module.Company.UserCompanies.Any(uc => uc.UserId == userId))
                .FirstOrDefault();

            if (location == null)
                throw new Exception("Location not found");

            _context.Locations.Remove(location);
            _context.SaveChanges();
        }
    }

}
