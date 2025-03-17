namespace Service.Api.Service.SystemManager.Models.DTO
{
    public class UpdateCompanyResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string> Tags { get; set; }
    }
}
