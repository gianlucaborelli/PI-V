using Microsoft.EntityFrameworkCore;
using Service.Api.Database;
using Service.Api.Service.DeviceManager.Models;
using Service.Api.Service.DeviceManager.Models.Maps;
using Service.Api.Service.SystemManager.Models.DTO;
using Service.Api.Service.SystemManager.Models.Mapper;
using System.Data;

namespace Service.Api.Service.DeviceManager
{
    public class DeviceService: IDeviceService
    {
        private readonly ServiceDatabaseContext _context;
        public DeviceService(ServiceDatabaseContext sensorDataRepository)
        {
            _context = sensorDataRepository;
        }
        public async Task<bool> AddSensorDataAsync(SensorDataRequest request)
        {
            var sensorData = request.ToEntity();

            _context.SensorDatas.Add(sensorData);
            return await _context.SaveChangesAsync() > 0 ;
        }

        public async Task<List<LocationDto>> ValidateModuleSincronizationAsync(string moduleToken)
        {
            var module = await _context.Modules
                .Include(m => m.AccessToken)
                .Include(m => m.Locations)
                .FirstOrDefaultAsync(m => m.AccessToken.Token == moduleToken);

            if ( module.AccessToken.IsNotValid(moduleToken))
            {
                throw new UnauthorizedAccessException("Invalid module token.");
            }

            module.AccessToken.Revoke();

            await _context.SaveChangesAsync();

            return module.Locations.ToDto();
        }
    }
}
