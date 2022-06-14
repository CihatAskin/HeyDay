
namespace Heyday.Shared.Schedule;
public class CreateScheduleRequest
{
    public string Title { get; set; }   
    public string Description { get; set; }   
    public TimeSpan Period { get; set; }
    public DateTime StartDate { get; set; }
    public Guid ManagerId { get; set; }
    public List<short> HourKeys { get; set; }
    public List<Guid> UserIds { get; set; }
}

