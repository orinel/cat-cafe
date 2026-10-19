using ScheduleService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ScheduleService.Data;

public class ScheduleDbContext : DbContext
{
    public ScheduleDbContext(DbContextOptions<ScheduleDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<ScheduleEntity> Schedules { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ScheduleEntity>(entity =>
        {
            entity.ToTable("schedule");

            entity.Property(activity => activity.Id)
                .HasColumnName("id")
                .UseIdentityByDefaultColumn();

            entity.Property(activity => activity.VisitorId)
                .HasColumnName("visitor_id");

            entity.Property(activity => activity.CatId)
                .HasColumnName("cat_id");
            
            entity.Property(activity => activity.ActivityType)
                .HasColumnName("activity_type");
            
            entity.Property(activity => activity.StartTime)
                .HasColumnName("start_time");
            
            entity.Property(activity => activity.EndTime)
                .HasColumnName("end_time");
        });
    }
}

