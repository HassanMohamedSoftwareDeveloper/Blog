using Blog.Infrastructure.Persistence.Models.Read;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Persistence.Config.Read;

internal sealed class UserConfiguration : IEntityTypeConfiguration<UserReadModel>
{
    public void Configure(EntityTypeBuilder<UserReadModel> builder)
    {
        builder.ToTable("AspNetUsers");
        builder.HasKey(p => p.Id);
    }
}