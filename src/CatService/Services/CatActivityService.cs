using Google.Protobuf.WellKnownTypes;

namespace CatService.Services;

public class CatActivityService
{
    private static readonly Activity[] _internalActivities = [Activity.Idle, Activity.Playing, Activity.Eating, Activity.Resting];
    private static CatActivityOptions _options = new();
    
    public static void Configure(CatActivityOptions options)
    {
        _options = options;
    }
    
    public static CatActivityState GetRandomInternalActivity()
    {
        var now = DateTimeOffset.UtcNow;
        var activity = _internalActivities[Random.Shared.Next(_internalActivities.Length)];
        
        var duration = activity switch
        {
            Activity.Eating => _options.Eating,
            Activity.Playing => _options.Playing,
            Activity.Resting => _options.Resting,
            Activity.Idle => _options.Idle,
            _ => throw new ArgumentOutOfRangeException(nameof(activity))
        };

        var durationMinutes = Random.Shared.Next(
            duration.MinMinutes,
            duration.MaxMinutes + 1);
        
        return new CatActivityState {
            Activity =  activity,
            StartTime = Timestamp.FromDateTimeOffset(now),
            EndTime = Timestamp.FromDateTimeOffset(now.AddMinutes(durationMinutes))
        };
    }
}


public class CatActivityOptions
{
    public ActivityDurationOptions Eating { get; set; } = new();
    public ActivityDurationOptions Playing { get; set; } = new();
    public ActivityDurationOptions Resting { get; set; } = new();
    public ActivityDurationOptions Idle { get; set; } = new();
}

public class ActivityDurationOptions
{
    public int MinMinutes { get; set; }
    public int MaxMinutes { get; set; }
}