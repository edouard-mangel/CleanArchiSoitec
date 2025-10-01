namespace Domain;
public class ScheduleSnapshot 
{
    public ScheduleSnapshot(decimal principal, decimal annualRate, DateTime unlockDate, decimal monthlyAmount, List<InstallmentSnapshot> installments) :
        this(principal, annualRate, unlockDate, monthlyAmount, installments, null){ }
    public ScheduleSnapshot(decimal principal, decimal annualRate, DateTime unlockDate, decimal monthlyAmount, List<InstallmentSnapshot> installments, int? id)
    {
        Principal = principal;
        AnnualRate = annualRate;
        UnlockDate = unlockDate;
        MonthlyAmount = monthlyAmount;
        Installments = installments;
        Id = id;
    }

    public int? Id { get; set; }
    public decimal Principal { get; set; }
    public decimal AnnualRate { get; set; }
    public DateTime UnlockDate { get; set; }
    public decimal MonthlyAmount { get; set; }
    public List<InstallmentSnapshot> Installments {get; set; } = new();
}