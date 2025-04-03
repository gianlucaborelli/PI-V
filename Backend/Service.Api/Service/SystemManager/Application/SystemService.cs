using Microsoft.EntityFrameworkCore;
using Service.Api.Database;
using Service.Api.Service.Authentication;
using Service.Api.Service.Authentication.Models;
using Service.Api.Service.SystemManager.Models;
using Service.Api.Service.SystemManager.Models.DTO;
using Service.Api.Service.SystemManager.Models.Mapper;

namespace Service.Api.Service.SystemManager.Application;

public class SystemService : ISystemService
{
    private readonly ServiceDatabaseContext _context;
    private readonly ILoggedInUser _loggedInUser;

    public SystemService(ServiceDatabaseContext context, ILoggedInUser loggedInUser )
    {
        _context = context;        
        _loggedInUser = loggedInUser;
    }

    public List<CompanyDto> GetAllCompanies()
    {
        var userId = _loggedInUser.GetUserId();

        var companies = _context.Companies
            .Where(c => c.UserCompanies.Any(uc => uc.UserId == userId)) 
            .Include(c => c.UserCompanies)
            .ToList();

        return companies.ToCompanyDto();
    }

    public CompanyDto GetCompanyById(Guid id)
    {
        var userId = _loggedInUser.GetUserId();

        var company = _context.Companies
            .Where(c => c.Id == id && c.UserCompanies.Any(uc => uc.UserId == userId)) 
            .Include(c => c.UserCompanies) 
            .FirstOrDefault();

        if (company == null)
        {
            throw new Exception("Company not found");
        }
        return company.ToCompanyDto();
    }

    public NewCompanyResponse RegisterNewCompany(NewCompanyRequest request)
    {
        var newConpany = request.MapToCompany();

        _context.Companies.Add(newConpany);

        _context.SaveChanges();        

        var newUserCompany = new UserCompany
        {
            CompanyId = newConpany.Id,
            UserId = _loggedInUser.GetUserId()
        };

        _context.UserCompanies.Add(newUserCompany);
        _context.SaveChanges();

        return newConpany.ToNewCompanyResponseDto();
    }

    public UpdateCompanyResponse UpdateCompany(UpdateCompanyRequest request)
    {
        var company = _context.Companies.FirstOrDefault(x => x.Id == request.Id);
        if (company == null)
        {
            throw new Exception("Company not found");
        }
        company.Name = request.Name;
        company.Tags = request.Tags;
        
        _context.Companies.Update(company);
        _context.SaveChanges();
        return company.ToCompanyUpdateResponseDto();
    }

    public void DeleteCompany(Guid id)
    {
        var userId = _loggedInUser.GetUserId();

        var company = _context.Companies
            .Where(c => c.Id == id && c.UserCompanies.Any(uc => uc.UserId == userId))
            .Include(c => c.UserCompanies)
            .FirstOrDefault();

        if (company == null)
        {
            throw new Exception("Company not found");
        }

        _context.Companies.Remove(company);
        _context.SaveChanges();
    }

    public List<ModuleDto> GetAllModules(Guid companyId)
    {
        var userId = _loggedInUser.GetUserId();
        var modules = _context.Modules
            .Where(m => m.CompanyId == companyId && m.Company.UserCompanies.Any(uc => uc.UserId == userId))
            .Include(m => m.Sensors)
            .ToList();
        return modules.ToModuleDto();
    }

    public ModuleDto GetModuleById(Guid companyId, Guid id)
    {
        var userId = _loggedInUser.GetUserId();
        var module = _context.Modules
            .Where(m => m.Id == id && m.CompanyId == companyId && m.Company.UserCompanies.Any(uc => uc.UserId == userId))
            .Include(m => m.Sensors)
            .FirstOrDefault();
        if (module == null)
        {
                throw new Exception("Module not found");
        }
        return module.ToModuleDto();
    }

