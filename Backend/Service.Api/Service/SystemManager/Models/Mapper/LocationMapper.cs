using Service.Api.Service.SystemManager.Models.DTO;
using Service.Api.Service.SystemManager.Models.Risks;

namespace Service.Api.Service.SystemManager.Models.Mapper
{
    public static class LocationMapper
    {
        public static Location MapToLocation(this NewLocationRequest request)
        {
            return new Location
            {                
                Name = request.Name,                
                ModuleId = request.ModuleId,
                Description = request.Description
            };
        }

        public static LocationDto ToDto(this Location location)
        {
            return new LocationDto
            {
                Id = location.Id,
                Name = location.Name,
                Description = location.Description,
                ModuleId = location.ModuleId,
                RiskLimits = location.RiskLimits.Select(r => r.ToDto()).ToList()
            };
        }

        public static List<LocationDto> ToDto(this List<Location> locations)
        {
            return locations.Select(l => l.ToDto()).ToList();
        }                      
    }
}
