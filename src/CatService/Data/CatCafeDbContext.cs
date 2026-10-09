using CatService.Data.Entities;
using Microsoft.EntityFrameworkCore;

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
            
            entity.Property(cat => cat.Gender)
                .HasColumnName("gender");

            entity.Property(cat => cat.Breed)
                .HasColumnName("breed");
            
            entity.Property(cat => cat.Activity)
                .HasColumnName("activity");
            
            entity.Property(cat => cat.ExternalActivityId)
                .HasColumnName("external_activity_id");
            
            entity.Property(cat => cat.ActivityStartsAt)
                .HasColumnName("activity_starts_at");
            
            entity.Property(cat => cat.ActivityEndsAt)
                .HasColumnName("activity_ends_at");

            entity.Property(cat => cat.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
    }
}