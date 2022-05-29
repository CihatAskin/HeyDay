using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heyday.Shared.Schedule
{
    public class CreateScheduleRequest
    {
        public string Title { get; set; }
        public Guid ManagerId { get; set; }
        public TimeSpan Period { get; set; }
        public List<Guid> UserIds { get; set; }
    }
}
