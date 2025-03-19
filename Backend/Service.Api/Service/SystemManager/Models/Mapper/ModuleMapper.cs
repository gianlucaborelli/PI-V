using Service.Api.Service.SystemManager.Models.DTO;

namespace Service.Api.Service.SystemManager.Models.Mapper
{
    public static class ModuleMapper
    {
        public static Module MapToModule(this NewModuleRequest request)
        {
            return new Module
            {
                Tag = request.Tag,
                EspId = request.EspId,
                CompanyId = request.CompanyId,
                Sensors = request.Sensors.Select(x => x.MapToSensor()).ToList()
            };
        }
        
        public static ModuleDto ToModuleDto(this Module module)
        {
            return new ModuleDto
            {
                Id = module.Id,
                Tag = module.Tag,
                EspId = module.EspId,
                CompanyId = module.CompanyId,
                Sensors = module.Sensors.Select(x => x.ToSensorDto()).ToList()
            };
        }

        public static List<ModuleDto> ToModuleDto(this List<Module> module)
        {
            return module.Select(x => x.ToModuleDto()).ToList();
        }
    }
}
