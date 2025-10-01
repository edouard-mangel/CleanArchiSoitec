using Domain;

namespace TestDataGeneration;

public class ScheduleBuilder
{
    public int Id { get; set; } = 1; 
    public decimal Principal { get; private set; } = 10000;
    public decimal AnnualRate { get; private set; } = 5;
    public decimal MonthlyAmount { get; private set; } = 856.07m;
    public List<Installment> Installments { get; private set; } = new();
    public int DurationInMonths { get; private set; } = 12;
    public DateTime UnlockDate { get; private set; } = new(2025, 1, 5);

    public Schedule Build()
    {
        return new(new ScheduleSnapshot(Principal, AnnualRate, DurationInMonths, UnlockDate, MonthlyAmount, Id));
    }

    public ScheduleBuilder WithDurationInMonths(int newDuration)
    {
        this.DurationInMonths = newDuration;
        return this; 
    }
}
