using Blog.Infrastructure.Persistence.Models.Read;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Persistence.Config.Read;

internal sealed class PostConfiguration : IEntityTypeConfiguration<PostReadModel>
{
    public void Configure(EntityTypeBuilder<PostReadModel> builder)
    {
        builder.ToTable("Posts");
        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.Category).WithMany(c => c.Posts).HasForeignKey(p => p.CategoryId);
        builder.HasOne(p => p.User);
        builder.HasMany(p => p.Comments).WithOne(x => x.Post).HasForeignKey(x => x.PostId);
        builder.HasMany(p => p.Likes).WithOne(x => x.Post).HasForeignKey(x => x.PostId);
    }
}
