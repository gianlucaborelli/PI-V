using Service.Api.Service.SystemManager.Models.DTO;

namespace Service.Api.Service.SystemManager.Models.Mapper
{
    public static class ModuleMapper
    {
        public static Module MapToModule(this NewModuleRequest request)
        {
            return new Module(request.Name, 
                              request.CompanyId, 
                              request.Description, 
                              request.EspId);
            
        }
        
        public static ModuleDto ToModuleDto(this Module module)
        {
            return new ModuleDto
            {
                Id = module.Id,
                Name = module.Name,
                Description = module.Description,
                EspId = module.EspId,
                CompanyId = module.CompanyId,
                Locations = module.Locations.Select(l => l.ToLocationDto()).ToList(),
                AccessToken = module.AccessToken.ToAccessTokenDto()
            };
        }

        public static List<ModuleDto> ToModuleDto(this List<Module> module)
        {
            return module.Select(x => x.ToModuleDto()).ToList();
        }

        public static ModuleAccessTokenDto ToAccessTokenDto (this ModuleAccessToken accessToken)
        {
            return new ModuleAccessTokenDto(
                accessToken.Token,
                accessToken.IsActive,
                accessToken.ExpiresAt
            );
        }
    }
}
