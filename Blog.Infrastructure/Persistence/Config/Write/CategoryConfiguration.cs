using Blog.Domain.Aggregates;
using Blog.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Blog.Infrastructure.Persistence.Config.Write;

internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        builder.HasKey(p => p.Id);

        var nameConverter = new ValueConverter<CategoryName, string>(cat => cat.Value, cat => new CategoryName(cat));


        builder.Property(p => p.Id).HasConversion(id => id.Value, id => new CategoryId(id));

        builder.Property(typeof(CategoryName), "_categoryName").HasConversion(nameConverter).HasColumnName("Name");
    }
}
