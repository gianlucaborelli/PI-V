using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.DTOs
{
    public class SensorDataByDayAndModuleId
    {
        public int ModuleId { get; set; }
        public DateTime Date { get; set; }
    }
}
