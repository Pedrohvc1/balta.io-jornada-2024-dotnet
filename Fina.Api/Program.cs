using Fina.Api.Data;
using Fina.Api.Handlers;
using Fina.Core.Handlers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


//Read Configuration from appSettings
var connectionString = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connectionString.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();


var app = builder.Build();

app.Run();
