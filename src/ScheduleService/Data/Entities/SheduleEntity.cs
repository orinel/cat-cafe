namespace ScheduleService.Data.Entities;

public class ScheduleEntity
{
    public int Id { get; set; }
    public int? VisitorId { get; set; }
    public int CatId { get; set; }
    public ActivityType  ActivityType { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}