    public ModuleDto RegisterNewModule(NewModuleRequest request)
    {
        var userId = _loggedInUser.GetUserId();
        var company = _context.Companies
            .Where(c => c.Id == request.CompanyId && c.UserCompanies.Any(uc => uc.UserId == userId))
            .FirstOrDefault();
        if (company == null)
        {
            throw new Exception("Company not found");
        }
        var newModule = request.MapToModule();
        _context.Modules.Add(newModule);
        _context.SaveChanges();
        return newModule.ToModuleDto();
    }

    public ModuleDto UpdateModule(UpdateModuleRequest request)
    {
        var userId = _loggedInUser.GetUserId();
        var module = _context.Modules
            .Where(m => m.Id == request.Id && m.CompanyId == request.CompanyId && m.Company.UserCompanies.Any(uc => uc.UserId == userId))
            .FirstOrDefault();
        if (module == null)
        {
            throw new Exception("Module not found");
        }
        module.EspId = request.EspId;
        module.Tag = request.Tag;
        _context.Modules.Update(module);
        _context.SaveChanges();
        return module.ToModuleDto();
    }

    public void DeleteModule(Guid companyId, Guid id)
    {
        var userId = _loggedInUser.GetUserId();
        var module = _context.Modules
            .Where(m => m.Id == id && m.CompanyId == companyId && m.Company.UserCompanies.Any(uc => uc.UserId == userId))
            .FirstOrDefault();
        if (module == null)
        {
            throw new Exception("Module not found");
        }
        _context.Modules.Remove(module);
        _context.SaveChanges();
    }

    public List<SensorDto> GetAllSensors(Guid companyId, Guid moduleId)
    {
        var userId = _loggedInUser.GetUserId();
        var sensors = _context.Sensors
            .Where(s => s.ModuleId == moduleId && s.Module.CompanyId == companyId && s.Module.Company.UserCompanies.Any(uc => uc.UserId == userId))
            .ToList();
        return sensors.ToSensorDto();
    }

    public SensorDto GetSensorById(Guid companyId, Guid moduleId, Guid sensorId)
    {
        var userId = _loggedInUser.GetUserId();
        var sensor = _context.Sensors
            .Where(s => s.Id == sensorId && s.ModuleId == moduleId && s.Module.CompanyId == companyId && s.Module.Company.UserCompanies.Any(uc => uc.UserId == userId))
            .FirstOrDefault();
        if (sensor == null)
        {
            throw new Exception("Sensor not found");
        }
        return sensor.ToSensorDto();
    }

    public SensorDto RegisterNewSensor(NewSensorRequest request)
    {
        var userId = _loggedInUser.GetUserId();
        var module = _context.Modules
            .Where(m => m.Id == request.ModuleId && m.Company.UserCompanies.Any(uc => uc.UserId == userId))
            .FirstOrDefault();
        if (module == null)
        {
            throw new Exception("Module not found");
        }
        var newSensor = request.MapToSensor();
        _context.Sensors.Add(newSensor);
        _context.SaveChanges();
        return newSensor.ToSensorDto();
    }

    public SensorDto UpdateSensor(SensorDto request)
    {
        var userId = _loggedInUser.GetUserId();
        var sensor = _context.Sensors
            .Where(s => s.Id == request.Id && s.Module.Company.UserCompanies.Any(uc => uc.UserId == userId))
            .FirstOrDefault();
        if (sensor == null)
        {
            throw new Exception("Sensor not found");
        }
        sensor.Name = request.Name;
        sensor.Description = request.Description;
        sensor.SensorType = SensorType.FromName(request.Type);
        _context.Sensors.Update(sensor);
        _context.SaveChanges();
        return sensor.ToSensorDto();
    }

    public void DeleteSensor(Guid sensorId)
    {
        var userId = _loggedInUser.GetUserId();
        var sensor = _context.Sensors
            .Where(s => s.Id == sensorId && s.Module.Company.UserCompanies.Any(uc => uc.UserId == userId))
            .FirstOrDefault();
        if (sensor == null)
        {
            throw new Exception("Sensor not found");
        }
        _context.Sensors.Remove(sensor);
        _context.SaveChanges();
    }
}
