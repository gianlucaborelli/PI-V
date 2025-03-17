namespace Service.Api.Service.SystemManager.Models;

public class SensorType
{
    public static SensorType Humidity = new(nameof(Humidity));
    public static SensorType DryBulbTemperature = new(nameof(DryBulbTemperature));
    public static SensorType DarkBulbTemperature = new(nameof(DarkBulbTemperature));
    public static SensorType BarometricPressure = new(nameof(BarometricPressure));

    public static IEnumerable<SensorType> GetAll() => new[]
    {
        Humidity, DryBulbTemperature, DarkBulbTemperature, BarometricPressure
    };

    public string Name { get; }

    private SensorType(string name) => Name = name;

    public static SensorType FromName(string name) =>
        GetAll().FirstOrDefault(t => t.Name == name) ?? throw new ArgumentException("Invalid SensorType");

}
