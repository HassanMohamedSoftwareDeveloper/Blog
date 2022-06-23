using Blog.Domain.Aggregates;
using Blog.Domain.Entities;
using Blog.Infrastructure.Persistence.Config.Write;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Persistence.Contexts;

internal class WriteDbContext : IdentityDbContext
{
    #region CTORS :
    public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options)
    {
    }
    #endregion

    #region PROPS :
    public DbSet<Post> Posts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Reply> Replies { get; set; }
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
