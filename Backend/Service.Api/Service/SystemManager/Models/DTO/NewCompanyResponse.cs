namespace Service.Api.Service.SystemManager.Models.DTO
{
    public class NewCompanyResponse
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }        
    }
}
