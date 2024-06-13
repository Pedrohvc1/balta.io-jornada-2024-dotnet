using Fina.Api.Data;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Handlers;

public class CategoryHandler(AppDbContext context) : ICategoryHandler
{
    public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
    {
        var category = new Category
        {
            UserId = request.UserId,
            Title = request.Title,
            Description = request.Description
        };
        try
        {
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            return new Response<Category?>(category, 201, "Categoria criada com sucesso!");
        }
        catch (Exception e)
        {
            // add depois serilog 
            Console.WriteLine(e.Message);
            return new Response<Category?>(null, 500, "Não foi possível criar uma categoria!");

        }
    }

    public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (category is null)
                return new Response<Category?>(null, 404, "Categoria não encontrada!");


            category.Title = request.Title;
            category.Description = request.Description;

            context.Categories.Update(category); // não tem UpdateAsync pra não ter uma sobrecarga de métodos
            await context.SaveChangesAsync();

            return new Response<Category?>(category, 200, "Categoria atualizada com sucesso!");
        }
        catch (Exception e)
        {
            // add depois Serilog 
            Console.WriteLine(e.Message);
            return new Response<Category?>(null, 500, "Não foi possível atualizar a categoria!");
        }
    }

    public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (category is null)
                return new Response<Category?>(null, 404, "Categoria não encontrada!");

            context.Categories.Remove(category); // não tem UpdateAsync pra não ter uma sobrecarga de métodos
            await context.SaveChangesAsync();

            return new Response<Category?>(category, 200, "Categoria exclupida com sucesso!");
        }
        catch (Exception e)
        {
            // add depois Serilog 
            Console.WriteLine(e.Message);
            return new Response<Category?>(null, 500, "Não foi possível excluir a categoria!");
        }
    }

    public async Task<PagedResponse<List<Category>?>> GetAllAsync(GetAllCategoriesRequest request)
    {
        try
        {
            var query = context.Categories
        .AsNoTracking()
        .Where(x => x.UserId == request.UserId)
        .OrderBy(x => x.Title);

            var categories = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

            var count = await query.CountAsync();

            return new PagedResponse<List<Category>?>(
                categories,
                count,
                request.PageNumber,
                request.PageSize
            );
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return new PagedResponse<List<Category>?>(null, 500, "Não foi possível buscar as categorias!");
        }

    }

    public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
    {
        try
        {
            var category = await context
            .Categories
            .AsNoTracking() // é muito mais rápido, pois não faz o tracking das entidades (só pode fazer isso quando não manipular a entidade)
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            //AsNoTrackingWithIdentityResolution quando for trazer endpoints com muitos registros.

            return category is null
            ? new Response<Category?>(null, 404, "Categoria não encontrada!")
            : new Response<Category?>(category);

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return new Response<Category?>(null, 500, "Não foi possível buscar a categoria!");
        }
    }

}
