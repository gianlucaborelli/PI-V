using Service.Api.Database;
using Service.Api.Service.SystemManager.Models;

namespace Service.Api.Service.DeviceManager
{
    public class SensorDataService: ISensorDataService
    {
        private readonly ServiceDatabaseContext _context;
        public SensorDataService(ServiceDatabaseContext sensorDataRepository)
        {
            _context = sensorDataRepository;
        }
        public async Task<bool> AddSensorDataAsync(Guid sensorId, double value)
        {
            var sensorData = new SensorData
            {
                SensorId = sensorId,
                Value = value
            };

            _context.SensorDatas.Add(sensorData);
            return _context.SaveChanges() > 0 ;
        }
    }
}
