namespace Service.Api.Service.SystemManager.Models.DTO
{
    public class SensorDataDto
    {        
        public string Name { get; set; }
        public List<SensorSeries> Series { get; set; }
    }

    public class SensorSeries
    {
        public double Value { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    //Mapping
    public static class SensorDataMapping
    {
        public static SensorDataDto ToDashboardDto(this Sensor sensor)
        {
            return new SensorDataDto
            {
                Name = sensor.SensorType.Name,
                Series = sensor.SensorDatas.Select(s => new SensorSeries
                {
                    Value = s.Value,
                    CreatedAt = s.CreatedAt
                }).ToList()
            };
        }
    }
}
