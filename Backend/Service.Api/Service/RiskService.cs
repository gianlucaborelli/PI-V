using Microsoft.EntityFrameworkCore;
using Service.Api.Database;
using Service.Api.Service.SystemManager.Models.DTO;
using Service.Api.Service.SystemManager.Models.Mapper;

namespace Service.Api.Service
{
    public class RiskService : IRiskService
    {
        private readonly ServiceDatabaseContext  _context;

        public RiskService(ServiceDatabaseContext  context)
        {
            _context = context;
        }

        public async Task<List<RiskDto>> GetAllRIsk(string riskType)
        {
            var riscos = await _context.Risks
                .Where(r => EF.Property<string>(r, "RiskType") == riskType)
                .ToListAsync();
                        
            return riscos.Select(r => r.ToDto()).ToList();
        }
    }
}
