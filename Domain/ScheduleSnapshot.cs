namespace Domain;
public class ScheduleSnapshot 
{
    public ScheduleSnapshot(decimal principal, decimal annualRate, int durationInMonths, DateTime unlockDate, decimal monthlyAmount) :
        this(principal, annualRate, durationInMonths, unlockDate, monthlyAmount, null){ }
    public ScheduleSnapshot(decimal principal, decimal annualRate, int durationInMonths, DateTime unlockDate, decimal monthlyAmount, int? id)
    {
        Principal = principal;
        AnnualRate = annualRate;
        DurationInMonths = durationInMonths;
        UnlockDate = unlockDate;
        MonthlyAmount = monthlyAmount;
        Id = id;
    }

    public int? Id { get; set; }
    public decimal Principal { get; set; }
    public decimal AnnualRate { get; set; }
    public int DurationInMonths { get; set; }
    public DateTime UnlockDate { get; set; }
    public decimal MonthlyAmount { get; set; }
    public List<InstallmentSnapshot> Installments {get; set; } = new();
}