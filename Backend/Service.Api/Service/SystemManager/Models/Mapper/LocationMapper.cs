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

        public static LocationDto ToLocationDto(this Location location)
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

        public static List<LocationDto> ToLocationDtoList(this List<Location> locations)
        {
            return locations.Select(l => l.ToLocationDto()).ToList();
        }

        // ---------------- RISK LIMIT ----------------

        //public static RiskLimitDto ToDto(this RiskLimit riskLimit)
        //{
        //    return new RiskLimitDto
        //    {
        //        Id = riskLimit.Id,
                
        //        Type = riskLimit.GetType().Name // "IBUTGRiskLimit" etc.
        //    };
        //}

        //public static RiskLimit ToEntity(this NewRiskLimitRequest request)
        //{
        //    return new RiskLimit
        //    {
        //        LocationId = request.LocationId,
        //        RiskId = request.RiskId                
        //    };
        //}

        // ---------------- SENSOR DATA ----------------

        public static SensorDataDto ToDto(this SensorData sensorData)
        {
            return new SensorDataDto
            {
                Id = sensorData.Id,
                Humidity = sensorData.Humidity,
                DryBulbTemperature = sensorData.DryBulbTemperature,
                DarkBulbTemperature = sensorData.DarkBulbTemperature
            };
        }        
    }
}
