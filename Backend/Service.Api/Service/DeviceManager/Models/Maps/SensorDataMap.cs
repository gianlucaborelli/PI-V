using Service.Api.Service.SystemManager.Models;

namespace Service.Api.Service.DeviceManager.Models.Maps
{
    public static class SensorDataMap
    {
        public static SensorData ToEntity(this SensorDataRequest request)
        {
            return new SensorData
            {
                LocationId = request.LocalizationId,
                Humidity = request.Humidity,
                DryBulbTemperature = request.DryBulbTemperature,
                DarkBulbTemperature = request.DarkBulbTemperature
            };
        }
    }
}
