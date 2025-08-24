namespace Service.Api.Service.SystemManager.Models.DTO
{
    public class ModuleDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? EspId { get; set; }
        public Guid CompanyId { get; set; }

        public List<LocationDto> Locations { get; set; } = [];
        public ModuleAccessTokenDto AccessToken { get; set; } = new ModuleAccessTokenDto(string.Empty, false, DateTime.UtcNow);
    }
}
