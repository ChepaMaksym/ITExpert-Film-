using FilmsCatalogMVC.Interfaces;
using FilmsCatalogMVC.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient("categoryapi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5055/api/category");    
});
builder.Services.AddHttpClient("filmapi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5055/api/film");
});
builder.Services.AddHttpClient("filmcategorizerapi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5055/api/filmcategorizer");
});



builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IFilmService, FilmService>();
builder.Services.AddScoped<IFilmCategorizerService, FilmCategorizerService>();


var app = builder.Build();

app.MapControllers();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=film}/{action=index}");

app.Run();
