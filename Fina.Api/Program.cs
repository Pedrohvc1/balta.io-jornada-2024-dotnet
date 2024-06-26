using Fina.Api;
using Fina.Api.Common.Api;
using Fina.Api.Endpoints;
using Fina.Core;
using Fina.Api.Data;
using Fina.Api.Handlers;
using Fina.Core.Handlers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();


//Read Configuration from appSettings
// var connectionString = new ConfigurationBuilder()
//     .AddJsonFile("appsettings.json")
//     .Build();

// builder.Services.AddEntityFrameworkNpgsql().AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connectionString.GetConnectionString("DefaultConnection")));

// builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
// builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();


var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

app.UseCors(ApiConfiguration.CorsPolicyName);
app.MapEndpoints();


app.Run();
