namespace Service.Api.Service.SystemManager.Models.DTO
{
    public class SensorDataDto
    {
        public Guid Id { get; set; }
        public double? Humidity { get; set; }
        public double? DryBulbTemperature { get; set; }
        public double? DarkBulbTemperature { get; set; }
    }    
}
