using Blog.Domain.Entities;
using Blog.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Blog.Infrastructure.Persistence.Config.Write;

internal sealed class LikeConfiguration : IEntityTypeConfiguration<Like>
{
    public void Configure(EntityTypeBuilder<Like> builder)
    {
        builder.ToTable("Likes");
        builder.HasKey(p => p.Id);

        var userIdConverter = new ValueConverter<UserId, string>(usr => usr.Value, user => new UserId(user));

        builder.Property(p => p.Id).HasConversion(id => id.Value, id => new LikeId(id));

        builder.Property(typeof(UserId), "_userId").HasConversion(userIdConverter).HasColumnName("UserId");

        builder.Property(typeof(DateTime), "_created").HasColumnName("Created");
    }
}
