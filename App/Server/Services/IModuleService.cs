using Shared.Models;
using Shared.Models.DTOs;
using Shared.Responses;

namespace Server.Services;
public interface IModuleService
{
    Task<Response<int>> Create(CreateModuleRequest request);
    Task<Response<int>> Update(UpdateModuleRequest request);
    Task<Response<bool>> Delete(int id);
    Task<Response<int>> AddSensor(AddSensorOnModuleRequest request);
    Task<Response<int>> UpdateSensor(UpdateSensorOnModuleRequest request);
    Task<Response<bool>> DeleteSensor(int id);

    Task<Response<ModuleEspDto?>> GetModule(int id);
    Task<Response<List<ModuleEspDto>>> GetModules();
}
