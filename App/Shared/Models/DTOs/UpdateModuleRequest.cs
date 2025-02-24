namespace Shared.Models.DTOs;

public class UpdateModuleRequest
{
    public int ModuleId { get; set; }
    public string Description { get; set; } = string.Empty;
}
