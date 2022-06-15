using Blog.Models;
using Blog.Models.Comments;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Post> Posts { get; set; }
    public DbSet<MainComment> MainComments { get; set; }
    public DbSet<SubComment> SubComments { get; set; }
    public DbSet<Viewer> Viewers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Post>().HasMany(x => x.Viewers);
        builder.Entity<User>().HasMany(x => x.Posts).WithOne(x => x.User).HasForeignKey(x => x.UserId);
        builder.Entity<MainComment>().HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        builder.Entity<SubComment>().HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
    }
}
