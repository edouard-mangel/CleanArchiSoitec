using Domain;

namespace Infrastructure;

public class InstallmentEntity
{
    public int ScheduleId { get; set; }
    public int Number { get; set; }
    public decimal Principal { get; set; }
    public decimal Interest { get; set; }
    public decimal Total { get; set; }
    public DateTime Date { get; set; }
    public ScheduleEntity Schedule { get; set; } = default!;
}