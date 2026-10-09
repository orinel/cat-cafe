using CatService.Repositories;

using Shared.Filtering;

namespace  CatService.Services;

public class CatActivityBackgroundService(IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
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
                var activity = CatActivityService.GetRandomInternalActivity();
                repository.SetCatActivity(
                    cat.Id,
                    activity.Activity,
                    null,
                    activity.StartTime.ToDateTimeOffset(),
                    activity.EndTime.ToDateTimeOffset());
            }
            
            await Task.Delay(5000, stoppingToken);
        }
    }
}