using CatService.Repositories;
using Shared.Filtering;

namespace  CatService.Services;

public class CatActivityBackgroundService(IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    private readonly Activity[] _internalActivities = [Activity.Idle, Activity.Playing, Activity.Eating, Activity.Resting];

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = serviceScopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<ICatRepository>();
            var cats = Filtering.Filter(repository.GetAllCats(), 
                cat => cat.CatActivityState.EndTime.ToDateTimeOffset() <= DateTimeOffset.UtcNow);

            foreach (var cat in cats)
            {
                cat.CatActivityState.Activity = _internalActivities[Random.Shared.Next(_internalActivities.Length)];
                var now = DateTimeOffset.UtcNow;
                repository.SetCatActivity(
                    cat.Id,
                    cat.CatActivityState.Activity,
                    null,
                    now,
                    now.AddMinutes(1));
            }
            
            await Task.Delay(5000, stoppingToken);
        }
    }
}