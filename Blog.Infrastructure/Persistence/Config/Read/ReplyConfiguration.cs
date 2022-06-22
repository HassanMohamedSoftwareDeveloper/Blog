using Blog.Infrastructure.Persistence.Models.Read;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Persistence.Config.Read;

internal sealed class ReplyConfiguration : IEntityTypeConfiguration<ReplyReadModel>
{
    public void Configure(EntityTypeBuilder<ReplyReadModel> builder)
    {
        builder.ToTable("Replies");
        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.User);
    }
}