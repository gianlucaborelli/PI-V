using Service.Api.Service.SystemManager.Models.DTO;

namespace Service.Api.Service
{
    public interface ISensorService
    {
        List<SensorDto> GetAllSensors(Guid companyId, Guid moduleId);
        SensorDto GetSensorById(Guid companyId, Guid moduleId, Guid sensorId);
        SensorDto RegisterNewSensor(NewSensorRequest request);
        SensorDto UpdateSensor(SensorDto request);
        void DeleteSensor(Guid sensorId);
    }
}
