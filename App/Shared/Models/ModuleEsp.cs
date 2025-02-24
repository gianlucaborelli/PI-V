namespace Shared.Models
{
    public class ModuleEsp
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<Sensor> Sensors { get; set; } = new List<Sensor>();

        public ModuleEsp( int id, string description)
        {
            Id = id;
            Description = description;            
        }

        public ModuleEsp(string description)
        {
            var random = new Random();
            Id = random.Next(100000, 999999);
            Description = description;            
        }

        //Constructor for EF-Core
        public ModuleEsp() 
        {
           
        }
    }
}
