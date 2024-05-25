using Microsoft.EntityFrameworkCore;
using Personalized_Library_System.Models;

namespace Personalized_Library_System.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
        });
    }
    public DbSet<User> User { get; set; }
}