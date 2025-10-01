namespace CleanArchiSoitec.Infrastructure.Entities;

public class ScheduleEntity
{
    public int Id { get; set; }
    public decimal Principal { get; set; }
    public int DurationInMonths { get; set; }
    public decimal AnnualRate { get; set; }
    public DateTime UnlockDate { get; set; }
    public decimal MonthlyAmount { get; set; }

    public List<InstallmentEntity> Installments { get; set; } = new();
}