using System.Text.Json.Serialization;

namespace Shared.Models
{
    public class Sensor
    {
        public int Id { get; set; }        
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int ModuleId { get; set; }
        public ModuleEsp Module { get; set; } = null!;
        public List<SensorData> Datas { get; set; } = new List<SensorData>();

        public Sensor(string description, ModuleEsp moduleEsp)
        {
            var random = new Random();
            Id = random.Next(100000, 999999);
            Description = description;
            ModuleId = moduleEsp.Id;
            Module = moduleEsp;
        }

        protected Sensor() { }
    }
}
