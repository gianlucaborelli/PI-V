using Service.Api.Core.Entity;
using Service.Api.Service.SystemManager.Models.Risks;

namespace Service.Api.Service.SystemManager.Models
{
    public class RiskLimit : EntityBase
    {
        public Guid LocationId { get; set; }
        public Location Location { get; set; } = null!;
        public Guid RiskId { get; set; }              
        public virtual Risk Risk { get; set; } = null!; 
    }
}
