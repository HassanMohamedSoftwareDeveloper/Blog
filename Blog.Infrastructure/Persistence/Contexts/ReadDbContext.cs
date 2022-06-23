using Blog.Infrastructure.Persistence.Config.Read;
using Blog.Infrastructure.Persistence.Models.Read;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Persistence.Contexts;

internal sealed class ReadDbContext : IdentityDbContext
{
    #region CTORS :
    public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
    {
    }
    #endregion

    #region PROPS :
    public DbSet<PostReadModel> Posts { get; set; }
    public DbSet<CategoryReadModel> Categories { get; set; }
    public DbSet<CommentReadModel> Comments { get; set; }
    public DbSet<ReplyReadModel> Replies { get; set; }
    #endregion

    #region Methods :
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new PostConfiguration());
        builder.ApplyConfiguration(new CategoryConfiguration());
        builder.ApplyConfiguration(new CommentConfiguration());
        builder.ApplyConfiguration(new ReplyConfiguration());
    }
    #endregion
}
