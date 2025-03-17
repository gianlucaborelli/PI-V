using Service.Api.Service.SystemManager.Models.DTO;

namespace Service.Api.Service.SystemManager.Models.Mapper
{
    public static class CompanyMapper
    {
        public static Company MapToCompany(this NewCompanyRequest request)
        {
            return new Company
            {
                Name = request.Name,
                Tags = request.Tags
            };
        }

        public static Company MapToCompany(this UpdateCompanyRequest request)
        {
            return new Company
            {
                Id = request.Id,
                Name = request.Name,
                Tags = request.Tags
            };
        }

        public static NewCompanyResponse ToNewCompanyResponseDto(this Company company)
        {
            return new NewCompanyResponse
            {
                Id = company.Id,
                Name = company.Name,
                Tags = company.Tags
            };
        }

        public static UpdateCompanyResponse ToCompanyUpdateResponseDto(this Company company)
        {
            return new UpdateCompanyResponse
            {
                Id = company.Id,
                Name = company.Name,
                Tags = company.Tags
            };
        }

        public static CompanyDto ToCompanyDto(this Company company)
        {
            return new CompanyDto
            {
                Id = company.Id,
                Name = company.Name,
                Tags = company.Tags
            };
        }

        public static List<CompanyDto> ToCompanyDto(this List<Company> company)
        {
            return company.Select(x => x.ToCompanyDto()).ToList();
        }
    }
}
