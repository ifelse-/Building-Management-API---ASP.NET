using BuildingManagement.API.Interfaces;
using BuildingManagement.API.Services;
using Microsoft.EntityFrameworkCore;
using BuildingManagement.API.Data;
using BuildingManagement.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddControllers();

// (DI) .NET sees IBuildingService here and automatically injects it
builder.Services.AddScoped<IBuildingService, BuildingService>();


// Register the database context with dependency injection
// ApplicationDbContext is our bridge between C# and PostgreSQL
// AddDbContext makes it available throughout the app via DI
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // Tell Entity Framework to use PostgreSQL as the database provider
    // UseNpgsql comes from the Npgsql.EntityFrameworkCore.PostgreSQL package
    options.UseNpgsql(
        // Pull the connection string from appsettings.json
        // looks for "ConnectionStrings": { "DefaultConnection": "..." }
        builder.Configuration.GetConnectionString("DefaultConnection"));
});


var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseRouting();


// Configure middleware
app.UseHttpsRedirection();
app.UseAuthorization();
// Enable controllers
app.MapControllers();

app.Run();