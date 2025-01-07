using Microsoft.EntityFrameworkCore;
using SimpleServer.Data;
using SimpleServer.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container before calling Build()
builder.Services.AddControllers();
builder.Services.AddSignalR(); // Add SignalR services

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:5108", "https://localhost:7145") // Replace with your front-end URL(s)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials(); // Credentials allowed with specific origins
    });
});

Console.WriteLine("Services configured successfully.");

// Add Pomelo MySQL DbContext with connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine($"Connection string: {connectionString}");

builder.Services.AddDbContext<ChatDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 33))));

var app = builder.Build(); // Now build the app after services have been registered

// Use CORS policy
app.UseCors("AllowAll");

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
    Console.WriteLine("App is running in production mode.");
}

app.UseStaticFiles(); // This is fine if you're serving static files

app.UseRouting();

// Add authorization if necessary
// app.UseAuthorization(); // Uncomment if you are using authorization

// Map API controllers
app.MapControllers();

// Map the SignalR hub
app.MapHub<ChatHub>("/chatHub");
Console.WriteLine("SignalR Hub mapped to /chatHub.");

app.Run();
