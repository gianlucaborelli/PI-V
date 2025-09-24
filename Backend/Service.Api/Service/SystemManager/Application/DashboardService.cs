using Microsoft.EntityFrameworkCore;
using Service.Api.Database;
using Service.Api.Service.SystemManager.Helpers;
using Service.Api.Service.SystemManager.Models;
using Service.Api.Service.SystemManager.Models.DTO;
using Service.Api.Service.SystemManager.Models.Mapper;

namespace Service.Api.Service.SystemManager.Application
{
    public class DashboardService : IDashboardService
    {
        public ServiceDatabaseContext _context;

        public DashboardService(ServiceDatabaseContext context)
        {
            _context = context;
        }

        public async Task<DashboardResponse> GetDashboardDataAsync(Guid moduleId, DateTime? queryDate)
        {
            var module = await _context.Modules
                .Where(m => m.Id == moduleId)
                .Include(m => m.Locations)                    
                .FirstOrDefaultAsync();

            if (module == null)
                throw new Exception("Module not found");

            var tz = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            var localDate = DateTime.SpecifyKind((queryDate ?? DateTime.Now).Date, DateTimeKind.Unspecified);

            var date = TimeZoneInfo.ConvertTimeToUtc(localDate, tz);
            var nextDate = date.AddDays(1);

            foreach (var location in module.Locations)
            {
                location.SensorDatas = _context.SensorDatas
                    .Where(d => d.CreatedAt >= date && d.CreatedAt < nextDate && d.LocationId == location.Id)
                    .OrderBy(d => d.CreatedAt)
                    .ToList();
            }

            var dashboardResponse = new DashboardResponse();
            foreach (var location in module.Locations)
            {
                var locationDataSummary = new LocationDataSummary();
                var maxTemperature = location.SensorDatas.MaxBy(s => s.DryBulbTemperature);

                var cutoffBefore = maxTemperature!.CreatedAt.AddMinutes(-30);
                var cutoffAfter = maxTemperature!.CreatedAt.AddMinutes(30);

                var temperature30MinutesBefore = location.SensorDatas
                    .OrderBy(s => Math.Abs((s.CreatedAt - cutoffBefore).Ticks))
                    .FirstOrDefault();

                var temperature30MinutesAfter = location.SensorDatas
                    .OrderBy(s => Math.Abs((s.CreatedAt - cutoffAfter).Ticks))
                    .FirstOrDefault();

                // Qual é mais proximo da temperatura maxima
                double diffBefore = Math.Abs((temperature30MinutesBefore!.DryBulbTemperature ?? 0) - (maxTemperature!.DryBulbTemperature ?? 0));
                double diffAfter = Math.Abs((temperature30MinutesAfter!.DryBulbTemperature ?? 0) - (maxTemperature!.DryBulbTemperature ?? 0));

                var closestTemperature = diffBefore <= diffAfter
                    ? temperature30MinutesBefore
                    : temperature30MinutesAfter;

                var amostragem = new List<SensorData>();
                if (closestTemperature!.CreatedAt > maxTemperature.CreatedAt)
                {
                   amostragem = location.SensorDatas
                        .Where(s => s.CreatedAt >= maxTemperature.CreatedAt && s.CreatedAt <= maxTemperature.CreatedAt.AddHours(1))
                        .ToList();
                }
                else
                {
                    amostragem = location.SensorDatas
                        .Where(s => s.CreatedAt >= closestTemperature.CreatedAt.AddMinutes(-30) && s.CreatedAt <= closestTemperature.CreatedAt.AddMinutes(30))
                        .ToList();
                }

                var humidityAverage = amostragem.Average(s => s.Humidity ?? 0);
                var dryBulbAverage = amostragem.Average(s => s.DryBulbTemperature ?? 0);
                var darkBulbAverage = amostragem.Average(s => s.DarkBulbTemperature ?? 0);

                var wetBulbTemperature = Calcules.WetBulbTemperatureEstimation(
                    dryBulbAverage,
                    humidityAverage
                );

                var ibtgEstimation = Calcules.IUBTGEstimation(
                    wetBulbTemperature,
                    darkBulbAverage,
                    dryBulbAverage
                );

                var ibtgEstimationWithoutSunRadiator = Calcules.IUBTGEstimation(
                    wetBulbTemperature,
                    dryBulbAverage
                );

                locationDataSummary.Name = location.Name;
                locationDataSummary.IbutgEstimation = ibtgEstimation;
                locationDataSummary.HumidityAverage = Math.Round(humidityAverage, 2);
                locationDataSummary.TemperatureAverage = Math.Round(dryBulbAverage, 2);
                locationDataSummary.MaxTemperature = Math.Round(location.SensorDatas.Max(s => s.DryBulbTemperature ?? 0), 2);
                locationDataSummary.MinTemperature = Math.Round(location.SensorDatas.Min(s => s.DryBulbTemperature ?? 0), 2);
                locationDataSummary.Description = location.Description;
                locationDataSummary.Series = location.SensorDatas.ToDtoList();



                dashboardResponse.LocationsSummary.Add(locationDataSummary);
            }
            

            //var humidityAverage = humiditySensor.SensorDatas.Where(s => s.CreatedAt >= maxTemperature.CreatedAt && s.CreatedAt <= maxTemperature.CreatedAt.AddHours(1)).ToList().Average(s => s.Value);
            //var dryBulbAverage = dryBulbTemperatureSensor.SensorDatas.Where(s => s.CreatedAt >= maxTemperature.CreatedAt && s.CreatedAt <= maxTemperature.CreatedAt.AddHours(1)).ToList().Average(s => s.Value);
            //var darkBulbAverage = darkBulbTemperatureSensor.SensorDatas.Where(s => s.CreatedAt >= maxTemperature.CreatedAt && s.CreatedAt <= maxTemperature.CreatedAt.AddHours(1)).ToList().Average(s => s.Value);

            //var wetBulbTemperature = Calcules.WetBulbTemperatureEstimation(
            //    dryBulbAverage,
            //    humidityAverage
            //);

            //var ibtgEstimation = Calcules.IUBTGEstimation(
            //    wetBulbTemperature,
            //    darkBulbAverage,
            //    dryBulbAverage
            //);

            //var ibtgEstimationWithoutSunRadiator = Calcules.IUBTGEstimation(
            //    wetBulbTemperature,                
            //    dryBulbAverage
            //);

            //var dashboardResponse = new DashboardResponse();

            //dashboardResponse.IbtgEstimation = ibtgEstimation;
            //dashboardResponse.HumidityAverage = humidityAverage;
            //dashboardResponse.TemperatureAverage = dryBulbAverage;
            //dashboardResponse.MaxTemperature = maxTemperature.Value;

            //dashboardResponse.Series.Add(
            //    Calcules.NormalizeSensorData("Humidity", humiditySensor.SensorDatas, TimeSpan.FromMinutes(10))
            //);

            //dashboardResponse.Series.Add(
            //    Calcules.NormalizeSensorData("DryBulbTemperature", dryBulbTemperatureSensor.SensorDatas, TimeSpan.FromMinutes(10))
            //);

            //dashboardResponse.Series.Add(
            //    Calcules.NormalizeSensorData("DarkBulbTemperature", darkBulbTemperatureSensor.SensorDatas, TimeSpan.FromMinutes(10))
            //);

           
            return dashboardResponse;
        }
    }
}
