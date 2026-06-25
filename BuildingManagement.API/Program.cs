var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddControllers();

// (DI) .NET sees IBuildingService here and automatically injects it
builder.Services.AddScoped<IBuildingService, BuildingService>();

var app = builder.Build();

// Configure middleware
app.UseHttpsRedirection();
app.UseAuthorization();
// Enable controllers
app.MapControllers();

app.Run();