using Fina.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


//Read Configuration from appSettings
var connectionString = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connectionString.GetConnectionString("DefaultConnection")));


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
