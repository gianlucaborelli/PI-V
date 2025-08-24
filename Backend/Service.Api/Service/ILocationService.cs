using Service.Api.Service.SystemManager.Models.DTO;

namespace Service.Api.Service
{
    public interface ILocationService
    {
        List<LocationDto> GetAllLocations(Guid companyId, Guid moduleId);
        LocationDto GetLocationById(Guid companyId, Guid moduleId, Guid sensorId);
        LocationDto RegisterNewLocation(NewLocationRequest request);
        LocationDto UpdateLocation(LocationDto request);
        void DeleteLocation(Guid sensorId);
    }
}
