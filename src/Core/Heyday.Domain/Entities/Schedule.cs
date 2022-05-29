using Heyday.Domain.Contracts;

namespace Heyday.Domain.Entities
{
    public class Schedule : AuditableEntity
    {
        public string   title       { get; set; }
        public TimeSpan period       { get; set; }
        public Guid     manager_id { get; set; }

        public string? result { get; set; }
        public bool is_executed { get; set; }
        public DateTime? start_date { get; set; }

        public IList<UserSchedule> user_schedule { get; set; } = new List<UserSchedule>();

        public User user { get; set; }
    }
}
