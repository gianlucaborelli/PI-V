using Service.Api.Database;
using Service.Api.Service.Authentication;
using Service.Api.Service.SystemManager.Models;
using Service.Api.Service.SystemManager.Models.DTO;
using Service.Api.Service.SystemManager.Models.Mapper;

namespace Service.Api.Service
{
    public class SensorService : ISensorService
    {
        private readonly ServiceDatabaseContext _context;
        private readonly ILoggedInUser _loggedInUser;

        public SensorService(ServiceDatabaseContext context, ILoggedInUser loggedInUser)
        {
            _context = context;
            _loggedInUser = loggedInUser;
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
                throw new Exception("Sensor not found");

            return sensor.ToSensorDto();
        }

        public SensorDto RegisterNewSensor(NewSensorRequest request)
        {
            var userId = _loggedInUser.GetUserId();

            var module = _context.Modules
                .Where(m => m.Id == request.ModuleId && m.Company.UserCompanies.Any(uc => uc.UserId == userId))
                .FirstOrDefault();

            if (module == null)
                throw new Exception("Module not found");

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
                throw new Exception("Sensor not found");

            sensor.Name = request.Name;
            sensor.Description = request.Description;
            sensor.SensorType = SensorType.FromName(request.Type);

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
                throw new Exception("Sensor not found");

            _context.Sensors.Remove(sensor);
            _context.SaveChanges();
        }
    }

}
