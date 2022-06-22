using Blog.Infrastructure.Persistence.Models.Read;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Persistence.Config.Read;

internal sealed class CategoryConfiguration : IEntityTypeConfiguration<CategoryReadModel>
{
    public void Configure(EntityTypeBuilder<CategoryReadModel> builder)
    {
        builder.ToTable("Categories");
        builder.HasKey(p => p.Id);

        builder.HasMany(p => p.Posts).WithOne(p => p.Category).HasForeignKey(p => p.CategoryId);
    }
}