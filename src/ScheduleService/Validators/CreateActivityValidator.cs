using Grpc.Core;

namespace ScheduleService.Validators;

public static class CreateActivityValidator
{
    public static void Validate(CreateActivityRequest request)
    {
        if (request.StartTime >= request.EndTime)
        {
            throw new RpcException(
                new Status(StatusCode.InvalidArgument, "Start time must be before end time"));
        }

        var workdayStart = new TimeOnly(10, 0);
        var workdayEnd = new TimeOnly(22, 0);
        
        if (TimeOnly.FromDateTime(request.EndTime.ToDateTime()) > workdayEnd)
        {
            throw new RpcException(
                new Status(StatusCode.InvalidArgument, "The end of a booking cannot be later than the end of the workday"));
        }

        if (TimeOnly.FromDateTime(request.StartTime.ToDateTime()) < workdayStart)
        {
            throw new RpcException(
                new Status(StatusCode.InvalidArgument, "The start of a booking cannot be earlier than the start of the workday."));
        }
        
        if (!Enum.IsDefined(request.ActivityType) || request.ActivityType == ActivityType.Unspecified)
        {
            throw new RpcException(
                new Status(StatusCode.InvalidArgument, "Activity type is invalid"));
        }
    }
}