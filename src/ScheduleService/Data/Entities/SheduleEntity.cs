namespace ScheduleService.Data.Entities;

public class ScheduleEntity
{
    public int Id { get; set; }
    public int? VisitorId { get; set; }
    public int CatId { get; set; }
    public ActivityType  ActivityType { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
}