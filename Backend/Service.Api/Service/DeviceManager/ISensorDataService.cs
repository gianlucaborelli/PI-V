namespace Service.Api.Service.DeviceManager
{
    public interface ISensorDataService
    {
        public Task<bool> AddSensorDataAsync(Guid sensorId, double value);
    }
}
