namespace Shared.Models.DTOs;

public class AddSensorOnModuleRequest
{
    public int ModuleId { get; set; }
    public string Description { get; set; } = string.Empty;        
}
