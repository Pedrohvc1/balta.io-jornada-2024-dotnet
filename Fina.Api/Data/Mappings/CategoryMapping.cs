using Fina.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fina.Api.Data.Mappings;

public class CategoryMapping : IEntityTypeConfiguration<Category> // adiciona a interface IEntityTypeConfiguration que é necessária para mapear as entidades
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Category");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title) // Property adiciona a propriedades da entidade
            .IsRequired(true) // not null
            .HasColumnType("TEXT")
            .HasMaxLength(80);

        builder.Property(x => x.Description)
            .IsRequired(false)
            .HasColumnType("TEXT")
            .HasMaxLength(255);

        builder.Property(x => x.UserId)
            .IsRequired(true)
            .HasColumnType("TEXT")
            .HasMaxLength(160);
    }
}
