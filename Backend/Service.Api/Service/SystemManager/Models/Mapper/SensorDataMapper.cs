using Service.Api.Service.SystemManager.Models.DTO;

namespace Service.Api.Service.SystemManager.Models.Mapper
{
    public static class SensorDataMapper
    {
        public static SensorDataDto ToDto(this SensorData sensorData)
        {
            return new SensorDataDto
            {
                Id = sensorData.Id,
                CreatedAt = sensorData.CreatedAt,
                Humidity = sensorData.Humidity,
                DryBulbTemperature = sensorData.DryBulbTemperature,
                DarkBulbTemperature = sensorData.DarkBulbTemperature
            };
        }

        public static List<SensorDataDto> ToDtoList(this List<SensorData> sensorDataList)
        {
            return sensorDataList.Select(sd => sd.ToDto()).ToList();
        }

    }
}
