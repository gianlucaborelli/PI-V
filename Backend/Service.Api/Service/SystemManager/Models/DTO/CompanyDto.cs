namespace Service.Api.Service.SystemManager.Models.DTO
{
    public class CompanyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string> Tags { get; set; }
    }
}
