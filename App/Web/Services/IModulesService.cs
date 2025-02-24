using Shared.Models;
using Shared.Models.DTOs;
using Shared.Responses;

namespace Web.Services
{
    public interface IModulesService
    {
        Task<Response<List<ModuleEspDto>>> LoadModulesAsync();
        Task<Response<ModuleEspDto>> LoadModuleDetailAsync(int id);
        Task<Response<int>> CreateModuleAsync(CreateModuleRequest? request);
        Task<Response<int>> UpdateModule(UpdateModuleRequest? request);
        Task<Response<int>> UpdateSensorOnModule(UpdateSensorOnModuleRequest? request);
        Task<Response<int>> AddSensorAsync(AddSensorOnModuleRequest? request);
        Task<Response<bool>> DeleteModuleAsync(int request);
        Task<Response<bool>> DeleteSensorAsync(int request);
    }
}
