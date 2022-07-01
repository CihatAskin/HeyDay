using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heyday.Shared.Schedule
{
    public class ScheduleSearchDto : IDto
    {
        public Guid     Id          { get; set; }
        public string   Title           { get; set; }
        public TimeSpan Period       { get; set; }
        public bool     IsExecuted { get; set; }

    }
}
