namespace Service.Api.Service.DeviceManager.Models
{
    public class SensorDataRequest
    {
        public Guid LocalizationId { get; set; }
        public double? Humidity { get; set; }
        public double? DryBulbTemperature { get; set; }
        public double? DarkBulbTemperature { get; set; }
    }
}
