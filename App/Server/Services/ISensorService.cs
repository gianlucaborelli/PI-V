using Shared.Models;
using Shared.Models.DTOs;
using Shared.Requests;
using Shared.Responses;

namespace Server.Services
{
    public interface ISensorService
    {
        Task<Response<List<SensorData>>> GetSensorDataByDay(SensorDataByDayRequest request);
        Task<Response<Guid>> SetSensorDataAsync(SensorDataDto request);
        Task<Response<List<SensorData>>> GetSensorDataByModuleIdAndDate(SensorDataByDayAndModuleId request);
    }
}
