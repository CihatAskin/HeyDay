using System;
using System.Collections.Generic;

namespace Heyday.Infrastructure.Entities
{
    public partial class Schedule
    {
        public long Id { get; set; }
        public string? Notes { get; set; }
        public DateTime DateTime { get; set; }
        public string SuitableHours { get; set; } = null!;
    }
}
