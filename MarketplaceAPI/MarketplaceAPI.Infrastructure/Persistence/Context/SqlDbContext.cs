using MarketplaceAPI.Domain.Listings;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceAPI.Infrastructure.Persistence.Context;

public class SqlDbContext : DbContext
{
    public SqlDbContext(DbContextOptions<SqlDbContext> options)
        : base(options)
    {
    }

    public DbSet<Listing>? Listings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Listing>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<Listing>()
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(255);

        modelBuilder.Entity<Listing>()
            .Property(x => x.Description)
            .HasMaxLength(1000);
        
        modelBuilder.Entity<Listing>()
            .Property(x => x.Price)
            .IsRequired();
        
        modelBuilder.Entity<Listing>()
            .Property(x => x.Views)
            .HasDefaultValue(0);
    }
}