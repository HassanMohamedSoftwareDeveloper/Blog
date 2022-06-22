using Blog.Infrastructure.Persistence.Models.Read;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Persistence.Config.Read;

internal sealed class CommentConfiguration : IEntityTypeConfiguration<CommentReadModel>
{
    public void Configure(EntityTypeBuilder<CommentReadModel> builder)
    {
        builder.ToTable("Comments");
        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.User);
        builder.HasMany(p => p.Replies);
    }
}