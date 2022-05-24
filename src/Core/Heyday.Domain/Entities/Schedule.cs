using Heyday.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heyday.Domain.Entities
{
    public class Schedule: AuditableEntity
    {
        public string title { get; set; }
        public string? notes { get; set; }
        public string suitable_hours { get; set; } = null!;

        public bool is_executed { get; set; }
        public DateTime? start_date { get; set; }
        public TimeSpan period { get; set; }
    }
}
