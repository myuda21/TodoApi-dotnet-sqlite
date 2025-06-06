using TODOAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Tambahkan service OpenAPI (Swagger)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Tambahkan controller sebelum `Build()`
builder.Services.AddControllers();

// konfigurasi agar ASP.NET Core bisa menyimpan session
builder.Services.AddDistributedMemoryCache();
// Middleware routing
builder.Services.AddSession();

// Konfigurasi koneksi database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Swagger hanya aktif di development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Optional: redirect ke HTTPS
// app.UseHttpsRedirection();

app.UseRouting();
app.UseSession();
app.UseAuthorization();



// ✅ Tambahkan middleware untuk endpoint controller
app.UseAuthorization();
app.MapControllers();

// Dummy data: Weather
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

// Endpoint: GET /weatherforecast
// app.MapGet("/weatherforecast", () =>
// {
//     var forecast = Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();

//     return forecast;
// })
// .WithName("GetWeatherForecast");

// Dummy endpoint: /api/todo
var todos = new List<string> { "Belajar .NET", "Ngoding Flutter" };
// app.MapGet("/api/todo", () => todos)
//    .WithName("GetTodoList");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}