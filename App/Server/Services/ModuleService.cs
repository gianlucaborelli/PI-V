using Microsoft.EntityFrameworkCore;
using Server.Data;
using Shared.Models;
using Shared.Models.DTOs;
using Shared.Responses;

namespace Server.Services
{
    public class ModuleService : IModuleService
    {
        private readonly AppDbContext _context;

        public ModuleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Response<ModuleEspDto?>> GetModule(int id)
        {
            var result = await _context.Modules
                 .Include(m => m.Sensors)
                 .FirstOrDefaultAsync(m => m.Id == id);

            if (result is null)
            {
                return new Response<ModuleEspDto?>(null, 404, "Informações não encontradas");
            }

            var dto = new ModuleEspDto
            {
                Id = result.Id,
                Description = result.Description,
                Sensors = result.Sensors.Select(s => new SensorDto
                {
                    Id = s.Id,
                    Description = s.Description
                }).ToList()
            };

            return new Response<ModuleEspDto?>(dto);
        }
        public async Task<Response<List<ModuleEspDto>>> GetModules()
        {
            var result = await _context.Modules
        .Include(m => m.Sensors) // Inclui os sensores relacionados
        .ToListAsync();

            // Verifica se o resultado é nulo
            if (result == null || !result.Any())
            {
                return new Response<List<ModuleEspDto>>(null, 404, "Informações não encontradas");
            }

            // Mapeia os módulos para a lista de DTOs
            var moduleDtos = result.Select(m => new ModuleEspDto
            {
                Id = m.Id,
                Description = m.Description,
                Sensors = m.Sensors.Select(s => new SensorDto
                {
                    Id = s.Id,
                    Description = s.Description
                }).ToList()
            }).ToList();

            return new Response<List<ModuleEspDto>>(moduleDtos);
        }

        public async Task<Response<int>> Create(CreateModuleRequest request)
        {
            var newModule = new ModuleEsp(request.Description);
                        
            _context.Modules.Add(newModule);
            var result = await _context.SaveChangesAsync() > 0;

            return result is false
                    ? new Response<int>(0, 404, "Informações não encontrada")
                    : new Response<int>(newModule.Id);
        }

        public async Task<Response<int>> Update(UpdateModuleRequest request)
        {            
            var module = await _context.Modules.FirstOrDefaultAsync(m => m.Id == request.ModuleId);

            module.Description = request.Description;

            _context.Update(module);
            var result = await _context.SaveChangesAsync() > 0;

            return result is false
                    ? new Response<int>(0, 404, "Informações não encontrada")
                    : new Response<int>(module.Id);
        }

        public async Task<Response<bool>> Delete(int id)
        {
            var module = await _context.Modules.FirstOrDefaultAsync(m => m.Id == id);
            _context.Modules.Remove(module);
            var result = await _context.SaveChangesAsync() > 0;
            return result is false
                    ? new Response<bool>(false, 404, "Informações não encontrada")
                    : new Response<bool>(result);
        }

        public async Task<Response<int>> AddSensor(AddSensorOnModuleRequest request)
        {
            var module = await _context.Modules
                .Include(m => m.Sensors)
                .FirstOrDefaultAsync(m => m.Id == request.ModuleId);

            if (module == null)
            {
                return new Response<int>(0, 404, "Informações não encontradas");
            }

            var newSensor = new Sensor(request.Description, module)
            {
                ModuleId = module.Id // Define a chave estrangeira explicitamente
            };

            _context.Sensors.Add(newSensor);
            module.Sensors.Add(newSensor); // Adiciona à coleção para garantir vínculo em memória

            var result = await _context.SaveChangesAsync() > 0;

            return result
                ? new Response<int>(newSensor.Id)
                : new Response<int>(0, 404, "Informações não encontradas");
        }

        public async Task<Response<int>> UpdateSensor(UpdateSensorOnModuleRequest request)
        {
            var sensor = await _context.Sensors.FirstOrDefaultAsync(s => s.Id == request.Id);

            sensor.Description = request.Description;

            _context.Update(sensor);
            var result = await _context.SaveChangesAsync() > 0;

            return result is false
                    ? new Response<int>(0, 404, "Informações não encontrada")
                    : new Response<int>(sensor.Id);
        }

        public async Task<Response<bool>> DeleteSensor(int id)
        {
            var sensor = await _context.Sensors.FirstOrDefaultAsync(m => m.Id == id);
            _context.Sensors.Remove(sensor);
            var result = await _context.SaveChangesAsync() > 0;
            return result is false
                    ? new Response<bool>(false, 404, "Informações não encontrada")
                    : new Response<bool>(result);
        }
    }
}
