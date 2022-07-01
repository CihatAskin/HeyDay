using Heyday.Shared.Filters;

namespace Heyday.Shared.Schedule
{
    public class ScheduleListFilter : PaginationFilter
    {
        public Guid? ManagerId { get; set; }
    }
}
