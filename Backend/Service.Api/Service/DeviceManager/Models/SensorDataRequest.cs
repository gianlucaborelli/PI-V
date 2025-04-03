namespace Service.Api.Service.DeviceManager.Models
{
    public class SensorDataRequest
    {
        public Guid SensorId { get; set; }
        public double Value { get; set; }        
    }
}
