using Service.Api.Core.Entity;

namespace Service.Api.Service.SystemManager.Models.Risks
{
    public class Risk: EntityBase
    {
        public List<Location> Locations { get; set; } = [];
    }
}
