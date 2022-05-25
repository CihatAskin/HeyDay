using Heyday.Domain.Contracts;

namespace Heyday.Domain.Entities
{
    public class Schedule : AuditableEntity
    {
        public string title { get; set; }
        public string? notes { get; set; }

        public bool is_executed { get; set; }
        public DateTime? start_date { get; set; }
        public TimeSpan period { get; set; }

        public IList<UserSchedule> user_schedule { get; set; } = new List<UserSchedule>();

        public Guid manager_id { get; set; }
        public User user { get; set; }
    }
}
