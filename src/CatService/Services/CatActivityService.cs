using Google.Protobuf.WellKnownTypes;

namespace CatService.Services;

public class CatActivityService
{
    private static readonly Activity[] _internalActivities = [Activity.Idle, Activity.Playing, Activity.Eating, Activity.Resting];

    public static CatActivityState GetRandomInternalActivity()
    {
        var now = DateTimeOffset.UtcNow;
        return new CatActivityState {
            Activity = _internalActivities[Random.Shared.Next(_internalActivities.Length)],
            StartTime = Timestamp.FromDateTimeOffset(now),
            EndTime = Timestamp.FromDateTimeOffset(now.AddMinutes(1))
        };
    }
}