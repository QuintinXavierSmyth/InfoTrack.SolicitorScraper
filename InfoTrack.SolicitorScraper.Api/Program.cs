using InfoTrack.SolicitorScraper.Infrastructure;
using InfoTrack.SolicitorScraper.Infrastructure.Persistence;
using InfoTrack.SolicitorScraper.Infrastructure.Seed;
using InfoTrack.SolicitorScraper.Application;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("VueApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddOpenApi();


// Register application infrastructure
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();


var app = builder.Build();


// Seed initial data
using (var scope = app.Services.CreateScope())
{
    var dataStore = scope.ServiceProvider
        .GetRequiredService<InMemoryDataStore>();

    LocationSeeder.Seed(dataStore);
}


// Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("VueApp");

app.UseAuthorization();

app.MapControllers();

app.Run();