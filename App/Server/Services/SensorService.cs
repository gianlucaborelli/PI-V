using Microsoft.EntityFrameworkCore;
using Server.Data;
using Shared.Models;
using Shared.Models.DTOs;
using Shared.Requests;
using Shared.Responses;

namespace Server.Services
{
    public class SensorService(AppDbContext context) : ISensorService
    {
        public async Task<Response<List<SensorData>>> GetSensorDataByDay(SensorDataByDayRequest request)
        {
            try
            {
                var sensorData = await context
                    .SensorDatas
                    .AsNoTracking()
                    .Where(x => x.CreatedAt.Day == request.Date.Day && x.Sensor.Id == request.SensorId)
                    .ToListAsync();

                return sensorData is null
                    ? new Response<List<SensorData>>(null, 404, "Informações não encontrada")
                    : new Response<List<SensorData>>(sensorData);
            }
            catch(Exception e)
            {
                return new Response<List<SensorData>>(null, 500, e.Message);
            }
        }

        public async Task<Response<List<SensorData>>> GetSensorDataByModuleIdAndDate(SensorDataByDayAndModuleId request)
        {
            try
            {
                var sensorData = await context
                    .SensorDatas
                    .AsNoTracking()
                    .Where(x => x.CreatedAt.Day == request.Date.Day && x.Sensor.Module.Id == request.ModuleId)
                    .ToListAsync();

                return sensorData is null
                    ? new Response<List<SensorData>>(null, 404, "Informações não encontrada")
                    : new Response<List<SensorData>>(sensorData);
            }
            catch (Exception e)
            {
                return new Response<List<SensorData>>(null, 500, e.Message);
            }
        }

        public async Task<Response<Guid>> SetSensorDataAsync(SensorDataDto request)
        {
            try
            {
                var sensor = await context
                    .Sensors.FirstOrDefaultAsync(x => x.Id == request.SensorId);

                var sensorData = new SensorData()
                {
                    Id = Guid.NewGuid(),
                    Sensor = sensor,
                    Humidity = request.Humidity,
                    Temperature = request.Temperature,
                    CreatedAt = DateTime.UtcNow
                };

                context.SensorDatas.Add(sensorData);
                var result = context.SaveChanges();                    

                return result <= 0
                    ? new Response<Guid>(sensorData.Id, 404, "Informações não encontrada")
                    : new Response<Guid>(sensorData.Id);
            }
            catch (Exception e)
            {
                return new Response<Guid>(Guid.Empty, 500, e.Message);
            }
        }
    }
}
