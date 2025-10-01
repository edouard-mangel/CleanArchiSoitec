using Domain;

namespace TestDataGeneration;

public class ScheduleBuilder
{
    public int Id { get; set; } = 1;
    public decimal Principal { get; private set; } = 10000;
    public decimal AnnualRate { get; private set; } = 5;
    public decimal MonthlyAmount { get; private set; } = 856.07m;
    public List<Installment> Installments { get; private set; } = new();
    public int DurationInMonths { get; private set; } = 4;
    public DateTime UnlockDate { get; private set; } = new(2025, 1, 5);

    public ScheduleBuilder()
    {
        GenerateDefaultInstallments();
    }


    public Schedule Build()
    {
        return new(new ScheduleSnapshot(Principal, AnnualRate, DurationInMonths, UnlockDate, MonthlyAmount, Id));
    }

    public ScheduleBuilder WithDurationInMonths(int newDuration)
    {
        this.DurationInMonths = newDuration;
        return this;
    }

    public void GenerateDefaultInstallments()
    {
        Installments.Add(new(new InstallmentSnapshot(1, 252.61m, 4.17m, 248.44m, UnlockDate.AddMonths(0))));
        Installments.Add(new(new InstallmentSnapshot(2, 252.61m, 3.13m, 249.48m, UnlockDate.AddMonths(1))));
        Installments.Add(new(new InstallmentSnapshot(3, 252.61m, 2.09m, 250.52m, UnlockDate.AddMonths(2))));
        Installments.Add(new(new InstallmentSnapshot(4, 252.61m, 1.05m, 251.56m, UnlockDate.AddMonths(3))));

    }
}
