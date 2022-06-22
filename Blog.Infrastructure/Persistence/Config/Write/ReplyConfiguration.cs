using Blog.Domain.Entities;
using Blog.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Blog.Infrastructure.Persistence.Config.Write;

internal sealed class ReplyConfiguration : IEntityTypeConfiguration<Reply>
{
    public void Configure(EntityTypeBuilder<Reply> builder)
    {
        builder.ToTable("Replies");
        builder.HasKey(p => p.Id);

        var messageConverter = new ValueConverter<Message, string>(msg => msg.Value, msg => new Message(msg));
        var userIdConverter = new ValueConverter<UserId, string>(usr => usr.Value, user => new UserId(user));

        builder.Property(p => p.Id).HasConversion(id => id.Value, id => new CommentId(id));

        builder.Property(typeof(Message), "_message").HasConversion(messageConverter).HasColumnName("Message");
        builder.Property(typeof(UserId), "_userId").HasConversion(userIdConverter).HasColumnName("UserId");

        builder.Property(typeof(DateTime), "_created").HasColumnName("Created");
    }
}
