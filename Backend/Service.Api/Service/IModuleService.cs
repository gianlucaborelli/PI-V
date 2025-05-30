using Service.Api.Service.SystemManager.Models.DTO;

namespace Service.Api.Service
{
    public interface IModuleService
    {
        List<ModuleDto> GetAllModules(Guid companyId);
        ModuleDto GetModuleById(Guid companyId, Guid id);
        ModuleDto RegisterNewModule(NewModuleRequest request);
        ModuleDto UpdateModule(UpdateModuleRequest request);
        void DeleteModule(Guid companyId, Guid id);
    }
}
