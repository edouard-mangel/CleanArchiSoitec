using CleanArchiSoitec.Infrastructure.Entities;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class CreditDbContext(DbContextOptions<CreditDbContext> options) : DbContext(options)
{
    public DbSet<ScheduleEntity> Schedules { get; set; }
    public DbSet<InstallmentEntity> Installments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<ScheduleEntity>(entity =>
            {
                entity.HasKey(entity => entity.Id);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });
            

        modelBuilder
            .Entity<InstallmentEntity>()
            .HasKey(entity => new { entity.Schedule.Id, entity.Number });

        modelBuilder
            .Entity<InstallmentEntity>()
            .HasOne(entity => entity.Schedule)
            .WithMany(entity => entity.Installments)
            .HasForeignKey(entity => entity.ScheduleId);
    }
}