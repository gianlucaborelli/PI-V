using Service.Api.Core.Entity;

namespace Service.Api.Service.SystemManager.Models
{
    public class SensorData : EntityBase
    {
        public double Humidity { get; set; } 
        public double DryBulbTemperature { get; set; }        
        public double DarkBulbTemperature { get; set; }

        public Guid LocationId { get; set; }
        public Location Location { get; set; } = null!;
    }
}
