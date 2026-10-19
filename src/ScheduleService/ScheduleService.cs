using ScheduleService.Repositories;
using ScheduleService.Validators;
using Grpc.Core;
using Google.Protobuf.WellKnownTypes;


namespace ScheduleService;

public class ScheduleGrpcService(PostgresScheduleRepository repository) : Schedule.ScheduleBase
{
    public override Task<CreateActivityResponse> CreateActivity(
        CreateActivityRequest request, 
        ServerCallContext context)
    {
        ActivityTimeValidator.Validate(request.StartTime.ToDateTime(), request.EndTime.ToDateTime());
        ActivityValidator.Validate(request.ActivityType);
        
        var availabilityPeriod = repository.CheckCatAvailability(
            request.CatId,
            request.StartTime.ToDateTimeOffset(),
            request.EndTime.ToDateTimeOffset());

        if (!availabilityPeriod)
        {
            throw new RpcException(
                new Status(
                    StatusCode.AlreadyExists,
                    $"{request.CatId} already has an activity during this period"));
        }
        
        var scheduledActivity = repository.CreateActivity(
            request.HasVisitorId ? request.VisitorId : null,
            request.CatId,
            request.ActivityType,
            request.StartTime.ToDateTimeOffset(),
            request.EndTime.ToDateTimeOffset());
        
        return Task.FromResult(new CreateActivityResponse
        {
            ScheduledActivity =  scheduledActivity
        });
    }

    public override Task<GetScheduleResponse> GetSchedule(
        GetScheduleRequest request,
        ServerCallContext context)
    {
        var res = repository.GetScheduledActivities(
            (!request.HasVisitorId ? null : request.VisitorId),
            (!request.HasCatId ? null : request.CatId),
            (request.StartTime == null ? null : request.StartTime.ToDateTimeOffset()),
            (request.EndTime == null ? null : request.EndTime.ToDateTimeOffset()));

        var response = new GetScheduleResponse();
        response.ScheduledActivity.AddRange(res);
        
        return Task.FromResult(response);
    }

    public override Task<GetActivityResponse> GetActivity(
        GetActivityRequest request,
        ServerCallContext context)
    {
        var activity = repository.GetActivityById(request.ScheduledActivityId);

        if (activity == null)
        {
            throw new RpcException(
                new Status(StatusCode.NotFound, $"Scheduled activity id: {request.ScheduledActivityId} not found"));
        }
        
        var response = new GetActivityResponse
        {
            ScheduledActivity = activity
        };
        return Task.FromResult(response);
    }

    public override Task<CheckCatAvailabilityResponse> CheckCatAvailability(
        CheckCatAvailabilityRequest request,
        ServerCallContext context)
    {
        ActivityTimeValidator.Validate(request.StartTime.ToDateTime(), request.EndTime.ToDateTime());
        
        var res = repository.CheckCatAvailability(request.CatId,
            request.StartTime.ToDateTimeOffset(),
            request.EndTime.ToDateTimeOffset());

        return Task.FromResult(new CheckCatAvailabilityResponse
        {
            IsAvailable = res
        });
    }

    public override Task<Empty> CancelActivity(
        CancelActivityRequest request,
        ServerCallContext context)
    {
        var res = repository.CancelActivity(request.ScheduledActivityId);
        
        if (!res)
        {
            throw new RpcException(
                new Status(StatusCode.NotFound, $"Scheduled activity id: {request.ScheduledActivityId} not found"));
        }
        
        return Task.FromResult(new Empty());
    }
}
