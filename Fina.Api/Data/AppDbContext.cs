using Fina.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Data;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Category> Categories { get; set; } = null!; // eles não podem ser null, então eu coloco o null! (null not) para dizer que não é null
    public DbSet<Transaction> Transactions { get; set; } = null!;


    // OnModelCreating é um método que é chamado quando o contexto é criado e é onde eu posso configurar o mapeamento das entidades
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ApplyConfigurationsFromAssembly é um método que procura por todas as classes que implementam a interface IEntityTypeConfiguration e aplica o mapeamento
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
