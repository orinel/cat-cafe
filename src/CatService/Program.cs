using CatService.Repositories;
using CatService.Data;
using CatService.Services;
using CatService;
using Microsoft.EntityFrameworkCore;

DotNetEnv.Env.TraversePath().Load();

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("CatCafe");

if (string.IsNullOrWhiteSpace(connectionString))
{
    connectionString =
        $"Host=localhost;" +
        $"Port=5432;" +
        $"Database={Environment.GetEnvironmentVariable("CAT_SERVICE_DB_NAME")};" +
        $"Username={Environment.GetEnvironmentVariable("CAT_SERVICE_DB_USER")};" +
        $"Password={Environment.GetEnvironmentVariable("CAT_SERVICE_DB_PASSWORD")}";
}

// Register application services
builder.Services.AddGrpc();
builder.Services.AddScoped<ICatRepository, PostgresCatRepository>();
builder.Services.AddHostedService<CatActivityBackgroundService>();
builder.Services.AddSingleton<CatActivityService>();

// Register database context
builder.Services.AddDbContext<CatCafeDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

// Apply pending database migrations and seed initial cats if the database is empty
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CatCafeDbContext>();
    db.Database.Migrate();

    var repository = scope.ServiceProvider.GetRequiredService<ICatRepository>();

    if (repository.GetAllCats().Count == 0)
    {
        repository.CreateCat("Барсик", 3, Gender.Male, Breed.DomesticCat,
            CatActivityService.GetRandomInternalActivity());

        repository.CreateCat("Мурка", 5, Gender.Female, Breed.DomesticCat,
            CatActivityService.GetRandomInternalActivity());

        repository.CreateCat("Васька", 2, Gender.Male, Breed.DomesticCat,
            CatActivityService.GetRandomInternalActivity());
    }
}

// Configure the HTTP request pipeline.
app.MapGrpcService<CatService.CatGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
