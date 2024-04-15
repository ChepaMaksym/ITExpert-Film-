using FilmsCatalogAPI.Data;
using FilmsCatalogAPI.Interfaces;
using FilmsCatalogAPI.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(
    op => op
    .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    .UseSnakeCaseNamingConvention());
builder.Services.AddTransient<Seed>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IFilmCategorizerService, FilmCategorizerService>();
builder.Services.AddScoped<IFilmService, FilmService>();

var app = builder.Build();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<DataContext>();
        var seed = services.GetRequiredService<Seed>();
        seed.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.Run();
