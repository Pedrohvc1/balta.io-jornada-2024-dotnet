using Fina.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Obtenha a string de conexão da variável de ambiente
string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connectionString));


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
