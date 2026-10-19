using Grpc.Core;

namespace ScheduleService.Validators;

public static class ActivityValidator
{
    public static void Validate(ActivityType activityType)
    {
        if (!Enum.IsDefined(activityType) || activityType == ActivityType.Unspecified)
        {
            throw new RpcException(
                new Status(StatusCode.InvalidArgument, "Activity type is invalid"));
        }
    }
}