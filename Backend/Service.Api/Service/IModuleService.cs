using Service.Api.Service.SystemManager.Models;
using Service.Api.Service.SystemManager.Models.DTO;

namespace Service.Api.Service
{
    public interface IModuleService
    {
        List<ModuleDto> GetAllModules(Guid companyId);
        ModuleDto GetModuleById(Guid companyId, Guid id);
        ModuleDto RegisterNewModule(NewModuleRequest request);
        ModuleDto UpdateModule(UpdateModuleRequest request);

        // Generate Access Token for Module
        //string GenerateAccessToken(Guid companyId, Guid id);
        //GET Module Access Token
        bool GetNewModuleAccessToken(Guid companyId, Guid id);
        //Validate Module Access Token
        Task<ModuleDto> ValidateModuleAccessToken(Guid companyId, Guid id, string moduleAccessToken);
        // Revoke Module Access Token
        void RevokeModuleAccessToken(Guid companyId, Guid id);

        void DeleteModule(Guid companyId, Guid id);
    }
}
