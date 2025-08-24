namespace Service.Api.Service.SystemManager.Models.DTO
{
    public class UpdateCompanyRequest
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }
}
