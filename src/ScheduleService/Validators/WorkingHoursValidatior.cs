using Grpc.Core;

namespace ScheduleService.Validators;

public static class ActivityTimeValidator
{
    public static void Validate(DateTime startTime, DateTime endTime)
    {
        var workdayStart = new TimeOnly(10, 0);
        var workdayEnd = new TimeOnly(22, 0);
        
        if (TimeOnly.FromDateTime(startTime) < workdayStart)
        {
            throw new RpcException(
                new Status(StatusCode.InvalidArgument, "The start of a booking cannot be earlier than the start of the workday."));
        }
        
        if (TimeOnly.FromDateTime(endTime) > workdayEnd)
        {
            throw new RpcException(
                new Status(StatusCode.InvalidArgument, "The end of a booking cannot be later than the end of the workday"));
        }
        
        if (startTime >= endTime)
        {
            throw new RpcException(
                new Status(StatusCode.InvalidArgument, "Start time must be before end time"));
        }
    }
}