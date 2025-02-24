using Shared.Responses;
using Shared.Models;

namespace Web.Services
{
    public interface ISensorsService
    {
        Task<Response<List<SensorData>>> LoadSensorsDataAsync(DateTime? request, int sensorID);
        Task<Response<List<SensorData>>> LoadSensorsDataByDayAndModuleIdAsync(DateTime? request, int moduleId);
    }
}
