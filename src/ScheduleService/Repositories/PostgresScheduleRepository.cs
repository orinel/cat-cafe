using ScheduleService.Data;
using ScheduleService.Data.Entities;
using Google.Protobuf.WellKnownTypes;

namespace ScheduleService.Repositories;

public class PostgresScheduleRepository(ScheduleDbContext context)
{
    private static ScheduledActivity MapToScheduledActivity(ScheduleEntity scheduleEntity)
    {
        var activity = new ScheduledActivity
        {
            Id = scheduleEntity.Id,
            CatId = scheduleEntity.CatId,
            ActivityType = scheduleEntity.ActivityType,
            StartTime = Timestamp.FromDateTimeOffset(scheduleEntity.StartTime),
            EndTime = Timestamp.FromDateTimeOffset(scheduleEntity.EndTime)
        };

        if (scheduleEntity.VisitorId != null)
        {
            activity.VisitorId = scheduleEntity.VisitorId.Value;
        }
        
        return activity;
    }
    
    public ScheduledActivity CreateActivity(
        int? visitorId,
        int catId,
        ActivityType activityType,
        DateTimeOffset startTime,
        DateTimeOffset endTime)
    {
        var activity = new ScheduleEntity
        {
            VisitorId = visitorId,
            CatId = catId,
            ActivityType = activityType,
            StartTime = startTime,
            EndTime = endTime
        };
        
        context.Schedules.Add(activity);
        context.SaveChanges();
        return MapToScheduledActivity(activity);
    }

    public List<ScheduledActivity> GetScheduledActivities(
        int? visitorId,
        int? catId,
        DateTimeOffset? startTime,
        DateTimeOffset? endTime)
    {
        var activities = context.Schedules
            .Where(activity => (visitorId == null || activity.VisitorId == visitorId) 
                               && (catId == null || activity.CatId == catId)
                               && (startTime == null || activity.StartTime >= startTime)
                               && (endTime == null || activity.EndTime <= endTime))
            .ToList();
        return activities.Select(MapToScheduledActivity).ToList();
    }

    public ScheduledActivity? GetActivityById(int id)
    {
        var activity = context.Schedules.Find(id);
        return activity == null ? null : MapToScheduledActivity(activity);
    }

    public bool CheckCatAvailability(int catId, DateTimeOffset startNewActivityTime, DateTimeOffset endNewActivityTime)
    {
        return !context.Schedules
            .Where(activity => activity.CatId == catId)
            .Any(activity =>
            activity.StartTime < endNewActivityTime &&
            activity.EndTime > startNewActivityTime);
    }

    public bool CancelActivity(int id)
    {
        var activity = context.Schedules.Find(id);
        if (activity == null)
        {
            return false;
        }

        context.Schedules.Remove(activity);
        return context.SaveChanges() > 0;
    }
}
