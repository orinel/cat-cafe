using ScheduleService;
using ScheduleService.Data;
using ScheduleService.Repositories;
using Microsoft.EntityFrameworkCore;

DotNetEnv.Env.TraversePath().Load();

var builder = WebApplication.CreateBuilder(args);

// Register application services
builder.Services.AddGrpc();

// Register database context
var connectionString = builder.Configuration.GetConnectionString("ScheduleService");

if (string.IsNullOrWhiteSpace(connectionString))
{
    connectionString =
        $"Host=localhost;" +
        $"Port=5433;" +
        $"Database={Environment.GetEnvironmentVariable("SCHEDULE_SERVICE_DB_NAME")};" +
        $"Username={Environment.GetEnvironmentVariable("SCHEDULE_SERVICE_DB_USER")};" +
        $"Password={Environment.GetEnvironmentVariable("SCHEDULE_SERVICE_DB_PASSWORD")};";
}

builder.Services.AddDbContext<ScheduleDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddScoped<PostgresScheduleRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<ScheduleDbContext>();

    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
app.MapGrpcService<ScheduleGrpcService>();

app.Run();
