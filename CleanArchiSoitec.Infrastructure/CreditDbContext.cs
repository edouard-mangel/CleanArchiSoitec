using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class CreditDbContext(DbContextOptions<CreditDbContext> options) : DbContext(options)
{
    public DbSet<ScheduleEntity> Schedules{ get; set; }
    public DbSet<InstallmentEntity> Installments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<ScheduleEntity>()
            .HasKey(entity => entity.Id);

        modelBuilder
            .Entity<InstallmentEntity>()
            .HasKey(entity => new { entity.Schedule.Id, entity.Number});

        modelBuilder
            .Entity<InstallmentEntity>()
            .HasOne(entity => entity.Schedule)
            .WithMany(entity => entity.Installments)
            .HasForeignKey(entity => entity.ScheduleId);
    }
}