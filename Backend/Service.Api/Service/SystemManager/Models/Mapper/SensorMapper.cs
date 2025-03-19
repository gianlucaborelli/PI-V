using Service.Api.Service.SystemManager.Models.DTO;

namespace Service.Api.Service.SystemManager.Models.Mapper
{
    public static class SensorMapper
    {
        public static Sensor MapToSensor(this NewSensorRequest request)
        {
            return new Sensor
            {
                Name = request.Name,
                Description = request.Description,
                SensorType =  SensorType.FromName(request.Type),
            };
        }
        public static SensorDto ToSensorDto(this Sensor sensor)
        {
            return new SensorDto
            {
                Id = sensor.Id,
                Description = sensor.Description,
                ModuleId = sensor.ModuleId,
                Type = sensor.SensorType.Name
            };
        }
    }
}
