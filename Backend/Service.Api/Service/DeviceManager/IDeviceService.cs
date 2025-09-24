using Service.Api.Service.DeviceManager.Models;
using Service.Api.Service.SystemManager.Models.DTO;

namespace Service.Api.Service.DeviceManager
{
    public interface IDeviceService
    {
        Task<bool> AddSensorDataAsync(SensorDataRequest request);

        Task<ModuleDto> ValidateModuleSincronizationAsync(string moduleToken);
    }
}
