using Blog.Domain.Aggregates;
using Blog.Domain.Entities;
using Blog.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Blog.Infrastructure.Persistence.Config.Write;

internal sealed class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Posts");
        builder.HasKey(p => p.Id);

        var titleConverter = new ValueConverter<Title, string>(t => t.Value, t => new Title(t));
        var descriptionConverter = new ValueConverter<Description, string>(d => d.Value, d => new Description(d));
        var tagConverter = new ValueConverter<Tag, string>(t => t.Value, t => new Tag(t));
        var bodyConverter = new ValueConverter<Body, string>(b => b.Value, b => new Body(b));
        var imageConverter = new ValueConverter<Image, string>(img => img.Value, img => new Image(img));
        var userIdConverter = new ValueConverter<UserId, string>(usr => usr.Value, user => new UserId(user));
        var categoryIdConverter = new ValueConverter<CategoryId, Guid>(cat => cat.Value, cat => new CategoryId(cat));


        builder.Property(p => p.Id).HasConversion(id => id.Value, id => new PostId(id));
        builder.Property(p => p.Image).HasConversion(img => img.Value, img => new Image(img));

        builder.Property(typeof(Title), "_title").HasConversion(titleConverter).HasColumnName("Title");
        builder.Property(typeof(Description), "_description").HasConversion(descriptionConverter).HasColumnName("Description");
        builder.Property(typeof(Tag), "_tag").HasConversion(tagConverter).HasColumnName("Tags");
        builder.Property(typeof(Body), "_body").HasConversion(bodyConverter).HasColumnName("Body");
        builder.Property(typeof(UserId), "_userId").HasConversion(userIdConverter).HasColumnName("UserId");
        builder.Property(typeof(CategoryId), "_categoryId").HasConversion(categoryIdConverter).HasColumnName("CategoryId");
        builder.Property(typeof(DateTime), "_created").HasColumnName("Created");
        builder.Property(typeof(int), "_viewersCount").HasColumnName("ViewersCount");
        builder.Property(typeof(DateTime?), "_updated").HasColumnName("Updated").IsRequired(false);

        builder.HasMany(typeof(Comment), "_comments");
        builder.HasMany(typeof(Like), "_likes");

    }
}
