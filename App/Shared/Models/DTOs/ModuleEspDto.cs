namespace Shared.Models.DTOs
{
    public class ModuleEspDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<SensorDto> Sensors { get; set; }
    }
}
