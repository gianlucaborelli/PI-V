﻿using Microsoft.EntityFrameworkCore;
using Service.Api.Database;
using Service.Api.Service.SystemManager.Helpers;
using Service.Api.Service.SystemManager.Models;
using Service.Api.Service.SystemManager.Models.DTO;

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
            var date = queryDate == null ? DateTime.UtcNow.Date : queryDate.Value.ToUniversalTime().Date;
            var nextDate = date.AddDays(1);

            var humiditySensor = await _context.Sensors
                .Where(s => s.SensorType == SensorType.Humidity && s.ModuleId == moduleId)
                .Include(s => s.SensorDatas
                    .Where(d => d.CreatedAt >= date && d.CreatedAt < nextDate)
                    .OrderBy(d => d.CreatedAt))
                .FirstOrDefaultAsync();

            var dryBulbTemperatureSensor = await _context.Sensors
                .Where(s => s.SensorType == SensorType.DryBulbTemperature && s.ModuleId == moduleId)
                .Include(s => s.SensorDatas
                    .Where(d => d.CreatedAt >= date && d.CreatedAt < nextDate)
                    .OrderBy(d => d.CreatedAt))
                .FirstOrDefaultAsync();

            var darkBulbTemperatureSensor = await _context.Sensors
                .Where(s => s.SensorType == SensorType.DarkBulbTemperature && s.ModuleId == moduleId)
                .Include(s => s.SensorDatas
                    .Where(d => d.CreatedAt >= date && d.CreatedAt < nextDate)
                    .OrderBy(d => d.CreatedAt))
                .FirstOrDefaultAsync();

            var maxTemperature = dryBulbTemperatureSensor.SensorDatas.MaxBy(s => s.Value);

            var humidityAverage = humiditySensor.SensorDatas.Where(s => s.CreatedAt >= maxTemperature.CreatedAt && s.CreatedAt <= maxTemperature.CreatedAt.AddHours(1)).ToList().Average(s => s.Value);
            var dryBulbAverage = dryBulbTemperatureSensor.SensorDatas.Where(s => s.CreatedAt >= maxTemperature.CreatedAt && s.CreatedAt <= maxTemperature.CreatedAt.AddHours(1)).ToList().Average(s => s.Value);
            var darkBulbAverage = darkBulbTemperatureSensor.SensorDatas.Where(s => s.CreatedAt >= maxTemperature.CreatedAt && s.CreatedAt <= maxTemperature.CreatedAt.AddHours(1)).ToList().Average(s => s.Value);
                        
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

            var dashboardResponse = new DashboardResponse();

            dashboardResponse.IbtgEstimation = ibtgEstimation;
            dashboardResponse.Series.Add(humiditySensor.ToDashboardDto());
            dashboardResponse.Series.Add(dryBulbTemperatureSensor.ToDashboardDto());
            dashboardResponse.Series.Add(darkBulbTemperatureSensor.ToDashboardDto());

            return dashboardResponse;
        }
    }
}
