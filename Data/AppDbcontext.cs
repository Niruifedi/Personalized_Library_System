using Microsoft.EntityFrameworkCore;
using Personalized_Library_System.Models;

namespace Personalized_Library_System.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // This is where the relationships between the tables are defined
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        modelBuilder.Entity<Books>(entity =>
        {
            // This defines the relationship between the Books and Catalogue tables
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Catalogue)
                .WithMany(e => e.Books)
                .HasForeignKey(e => e.CatalogueId);
        });

        // ;
    }
    public DbSet<User> User { get; set; }
    public DbSet<Books> Books { get; set; }
    public DbSet<User_Catalogue> Catalogues { get; set; }
}
