using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heyday.Domain.Entities
{
    public class UserSchedule
    {
        public string suitable_hours { get; set; } = null!;
        public string? exception { get; set; }

        public Guid user_id { get; set; }
        public User user { get; set; }
        
        public Guid schedule_id { get; set; }
        public Schedule schedule { get; set; }
    }
}
