namespace Shared.Models;
public class SensorData
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public double Humidity { get; set; }
    public double Temperature { get; set; }
    public DateTime CreatedAt { get; set; }
    public Sensor Sensor { get; set; }
}
