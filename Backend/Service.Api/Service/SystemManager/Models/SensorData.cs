﻿using Service.Api.Core.Entity;

namespace Service.Api.Service.SystemManager.Models
{
    public class SensorData : EntityBase
    {
        public double Value { get; set; }

        // Relacionamento com Sensor
        public Guid SensorId { get; set; }
        public Sensor Sensor { get; set; } = null!;
    }
}
