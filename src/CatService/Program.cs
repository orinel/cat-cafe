using CatService.Repositories;
using CatService.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register application services
builder.Services.AddGrpc();
builder.Services.AddScoped<ICatRepository, PostgresCatRepository>();

// Register database context
builder.Services.AddDbContext<CatCafeDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("CatCafe")));

var app = builder.Build();

// Apply pending database migrations
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CatCafeDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
app.MapGrpcService<CatService.CatGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
