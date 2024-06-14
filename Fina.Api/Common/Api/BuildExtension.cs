using Fina.Api.Data;
using Fina.Api.Handlers;
using Fina.Core;
using Fina.Core.Handlers;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Common.Api;

public static class BuildExtension
{
    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
        Configuration.FrontendUrl = builder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;
    }

    public static void AddDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(x => { x.CustomSchemaIds(n => n.FullName); }); //garantir que o nome das entidades vao sempre usar os namespaces
    }

    public static void AddDataContexts(this WebApplicationBuilder builder)
    {
        var connectionString = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        builder
            .Services
            .AddEntityFrameworkNpgsql()
            .AddDbContext<AppDbContext>(
                opt =>
                {
                    opt.UseNpgsql(connectionString.GetConnectionString("DefaultConnection"));
                });
    }

    /* TODO - configurando a política CORS (Cross-Origin Resource Sharing) para a aplicação. 
       Criando a permissão de acessar a API de qualquer origem (com o WithOrigins("*")) e permitindo qualquer 
       método (com o AllowAnyMethod()) e qualquer cabeçalho (com o AllowAnyHeader()) e permitindo credenciais (com o AllowCredentials()).
    */

    public static void AddCrossOrigin(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(
            options => options.AddPolicy(
                ApiConfiguration.CorsPolicyName,
                policy => policy
                    .WithOrigins([
                        Configuration.BackendUrl,
                        Configuration.FrontendUrl
                    ])
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
            ));
    }

    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder
            .Services
            .AddTransient<ICategoryHandler, CategoryHandler>();

        builder
            .Services
            .AddTransient<ITransactionHandler, TransactionHandler>();
    }
}