using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Categories;

public class CreateCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapPost("/", HandleAsync)
            .WithName("Categories: Create")
            .WithSummary("Cria uma nova categoria")
            .WithDescription("Cria uma nova categoria")
            .WithOrder(1)
            .Produces<Response<Category?>>();

    // TODO = TypedResults já cria uma resposta do tipo especificado, que é um objeto que contém um status code e um objeto de resposta
    // TODO = O Results não vai, pois ele retorna um objeto do tipo IActionResult, que é um objeto do ASP.NET Core
    private static async Task<IResult> HandleAsync(ICategoryHandler handler, CreateCategoryRequest request)
    {
        var response = await handler.CreateAsync(request);
        return response.IsSuccess
        ? TypedResults.Created($"v1/categories/{response.Data?.Id}", response)
        : TypedResults.BadRequest(response);
    }
}
