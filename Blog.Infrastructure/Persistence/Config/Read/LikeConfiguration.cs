using Blog.Infrastructure.Persistence.Models.Read;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Persistence.Config.Read;

internal sealed class LikeConfiguration : IEntityTypeConfiguration<LikeReadModel>
{
    public void Configure(EntityTypeBuilder<LikeReadModel> builder)
    {
        builder.ToTable("Likes");
        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.User);
        builder.HasOne(p => p.Post);
    }
}
