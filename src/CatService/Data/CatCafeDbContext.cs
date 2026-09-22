using Microsoft.EntityFrameworkCore;
using CatService.Data.Entities;

namespace CatService.Data;

public class CatCafeDbContext : DbContext
{
    public CatCafeDbContext(DbContextOptions<CatCafeDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<CatEntity> Cats { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CatEntity>(entity =>
        {
            entity.ToTable("cats");

            entity.Property(cat => cat.Id)
                .HasColumnName("id")
                .UseIdentityByDefaultColumn();

            entity.Property(cat => cat.Name)
                .HasColumnName("name")
                .HasMaxLength(100);

            entity.Property(cat => cat.Age)
                .HasColumnName("age");

            entity.Property(cat => cat.Breed)
                .HasColumnName("breed");

            entity.Property(cat => cat.Status)
                .HasColumnName("status");

            entity.Property(cat => cat.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
    }
}