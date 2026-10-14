using ScheduleService;
using ScheduleService.Data;
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

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<ScheduleGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
