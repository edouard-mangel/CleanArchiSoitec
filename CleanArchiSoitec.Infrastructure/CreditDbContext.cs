using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class CreditDbContext(DbContextOptions<CreditDbContext> options) : DbContext(options)
{
    public DbSet<ScheduleEntity> Schedules{ get; set; }
    public DbSet<InstallmentEntity> Installments { get; set; }

}