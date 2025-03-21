using Service.Api.Service.SystemManager.Models.DTO;

namespace Service.Api.Service.SystemManager.Application;

public interface ISystemService
{
    // Companies
    List<CompanyDto> GetAllCompanies();
    CompanyDto GetCompanyById(Guid id);    
    NewCompanyResponse RegisterNewCompany (NewCompanyRequest request);
    UpdateCompanyResponse UpdateCompany(UpdateCompanyRequest request);
    void DeleteCompany(Guid id);

    // Modules
    List<ModuleDto> GetAllModules(Guid companyId);
    ModuleDto GetModuleById(Guid companyId, Guid id);
    ModuleDto RegisterNewModule(NewModuleRequest request);
    ModuleDto UpdateModule(UpdateModuleRequest request);
    void DeleteModule(Guid companyId, Guid id);

    // Sensors
    List<SensorDto> GetAllSensors(Guid companyId, Guid moduleId);
    SensorDto GetSensorById(Guid companyId, Guid moduleId, Guid SensorId);
    SensorDto RegisterNewSensor(NewSensorRequest request);
    SensorDto UpdateSensor(SensorDto request);
    void DeleteSensor(Guid sensorId);
}
